using System;
using System.Collections.Generic;

namespace RFD.Arbitration
{
    using TArbID = UInt64;

    public class TRxDataFromArbiter
    {
        public readonly byte BusID;
        public readonly byte ActiveBusID;
        public readonly UInt64 UniqueID;

        public TRxDataFromArbiter(byte BusID, byte ActiveBusID, UInt64 UniqueID)
        {
            this.BusID = BusID;
            this.ActiveBusID = ActiveBusID;
            this.UniqueID = UniqueID;
        }
    }

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

    public class TRxDataFromFlightController
    {
        public readonly TFunctionalState FS;
        public readonly UInt64 TimeInState;
        public readonly UInt64 UpTime;
        public readonly float EKFErrorScore;

        public TRxDataFromFlightController(TFunctionalState FS,
            UInt64 TimeInState, UInt64 UpTime, float EKFErrorScore)
        {
            this.FS = FS;
            this.TimeInState = TimeInState;
            this.UpTime = UpTime;
            this.EKFErrorScore = EKFErrorScore;
        }
    }

    public enum TFunctionalState
    {
        STANDBY,
        ACTIVE,
        UNKNOWN,
    }

    public class TArbitrationState
    {
        Dictionary<TFCID, TArbDataCollection> _ArbData = new Dictionary<TFCID, TArbDataCollection>();
        Dictionary<TFCID, TRxDataFromFlightController> _FCData = new Dictionary<TFCID, TRxDataFromFlightController>();
        int _ChangeCount = 0;

        public void ProcessDataFromArb(TFCID FCID, TRxDataFromArbiter Data)
        {
            if (!_ArbData.ContainsKey(FCID))
            {
                _ArbData[FCID] = new TArbDataCollection();
                _ChangeCount++;
            }
            _ArbData[FCID][Data.UniqueID] = Data;
        }

        public void ProcessDataFromFC(TFCID FCID, TRxDataFromFlightController Data)
        {
            _FCData[FCID] = Data;
        }

        public void GotMsg(MAVLink.mavlink_arb_use_fc_t msg)
        {
            
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
                TStatus.TFlightController FC = new TStatus.TFlightController(kvp.Value.FS, kvp.Value.TimeInState, 
                    kvp.Value.EKFErrorScore, kvp.Value.UpTime, GetBusIDForFC(kvp.Key));
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

        class TArbDataCollection : Dictionary<TArbID, TRxDataFromArbiter>
        {
        }
    }

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