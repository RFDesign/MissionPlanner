using RFD.RFD900;
using RFDCommon.Radio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        public string ATI1 { get; set; }
        public string ATI2 { get; set; }
        public string ATI4 { get; set; }

        public string COUNTRY { get; set; }


        private string _freq;
        public string FREQ
        {
            get => _freq;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _freq = value;
                    return;
                }

                NumberStyles style = NumberStyles.Any;
                int multipoint_fix = -1;
                //Some multipoint firmware versions don't reply to ATI command with [n] at start of reply, but they do for ATI3 command, so check for [n] again...
                if (multipoint_fix < 0 && value.StartsWith("["))
                {
                    multipoint_fix = value.IndexOf(']') + 1;
                }

                if (multipoint_fix > 0)
                {
                    value = value.Substring(multipoint_fix).Trim();
                }

                if (value.ToLower().Contains("x"))
                    style = NumberStyles.AllowHexSpecifier;

                try
                {
                    var freq = (Uploader.Frequency)Enum.Parse(
                        typeof(Uploader.Frequency),
                        int.Parse(value.ToLower().Replace("x", ""),
                        style
                    ).ToString());
                    _freq = freq.ToString();
                    return;
                }
                catch (Exception ex)
                {
                    _freq = Uploader.Frequency.FAILED.ToString();
                }
            }
        }
            

        public string BOARD { get; set; }
        public TSettings Settings { get; set; }

        public Type SettingType(string key)
        {
            if (Settings.WorkingSettings.TryGetValue(key, out var value))
            {
                return value.GetType();
            }
            return null;
        }

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
                var nodeId = Get<TSetting>("NODEID");
                if (nodeId != null)
                {
                    name = $"[{nodeId.Value}] {name}";
                }
                return name;
            }
        }
    }
}
