using System;
using System.Collections.Generic;

namespace RFD.Arbitration
{
    using TArbID = UInt64;

    /// <summary>
    /// Data forwarded by a flight controller from an arbiter.
    /// </summary>
    public class TRxDataFromArbiter
    {
        /// <summary>
        /// The bus ID of the message.
        /// </summary>
        public readonly byte BusID;
        /// <summary>
        /// The active bus ID of the message.
        /// </summary>
        public readonly byte ActiveBusID;
        /// <summary>
        /// The unqiue arbiter ID of the message.
        /// </summary>
        public readonly UInt64 UniqueID;
        public readonly UInt64 TickStamp;

        public TRxDataFromArbiter(byte BusID, byte ActiveBusID, UInt64 UniqueID)
        {
            this.BusID = BusID;
            this.ActiveBusID = ActiveBusID;
            this.UniqueID = UniqueID;
            this.TickStamp = RFDLib.Time.Time.GetTicks();
        }
    }

    /// <summary>
    /// A unqie flight controller ID.  Used as a key for some of the dictionaries in TArbitrationState
    /// </summary>
    public struct TFCID
    {
        public readonly byte SysID;
        public readonly byte CompID;

        public TFCID(byte SysID, byte CompID)
        {
            this.SysID = SysID;
            this.CompID = CompID;
        }

        public override int GetHashCode()
        {
            return (((int)SysID) << 8) | CompID;
        }

        public override bool Equals(object obj)
        {
            return (this.SysID == ((TFCID)obj).SysID) && (this.CompID == ((TFCID)obj).CompID);
        }
    }

    /// <summary>
    /// Data received from a flight controller.
    /// </summary>
    public class TRxDataFromFlightController
    {
        /// <summary>
        /// The functional state the flight controller is operating in.
        /// </summary>
        public readonly TFunctionalState FS;
        /// <summary>
        /// The time the flight controller has been in the functional state for, in milliseconds.
        /// </summary>
        public readonly UInt64 TimeInState;
        /// <summary>
        /// The amount of time the flight controller has been running for, in milliseconds.
        /// </summary>
        public readonly UInt64 UpTime;
        /// <summary>
        /// The EKF error score of the flight controller.
        /// </summary>
        public readonly float EKFErrorScore;
        /// <summary>
        /// The tick stamp the message was received.
        /// </summary>
        readonly UInt64 _TickStamp;

        public TRxDataFromFlightController(TFunctionalState FS,
            UInt64 TimeInState, UInt64 UpTime, float EKFErrorScore)
        {
            this.FS = FS;
            this.TimeInState = TimeInState;
            this.UpTime = UpTime;
            this.EKFErrorScore = EKFErrorScore;
            _TickStamp = RFDLib.Time.Time.GetTicks();
        }

        public UInt64 TimeInStateUpdated
        {
            get
            {
                return TimeInState + RFDLib.Time.Time.GetTicksSince(_TickStamp);
            }
        }

        public UInt64 UpTimeUpdated
        {
            get
            {
                return UpTime + RFDLib.Time.Time.GetTicksSince(_TickStamp);
            }
        }
    }

    public enum TFunctionalState
    {
        STANDBY,
        ACTIVE,
        UNKNOWN,
    }

    /// <summary>
    /// The current state of the redundant flight controller / arbitration system.  
    /// </summary>
    public class TArbitrationState
    {
        Dictionary<TFCID, TArbDataCollection> _ArbData = new Dictionary<TFCID, TArbDataCollection>();
        Dictionary<TFCID, TRxDataFromFlightController> _FCData = new Dictionary<TFCID, TRxDataFromFlightController>();

        public void ProcessDataFromArb(TFCID FCID, TRxDataFromArbiter Data)
        {
            if (!_ArbData.ContainsKey(FCID))
            {
                _ArbData[FCID] = new TArbDataCollection();
            }
            _ArbData[FCID][Data.UniqueID] = Data;
        }

        public void ProcessDataFromFC(TFCID FCID, TRxDataFromFlightController Data)
        {
            _FCData[FCID] = Data;
        }

        byte GetBusIDForFC(TFCID FCID)
        {
            if (_ArbData.ContainsKey(FCID))
            {
                foreach (var kvp in _ArbData[FCID])
                {
                    return kvp.Value.BusID;
                }
            }

            return 0;
        }

        TFunctionalState GetDeeming(TFCID FCID, TArbID ArbID)
        {
            if (_ArbData.ContainsKey(FCID))
            {
                var ADC = _ArbData[FCID];
                if (ADC.ContainsKey(ArbID))
                {
                    return (ADC[ArbID].BusID == ADC[ArbID].ActiveBusID) ? TFunctionalState.ACTIVE : TFunctionalState.STANDBY;
                }
            }

            return TFunctionalState.UNKNOWN;
        }

