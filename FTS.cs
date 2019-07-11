using System;

/// <summary>
/// Flight termination system.
/// </summary>

namespace FTS
{
    public static class Manager
    {
        const string TERM_PARAM_NAME = "AFS_TERMINATE";

        static double GetParamValue(string ParamName)
        {
            return (double)MissionPlanner.MainV2.comPort.MAV.param[ParamName].Value;
        }

        /// <summary>
        /// 0 represents dead link, 100 represents perfect link.
        /// </summary>
        /// <returns></returns>
        public static float GetLinkStatus()
        {
            return MissionPlanner.MainV2.comPort.MAV.cs.linkqualitygcs;
        }

        /// <summary>
        /// Get rx RSSI.
        /// </summary>
        /// <returns></returns>
        public static float GetRxRSSI()
        {
            return MissionPlanner.MainV2.comPort.MAV.cs.rssi;
        }

        /// <summary>
        /// Get Tx RSSI
        /// </summary>
        /// <returns></returns>
        public static float GetTxRSSI()
        {
            return MissionPlanner.MainV2.comPort.MAV.cs.remrssi;
        }

        public static bool GetFTSHealth()
        {
            return MissionPlanner.MainV2.comPort.MAV.cs.hasHeartBeat;
        }

        /// <summary>
        /// Get the remote flight termination state.
        /// </summary>
        /// <returns></returns>
        public static TRemoteState GetRemoteState()
        {
            var P = MissionPlanner.MainV2.comPort.MAV.param[TERM_PARAM_NAME];
            if (P == null)
            {
                return TRemoteState.ERROR;
            }
            else
            {
                return (P.Value != 0) ? TRemoteState.TERMINATING : TRemoteState.NORMAL;
            }
        }

        /// <summary>
        /// Manually terminate the flight
        /// </summary>
        public static void TerminateFlight()
        {
            MissionPlanner.MainV2.comPort.setParam(TERM_PARAM_NAME, 1);
        }

        public static string GetRemoteStateDescription(TRemoteState RS)
        {
            switch (RS)
            {
                default:
                case TRemoteState.ERROR:
                    return "Error";
                case TRemoteState.NORMAL:
                    return "Normal";
                case TRemoteState.TERMINATING:
                    return "Terminating";
            }
        }

        public enum TRemoteState
        {
            ERROR,
            NORMAL,
            TERMINATING,
        }
    }
}
