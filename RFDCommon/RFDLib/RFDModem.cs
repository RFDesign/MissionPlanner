using RFD.RFD900;
using RFDCommon.Radio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace RFDCommon.RFDLib
{
    public class RFDModem
    {
        public Int64 DeviceId { get; set; }
        public bool AES_ENABLED { get; set; }
                
        public string RSSI { get; set; }
        
        public string ATI { get; set; }
        public string COUNTRY { get; set; }

        public string FREQ { get; set; }

        public string BOARD { get; set; }
        public TSettings Settings { get; set; }

        public T Get<T>(string key) where T : TBaseSetting
        {      
            if (Settings?.WorkingSettings == null)
            {
                return default;
            }
            if (Settings.WorkingSettings.TryGetValue(key, out var value))
            {
                return (T)value;
            }
            return default;
        }
        public void Set<T>(string key, T value) where T : TBaseSetting
        {
            Settings.WorkingSettings[key] = value;
        }

        public FirmwareMode Mode { get; set; }

        public bool IsLocal { get; set; }

        public string DisplayName
        {
            get
            {
                string name = IsLocal 
                    ? $"Local  - {ATI}" 
                    : $"Remote - {ATI}";
                var nodeId = Get<TSetting>("NODE_ID");
                if (nodeId != null)
                {
                    name += $" [{nodeId.Value}]";
                }
                return name;
            }
        }
    }
}