        public TStatus GetStatus()
        {
            List<TStatus.TFlightController> FCs = new List<TStatus.TFlightController>();
            List<TFCID> FCIDs = new List<TFCID>();

            foreach (var kvp in _FCData)
            {
                TStatus.TFlightController FC = new TStatus.TFlightController(kvp.Value.FS, kvp.Value.TimeInStateUpdated, 
                    kvp.Value.EKFErrorScore, kvp.Value.UpTimeUpdated, GetBusIDForFC(kvp.Key));
                FCs.Add(FC);
                FCIDs.Add(kvp.Key);
            }

            Dictionary<TArbID, TStatus.TArbiter> Arbs = new Dictionary<TArbID, TStatus.TArbiter>();

            foreach (var kvp in _ArbData)
            {
                foreach (var kvpArb in kvp.Value)
                {
                    if (!Arbs.ContainsKey(kvpArb.Key))
                    {
                        Arbs[kvpArb.Key] = new TStatus.TArbiter(new TFunctionalState[FCs.Count], kvpArb.Key);
                    }
                }
            }

            foreach (var kvp in Arbs)
            {
                for (int n = 0; n < FCs.Count; n++)
                {
                    kvp.Value.Deeming[n] = GetDeeming(FCIDs[n], kvp.Key);
                }
            }

            List<TStatus.TArbiter> ArbList = new List<TStatus.TArbiter>(Arbs.Values);

            return new TStatus(FCs.ToArray(), ArbList.ToArray());
        }

        int GetBusVotedFor(int QTYBuses)
        {
            Dictionary<TArbID, TRxDataFromArbiter> LatestArbData = new Dictionary<TArbID, TRxDataFromArbiter>();

            foreach (var kvpFC in _ArbData)
            {
                foreach (var kvpArb in kvpFC.Value)
                {
                    if (!LatestArbData.ContainsKey(kvpArb.Key) || (LatestArbData[kvpArb.Key].TickStamp < kvpArb.Value.TickStamp))
                    {
                        LatestArbData[kvpArb.Key] = kvpArb.Value;
                    }
                }
            }

            int[] Votes = new int[QTYBuses];

            foreach (var kvp in LatestArbData)
            {
                if (kvp.Value.ActiveBusID < QTYBuses)
                {
                    Votes[kvp.Value.ActiveBusID]++;
                }
            }

            int Winner = -1;

            for (int n = QTYBuses - 1; n > 0; n--)
            {
                if (Votes[n] != 0 && (Winner < 0 || Votes[n] > Votes[Winner]))
                {
                    Winner = n;
                }
            }

            return Winner;
        }

        /// <summary>
        /// Returns a flight controller for building status
        /// </summary>
        /// <param name="BusID">The flight controller bus ID.</param>
        /// <returns>The flight controller object for building status.</returns>
        TStatus.TFlightController GetFCForStatus(int BusID, int BusVotedFor)
        {
            TFunctionalState AssumedFS = (BusID == BusVotedFor) ? TFunctionalState.ACTIVE : ((BusVotedFor < 0) ? TFunctionalState.UNKNOWN : TFunctionalState.STANDBY);
            float AssumedEKFErrorScore = float.NaN;
            UInt64 AssumedUpTime = TStatus.TFlightController.TIME_UNKNOWN;

            foreach (var kvpFC in _ArbData)
            {
                foreach (var kvpArb in kvpFC.Value)
                {
                    if (kvpArb.Value.BusID == BusID)
                    {
                        if (_FCData.ContainsKey(kvpFC.Key))
                        {
                            AssumedEKFErrorScore = _FCData[kvpFC.Key].EKFErrorScore;
                            AssumedUpTime = _FCData[kvpFC.Key].UpTimeUpdated;
                            if (AssumedFS == TFunctionalState.UNKNOWN)
                            {
                                AssumedFS = _FCData[kvpFC.Key].FS;
                            }
                        }
                    }
                    else
                    {
                        if (_FCData.ContainsKey(kvpFC.Key) && (AssumedFS == TFunctionalState.UNKNOWN) && (_FCData[kvpFC.Key].FS == TFunctionalState.ACTIVE))
                        {
                            AssumedFS = TFunctionalState.STANDBY;
                        }
                    }
                }
            }

            return new TStatus.TFlightController(AssumedFS, TStatus.TFlightController.TIME_UNKNOWN, AssumedEKFErrorScore, 
                AssumedUpTime, BusID);
        }

