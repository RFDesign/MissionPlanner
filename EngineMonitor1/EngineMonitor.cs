using System;
using System.Collections.Generic;

namespace RFD.EngineMonitor
{
    public class TValid<T>
    {
        bool _Valid = false;
        T _Value;

        public void Set(T Value)
        {
            _Value = Value;
            _Valid = true;
        }

        public bool Valid
        {
            get
            {
                return _Valid;
            }
        }

        public T Value
        {
            get
            {
                return _Value;
            }
        }

    }

    public class TEngineStatus
    {
        public TValid<MAVLink.mavlink_ice_engine_data_t> Data = 
            new TValid<MAVLink.mavlink_ice_engine_data_t>();
        public TValid<MAVLink.mavlink_ice_engine_status_t> Status = 
            new TValid<MAVLink.mavlink_ice_engine_status_t>();
        public TValid<MAVLink.mavlink_ice_engine_exh_temp_1_t> ExhTemp1 = 
            new TValid<MAVLink.mavlink_ice_engine_exh_temp_1_t>();
        public TValid<MAVLink.mavlink_ice_engine_exh_temp_2_t> ExhTemp2 = 
            new TValid<MAVLink.mavlink_ice_engine_exh_temp_2_t>();
        public TValid<MAVLink.mavlink_ice_engine_fault_stats_1_t> FaultStats1 = 
            new TValid<MAVLink.mavlink_ice_engine_fault_stats_1_t>();
        public TValid<MAVLink.mavlink_ice_engine_fault_stats_2_t> FaultStats2 = 
            new TValid<MAVLink.mavlink_ice_engine_fault_stats_2_t>();
    }

    public class TVehicleStatus
    {
        object _Locker = new object();
        Dictionary<uint, TEngineStatus> _EngineStatus = new Dictionary<uint, TEngineStatus>();

        void GotMsg<T>(uint EngNo, T msg, Func<TEngineStatus, TValid<T>> PropertyGetFn)
        {
            lock (_Locker)
            {
                if (!_EngineStatus.ContainsKey(EngNo))
                {
                    _EngineStatus[EngNo] = new TEngineStatus();
                }

                PropertyGetFn(_EngineStatus[EngNo]).Set(msg);
            }
        }

        public void GotMsg(MAVLink.mavlink_ice_engine_data_t msg)
        {
            GotMsg(msg.EngineNumber, msg, (s) => s.Data);
        }

        public void GotMsg(MAVLink.mavlink_ice_engine_status_t msg)
        {
            GotMsg(msg.EngineNumber, msg, (s) => s.Status);
        }

        public void GotMsg(MAVLink.mavlink_ice_engine_exh_temp_1_t msg)
        {
            GotMsg(msg.EngineNumber, msg, (s) => s.ExhTemp1);
        }

        public void GotMsg(MAVLink.mavlink_ice_engine_exh_temp_2_t msg)
        {
            GotMsg(msg.EngineNumber, msg, (s) => s.ExhTemp2);
        }

        public void GotMsg(MAVLink.mavlink_ice_engine_fault_stats_1_t msg)
        {
            GotMsg(msg.EngineNumber, msg, (s) => s.FaultStats1);
        }

        public void GotMsg(MAVLink.mavlink_ice_engine_fault_stats_2_t msg)
        {
            GotMsg(msg.EngineNumber, msg, (s) => s.FaultStats2);
        }

        public void GotMsg(MAVLink.MAVLinkMessage msg, MAVLink.MAVLINK_MSG_ID ID)
        {
            switch (ID)
            {
                case MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_DATA:
                    {
                        var es = msg.ToStructure<MAVLink.mavlink_ice_engine_data_t>();
                        GotMsg(es);
                    }
                    break;
                case MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_EXH_TEMP_1:
                    {
                        var es = msg.ToStructure<MAVLink.mavlink_ice_engine_exh_temp_1_t>();
                        GotMsg(es);
                    }
                    break;
                case MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_EXH_TEMP_2:
                    {
                        var es = msg.ToStructure<MAVLink.mavlink_ice_engine_exh_temp_2_t>();
                        GotMsg(es);
                    }
                    break;
                case MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_FAULT_STATS_1:
                    {
                        var es = msg.ToStructure<MAVLink.mavlink_ice_engine_fault_stats_1_t>();
                        GotMsg(es);
                    }
                    break;
                case MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_FAULT_STATS_2:
                    {
                        var es = msg.ToStructure<MAVLink.mavlink_ice_engine_fault_stats_2_t>();
                        GotMsg(es);
                    }
                    break;
                case MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_STATUS:
                    {
                        var es = msg.ToStructure<MAVLink.mavlink_ice_engine_status_t>();
                        GotMsg(es);
                    }
                    break;
            }
        }

        void CheckPacket(MissionPlanner.MAVState MAV,
            MAVLink.MAVLINK_MSG_ID ID, RFD.EngineMonitor.TVehicleStatus VS)
        {
            var mavLinkMessage = MAV.getPacket((uint)ID);
            if (mavLinkMessage != null)
            {
                VS.GotMsg(mavLinkMessage, ID);
            }
        }

        public void CheckPackets(MissionPlanner.MAVState MAV,
            RFD.EngineMonitor.TVehicleStatus VS)
        {
            CheckPacket(MAV, MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_DATA, VS);
            CheckPacket(MAV, MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_EXH_TEMP_1, VS);
            CheckPacket(MAV, MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_EXH_TEMP_2, VS);
            CheckPacket(MAV, MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_FAULT_STATS_1, VS);
            CheckPacket(MAV, MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_FAULT_STATS_2, VS);
            CheckPacket(MAV, MAVLink.MAVLINK_MSG_ID.ICE_ENGINE_STATUS, VS);
        }

        public TEngineStatus GetEngineStatus(uint Number)
        {
            lock (_Locker)
            {
                if (_EngineStatus.ContainsKey(Number))
                {
                    return _EngineStatus[Number];
                }
                else
                {
                    return null;
                }
            }
        }

        public List<uint> GetEngineNumbers()
        {
            List<uint> Result = new List<uint>();
            foreach (var kvp in _EngineStatus)
            {
                Result.Add(kvp.Key);
            }
            return Result;
        }
    }
}