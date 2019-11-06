using System;
using System.Collections.Generic;

/// <summary>
/// Flight termination system.
/// </summary>

namespace FTS
{
    public static class Manager
    {
        public static List<MissionPlanner.MAVState> GetMavStates()
        {
            return new List<MissionPlanner.MAVState>(MissionPlanner.MainV2.comPort.MAVlist);
        }

        public static bool AreMavStatesTheSame(MissionPlanner.MAVState MS1, MissionPlanner.MAVState MS2)
        {
            return (MS1.sysid == MS2.sysid) && (MS1.compid == MS2.compid);
        }
    }

    public class TSingleFTSManager
    {
        const string TERM_PARAM_NAME = "AFS_TERMINATE";
        const string GEOFENCE_PARAM_NAME = "AFS_GEOFENCE";
        MissionPlanner.MAVState _MS;
        System.Diagnostics.Stopwatch _Start;
        Int64 _LastParamRequest = 0;
        bool _GeofenceTerm = false;

        public TSingleFTSManager(MissionPlanner.MAVState MS)
        {
            _MS = MS;
            _Start = new System.Diagnostics.Stopwatch();
            _Start.Start();
        }

        public MissionPlanner.MAVState MS
        {
            get
            {
                return _MS;
            }
        }

        double GetParamValue(string ParamName)
        {
            return (double)_MS.param[ParamName].Value;
        }

        /// <summary>
        /// 0 represents dead link, 100 represents perfect link.
        /// </summary>
        /// <returns></returns>
        public float GetLinkStatus()
        {
            return _MS.cs.linkqualitygcs;
        }

        float GetRSSIAsdBm(float CSValue)
        {
            if (CSValue == 0)
            {
                return float.NaN;
            }
            else
            {
                return (CSValue / 2) - 152;
            }
        }

        /// <summary>
        /// Get rx RSSI.
        /// </summary>
        /// <returns></returns>
        public float GetRxRSSI()
        {
            return GetRSSIAsdBm(_MS.cs.rssi);
        }

        /// <summary>
        /// Get Tx RSSI
        /// </summary>
        /// <returns></returns>
        public float GetTxRSSI()
        {
            return GetRSSIAsdBm(_MS.cs.remrssi);
        }

        public bool GetFTSHealth()
        {
            return _MS.cs.hasHeartBeat;
        }

        /// <summary>
        /// Get the remote flight termination state.
        /// </summary>
        /// <returns></returns>
        public TRemoteState GetRemoteState()
        {
            if ((_Start.ElapsedMilliseconds - _LastParamRequest) > 2000)
            {
                MissionPlanner.MainV2.comPort.GetParam(_MS.sysid, _MS.compid, TERM_PARAM_NAME, -1, false);
                MissionPlanner.MainV2.comPort.GetParam(_MS.sysid, _MS.compid, GEOFENCE_PARAM_NAME, -1, false);
                _LastParamRequest = _Start.ElapsedMilliseconds;
            }

            try
            {
                var P = _MS.param[TERM_PARAM_NAME];
                if (P == null)
                {
                    return TRemoteState.ERROR;
                }
                else
                {
                    if (P.Value == 0)
                    {
                        _GeofenceTerm = false;
                        return TRemoteState.NORMAL;
                    }
                    else
                    {
                        var GP = _MS.param[GEOFENCE_PARAM_NAME];

                        if (GP == null)
                        {
                            return TRemoteState.ERROR;
                        }
                        else
                        {
                            if (_GeofenceTerm)
                            {
                                return TRemoteState.TERMINATING_GEOFENCE;
                            }
                            else
                            {
                                if ((GP.Value == 1) && _MS.cs.GeoFenceBreached)
                                {
                                    _GeofenceTerm = true;
                                    return TRemoteState.TERMINATING_GEOFENCE;
                                }
                                else
                                {
                                    return TRemoteState.TERMINATING_MANUAL;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return TRemoteState.ERROR;
            }
        }

        /// <summary>
        /// Manually terminate the flight
        /// </summary>
        public void TerminateFlight()
        {
            MissionPlanner.MainV2.comPort.setParam(_MS.sysid, _MS.compid, TERM_PARAM_NAME, 1);
        }

        public string GetRemoteStateDescription(TRemoteState RS)
        {
            switch (RS)
            {
                default:
                case TRemoteState.ERROR:
                    return "Error";
                case TRemoteState.NORMAL:
                    return "Normal";
                case TRemoteState.TERMINATING_MANUAL:
                    return "Manual Term.";
                case TRemoteState.TERMINATING_GEOFENCE:
                    return "Geofence Term.";
            }
        }

        public enum TRemoteState
        {
            ERROR,
            NORMAL,
            TERMINATING_MANUAL,
            TERMINATING_GEOFENCE,
        }
    }
}
