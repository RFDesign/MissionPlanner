using MissionPlanner.Comms;
using RFD.RFD900;

using RFDCommon.Interface;
using RFDCommon.Radio;
using RFDCommon.RFDLib;
using RFDLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace RFDCommon
{
    public class ConfigManager : INotifyPropertyChanged
    {
        

        private IModemComms _modemComms;
        public ConfigManager(IModemComms modemComms)
        {
            _modemComms = modemComms;
        }
        
        // Clear all loaded settings and modems
        public void ClearSettings()
        {
            // Clear modems
            _modems.Clear();
            OnPropertyChanged();
        }

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<MessageBoxEventArgs> ShowMessageBox;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void ShowBox(string title, string text)
        {
            ShowMessageBox?.Invoke(this, new MessageBoxEventArgs(title, text));
        }
        #endregion

        #region Modem Settings
        private Dictionary<Int64,RFDModem> _modems = new Dictionary<Int64,RFDModem>();
        public List<RFDModem> Modems
        {
            get { return _modems.Values.ToList(); }
            set { _modems = value.ToDictionary((x) => x.DeviceId); OnPropertyChanged(); }
        }
        private RFDModem _currentModem = new RFDModem();
        public RFDModem Current
        {
            get { return _currentModem; }
            set {
                if (_currentModem?.DeviceId == value?.DeviceId)
                    return;
                _currentModem = value;
                OnPropertyChanged(nameof(Current));
                Task.Delay(500);
                OnPropertyChanged(null);                
            }
        }
        public RFDModem Local
        {
            get { return Modems.FirstOrDefault(x => x.IsLocal); }
        }
        public RFDModem Remote
        {
            get { return Modems.FirstOrDefault(x => !x.IsLocal); }
        }
        public RFD900 Modem { get => _modemComms.GetSession().GetModemObject(); }
        public TSettings GetChangedSettings(RFDModem modem)
        {
            if (modem?.Settings?.WorkingSettings == null)
            {
                return new TSettings(new Dictionary<string, TBaseSetting>());
            }
            var updatedSettings = modem.Settings?.WorkingSettings.Where(s => s.Value.GetValueAsString() != modem.Settings.Settings[s.Key].GetValueAsString()).ToDictionary(s => s.Key, s => s.Value);

            TSettings settings = new TSettings(updatedSettings);
            return settings;
        }
        public bool IsDirty => GetChangedSettings(Current).Settings.Count() > 0;
        public bool ValidateWorkingSettings(RFDModem modem)
        {
            var updatedSettings = GetChangedSettings(modem);
            var errors = updatedSettings.CheckValid();
            if (errors.Length == 0)
            {
                return true;
            }
            else
            {
                // Build a string from the array of errors


                string errorMsg = $"Settings invalid on device ({modem.DisplayName}), operation aborted:";

                foreach (var em in errors)
                {
                    errorMsg += "\n\t" + em;
                }

                ShowBox("Invalid Settings", errorMsg);

                return false;
            }
        }

        #endregion

        #region AutoSync
        private bool _autoSync = true;
        public bool AutoSync
        {
            get { return _autoSync; }
            set {
                if (value == _autoSync) return;
                _autoSync = value;
                OnPropertyChanged(nameof(AutoSync));
            }
        }
        public string[] AutoSyncProperties = { "AIR_SPEED", "NETID", "MAVLINK", "MIN_FREQ", "MAX_FREQ", "NUM_CHANNELS", "MAX_WINDOW", "ENCRYPTION_LEVEL", "AESKEY" };
        private void DoAutoSync<T>(T setting) where T : TBaseSetting
        {
            if (!AutoSync)
                return;

            if (AutoSyncProperties.Contains(setting.Name) && Remote?.ATI != null)
            {
                AddLog($"Auto-Sync trigger on {setting.Name}: {setting.GetValueAsString()}",true);
                // Sync other?
                if (_currentModem.IsLocal)
                {
                    Remote.Get<T>(setting.Name).SetValueFromString(setting.GetValueAsString());
                } else
                {
                    Local.Get<T>(setting.Name).SetValueFromString(setting.GetValueAsString());
                }
            }
        }
        #endregion

        #region Log Console
        private StringBuilder _log = new StringBuilder();
        public string Log => _log.ToString();        
        public void AddLog(string log, bool updateStatus = false) {
            _log.AppendLine(log);
            if (updateStatus ) {
                _lastLog = log;
            }            
            OnPropertyChanged("Log");

            WriteConsoleToTempFile(log);
        }
        private string _lastLog = string.Empty;
        public string LastLog => _lastLog;

        private string _tempFilePath;
        public void WriteConsoleToTempFile(string line)
        {
            try
            {
                // Generate a unique temporary file name. 
                // Path.GetTempFileName creates a 0-byte file and returns the path; we can overwrite it.
                if (string.IsNullOrWhiteSpace(_tempFilePath))
                {
                    _tempFilePath = Path.GetTempFileName();
                    AddLog($"Initializing temporary file for console log: {_tempFilePath}");
                }

                // Using StreamWriter to write to the temp file.
                using (StreamWriter writer = new StreamWriter(_tempFilePath, true))
                {
                    writer.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions here
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }


        #endregion

        #region Progress?
        public void UpdateProgress(string status, double progress)
        {
            if (status != null)
            {
                AddLog(status);
            }
            if (!double.IsNaN(progress))
            {
                Progress = progress;
            }
        }
        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set { 
                _progress = value;
                OnPropertyChanged();
            }
        }
        public const double PROGRESS_INCREMENT = 0.015;
        public void IncrementProgress()
        {
            Progress += PROGRESS_INCREMENT;
        }
        public void ClearProgress()
        {
            Progress = 0;
        }
        public void CompleteProgress()
        {
            Progress = 1;
        }
        #endregion

        public bool FirmwareUpdateInProgress { get; set; } = false;

        #region Button Enabled Properties
        public bool LoadEnabled => _modemComms.IsConnected() && !FirmwareUpdateInProgress;
        public bool SaveEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool ImportEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool ExportEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool ResetEnabled => _modemComms.IsConnected() && !FirmwareUpdateInProgress;
        public bool FirmwareEnabled => _modemComms.IsConnected() && !FirmwareUpdateInProgress; // && _currentModem?.Settings != null;
        public bool RebootEnabled => _modemComms.IsConnected() && !FirmwareUpdateInProgress;
        #endregion

        #region GroupBox Enabled Properties
        public bool DeviceGroupEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null;
        public bool SerialEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool RadioEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool SecurityEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool PinEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool InfoEnabled => _modemComms.IsConnected() && _currentModem?.Settings != null && !FirmwareUpdateInProgress;
        public bool DataEnabled => _modemComms.IsConnected() && _currentModem != null && (_currentModem.Mode == FirmwareMode.MULTIPOINT || _currentModem.Mode == FirmwareMode.ASYNC) && !FirmwareUpdateInProgress;
        #endregion

        #region // Getters for read only properties
        public string ATI => _currentModem?.ATI;
        public string FREQ => _currentModem?.FREQ;
        public string BOARD => _currentModem?.BOARD;
        public string COUNTRY => _currentModem?.COUNTRY;
        public bool AES_ENABLED => _currentModem?.AES_ENABLED ?? false;
        public string RSSI => _currentModem?.RSSI;
        public string FORMAT => _currentModem?.Get<TSetting>("FORMAT")?.GetValueAsString();
        #endregion

        #region Pin Function Abstractions   

        public BindingList<PinFunction> GPIO1_Items { get; } = new BindingList<PinFunction>
        {
            new PinFunction() { Name= "None", Value = ""},
            new PinFunction() { Name = "R/CIN", Value = "GPI1_1R/CIN" },
            new PinFunction() { Name = "R/COUT", Value = "GPO1_1R/COUT" },
            new PinFunction() { Name = "SBUSIN", Value = "GPO1_1SBUSIN" },
            new PinFunction() { Name = "SBUSOUT", Value = "GPO1_1SBUSOUT" },
        };

        public class PinFunction
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public BindingList<PinFunction> GPIO3_Items { get; } = new BindingList<PinFunction>
        {
            new PinFunction() { Name= "None", Value = ""},
            new PinFunction() { Name = "AUXOUT", Value = "GPO1_3AUXOUT" },
            new PinFunction() { Name = "STATLED", Value = "GPO1_3STATLED" }
        };

        public BindingList<PinFunction> GPIO0_Items { get; } = new BindingList<PinFunction>
        {
            new PinFunction() { Name= "None", Value = ""},
            new PinFunction() { Name = "TXEN485", Value = "GPO1_0TXEN485" }
        };

        public BindingList<PinFunction> GPIO2_Items { get; } = new BindingList<PinFunction>
        {
            new PinFunction() { Name= "None", Value = ""},
            new PinFunction() { Name = "AUXIN", Value = "GPI1_2AUXIN" }
        };
                
        public string GPIO3
        {            
            get
            {
                return GetPinSetting(GPIO3_Items);
            }
            set
            {
                if (value == GetPinSetting(GPIO3_Items)) return;

                SetPin(GPIO3_Items, value);
                OnPropertyChanged();
            }
        }

        public string GPIO0
        {
            get
            {
                return GetPinSetting(GPIO0_Items);
            }
            set
            {
                if (value == GetPinSetting(GPIO0_Items)) return;

                SetPin(GPIO0_Items, value);
                OnPropertyChanged();
            }
        }
                
        public string GPIO2
        {
            get
            {
                return GetPinSetting(GPIO2_Items);
            }
            set
            {
                if (value == GetPinSetting(GPIO2_Items)) return;

                SetPin(GPIO2_Items, value);
                OnPropertyChanged();
            }
        }

        public string GPIO1
        {
            get
            {
                return GetPinSetting(GPIO1_Items);
            }
            set
            {
                if (value == GetPinSetting(GPIO1_Items)) return;

                SetPin(GPIO1_Items, value);
                //Task.Delay(1500);
                OnPropertyChanged(nameof(GPIO1));
            }
        }

        public string GetGPIOSetting(BindingList<PinFunction> settings, RFDModem modem)
        {
            if (modem?.Settings == null)
                return string.Empty;
            foreach (var item in settings)
            {
                // Ignore 'None'
                if (item.Value == "")
                    continue;
                var val = modem.Get<TSetting>(item.Value)?.Value;
                if (val == 1)
                {
#if DEBUG
                    //AddLog($"GPIO Func is on {(_currentModem.IsLocal ? "Local" : "Remote")}: {item.Value}");
#endif
                    return item.Value;
                }
            }
#if DEBUG
            //AddLog($"GPIO Func is not set");
#endif
            return string.Empty;
        }
        public string GetPinSetting(BindingList<PinFunction> settings)
        {
            return GetGPIOSetting(settings, _currentModem);            
        }
        private void SetPin(BindingList<PinFunction> settings, string value)
        {
            if (_currentModem?.Settings == null)
                return;

            foreach (var item in settings)
            {
                // Ignore 'None'
                if (item.Value == "")
                    continue;
                // Get Current
                var newVal = item.Value == value ? 1 : 0;
                _currentModem.Get<TSetting>(item.Value).Value = newVal;           
            }            
        }
        #endregion

        #region // Full properties for the editable properties     
        /*
         *  ^^ FORMAT
            SERIAL_SPEED
            AIR_SPEED
            NETID
            TXPOWER
            ECC
            MAVLINK
            OPPRESEND
            MIN_FREQ
            MAX_FREQ
            NUM_CHANNELS
            DUTY_CYCLE
            LBT_RSSI
            RTSCTS
            MAX_WINDOW
            ENCRYPTION_LEVEL
            GPI1_1R/CIN
            GPO1_1R/COUT
            GPO1_1SBUSIN
            GPO1_1SBUSOUT
            ANT_MODE
            GPO1_3STATLED
            GPO1_0TXEN485
            RATE/FREQBAND
            GPI1_2AUXIN
            GPO1_3AUXOUT
            AIR_FRAMELEN
            RSSI_IN_DBM
            FSFRAMELOSS
            AUXSER_SPEED
            AESKEY
         */
        public int SERIAL_SPEED
        {
            get => _currentModem?.Get<TSetting>(nameof(SERIAL_SPEED)).Value ?? default; 
            set
            {
                _currentModem.Get<TSetting>(nameof(SERIAL_SPEED)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(SERIAL_SPEED)));
                OnPropertyChanged(nameof(SERIAL_SPEED));                
            }
        }
        public int AIR_SPEED
        {
            get => _currentModem.Get<TSetting>("AIR_SPEED").Value;
            set
            {
                _currentModem.Get<TSetting>("AIR_SPEED").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(AIR_SPEED)));
                OnPropertyChanged(nameof(AIR_SPEED));
            }
        }
        public int NETID
        {
            get => _currentModem.Get<TSetting>("NETID").Value;
            set
            {
                _currentModem.Get<TSetting>("NETID").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(NETID)));
                OnPropertyChanged(nameof(NETID));
            }
        }
        public int TXPOWER
        {
            get => _currentModem.Get<TSetting>("TXPOWER").Value;
            set
            {
                _currentModem.Get<TSetting>("TXPOWER").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(TXPOWER)));
                OnPropertyChanged(nameof(TXPOWER));
            }
        }        

        public int RTSCTS
        {
            get => _currentModem?.Get<TSetting>("RTSCTS").Value ?? default;
            set
            {
                _currentModem.Get<TSetting>("RTSCTS").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(RTSCTS)));
                OnPropertyChanged(nameof(RTSCTS));
            }
        }

        public int MAVLINK
        {
            get => _currentModem.Get<TSetting>("MAVLINK").Value;
            set
            {
                _currentModem.Get<TSetting>("MAVLINK").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(MAVLINK)));
                OnPropertyChanged(nameof(MAVLINK));
            }
        }

        public int OPPRESEND
        {
            get => _currentModem.Get<TSetting>("OPPRESEND").Value;
            set
            {
                _currentModem.Get<TSetting>("OPPRESEND").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(OPPRESEND)));
                OnPropertyChanged(nameof(OPPRESEND));
            }
        }

        public int MIN_FREQ
        {
            get => _currentModem.Get<TSetting>("MIN_FREQ").Value;
            set
            {
                _currentModem.Get<TSetting>("MIN_FREQ").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(MIN_FREQ)));
                OnPropertyChanged(nameof(MIN_FREQ));
            }
        }

        public int MAX_FREQ
        {
            get => _currentModem.Get<TSetting>("MAX_FREQ").Value;
            set
            {
                _currentModem.Get<TSetting>("MAX_FREQ").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(MAX_FREQ)));
                OnPropertyChanged(nameof(MAX_FREQ));
            }
        }

        public int NUM_CHANNELS
        {
            get => _currentModem.Get<TSetting>("NUM_CHANNELS").Value;
            set
            {
                _currentModem.Get<TSetting>("NUM_CHANNELS").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(NUM_CHANNELS)));
                OnPropertyChanged(nameof(NUM_CHANNELS));
            }
        }

        public int DUTY_CYCLE
        {
            get => _currentModem.Get<TSetting>("DUTY_CYCLE").Value;
            set
            {
                _currentModem.Get<TSetting>("DUTY_CYCLE").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(DUTY_CYCLE)));
                OnPropertyChanged(nameof(DUTY_CYCLE));
            }
        }

        public int LBT_RSSI
        {
            get => _currentModem.Get<TSetting>("LBT_RSSI").Value;
            set
            {
                _currentModem.Get<TSetting>("LBT_RSSI").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(LBT_RSSI)));
                OnPropertyChanged(nameof(LBT_RSSI));
            }
        }

        public int MAX_WINDOW
        {
            get => _currentModem.Get<TSetting>("MAX_WINDOW").Value;
            set
            {
                _currentModem.Get<TSetting>("MAX_WINDOW").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(MAX_WINDOW)));
                OnPropertyChanged(nameof(MAX_WINDOW));
            }
        }

        public int ENCRYPTION_LEVEL
        {
            get => _currentModem.Get<TSetting>("ENCRYPTION_LEVEL").Value;
            set
            {
                var currentLevel = _currentModem.Get<TSetting>("ENCRYPTION_LEVEL");
                if (currentLevel.Value == value)
                    return;

                bool getKeyRequired = currentLevel.Value == 0 && value > 0;
                _currentModem.Get<TSetting>("ENCRYPTION_LEVEL").Value = value;
                
                // Does this need to be set immediateyl too?
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(ENCRYPTION_LEVEL)));
                
                OnPropertyChanged(nameof(ENCRYPTION_LEVEL));
                OnPropertyChanged(nameof(EncryptionEnabled));
                OnPropertyChanged(nameof(Encryption_Max_Key_Length));


                // Update Value immediately
                if (value == 0)
                {
                    
                    AESKEY = "";
                    return;
                }

                // When shifting from off to an on state, we must immediately set on the modem, in order to request the key
                if (getKeyRequired)
                {
                    EnableEncryption(Local, currentLevel.Designator, value);
                    if (Remote?.Settings != null)
                        EnableEncryption(Remote, currentLevel.Designator, value);
                    AESKEY = GetEncryptionKey(_currentModem.IsLocal).GetValueAsString();                                       
                }                
            }
        }

        private bool EnableEncryption(RFDModem modem, string designator, int value)
        {
            _modemComms.PutIntoATCommandMode();

            // Enable Encryption on all modems to retrieve key(s)?

            string command = $"{(modem.IsLocal ? "AT" : "RT")}{designator}={value}";
            var answer = _modemComms.DoCommand(command, true);
            AddLog($"{command} --> {answer}");

            _modemComms.PutIntoTransparentMode();
            return answer.Contains("OK");
        }
        public bool EncryptionEnabled
        {
            get {
                if (_currentModem?.Settings == null)
                    return false;
                return ENCRYPTION_LEVEL > 0; 
            }
        }
        public int Encryption_Max_Key_Length {
            get
            {
                switch (ENCRYPTION_LEVEL)
                {
                    case 1: // 128b
                        return 32;
                    case 2: // 256b
                        return 64;
                    case 0:
                    default:
                        return 0;
                        break;
                }                
            }
        }

        public int GPI1_1R_CIN
        {
            get => _currentModem.Get<TSetting>("GPI1_1R/CIN").Value;
            set
            {
                _currentModem.Get<TSetting>("GPI1_1R/CIN").Value = value;
                OnPropertyChanged(nameof(GPI1_1R_CIN));
            }
        }

        public int GPO1_1R_COUT
        {
            get => _currentModem.Get<TSetting>("GPO1_1R/COUT").Value;
            set
            {
                _currentModem.Get<TSetting>("GPO1_1R/COUT").Value = value;
                OnPropertyChanged(nameof(GPO1_1R_COUT));
            }
        }   

        public int GPO1_1SBUSIN
        {
            get => _currentModem.Get<TSetting>("GPO1_1SBUSIN").Value;
            set
            {
                _currentModem.Get<TSetting>("GPO1_1SBUSIN").Value = value;
                OnPropertyChanged(nameof(GPO1_1SBUSIN));
            }
        }

        public int GPO1_1SBUSOUT
        {
            get => _currentModem.Get<TSetting>("GPO1_1SBUSOUT").Value;
            set
            {
                _currentModem.Get<TSetting>("GPO1_1SBUSOUT").Value = value;
                OnPropertyChanged(nameof(GPO1_1SBUSOUT));
            }
        }

        public int ANT_MODE
        {
            get => _currentModem.Get<TSetting>("ANT_MODE").Value;
            set
            {
                _currentModem.Get<TSetting>("ANT_MODE").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(ANT_MODE)));
                OnPropertyChanged(nameof(ANT_MODE));
            }
        }
        // NEED an options binding property?
        public int GPO1_3STATLED
        {
            get => _currentModem.Get<TSetting>("GPO1_3STATLED").Value;
            set
            {
                _currentModem.Get<TSetting>("GPO1_3STATLED").Value = value;
                OnPropertyChanged(nameof(GPO1_3STATLED));
            }
        }

        public int GPO1_0TXEN485
        {
            get => _currentModem.Get<TSetting>("GPO1_0TXEN485").Value;
            set
            {
                _currentModem.Get<TSetting>("GPO1_0TXEN485").Value = value;
                OnPropertyChanged(nameof(GPO1_0TXEN485));
            }
        }

        public int RATE_FREQBAND
        {
            get => _currentModem.Get<TSetting>("RATE/FREQBAND").Value;
            set
            {
                _currentModem.Get<TSetting>("RATE/FREQBAND").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>("RATE/FREQBAND"));
                OnPropertyChanged(nameof(RATE_FREQBAND));
            }
        }

        public int GPI1_2AUXIN
        {
            get => _currentModem.Get<TSetting>("GPI1_2AUXIN").Value;
            set
            {
                _currentModem.Get<TSetting>("GPI1_2AUXIN").Value = value;
                OnPropertyChanged(nameof(GPI1_2AUXIN));
            }
        }

        public int GPO1_3AUXOUT
        {
            get => _currentModem.Get<TSetting>("GPO1_3AUXOUT").Value;
            set
            {
                _currentModem.Get<TSetting>("GPO1_3AUXOUT").Value = value;
                OnPropertyChanged(nameof(GPO1_3AUXOUT));
            }
        }

        public int AIR_FRAMELEN
        {
            get => _currentModem.Get<TSetting>("AIR_FRAMELEN").Value;
            set
            {
                _currentModem.Get<TSetting>("AIR_FRAMELEN").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(AIR_FRAMELEN)));
                OnPropertyChanged(nameof(AIR_FRAMELEN));
            }
        }

        public int RSSI_IN_DBM
        {
            get => _currentModem.Get<TSetting>("RSSI_IN_DBM").Value;
            set
            {
                _currentModem.Get<TSetting>("RSSI_IN_DBM").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(RSSI_IN_DBM)));
                OnPropertyChanged(nameof(RSSI_IN_DBM));
            }
        }

        public int FSFRAMELOSS
        {
            get => _currentModem.Get<TSetting>("FSFRAMELOSS").Value;
            set
            {
                _currentModem.Get<TSetting>("FSFRAMELOSS").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(FSFRAMELOSS)));
                OnPropertyChanged(nameof(FSFRAMELOSS));
            }
        }

        public int AUXSER_SPEED
        {
            get => _currentModem.Get<TSetting>("AUXSER_SPEED").Value;
            set
            {
                _currentModem.Get<TSetting>("AUXSER_SPEED").Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(AUXSER_SPEED)));
                OnPropertyChanged(nameof(AUXSER_SPEED));
            }
        }

        public string AESKEY
        {
            get => _currentModem.Get<TTextSetting>("AESKEY")?.GetValueAsString();
            set
            {
                _currentModem.Get<TTextSetting>("AESKEY").SetValueFromString(value);
                DoAutoSync<TTextSetting>(_currentModem.Get<TTextSetting>(nameof(AESKEY)));
                OnPropertyChanged(nameof(AESKEY));                
            }
        }

        public int NODEID
        {
            get => _currentModem.Get<TSetting>(nameof(NODEID)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(NODEID)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(NODEID)));
                OnPropertyChanged(nameof(NODEID));
            }
        }

        public int DESTID
        {
            get => _currentModem.Get<TSetting>(nameof(DESTID)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(DESTID)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(DESTID)));
                OnPropertyChanged(nameof(NODEID));
            }
        }

        public int TX_ENCAP_METHOD
        {
            get => _currentModem.Get<TSetting>(nameof(TX_ENCAP_METHOD)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(TX_ENCAP_METHOD)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(TX_ENCAP_METHOD)));
                OnPropertyChanged(nameof(TX_ENCAP_METHOD));
            }
        }

        public int RX_ENCAP_METHOD
        {
            get => _currentModem.Get<TSetting>(nameof(RX_ENCAP_METHOD)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(RX_ENCAP_METHOD)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(RX_ENCAP_METHOD)));
                OnPropertyChanged(nameof(RX_ENCAP_METHOD));
            }
        }

        public int MAX_DATA
        {
            get => _currentModem.Get<TSetting>(nameof(MAX_DATA)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(MAX_DATA)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(MAX_DATA)));
                OnPropertyChanged(nameof(MAX_DATA));
            }
        }

        public int MAX_RETRIES
        {
            get => _currentModem.Get<TSetting>(nameof(MAX_RETRIES)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(MAX_RETRIES)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(MAX_RETRIES)));
                OnPropertyChanged(nameof(MAX_RETRIES));
            }
        }

        public int SER_BRK_DETMS
        {
            get => _currentModem.Get<TSetting>(nameof(SER_BRK_DETMS)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(SER_BRK_DETMS)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(SER_BRK_DETMS)));
                OnPropertyChanged(nameof(SER_BRK_DETMS));
            }
        }

        public int GLOBAL_RETRIES
        {
            get => _currentModem.Get<TSetting>(nameof(GLOBAL_RETRIES)).Value;
            set
            {
                _currentModem.Get<TSetting>(nameof(GLOBAL_RETRIES)).Value = value;
                DoAutoSync<TSetting>(_currentModem.Get<TSetting>(nameof(GLOBAL_RETRIES)));
                OnPropertyChanged(nameof(GLOBAL_RETRIES));
            }
        }

        #endregion

        #region Equality
        public bool FORMAT_Equal => Remote == null ? false : Local.Get<TSetting>("FORMAT") == Remote.Get<TSetting>("FORMAT");
        #endregion

        // Do an early fetch of available modems?  Does this interfere with loading settings?
        public async Task<bool> RefreshComms(IModemComms comms)
        {            
            try
            {
                _modemComms = comms;
                _modemComms.PutIntoATCommandMode();
                var atiResponse = _modemComms.DoCommand("ATI");
                

                if (string.IsNullOrWhiteSpace(atiResponse))
                {
                    AddLog($"Could not identify device.");
                    return false;
                } else
                {
                    AddLog($"Connected To: {atiResponse}");
                    return true;
                }                
            }
            catch (Exception e)
            {
                AddLog(e.Message, true);
            }
            finally
            {
                if (_modemComms != null)
                    _modemComms.PutIntoTransparentMode();
            }
            return false;
        }

        public async Task<bool> RestartModem()
        {
            try
            {
                if (!_modemComms.IsConnected())
                    return false;

                // Restart All?
                _modemComms.DoCommand("RTZ");
                _modemComms.DoCommand("ATZ");
                return true;
            }
            catch (Exception e)
            {
                AddLog(e.Message,true);
                return false;
            }
        }

        private async Task<bool> LoadSettings(bool isLocal)
        {

            var session = _modemComms.GetSession();

            try
            {
                _modemComms.PutIntoATCommandMode();

                string commandPrefix = isLocal ? "A" : "R";

                // Identify the device
                string hexId = _modemComms.DoQueryWithRetry($"{commandPrefix}TI8", true).Trim();
                if (string.IsNullOrWhiteSpace(hexId) || hexId.Contains("ERROR"))
                {
                    string deviceType = isLocal ? "local" : "remote";
                    AddLog($"No {deviceType} device found", true);
                    return false;
                }
                IncrementProgress();

                var deviceId = Convert.ToInt64(hexId, 16);

                RFDModem modem;
                if (_modems.ContainsKey(deviceId))
                    modem = _modems[deviceId];
                else
                    modem = new RFDModem() { DeviceId = deviceId };

                // Update islocal
                modem.IsLocal = isLocal;

                // Initialize main ATI commands
                AddLog($"{session.Initialize(modem)}");
                IncrementProgress();
                
                // --- This is where Board based range fixes were?

                modem.RSSI = _modemComms.DoQueryWithRetry($"{commandPrefix}TI7", true).Trim();

                var ati5Response = session.ATCClient.DoQueryWithMultiLineResponse($"{commandPrefix}TI5", $"{commandPrefix}TI");

                bool Junk;                
                
                var settingsString = await QuerySettings(modem);
                var settings = session.ParseSettings(settingsString, session.Board, ati5Response, null, out Junk);  // Junk?!?
                IncrementProgress();

                // Add AESKEY to settings?
                var eKeySetting = GetEncryptionKey(modem.IsLocal);
                if (eKeySetting != null)
                {
                    settings.Add("AESKEY", eKeySetting);
                    AddLog($"AESKEY: {eKeySetting.GetValueAsString()}");
                }
                IncrementProgress();
                //var Settings = session.GetSettings(!isLocal, session.Board, ati5Response, null, out Junk);

                modem.Settings = new TSettings(Collections.Translate(settings, (x) => (TBaseSetting)x));
                AddLog($"Loaded {modem.Settings.Settings.Count} settings from {modem.DisplayName}");

                if (session.multipoint_fix == -1)
                {
                    var aesKey = _modemComms.DoQueryWithRetry($"{commandPrefix}T&E?", true).Trim();
                    if (aesKey.Contains("ERROR"))
                    {
                        modem.AES_ENABLED = false;
                    }
                    else
                    {
                        modem.Get<TTextSetting>("AESKEY").SetValueFromString(aesKey);
                        modem.AES_ENABLED = true;
                    }

                    // TODO is this needed?
                    //SetupComboForMavlink(MAVLINK, false);
                }
                else
                {
                    //local.AESKEY = "";
                    modem.AES_ENABLED = false;
                    // TODO is this needed?
                    //SetupComboForMavlink(MAVLINK, true);
                }

                if (_modems.ContainsKey(deviceId))
                    _modems[modem.DeviceId] = modem;
                else
                    _modems.Add(deviceId, modem); //.Add(local);

                return true;

            }
            catch (Exception e)
            {
                AddLog($"Error Loading Config: {e.Message}");
            }
            finally
            {
                _modemComms.PutIntoTransparentMode();
            }
            return false;
        }

        private const int RETRY_DELAY = 100;
        private const int MAX_QUERY_RETRIES = 10;
        private async Task<string> QuerySettings(RFDModem modem)
        {
            int i;
            string Result = "";
            string prefix = modem.IsLocal ? "ATI10:" : "RTI10:";

            for (i = 0; ; i++)
            {
                string cmd = prefix + i.ToString();
                int retryCount = 0;
                while (retryCount < MAX_QUERY_RETRIES) {
                    retryCount++;
                    string result = _modemComms.DoQueryWithRetry(cmd, waitForTerminator: true);
                    AddLog($"{cmd} --> {result}");
                    if (string.IsNullOrWhiteSpace(result) || Text.Contains(result, "error"))
                    {
                        // Failure?
                        AddLog($"Failure... retry in {RETRY_DELAY}");
                        await Task.Delay(RETRY_DELAY);
                    } else
                    {
                        Result += result + "\r\n";
                        if (Text.Contains(result, "eof") || !result.Contains("="))
                        {
                            return Result;
                        }
                        break;
                    }
                }
                IncrementProgress();
            }
        }

        TTextSetting GetEncryptionKey(bool isLocal)
        {
            _modemComms.PutIntoATCommandMode();
            string eKey = _modemComms.DoQueryWithRetry((isLocal ? "A" : "R") + "T&E?", true);
            AddLog($"{(isLocal ? "A" : "R")}T&E? -> {eKey}");
            var result = new TTextSetting();
            result.Designator = "&E";
            result.Name = "AESKEY";
            if (!eKey.Contains("OK") && !eKey.Contains("ERROR"))
            {
                foreach (char c in eKey)
                {
                    if (!Text.CheckIsHexNumeral(c))
                    {
                        result.Text = "";
                        return result;
                    }
                }                
                result.Text = eKey;                
            } 
            else
            {
                result.Text = "";
            }
            _modemComms.PutIntoTransparentMode();
            return result;
        }
        public void EndSession()
        {            
            OnPropertyChanged(null);
            _modemComms.EndSession();
        }

        public async Task<bool> Load()
        {
            try
            {
                if (_modemComms.PutIntoATCommandMode() == TSession.TMode.AT_COMMAND)
                {
                    //// Discard...
                    //_modemComms.DiscardInBuffer();

                    // cleanup
                    _modemComms.DoCommand("AT&T", false);
                    
                    var loaded = await LoadSettings(isLocal: true);
                    if (!loaded)
                    {                        
                        return false;
                    }

                    // Range fixes?
                    //if (session.Board == Uploader.Board.DEVICE_ID_RFD900X)
                    //{
                    //    //RFD900x has a new set of acceptable settings ranges...
                    //    SERIAL_SPEED_RANGE = new int[] { 1, 2, 4, 9, 19, 38, 57, 115, 230, 460 };
                    //    AIR_SPEED_RANGE = new int[] { 4, 64, 125, 250, 500 };
                    //    NET_ID_RANGE = Range(0, 1, 255).ToArray();
                    //    MIN_FREQ_RANGE = Range(902000, 1000, 927000).ToArray();
                    //    MAX_FREQ_RANGE = Range(903000, 1000, 928000).ToArray();
                    //    NUM_CHANNELS_RANGE = Range(1, 1, 50).ToArray();
                    //    MAX_WINDOW_RANGE = Range(20, 1, 400).ToArray();

                    //    LBT_RSSI_RANGE = Range(0, 25, 220).ToArray();
                    //}
                    //else
                    //{
                    //    LBT_RSSI_RANGE = Range(0, 1, 1).ToArray();
                    //}


                    //// 8 and 9
                    //if (freq == Uploader.Frequency.FREQ_915)
                    //{
                    //    MIN_FREQ_RANGE = Range(902000, 1000, 927000).ToArray();
                    //    MAX_FREQ_RANGE = Range(903000, 1000, 928000).ToArray();
                    //}
                    //else if (freq == Uploader.Frequency.FREQ_433)
                    //{
                    //    MIN_FREQ_RANGE = Range(414000, 50, 460000).ToArray();
                    //    MAX_FREQ_RANGE = Range(414000, 50, 460000).ToArray();
                    //}
                    //else if (freq == Uploader.Frequency.FREQ_868)
                    //{
                    //    MIN_FREQ_RANGE = Range(849000, 1000, 889000).ToArray();
                    //    MAX_FREQ_RANGE = Range(849000, 1000, 889000).ToArray();
                    //}

                    //if (session.Board == Uploader.Board.DEVICE_ID_RFD900 ||
                    //        session.Board == Uploader.Board.DEVICE_ID_RFD900A
                    //        || session.Board == Uploader.Board.DEVICE_ID_RFD900P ||
                    //        session.Board == Uploader.Board.DEVICE_ID_RFD900X)
                    //{
                    //    TX_POWER_RANGE = Range(0, 1, 30).ToArray();
                    //}
                    //else
                    //{
                    //    TX_POWER_RANGE = Range(0, 1, 20).ToArray();
                    //}

                    
                    // ^^^ LOCAL
                    // *********************************************************************//
                    // ,,, REMOTE

                    #region REMOTE
                    
                    var remoteLoaded = await LoadSettings(isLocal: false);
                    if (!remoteLoaded)
                    {
                        AddLog("No remote found");
                    } else
                    {
                        if (Local.Settings.Settings.Count > Remote.Settings.Settings.Count)
                        {
                            if (Remote.Settings.Settings.Count == 0)
                                _modems.Remove(Remote.DeviceId);// get rid of it?
                            ShowBox("Settings Mismatch","Settings Count Mismatch - reload may be required");
                        }
                    }


                    #endregion
                                        
                    // Select Local
                    Current = Local;                    
                    return true;
                }
                else
                {
                    
                    return false;                    
                }
            }
            catch (Exception e)
            {                
                AddLog($"Error loading settings: {e.Message}");
                return false;                    
            }
            finally
            {
                // off hook
                _modemComms.PutIntoTransparentMode();

                AddLog($"{_modems.Count} device(s) found");                            
            }
        }
                
        public async Task<bool> Save() {
            if (_modemComms.GetSession() == null)
            {
                return false;
            }

            // Validate All Settings before applying any
            foreach (var modem in Modems)
            {
                var valid = ValidateWorkingSettings(modem);
                if (!valid)
                {
                    // Early bail on invalid config?
                    AddLog($"Failed to validate config - save aborted", true);
                    return false;
                }
            }
            IncrementProgress();
            List<RFDModem> dirtyConfigs = new List<RFDModem>();
            // Apply Changes
            AddLog($"Connecting to Device: {Local.DisplayName}");
            if (_modemComms.PutIntoATCommandMode() == TSession.TMode.AT_COMMAND)
            {
                // cleanup
                _modemComms.DoCommand("AT&T", false, 1);

                _modemComms.DiscardInBuffer();

                // If no changes, do not restart and reload
                bool hasChanges = false;
                foreach (var modem in Modems)
                {
                    var changes = GetChangedSettings(modem);
                    if (changes.Settings.Count == 0)
                        continue; // No changes in this modem's config...

                    hasChanges = true;
                    AddLog($"{changes.Settings.Count} modified settings found for {modem.DisplayName}.");
                    foreach (var s in changes.Settings)
                    {
                        AddLog($"{s.Key}: {s.Value.GetValueAsString()}");
                    }
                    // Make sure Modem is ready for commands?
                    string response = _modemComms.DoCommand(modem.IsLocal ? "ATI" : "RTI");

                    AddLog($"Sending changes...");
                    // Save the changed settings
                    SaveChangedSettings(changes, modem);

                    // Add this modem to the list of dirty configs for restart
                    dirtyConfigs.Add(modem);

                    await Task.Delay(100);
                    
                    _modemComms.DiscardInBuffer();
                }

                if (!hasChanges)
                {
                    AddLog("No changes detected");
                    return false;
                }

                // Handle Encryption Changes
                

                // Write and restart dirty remotes
                if (dirtyConfigs.Any(x => !x.IsLocal)) {
                    AddLog($"Writing Remotes...");
                    // write it
                    _modemComms.DoCommand("RT&W");

                    AddLog($"Restarting Remotes...");
                    // return to normal mode
                    _modemComms.DoCommand("RTZ");
                }
                IncrementProgress();

                // Write and restart dirty local
                if (dirtyConfigs.Any(x => x.IsLocal))
                {
                    AddLog($"Writing Local...");
                    // write it
                    var cmdwriteanswer = _modemComms.DoCommand("AT&W");
                    if (!cmdwriteanswer.Contains("OK"))
                    {
                        ShowBox("Command Failure", "Failed to save config parameters");
                    }


                    AddLog($"Restarting Local...");
                    // return to normal mode
                    _modemComms.DoCommand("ATZ");
                }
                IncrementProgress();

                AddLog("Save Complete", true);
                
            }
            else
            {
                // return to normal mode
                _modemComms.DoCommand("ATZ");

                AddLog("Failed to write config to device", true); 
                ShowBox("Failure","Settings could not be saved to the device at this time");
                //EnableConfigControls(true, false);
                return false;
            }

            //Need to do this because modem rebooted.
            _modemComms.GetSession().PutIntoATCommandModeAssumingInTransparentMode();

            //            
            return true;
        }

        // Method to only push settings that have changed
        private void SaveChangedSettings(TSettings changedSettings, RFDModem modem)
        {            
            string commandPrefix = modem.IsLocal ? "A" : "R";

            foreach (var kvp in changedSettings.Settings)
            {
                if (!kvp.Key.Contains("FORMAT"))
                {
                    var cmdanswer = _modemComms.DoCommand(
                        commandPrefix + "T" + kvp.Value.Designator + "=" + kvp.Value.GetValueAsString(),
                        kvp.Value.Designator.Contains("&E"));

                    if (cmdanswer.Contains("OK"))
                    {
                        if (kvp.Key.Contains("GPO1_1R_COUT") ||
                            kvp.Key.Contains("GPO1_3SBUSOUT"))
                        {
                            if (kvp.Value is RFD.RFD900.TSetting && ((RFD.RFD900.TSetting)kvp.Value).Value == 1)
                            {
                                //Also need to set RTPO.
                                cmdanswer = _modemComms.DoCommand(
                                    commandPrefix + "TPO=1");
                            }
                            else
                            {
                                cmdanswer = _modemComms.DoCommand(
                                    commandPrefix + "TPI=1");
                            }
                        }
                        else if (kvp.Key.Contains("GPI1_1R_CIN") ||
                            kvp.Key.Contains("GPO1_3SBUSIN"))
                        {
                            //Also need to set RTPI.
                            cmdanswer = _modemComms.DoCommand(
                                commandPrefix + "TPI=1");
                        }
                        if (!cmdanswer.Contains("OK"))
                        {
                            ShowBox("Command Failed",$"Set Command error setting {kvp.Value.Designator} to {kvp.Value.GetValueAsString()}");
                        }

                    }
                    else
                    {
                        if (!modem.IsLocal && (kvp.Key == "ENCRYPTION_LEVEL"))
                        {
                            // set this on the local radio as well.
                            _modemComms.DoCommand("AT" + kvp.Value.Designator + "=" + kvp.Value.GetValueAsString());
                            // both radios should now be using the default key
                        }
                        else
                        {
                            ShowBox("Command Failed", $"Set Command error setting {kvp.Value.Designator} to {kvp.Value.GetValueAsString()}");
                        }
                    }
                }
                IncrementProgress();
            }
        }

        void DoCommandShowErrorIfNotOK(string cmd, string ErrorMsg)
        {
            string Result = _modemComms.DoCommand(cmd);
            if (!Result.Contains("OK"))
            {
               ShowBox("Command Failed",ErrorMsg);
            }
        }

        public async Task ResetDefaults()
        {
            if (_modemComms.PutIntoATCommandMode() == RFD.RFD900.TSession.TMode.AT_COMMAND)
            {
                // cleanup
                if (Remote?.ATI != null)
                {
                    _modemComms.DoCommand("RT&T");

                    _modemComms.DiscardInBuffer();

                    AddLog("Doing Command RTI & AT&F");

                    _modemComms.DoCommand("RT&F");

                    _modemComms.DoCommand("RT&W");

                    AddLog("Reset Remote");

                    _modemComms.DoCommand("RTZ");

                    _modemComms.DoCommand("RT&T");
                }

                _modemComms.DoCommand("AT&T", false, 1);

                _modemComms.DiscardInBuffer();

                AddLog("Doing Command ATI & AT&F");

                DoCommandShowErrorIfNotOK("AT&F", "Failed to reset parameters to factory defaults");

                DoCommandShowErrorIfNotOK("AT&W", "Failed to write parameters to EEPROM");

                AddLog("Reset Local");
                _modemComms.DoCommand("ATZ");

                //Session must be ended because modem rebooted.
                _modemComms.GetSession().PutIntoATCommandModeAssumingInTransparentMode();
            }
            else
            {
                // off hook
                _modemComms.PutIntoTransparentMode();

                AddLog("Fail");
                ShowBox("Error","Failed to enter command mode.  Try power-cycling modem.");
            }
        }

        public void SetPPMFailSafe()
        {
            string cmdPrefix = Current.IsLocal ? "A" : "R";
            string SetCmd = $"{cmdPrefix}T&R";
            string SaveCmd = $"{cmdPrefix}T&W";

            TSession Session = _modemComms.GetSession();

            if (Session == null)
            {
                return;
            }

            if (Session.PutIntoATCommandMode() == TSession.TMode.AT_COMMAND)
            {
                // cleanup
                //Session.Port.DiscardInBuffer();
                //doCommand(Session.Port, "AT&T", false, 1);
                
                Session.Port.DiscardInBuffer();
                bool Result = Session.ATCClient.DoCommand(SetCmd);

                Session.Port.DiscardInBuffer();
                Session.ATCClient.DoCommand(SaveCmd);

                // off hook
                _modemComms.PutIntoTransparentMode();
                
            }
            else
            {                
                ShowBox("Error", "Failed to enter command mode");                
            }
        }
        
        /// <summary>
        /// Tries to put the radio into AT command mode.
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        public async Task<bool> doConnect(ICommsSerial comPort)
        {
            try
            {
                Console.WriteLine("doConnect");

                var trys = 1;

                // setup a known enviroment
                comPort.Write("ATO\r\n");

            retry:

                // wait
                await Sleep(1500, comPort);
                comPort.DiscardInBuffer();
                // send config string
                comPort.Write("+");
                await Sleep(200, comPort);
                comPort.Write("+");
                await Sleep(200, comPort);
                comPort.Write("+");
                await Sleep(1500, comPort);
                // check for config response "OK"
                AddLog("Connect btr " + comPort.BytesToRead + " baud " + comPort.BaudRate);
                // allow time for data/response

                if (comPort.BytesToRead == 0 && trys <= 3)
                {
                    trys++;
                    AddLog("doConnect retry");
                    goto retry;
                }

                var buffer = new byte[20];
                var len = comPort.Read(buffer, 0, buffer.Length);
                var conn = Encoding.ASCII.GetString(buffer, 0, len);
                AddLog("Connect first response " + conn.Replace('\0', ' ') + " " + conn.Length);
                if (conn.Contains("OK"))
                {
                    //return true;
                }
                else
                {
                    // cleanup incase we are already in cmd mode
                    comPort.Write("\r\n");
                }

                _modemComms.DoCommand("AT&T", false, 1);

                var version = _modemComms.DoCommand("ATI");

                AddLog("Connect Version: " + version.Trim() + "\n");

                var regex = new Regex(@"SiK\s+(.*)\s+on\s+(.*)");

                if (regex.IsMatch(version))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task Sleep(int mstimeout, ICommsSerial comPort = null)
        {
            var endtime = DateTime.Now.AddMilliseconds(mstimeout);

            while (DateTime.Now < endtime)
            {
                await Task.Delay(1);
                Application.DoEvents();

                // prime the mavlinkserial loop with data.
                if (comPort != null)
                {
                    var test = comPort.BytesToRead;
                    test++;
                }
            }
        }

        //public void SetModem(TSession session)
        //{
        //    switch (session.Board)
        //    {
        //        case Uploader.Board.DEVICE_ID_RFD900X:
        //            Modem = new RFD900x(session);
        //            break;
        //        case Uploader.Board.DEVICE_ID_RFD900X2:
        //            Modem = new RFD900X2(session);
        //            break;
        //        case Uploader.Board.DEVICE_ID_RFD900P:
        //            Modem = new RFD900p(session);
        //            break;
        //        case Uploader.Board.DEVICE_ID_RFD900A:
        //            Modem = new RFD900a(session);
        //            break;
        //        case Uploader.Board.DEVICE_ID_RFD900UX:
        //            Modem = new RFD900ux(session);
        //            break;
        //        case Uploader.Board.DEVICE_ID_RFD900UX2:
        //            Modem = new RFD900UX2(session);
        //            break;
        //        case Uploader.Board.DEVICE_ID_RFD900U:
        //            Modem = new RFD900u(session);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        

        public void RandomizeEncryptionKey()
        {
            AESKEY = GetRandomKey(Encryption_Max_Key_Length);
        }

        /// <summary>
        /// Get a random key as a hex numeral string, with the given QTY of hex numerals.
        /// </summary>
        /// <param name="QTYHexDigits">The QTY of hex numerals.</param>
        /// <returns>The key.  Never null.</returns>
        string GetRandomKey(int QTYHexDigits)
        {
            string Result = "";

            System.Random R = new Random((int)(System.DateTime.Now.Ticks & 0xFFFFFFFF));

            for (; QTYHexDigits > 0; QTYHexDigits--)
            {
                Result += (((UInt32)R.Next()) & 0xF).ToString("X1");
            }

            return Result;
        }


        public static IEnumerable<int> Range(int start, int step, int end)
        {
            bool GotEnd = false;
            /*
             * To speed things up, might be best to use array and calculate
             * length ahead of time.
             * 
             * length = ((end - start - 1) / step) + 2
             * 
             * 1, 13, 3 = 5
             * 1, 14, 3 = 6
             * 1, 15, 3 = 6
             * 
             */

            try
            {

                int[] list;
                int index = 0;

                if (start == end)
                {
                    list = new int[1];
                }
                else
                {
                    list = new int[((end - start - 1) / step) + 2];
                }

                for (var a = start; a <= end; a += step)
                {
                    if (a == end)
                    {
                        GotEnd = true;
                    }
                    list[index++] = a;
                }

                if (!GotEnd)
                {
                    list[index++] = end;
                }

                return list;
            }
            catch (Exception e)
            {
                //Console.WriteLine();

                throw e;
            }
        }

        
    }
}