        /// <summary>
        /// Get the current status assuming there are the given quantity of flight controllers.
        /// </summary>
        /// <param name="QTYFlightControllers">The assumed quantity of flight controllers.</param>
        /// <returns>The current status.  Never null.</returns>
        public TStatus GetStatusAssumingNFlightControllers(int QTYFlightControllers)
        {
            List<TStatus.TFlightController> FlightControllers = new List<TStatus.TFlightController>();
            int BusVotedFor = GetBusVotedFor(QTYFlightControllers);

            for (int n = 0; n < QTYFlightControllers; n++)
            {
                FlightControllers.Add(GetFCForStatus(n, BusVotedFor));
            }

            return GetStatusAssumingFlightControllers(FlightControllers);
        }

        /// <summary>
        /// Get the function status deemed to the given bus by the given arbiter.
        /// </summary>
        /// <param name="BusID">The bus ID.</param>
        /// <param name="Arbiter">The arbiter ID.</param>
        /// <returns>The functional state deemed by the arbiter.</returns>
        TFunctionalState GetDeeming(int BusID, TArbID Arbiter)
        {
            foreach (var kvpFC in _ArbData)
            {
                if (kvpFC.Value.ContainsKey(Arbiter))
                {
                    return (kvpFC.Value[Arbiter].ActiveBusID == BusID) ? TFunctionalState.ACTIVE : TFunctionalState.STANDBY;
                }
            }

            return TFunctionalState.UNKNOWN;
        }

        /// <summary>
        /// Get the current system status assuming the given list of flight controllers.
        /// </summary>
        /// <param name="FlightControllersIn">The flight controllers.  Must not be null.</param>
        /// <returns>The current system status.  Never null.</returns>
        TStatus GetStatusAssumingFlightControllers(List<TStatus.TFlightController> FCs)
        {
            Dictionary<TArbID, TStatus.TArbiter> Arbs = new Dictionary<TArbID, TStatus.TArbiter>();

            foreach (var kvp in _ArbData)
            {
                foreach (var kvpArb in kvp.Value)
                {
                    if (!Arbs.ContainsKey(kvpArb.Key))
                    {
                        Arbs[kvpArb.Key] = new TStatus.TArbiter(new TFunctionalState[FCs.Count], kvpArb.Key);
                    }
                }
            }

            foreach (var kvp in Arbs)
            {
                for (int n = 0; n < FCs.Count; n++)
                {
                    kvp.Value.Deeming[n] = GetDeeming(n, kvp.Key);
                }
            }

            List<TStatus.TArbiter> ArbList = new List<TStatus.TArbiter>(Arbs.Values);

            return new TStatus(FCs.ToArray(), ArbList.ToArray());
        }

        class TFCForStatus
        {
            public readonly TFunctionalState FS;
            public readonly UInt64 TimeInState;
            public readonly float EKFErrorScore;
            public readonly UInt64 UpTime;
            public readonly int BusID;

            public TFCForStatus(TFunctionalState FS, UInt64 TimeInState, float EKFErrorScore, UInt64 UpTime, int BusID)
            {
                this.FS = FS;
                this.TimeInState = TimeInState;
                this.EKFErrorScore = EKFErrorScore;
                this.UpTime = UpTime;
                this.BusID = BusID;
            }
        }

        class TArbDataCollection : Dictionary<TArbID, TRxDataFromArbiter>
        {
        }
    }

    /// <summary>
    /// For representing a snapshot of the status of the redundant flight controller/arbitration system.
    /// </summary>
    public class TStatus
    {
        public readonly TFlightController[] FlightControllers;
        public readonly TArbiter[] Arbiters;

        public TStatus(TFlightController[] FlightControllers, TArbiter[] Arbiters)
        {
            this.FlightControllers = FlightControllers;
            this.Arbiters = Arbiters;
        }

        public class TFlightController
        {
            public readonly TFunctionalState FunctionalState;
            /// <summary>
            /// milliseconds
            /// </summary>
            public readonly UInt64 TimeInState;
            public readonly float ErrorScore;
            /// <summary>
            /// milliseconds
            /// </summary>
            public readonly UInt64 UpTime;
            public readonly int BusID;
            public const UInt64 TIME_UNKNOWN = 0xFFFFFFFFFFFFFFFF;

            public TFlightController(TFunctionalState FunctionalState, UInt64 TimeInState, float ErrorScore, UInt64 UpTime, int BusID)
            {
                this.FunctionalState = FunctionalState;
                this.TimeInState = TimeInState;
                this.ErrorScore = ErrorScore;
                this.UpTime = UpTime;
                this.BusID = BusID;
            }
        }

        public class TArbiter
        {
            public readonly TFunctionalState[] Deeming;
            public readonly UInt64 UniqueID;

            public TArbiter(TFunctionalState[] Deeming, UInt64 UniqueID)
            {
                this.Deeming = Deeming;
                this.UniqueID = UniqueID;
            }
        }
    }

}