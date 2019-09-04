using System;
using System.Collections.Generic;

namespace RFD.Arbitration
{
    public class TMAVLinkParser
    {
        public readonly Arbitration.TArbitrationState State = new TArbitrationState();

        public TMAVLinkParser()
        {
        }

        void GotUseFCMsg(byte SysID, byte CompID, MAVLink.mavlink_arb_use_fc_t msg)
        {
            State.ProcessDataFromArb(new TFCID(SysID, CompID),
                new TRxDataFromArbiter(msg.BusID, msg.BusIDToUse, msg.UniqueID));
        }

        void GotFCStatusMsg(byte SysID, byte CompID, MAVLink.mavlink_arb_fc_status_t msg)
        {
            State.ProcessDataFromFC(new TFCID(SysID, CompID),
                new TRxDataFromFlightController(MAVFSToFS((MAVLink.ARB_FUNCTIONAL_STATE)msg.FunctionalState), 
                msg.TimeInState, msg.UpTime, msg.EKFErrorScore));
        }

        void CheckPacket<TStruct>(MissionPlanner.MAVState MAV,
            MAVLink.MAVLINK_MSG_ID ID, byte SysID, byte CompID, Action<byte, byte, TStruct> Hdlr)
        {
            var mavLinkMessage = MAV.getPacket((uint)ID);
            if (mavLinkMessage != null)
            {
                Hdlr(SysID, CompID, mavLinkMessage.ToStructure<TStruct>());
            }
        }

        public void CheckPackets(MissionPlanner.MAVState MAV, byte SysID, byte CompID)
        {
            CheckPacket<MAVLink.mavlink_arb_use_fc_t>(MAV, MAVLink.MAVLINK_MSG_ID.ARB_USE_FC, SysID, CompID, GotUseFCMsg);
            CheckPacket<MAVLink.mavlink_arb_fc_status_t>(MAV, MAVLink.MAVLINK_MSG_ID.ARB_FC_STATUS, SysID, CompID, GotFCStatusMsg);
        }

        TFunctionalState MAVFSToFS(MAVLink.ARB_FUNCTIONAL_STATE MAVFS)
        {
            switch (MAVFS)
            {
                case MAVLink.ARB_FUNCTIONAL_STATE.ACTIVE:
                    return TFunctionalState.ACTIVE;
                case MAVLink.ARB_FUNCTIONAL_STATE.STANDBY:
                    return TFunctionalState.STANDBY;
                default:
                case MAVLink.ARB_FUNCTIONAL_STATE.UNKNOWN:
                    return TFunctionalState.UNKNOWN;
            }
        }
    }
}