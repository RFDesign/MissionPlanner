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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFDCommon
{
    public class ConfigManager : INotifyPropertyChanged
    {
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
                _currentModem = value; 
                OnPropertyChanged(null); 
            }
        }

        private StringBuilder _log = new StringBuilder();
        public string Log => _log.ToString();        
        public void AddLog(string log) {
            _log.AppendLine(log);
            OnPropertyChanged("Log");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<MessageBoxEventArgs> ShowMessageBox;
        

       
        // Method to invoke the PropertyChanged event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void ShowBox(string title, string text)
        {
            ShowMessageBox?.Invoke(this, new MessageBoxEventArgs(title, text));
        }
               
        

        public RFDModem Local
        {
            get { return Modems.FirstOrDefault(x => x.IsLocal); }            
        }
        public RFDModem Remote
        {
            get { return Modems.FirstOrDefault(x => !x.IsLocal); }            
        }


        #region // Getters for read only properties
        public string ATI => Current.ATI;

        public string FREQ => Current.FREQ;
        public string BOARD => Current.BOARD;
        public string COUNTRY => Current.COUNTRY;
        public bool AES_ENABLED => Current.AES_ENABLED;
        public string RSSI => Current.RSSI;
        public string FORMAT => Current?.Get<TSetting>("FORMAT")?.GetValueAsString();        
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
            get => Current.Get<TSetting>("SERIAL_SPEED").Value; 
            set
            {
                Current.Get<TSetting>("SERIAL_SPEED").Value = value;
                OnPropertyChanged(nameof(SERIAL_SPEED));                
            }
        }
        public int AIR_SPEED
        {
            get => Current.Get<TSetting>("AIR_SPEED").Value;
            set
            {
                Current.Get<TSetting>("AIR_SPEED").Value = value;
                OnPropertyChanged(nameof(AIR_SPEED));
            }
        }
        public int NETID
        {
            get => Current.Get<TSetting>("NETID").Value;
            set
            {
                Current.Get<TSetting>("NETID").Value = value;                
                OnPropertyChanged(nameof(NETID));
            }
        }
        public int TXPOWER
        {
            get => Current.Get<TSetting>("TXPOWER").Value;
            set
            {
                Current.Get<TSetting>("TXPOWER").Value = value;                
                OnPropertyChanged(nameof(TXPOWER));
            }
        }        

        public int RTSCTS
        {
            get => Current.Get<TSetting>("RTSCTS").Value;
            set
            {
                Current.Get<TSetting>("RTSCTS").Value = value;
                OnPropertyChanged(nameof(RTSCTS));
            }
        }

        public int MAVLINK
        {
            get => Current.Get<TSetting>("MAVLINK").Value;
            set
            {
                Current.Get<TSetting>("MAVLINK").Value = value;
                OnPropertyChanged(nameof(MAVLINK));
            }
        }

        public int OPPRESEND
        {
            get => Current.Get<TSetting>("OPPRESEND").Value;
            set
            {
                Current.Get<TSetting>("OPPRESEND").Value = value;
                OnPropertyChanged(nameof(OPPRESEND));
            }
        }

        public int MIN_FREQ
        {
            get => Current.Get<TSetting>("MIN_FREQ").Value;
            set
            {
                Current.Get<TSetting>("MIN_FREQ").Value = value;
                OnPropertyChanged(nameof(MIN_FREQ));
            }
        }

        public int MAX_FREQ
        {
            get => Current.Get<TSetting>("MAX_FREQ").Value;
            set
            {
                Current.Get<TSetting>("MAX_FREQ").Value = value;
                OnPropertyChanged(nameof(MAX_FREQ));
            }
        }

        public int NUM_CHANNELS
        {
            get => Current.Get<TSetting>("NUM_CHANNELS").Value;
            set
            {
                Current.Get<TSetting>("NUM_CHANNELS").Value = value;
                OnPropertyChanged(nameof(NUM_CHANNELS));
            }
        }

        public int DUTY_CYCLE
        {
            get => Current.Get<TSetting>("DUTY_CYCLE").Value;
            set
            {
                Current.Get<TSetting>("DUTY_CYCLE").Value = value;
            }
        }

        public int LBT_RSSI
        {
            get => Current.Get<TSetting>("LBT_RSSI").Value;
            set
            {
                Current.Get<TSetting>("LBT_RSSI").Value = value;
                OnPropertyChanged(nameof(LBT_RSSI));
            }
        }

        public int MAX_WINDOW
        {
            get => Current.Get<TSetting>("MAX_WINDOW").Value;
            set
            {
                Current.Get<TSetting>("MAX_WINDOW").Value = value;
                OnPropertyChanged(nameof(MAX_WINDOW));
            }
        }

        public int ENCRYPTION_LEVEL
        {
            get => Current.Get<TSetting>("ENCRYPTION_LEVEL").Value;
            set
            {
                var currentLevel = Current.Get<TSetting>("ENCRYPTION_LEVEL");
                if (currentLevel.Value == value)
                    return;

                bool getKeyRequired = currentLevel.Value == 0 && value > 0;
                Current.Get<TSetting>("ENCRYPTION_LEVEL").Value = value;                             
                OnPropertyChanged(nameof(ENCRYPTION_LEVEL));
                OnPropertyChanged(nameof(EncryptionEnabled));
                OnPropertyChanged(nameof(Encryption_Max_Key_Length));

                if (value == 0)
                {
                    AESKEY = "";
                    return;
                }

                // When shifting from off to an on state, we must immediately set on the modem, in order to request the key
                if (getKeyRequired)
                {
                    _modemComms.PutIntoATCommandMode();

                    // Enable Encryption on modem to retrieve key
                    string command = $"{(Current.IsLocal ? "AT" : "RT")}{currentLevel.Designator}={value}";
                    var answer = _modemComms.DoCommand(command, true);
                    AddLog($"Command Response: {answer}");
                    if (answer.Contains("OK"))
                    {
                        // Read Existing Key
                        AESKEY = _modemComms.DoQueryWithRetry(Current.IsLocal ? "AT&E?" : "RT&E", true).Trim();
                    }
                    _modemComms.PutIntoTransparentMode();   
                }
                
            }
        }
        public bool EncryptionEnabled
        {
            get {
                if (Current?.Settings == null)
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
            get => Current.Get<TSetting>("GPI1_1R/CIN").Value;
            set
            {
                Current.Get<TSetting>("GPI1_1R/CIN").Value = value;
                OnPropertyChanged(nameof(GPI1_1R_CIN));
            }
        }

        public int GPO1_1R_COUT
        {
            get => Current.Get<TSetting>("GPO1_1R/COUT").Value;
            set
            {
                Current.Get<TSetting>("GPO1_1R/COUT").Value = value;
                OnPropertyChanged(nameof(GPO1_1R_COUT));
            }
        }   

        public int GPO1_1SBUSIN
        {
            get => Current.Get<TSetting>("GPO1_1SBUSIN").Value;
            set
            {
                Current.Get<TSetting>("GPO1_1SBUSIN").Value = value;
                OnPropertyChanged(nameof(GPO1_1SBUSIN));
            }
        }

        public int GPO1_1SBUSOUT
        {
            get => Current.Get<TSetting>("GPO1_1SBUSOUT").Value;
            set
            {
                Current.Get<TSetting>("GPO1_1SBUSOUT").Value = value;
                OnPropertyChanged(nameof(GPO1_1SBUSOUT));
            }
        }

        public int ANT_MODE
        {
            get => Current.Get<TSetting>("ANT_MODE").Value;
            set
            {
                Current.Get<TSetting>("ANT_MODE").Value = value;
                OnPropertyChanged(nameof(ANT_MODE));
            }
        }
        // NEED an options binding property?
        public int GPO1_3STATLED
        {
            get => Current.Get<TSetting>("GPO1_3STATLED").Value;
            set
            {
                Current.Get<TSetting>("GPO1_3STATLED").Value = value;
                OnPropertyChanged(nameof(GPO1_3STATLED));
            }
        }

        public int GPO1_0TXEN485
        {
            get => Current.Get<TSetting>("GPO1_0TXEN485").Value;
            set
            {
                Current.Get<TSetting>("GPO1_0TXEN485").Value = value;
                OnPropertyChanged(nameof(GPO1_0TXEN485));
            }
        }

        public int RATE_FREQBAND
        {
            get => Current.Get<TSetting>("RATE/FREQBAND").Value;
            set
            {
                Current.Get<TSetting>("RATE/FREQBAND").Value = value;
                OnPropertyChanged(nameof(RATE_FREQBAND));
            }
        }

        public int GPI1_2AUXIN
        {
            get => Current.Get<TSetting>("GPI1_2AUXIN").Value;
            set
            {
                Current.Get<TSetting>("GPI1_2AUXIN").Value = value;
                OnPropertyChanged(nameof(GPI1_2AUXIN));
            }
        }

        public int GPO1_3AUXOUT
        {
            get => Current.Get<TSetting>("GPO1_3AUXOUT").Value;
            set
            {
                Current.Get<TSetting>("GPO1_3AUXOUT").Value = value;
                OnPropertyChanged(nameof(GPO1_3AUXOUT));
            }
        }

        public int AIR_FRAMELEN
        {
            get => Current.Get<TSetting>("AIR_FRAMELEN").Value;
            set
            {
                Current.Get<TSetting>("AIR_FRAMELEN").Value = value;
                OnPropertyChanged(nameof(AIR_FRAMELEN));
            }
        }

        public int RSSI_IN_DBM
        {
            get => Current.Get<TSetting>("RSSI_IN_DBM").Value;
            set
            {
                Current.Get<TSetting>("RSSI_IN_DBM").Value = value;
                OnPropertyChanged(nameof(RSSI_IN_DBM));
            }
        }

        public int FSFRAMELOSS
        {
            get => Current.Get<TSetting>("FSFRAMELOSS").Value;
            set
            {
                Current.Get<TSetting>("FSFRAMELOSS").Value = value;
                OnPropertyChanged(nameof(FSFRAMELOSS));
            }
        }

        public int AUXSER_SPEED
        {
            get => Current.Get<TSetting>("AUXSER_SPEED").Value;
            set
            {
                Current.Get<TSetting>("AUXSER_SPEED").Value = value;
                OnPropertyChanged(nameof(AUXSER_SPEED));
            }
        }

        public string AESKEY
        {
            get => Current.Get<TTextSetting>("AESKEY")?.GetValueAsString();
            set
            {
                Current.Get<TTextSetting>("AESKEY").SetValueFromString(value);
                OnPropertyChanged(nameof(AESKEY));                
            }
        }

        #endregion

        #region Equality
        public bool FORMAT_Equal => Remote == null ? false : Local.Get<TSetting>("FORMAT") == Remote.Get<TSetting>("FORMAT");
        #endregion

        public RFD900 Modem { get; set; }        

        public TSettings GetChangedSettings(RFDModem modem)
        {
            var updatedSettings = modem.Settings.WorkingSettings.Where(s => s.Value.GetValueAsString() != modem.Settings.Settings[s.Key].GetValueAsString()).ToDictionary(s => s.Key, s => s.Value);
            TSettings settings = new TSettings(updatedSettings);
            return settings;
        }

        //private int[] serial_speed_range;
        //public int[] SERIAL_SPEED_RANGE
        //{
        //    get { return serial_speed_range; }
        //    set { 
        //        serial_speed_range = value;
        //        OnPropertyChanged();            
        //    }
        //}

        //private int[] air_speed_range;
        //public int[] AIR_SPEED_RANGE
        //{
        //    get { return air_speed_range; }
        //    set { air_speed_range = value; OnPropertyChanged(); }
        //}

        //private int[] net_id_range;
        //public int[] NET_ID_RANGE
        //{
        //    get { return net_id_range; }
        //    set { net_id_range = value; OnPropertyChanged(); }
        //}

        //private int[] min_freq_range;
        //public int[] MIN_FREQ_RANGE
        //{
        //    get { return min_freq_range; }
        //    set { min_freq_range = value; OnPropertyChanged(); }
        //}

        //private int[] max_freq_range;
        //public int[] MAX_FREQ_RANGE

        //{
        //    get { return max_freq_range; }
        //    set { max_freq_range = value; OnPropertyChanged(); }
        //}

        //// pop for NUM_CHANNELS_RANGE
        //private int[] num_channels_range;
        //public int[] NUM_CHANNELS_RANGE
        //{
        //    get { return num_channels_range; }
        //    set { num_channels_range = value; OnPropertyChanged(); }
        //}

        //// prop for MAX_WINDOW_RANGE
        //private int[] max_window_range; 
        //public int[] MAX_WINDOW_RANGE
        //{
        //    get { return max_window_range; }
        //    set { max_window_range = value; OnPropertyChanged(); }
        //}

        //// prop for TX_POWER
        //private int[] tx_power_range;
        //public int[] TX_POWER_RANGE
        //{
        //    get { return tx_power_range; }
        //    set { tx_power_range = value; OnPropertyChanged(); }
        //}

        //// prop for LBT_RSSI_RANGE
        //private int[] lbt_rssi_range;
        //public int[] LBT_RSSI_RANGE
        //{
        //    get { return lbt_rssi_range; }
        //    set { lbt_rssi_range = value; OnPropertyChanged(); }
        //}


        //public bool FirmwareMismatch { get; set; } = false;

        private IModemComms _modemComms;
        public ConfigManager(IModemComms modemComms)
        {
            _modemComms = modemComms;
        }

        public async Task<bool> QueryModems()
        {            
            try
            {
                if (!_modemComms.IsConnected())
                    return false;
                return await Load();                
            }
            catch (Exception e)
            {
                AddLog(e.Message);
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
                AddLog(e.Message);
                return false;
            }
        }

        private async Task<bool> LoadSettings(bool isLocal)
        {
            var session = _modemComms.GetSession();
            
            _modemComms.DiscardInBuffer();
            string commandPrefix = isLocal ? "A" : "R";
            
            // Identify the device
            string hexId = _modemComms.DoQueryWithRetry($"{commandPrefix}TI8", false).Trim();
            if (string.IsNullOrWhiteSpace(hexId) || hexId.Contains("ERROR"))
            {
                AddLog("No device found");
                return false;
            }
            var deviceId = Convert.ToInt64(hexId, 16);

            RFDModem modem;
            if (_modems.ContainsKey(deviceId))
                modem = _modems[deviceId];
            else
                modem = new RFDModem() { DeviceId = deviceId };
            // Update islocal
            modem.IsLocal = isLocal;

            //Set the text box to show the radio version
            int multipoint_fix = -1;    //If this radio has multipoint firmware, the index within returned strings to use for returned values, otherwise -1.
            modem.ATI = _modemComms.DoQueryWithRetry($"{commandPrefix}TI", true).Trim();
            if (modem.ATI.StartsWith("["))
            {
                multipoint_fix = modem.ATI.IndexOf(']') + 1;
            }

            string LocalFWVer = RFD900.ATIResponseToFWVersion(modem.ATI);

            NumberStyles style = NumberStyles.Any;

            //Get the board frequency.
            var freqstring = _modemComms.DoQueryWithRetry($"{commandPrefix}TI3", true).Trim();

            //Some multipoint firmware versions don't reply to ATI command with [n] at start of reply, but they do for ATI3 command, so check for [n] again...
            if (multipoint_fix < 0 && freqstring.StartsWith("["))
            {
                multipoint_fix = freqstring.IndexOf(']') + 1;
            }

            if (multipoint_fix > 0)
            {
                freqstring = freqstring.Substring(multipoint_fix).Trim();
            }

            if (freqstring.ToLower().Contains('x'))
                style = NumberStyles.AllowHexSpecifier;

            var freq = (Uploader.Frequency)Enum.Parse(
                typeof(Uploader.Frequency),
                int.Parse(freqstring.ToLower().Replace("x", ""),
                style
            ).ToString());

            modem.FREQ = freq.ToString();

            style = NumberStyles.Any;

            var boardstring = _modemComms.DoQueryWithRetry($"{commandPrefix}TI2", true);

            if (multipoint_fix > 0)
            {
                boardstring = boardstring.Substring(multipoint_fix).Trim();
            }

            if (boardstring.ToLower().Contains('x'))
                style = NumberStyles.AllowHexSpecifier;

            session.Board =
                (Uploader.Board)
                    Enum.Parse(typeof(Uploader.Board),
                        int.Parse(boardstring.ToLower().Replace("x", ""), style).ToString());

            // We now know what type of board we are dealing with
            if (isLocal)
                SetModem(session);

            modem.COUNTRY = GetCountryCodeFromSession((m) => m.GetCountryCode());
            modem.BOARD = session.Board.ToString();

            // --- This is where Board based range fixes were?

            modem.RSSI = _modemComms.DoQueryWithRetry($"{commandPrefix}TI7", true).Trim();

            var answer = session.ATCClient.DoQueryWithMultiLineResponse($"{commandPrefix}TI5", $"{commandPrefix}TI");

            bool Junk;

            var Settings = session.GetSettings(false,
                session.Board, answer, null, out Junk);

            modem.Settings = new TSettings(Collections.Translate(Settings, (x) => (TBaseSetting)x));

            if (multipoint_fix == -1)
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

            var items = answer.Split('\n');

            if (modem.ATI.Contains("ASYNC"))
            {
                modem.Mode = FirmwareMode.ASYNC;
            }
            else
            {
                if (modem.ATI.Contains("MP on") && (session.Board == Uploader.Board.DEVICE_ID_RFD900X))
                {
                    //This is multipoint firmware.
                    modem.Mode = FirmwareMode.MULTIPOINT_X;
                }
                else if ((items.Length > 0) && items[0].StartsWith("["))
                {
                    modem.Mode = FirmwareMode.MULTIPOINT;
                }
                else
                {
                    modem.Mode = FirmwareMode.P2P;
                }
            }

            if (_modems.ContainsKey(deviceId))
                _modems[modem.DeviceId] = modem;
            else
                _modems.Add(deviceId, modem); //.Add(local);

            return true;
        }

        public void EndSession()
        {
            _modemComms.EndSession();
        }

        public async Task<bool> Load()
        {
            try
            {

                if (_modemComms.PutIntoATCommandMode() == TSession.TMode.AT_COMMAND)
                {                    
                    // cleanup
                    _modemComms.DoCommand("AT&T", false);

                    var loaded = await LoadSettings(isLocal: true);
                    if (!loaded)
                    {
                        ShowBox("Load Failed", $"Failed to load local settings");
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
                    _modemComms.DiscardInBuffer();

                    var remoteLoaded = await LoadSettings(isLocal: false);
                    if (remoteLoaded)
                    {
                        AddLog("No remote loaded...");
                    }
                    #endregion
                                        
                    // Select Local
                    Current = Local;                    
                    return true;
                }
                else
                {
                    
                    return false;
                    //EnableConfigControls(true, false);
                }
            }
            catch (Exception e)
            {
                return false;                    
            }
            finally
            {
                // off hook
                _modemComms.PutIntoTransparentMode();

                AddLog("Config Load Complete");
                // Update all
                OnPropertyChanged(null);                
            }
        }

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

                ShowBox("Invalid Settings",errorMsg);                

                return false;
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
                    return false;
                }
            }

            List<RFDModem> dirtyConfigs = new List<RFDModem>();
            // Apply Changes
            AddLog($"Connecting to Device: {Local.DisplayName}");
            if (_modemComms.PutIntoATCommandMode() == TSession.TMode.AT_COMMAND)
            {
                // cleanup
                _modemComms.DoCommand("AT&T", false, 1);

                _modemComms.DiscardInBuffer();
                                
                foreach (var modem in Modems)
                {
                    var changes = GetChangedSettings(modem);
                    if (changes.Settings.Count == 0)
                        continue; // No changes in this modem's config...

                    
                    // Make sure Modem is ready for commands?
                    string response = _modemComms.DoCommand(modem.IsLocal ? "ATI" : "RTI");
                    
                    // Save the changed settings
                    SaveChangedSettings(changes, modem);

                    // Add this modem to the list of dirty configs for restart
                    dirtyConfigs.Add(modem);

                    await Task.Delay(100);
                    
                    _modemComms.DiscardInBuffer();
                }

                // Handle Encryption Changes
                

                // Write and restart dirty remotes
                if (dirtyConfigs.Any(x => !x.IsLocal)) {
                    // write it
                    _modemComms.DoCommand("RT&W");

                    // return to normal mode
                    _modemComms.DoCommand("RTZ");
                }

                // Write and restart dirty local
                if (dirtyConfigs.Any(x => x.IsLocal))
                {
                    // write it
                    var cmdwriteanswer = _modemComms.DoCommand("AT&W");
                    if (!cmdwriteanswer.Contains("OK"))
                    {
                        ShowBox("Command Failure", "Failed to save config parameters");
                    }

                    // return to normal mode
                    _modemComms.DoCommand("ATZ");
                }
                                
                

                AddLog($"Config Save Complete.{Environment.NewLine}");
                ShowBox("Success", "Settings have been saved to the device successfully");
            }
            else
            {
                // return to normal mode
                _modemComms.DoCommand("ATZ");

                AddLog("Failed to write config to device"); 
                ShowBox("Failure","Settings could not be saved to the device at this time");
                //EnableConfigControls(true, false);
                return false;
            }

            //Need to do this because modem rebooted.
            _modemComms.GetSession().PutIntoATCommandModeAssumingInTransparentMode();
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
                            ShowBox("Command Failed","Set Command error");
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
                            ShowBox("Command Failed","Set Command error");
                        }
                    }
                }
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

                    AddLog("Reset");

                    _modemComms.DoCommand("RTZ");

                    _modemComms.DoCommand("RT&T");
                }

                _modemComms.DoCommand("AT&T", false, 1);

                _modemComms.DiscardInBuffer();

                AddLog("Doing Command ATI & AT&F");

                DoCommandShowErrorIfNotOK("AT&F", "Failed to reset parameters to factory defaults");

                DoCommandShowErrorIfNotOK("AT&W", "Failed to write parameters to EEPROM");

                AddLog("Reset");
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

        public void SetPPMFailSafe(string SetCmd, string SaveCmd)
        {
            
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

        public void SetModem(TSession session)
        {
            switch (session.Board)
            {
                case Uploader.Board.DEVICE_ID_RFD900X:
                    Modem = new RFD900x(session);
                    break;
                case Uploader.Board.DEVICE_ID_RFD900X2:
                    Modem = new RFD900X2(session);
                    break;
                case Uploader.Board.DEVICE_ID_RFD900P:
                    Modem = new RFD900p(session);
                    break;
                case Uploader.Board.DEVICE_ID_RFD900A:
                    Modem = new RFD900a(session);
                    break;
                case Uploader.Board.DEVICE_ID_RFD900UX:
                    Modem = new RFD900ux(session);
                    break;
                case Uploader.Board.DEVICE_ID_RFD900UX2:
                    Modem = new RFD900UX2(session);
                    break;
                case Uploader.Board.DEVICE_ID_RFD900U:
                    Modem = new RFD900u(session);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Get the country code from the modem as a string, or "--" if unknown or not locked to country.
        /// </summary>
        /// <param name="Session">The session.  Must not be null.</param>
        /// <param name="GetCC">The function to get the country code, given the modem object.  Must not be null.</param>
        /// <returns>The country code string.</returns>
        string GetCountryCodeFromSession(Func<RFD900xuxRevN, RFD900xux.TCountry> GetCC)
        {
            RFD900xux.TCountry CC;
            var Mdm = _modemComms.GetSession().GetModemObject();

            if (Mdm == null || !(Mdm is RFD900xuxRevN) ||
                !RFD900xux.GetIsCountryLocked(CC = GetCC((RFD900xuxRevN)Mdm)))
            {
                return "--";
            }
            else
            {
                return CC.ToString();
            }
        }

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
