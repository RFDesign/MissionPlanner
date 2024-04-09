using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using log4net;
using MissionPlanner.Utilities;
using RFDCommon.Interface;
using static RFD.RFD900.TSettings;
using RFD.RFD900;
using RFDCommon;
using RFDCommon.Radio;
using RFDCommon.RFDLib;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;


namespace MissionPlanner.Radio
{
    public partial class Sikradio : UserControl, IRFDConfigForm
    {
        public delegate void LogEventHandler(string message, int level = 0);

        

        //public delegate void ProgressEventHandler(double completed);

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool beta;
        private bool _started = false;
        private GroupBox _helpDisplay = null;

        private string firmwarefile = Path.GetTempFileName();
        
        private ConfigManager _configManager;// = new ConfigManager();
        private IModemComms _comms;

        public void AddBoundControl(Control c)
        {
            boundControls.Add(c.Name);
        }
        private HashSet<string> boundControls = new HashSet<string>();
        public void ClearBindings()
        {
            // Remove all runtime bindings?
            foreach (var item in boundControls)
            {
                var ctrl = this.Controls.Find(item, true).FirstOrDefault();
                if (ctrl != null)
                {
                    ctrl.DataBindings.Clear();
                    
                    if (ctrl is ComboBox)
                    {
                        var combo = ctrl as ComboBox;
                        if (combo.DataSource != null)
                            combo.DataSource = null;
                        
                    }
                    if (ctrl is TextBox)
                    {
                        (ctrl as TextBox).Text = "";
                    }
                }

            }
            boundControls.Clear();
        }

        /*
ATI5
S0: FORMAT=25
S1: SERIAL_SPEED=57
S2: AIR_SPEED=64
S3: NETID=40
S4: TXPOWER=30
S5: ECC=1
S6: MAVLINK=1
S7: OPPRESEND=1
S8: MIN_FREQ=915000
S9: MAX_FREQ=928000
S10: NUM_CHANNELS=50
S11: DUTY_CYCLE=100
S12: LBT_RSSI=0
S13: MANCHESTER=0
S14: RTSCTS=0
S15: MAX_WINDOW=131
         */

        public Sikradio()
        {
            
            InitializeComponent();            
        }

        public void Init(ConfigManager configManager)
        {
            _configManager = configManager;
            // Handle events coming from _configManager
            _configManager.ShowMessageBox += (sender, args) => MsgBox.CustomMessageBox.Show(args.Text, args.Title);            

            // Set DataBinding source...
            this.configManagerBindingSource.DataSource = _configManager;            

            CheckControlStates();
        }

        public void Start(IModemComms comms)
        {
            Visible = true;
            _started = true;
            //_modemComms = modemComms;
            //_configManager.Init(modemComms);
            _comms = comms;
            _comms.ConnectionStateChanged += _comms_ConnectionStateChanged;


            // Property changed to do sync indicators?
            _configManager.PropertyChanged += _configManager_PropertyChanged;
            
            // Have just connected, enable the form?
            //SetEnabled(this.Controls, true, true);

            // AutoLoad?
            //_configManager.Load(S);
        }

        private void _configManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == null || e.PropertyName == "Log" || e.PropertyName == "Progress")
                return;

            if (e.PropertyName == "Current")
                _configManager.AddLog($"Changed device: {_configManager.Current?.DisplayName ?? "Unknown"}");
            else
            {
                _configManager.AddLog($"Property Changed: {e.PropertyName}");
                bool isDirty = _configManager.IsDirty;
                btn_SaveSetting.ForeColor = isDirty ? Color.OrangeRed : Color.FromArgb(73, 82, 110);
                btn_SaveSetting.IconColor = isDirty ? Color.OrangeRed : Color.FromArgb(73, 82, 110);
            }

            // What should the indicator do?
            UpdateIndicator(e.PropertyName);            
        }

        private void UpdateIndicator(string propertyName)
        {
            // If no remote, bail
            if (_configManager.Remote?.Settings == null)
                return;

            if (propertyName == "Current")
            {
                // Update ALL indicators...
                foreach (var item in _configManager.Current.Settings.WorkingSettings)
                {
                    UpdateIndicator(item.Key);
                }

                UpdateIndicator("ATI");
                UpdateIndicator("FREQ");
                UpdateIndicator("GPIO0");
                UpdateIndicator("GPIO1");
                UpdateIndicator("GPIO2");
                UpdateIndicator("GPIO3");

                return;
            }
            

            // Does this property have an indicator?
            string indicatorName = $"{propertyName}_CHECK";
            var indicator = this.Controls.Find(indicatorName, true).FirstOrDefault() as IconPictureBox;
            if (indicator == null)
                return;

            string localValue, remoteValue;
            bool isAsymmetric = false, isValidAsymmetric = false;

            if (propertyName == "ATI")
            {
                localValue = _configManager.Local.ATI;
                remoteValue = _configManager.Remote.ATI;
            } 
            else if (propertyName == "FREQ")
            {
                localValue = _configManager.Local.FREQ;
                remoteValue = _configManager.Remote.FREQ;
            }
            else if (propertyName.StartsWith("GPIO"))
            {
                switch (propertyName)
                {
                    case "GPIO0":
                        localValue = _configManager.GetGPIOSetting(_configManager.GPIO0_Items, _configManager.Local);
                        remoteValue = _configManager.GetGPIOSetting(_configManager.GPIO0_Items, _configManager.Remote);
                        break;
                    case "GPIO1":
                        localValue = _configManager.GetGPIOSetting(_configManager.GPIO1_Items, _configManager.Local);
                        remoteValue = _configManager.GetGPIOSetting(_configManager.GPIO1_Items, _configManager.Remote);
                        if (string.IsNullOrWhiteSpace(localValue) && string.IsNullOrWhiteSpace(remoteValue))
                            break;
                        isAsymmetric = true;
                        isValidAsymmetric = (localValue.EndsWith("IN") && remoteValue.EndsWith("OUT")) || (localValue.EndsWith("OUT") && remoteValue.EndsWith("IN"));
                        break;
                    default:
                        return;
                }
            }
            else if (propertyName == "NODEID" || propertyName == "DESTID") {
                isAsymmetric = true;

                // Get NODE IDS
                var localNodeSetting = _configManager.Local.Get<RFD.RFD900.TSetting>("NODEID");
                var remoteNodeSetting = _configManager.Remote.Get<RFD.RFD900.TSetting>("NODEID");
                var localDestSetting = _configManager.Local.Get<RFD.RFD900.TSetting>("DESTID");
                var remoteDestSetting = _configManager.Remote.Get<RFD.RFD900.TSetting>("DESTID");
                
                isValidAsymmetric = localNodeSetting?.Value == remoteDestSetting?.Value && remoteNodeSetting?.Value == localDestSetting?.Value;
                localValue = propertyName == "NODEID" ? localNodeSetting.Value.ToString() : localDestSetting.Value.ToString();
                remoteValue = propertyName == "NODEID" ? remoteNodeSetting.Value.ToString() : remoteDestSetting.Value.ToString();
            }
            else
            {
                var settingType = _configManager.Local.SettingType(propertyName);
                if (settingType == typeof(RFD.RFD900.TSetting))
                {
                    var localSetting = _configManager.Local.Get<RFD.RFD900.TSetting>(propertyName);
                    localValue = localSetting.GetOptionNameForValue(localSetting.GetValueAsString());
                    if (string.IsNullOrWhiteSpace(localValue))
                        localValue = localSetting.Value.ToString();
                    
                    var remoteSetting = _configManager.Remote.Get<RFD.RFD900.TSetting>(propertyName);                    
                    remoteValue = remoteSetting?.GetOptionNameForValue(remoteSetting?.GetValueAsString());
                    if (string.IsNullOrWhiteSpace(remoteValue))
                        remoteValue = remoteSetting?.Value.ToString();
                } 
                else if (settingType == typeof(TTextSetting))
                {
                    localValue = _configManager.Local.Get<RFD.RFD900.TTextSetting>(propertyName).GetValueAsString();
                    remoteValue = _configManager.Remote.Get<RFD.RFD900.TTextSetting>(propertyName).GetValueAsString();
                } 
                else if (settingType == typeof(TShortSetting)) 
                { 
                    localValue = _configManager.Local.Get<RFD.RFD900.TShortSetting>(propertyName).GetValueAsString();
                    remoteValue = _configManager.Remote.Get<RFD.RFD900.TShortSetting>(propertyName).GetValueAsString();

                }
                else
                {
                    localValue = "Error-L";
                    remoteValue = "Error-R";
                }
            }
            if ((isAsymmetric && isValidAsymmetric) || (!isAsymmetric && localValue == remoteValue))
            {
                // They are equal, so go green
                indicator.IconColor = System.Drawing.Color.Green;
                indicator.IconChar = IconChar.CheckCircle;
                if (isAsymmetric)
                {
                    var otherValue = _configManager.Current.IsLocal ? remoteValue : localValue;
                    toolTip1.SetToolTip(indicator, otherValue);
                }
                else
                    toolTip1.SetToolTip(indicator, "In Sync");
            } 
            else
            {
                // They are not equal
                if (_configManager.AutoSyncProperties.Contains(propertyName))
                {
                    indicator.IconColor = System.Drawing.Color.Red;
                    indicator.IconChar = IconChar.TimesCircle;
                } else
                {
                    indicator.IconColor = System.Drawing.Color.Yellow;
                    indicator.IconChar = IconChar.CircleMinus;                    
                }
                
                var otherValue = _configManager.Current.IsLocal ? remoteValue : localValue;
                toolTip1.SetToolTip(indicator, string.IsNullOrWhiteSpace(otherValue) ? "Not Set" : otherValue);
            }            
        }

        private async void _comms_ConnectionStateChanged(object sender, EventArgs e)
        {
            if (_comms.IsConnected())
            {
                
                // Just connected... pull config?
                //await _configManager.Load();
            } 
            else
            {
                ClearBindings();
                _configManager.ClearSettings();
            }
            // Init or De-Init the form?
            CheckControlStates();
        }

        public void Stop()
        {
            Visible = false;
            // ??
            if (_started)
            {
                _started = false;
                if (_comms != null)
                    _comms.ConnectionStateChanged -= _comms_ConnectionStateChanged;
                if (_configManager != null)
                    _configManager.PropertyChanged -= _configManager_PropertyChanged;
            }
        }      

        private void SetEnabled(ControlCollection controls, bool setState, bool recursive)
        {            
            foreach (Control c in controls)
            {
                // Dont mess with Devices section enabled...
                if (c.Name == groupFirmware.Name)
                    return;

                // Dont mess with this control...
                if (c.Name == comboModemSelection.Name)
                    continue;

                // Dont mess with labels
                if (c is Label)
                    continue;

                // Checks for unused checkboxes to prevent enable
                if (c is ComboBox  && setState == true)
                {
                    // Dont enable controls with no options, or only a single option.
                    var cb = c as ComboBox;
                    if (cb.Items.Count == 0)
                        cb.Items.Add("N/A");
                    if (cb.Items.Count == 1)
                    {
                        cb.SelectedIndex = 0;
                        continue;
                    }
                    if (_configManager.Current?.Settings?.Settings != null 
                        && !_configManager.Current.Settings.Settings.ContainsKey(c.Name) 
                        && !_configManager.Current.Settings.Settings.ContainsKey(c.Name.Replace("_","/")) // Fix so RATE/FREQBAND matches if in settings
                        && !c.Name.StartsWith("GPIO"))
                        continue;
                }

                // Prevent enable on unused checkboxes
                if (c is CheckBox && setState == true)
                {
                    if (_configManager.Current?.Settings?.Settings != null && !_configManager.Current.Settings.Settings.ContainsKey(c.Name))
                        continue;
                }
                
                c.Enabled = setState;
                if (c.Controls.Count > 0 && recursive)
                {                    
                    SetEnabled(c.Controls, setState, recursive);
                }
            }            
        }        

        private bool getFirmware(Uploader.Board device, RFD.RFD900.RFD900 RFD900, bool custom = false)
        {
            if (custom)
            {
                return getFirmwareLocal(RFD900);
            }
            
            firmwarefile = Path.GetTempFileName();

            if (device == Uploader.Board.DEVICE_ID_HM_TRP)
            {
                if (beta)
                {
                    return Download.getFilefromNet("http://firmware.ardupilot.org/SiK/beta/radio~hm_trp.ihx", firmwarefile);
                }
                return Download.getFilefromNet("http://firmware.ardupilot.org/SiK/stable/radio~hm_trp.ihx",
                    firmwarefile);
            }
            if (device == Uploader.Board.DEVICE_ID_RFD900)
            {
                if (beta)
                {
                    return Download.getFilefromNet("http://firmware.ardupilot.org/SiK/beta/radio~rfd900.ihx", firmwarefile);
                }
                return Download.getFilefromNet("http://firmware.ardupilot.org/SiK/stable/radio~rfd900.ihx", firmwarefile);
            }
            if (device == Uploader.Board.DEVICE_ID_RFD900A)
            {
                if (beta)
                {
                    return Download.getFilefromNet("http://firmware.ardupilot.org/SiK/beta/radio~rfd900a.ihx",
                        firmwarefile);
                }
                return Download.getFilefromNet("http://firmware.ardupilot.org/SiK/stable/radio~rfd900a.ihx", firmwarefile);
            }
            if (device == Uploader.Board.DEVICE_ID_RFD900U)
            {
                if (beta)
                {
                    return Download.getFilefromNet("http://files.rfdesign.com.au/Files/firmware/MPSiK%20V2.6%20rfd900u.ihx", firmwarefile);
                }
                return Download.getFilefromNet("http://files.rfdesign.com.au/Files/firmware/RFDSiK%20V1.9%20rfd900u.ihx", firmwarefile);
            }
            if (device == Uploader.Board.DEVICE_ID_RFD900P)
            {
                if (beta)
                {
                    return Download.getFilefromNet("http://files.rfdesign.com.au/Files/firmware/MPSiK%20V2.6%20rfd900p.ihx", firmwarefile);
                }
                return Download.getFilefromNet("http://files.rfdesign.com.au/Files/firmware/RFDSiK%20V1.9%20rfd900p.ihx", firmwarefile);
            }
            if (device == Uploader.Board.DEVICE_ID_RFD900X)
            {
                return Download.getFilefromNet("http://files.rfdesign.com.au/Files/firmware/RFDSiK%20V2.60%20rfd900x.bin", firmwarefile);
            }
            return false;
        }

        /// <summary>
        /// Loads a local firmware file after prompting user for file.
        /// </summary>
        /// <returns></returns>
        private bool getFirmwareLocal(RFD.RFD900.RFD900 RFD900)
        {
            using (var openFileDialog1 = new OpenFileDialog())
            {
                string Filter = "Firmware|";
                string[] Exts = RFD900.FirmwareFileNameExtensions;
                for (int n = 0; n < Exts.Length; n++)
                {
                    if (n != 0)
                    {
                        Filter += ";";
                    }
                    Filter += "*." + Exts[n];
                }

                openFileDialog1.Filter = Filter;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Multiselect = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        firmwarefile = openFileDialog1.FileName;
                        //File.Copy(openFileDialog1.FileName, firmwarefile, true);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.CustomMessageBox.Show("Error copying file\n" + ex, "ERROR");
                        return false;
                    }
                    return true;
                }

                return false;
            }
        }        
              

        private void uploader_LogEvent(string message, int level = 0)
        {
            try
            {
                if (level == 0)
                {
                    Console.Write(message);
                    //lbl_status.Text = message;
                    log.Info(message);
                    Application.DoEvents();
                }
                else if (level < 5) // 5 = byte data
                {
                    log.Debug(message);
                }
            }
            catch
            {
            }
        }

        private void iHex_LogEvent(string message, int level = 0)
        {
            try
            {
                if (level == 0)
                {
                    //lbl_status.Text = message;
                    Console.WriteLine(message);
                    log.Info(message);
                    Application.DoEvents();
                }
            }
            catch
            {
            }
        }        

        string GetParamNumber(string Part1)
        {
            Part1 = Part1.Trim();
            int S = Part1.IndexOf('S');
            return Part1.Substring(S + 1);
        }

        class TBaseSetting
        {
        }

        class TSetting<T> : TBaseSetting
        {
            public T Value;

            public TSetting(T Value)
            {
                this.Value = Value;
            }
        }

        
        
        private void ShowMessageBox(string message, string caption)
        {
            MsgBox.CustomMessageBox.Show(message, caption);
        }
               

        /// <summary>
        /// Return an array of ints in a linear progression, but end is always included as the end.
        /// </summary>
        /// <param name="start">The start</param>
        /// <param name="step">The step</param>
        /// <param name="end">The end, always included</param>
        /// <returns>The array of ints, never null</returns>
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

        private void BindSettingToCheckBox(TSetting setting, CheckBox checkBox)
        {
            checkBox.DataBindings.Clear();
            var binding = new Binding("Checked", configManagerBindingSource, setting.Name.Replace("/","_"), true, DataSourceUpdateMode.OnPropertyChanged);
            // Setup formatting for 0-1 to false-true
            binding.Format += (s, e) =>
            {
                e.Value = ((int)e.Value == 1);
            };

            binding.Parse += (s, e) =>
            {
                e.Value = ((bool)e.Value) ? 1 : 0;
            };
            checkBox.DataBindings.Add(binding);
            AddBoundControl(checkBox);
        }

        // Setup binding for ComboBoxes options with TSettings
        public void BindSettingOptionsToComboBox(TSetting setting, ComboBox comboBox)
        {
            // Check if the Options array is not null
            if (setting.Options != null)
            {
                comboBox.DataBindings.Clear();
                // Convert TOption[] to a bindable list format
                var optionList = setting.Options.Select(option => new
                {
                    Name = option.OptionName,
                    Value = option.Value
                }).ToList();


                // Assign the list as the data source for the ComboBox
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "Value";
                comboBox.DataSource = optionList;

                // Optionally, set the selected item based on the current setting value
                var currentOption = setting.Options.FirstOrDefault(o => o.Value == setting.Value);
                if (currentOption != null)
                {
                    comboBox.SelectedValue = currentOption.Value;
                }

                // Setup Binding
                comboBox.DataBindings.Add("SelectedValue", configManagerBindingSource, setting.Name.Replace("/","_"), false, DataSourceUpdateMode.OnPropertyChanged);
                AddBoundControl(comboBox);
            } 
            else if (setting.Range != null)
            {
                // FIX to use Value instead?
                comboBox.DataBindings.Clear();
                comboBox.DataSource = null;

                var rangeOptions = setting.Range.GetOptionsIncludingValue(setting.Value).Select(s => new { Name= s.ToString(), Value = s }).ToList();
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "Value";
                comboBox.DataSource = rangeOptions;                
                comboBox.DataBindings.Add("SelectedValue", configManagerBindingSource, setting.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                AddBoundControl(comboBox);
            }            
        }

        public void BindSettingToTextBox(TSetting setting, TextBox textBox)
        {
            textBox.DataBindings.Clear();
            textBox.DataBindings.Add("Text", configManagerBindingSource, setting.Name, false,  DataSourceUpdateMode.OnPropertyChanged);
            AddBoundControl(textBox);
        }
               
        /// <summary>
        /// Given an array of lines returned from ATI5 command from a modem,
        /// remove the "[n]" from the start of the lines.  The "[n]" is returned
        /// from a modem at the start of the lines if it is running multipoint firmware.
        /// </summary>
        /// <param name="items">The raw lines returned from the modem.  Must not be null.</param>
        /// <param name="multipoint_fix">The character index into the string immediately after
        /// the initial "[n]", or -1 if not multipoint.</param>
        /// <returns>The modified lines.  Never null.</returns>
        string[] ModifyReturnedStringsForMultipoint(string[] items, int multipoint_fix)
        {
            string[] Result = new string[items.Length];

            for (int n = 0; n < items.Length; n++)
            {
                Result[n] = items[n];
                if (multipoint_fix > 0 && items[n].Length > multipoint_fix)
                {
                    Result[n] = items[n].Substring(multipoint_fix).Trim();
                }
            }

            return Result;
        }      
        
       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MsgBox.CustomMessageBox.Show(@"The Sik Radios have 2 status LEDs, one red and one green.
                                        green LED blinking - searching for another radio 
                                        green LED solid - link is established with another radio 
                                        red LED flashing - transmitting data 
                                        red LED solid - in firmware update mode");
        }

        void UpdateStatusCallback(string status, double progress)
        {
            _configManager.UpdateProgress(status, progress);            
        }

        void CheckControlStates()
        {
#if DEBUG
            var sw = Stopwatch.StartNew();
#endif
            //groupData.Enabled = _configManager.DataEnabled;
            //groupFirmware.Enabled = _configManager.DeviceGroupEnabled;
            //groupRadio.Enabled = _configManager.RadioEnabled;
            //groupSerial.Enabled = _configManager.SerialEnabled;
            //groupSecurity.Enabled = _configManager.SecurityEnabled;
            //groupGPIO.Enabled = _configManager.PinEnabled;
                        
#if DEBUG
            long snapshot = sw.ElapsedMilliseconds;
#endif
            //groupData.Visible = _configManager.DataEnabled;
#if DEBUG
            _configManager.AddLog($"Data Visibility in {sw.ElapsedMilliseconds - snapshot}ms");
            snapshot = sw.ElapsedMilliseconds;
#endif
            //groupInfo.Visible = _configManager.InfoEnabled;

            //SetEnabled(groupFirmware.Controls, _configManager.DeviceGroupEnabled, true);
            SetEnabled(groupSerial.Controls, _configManager.SerialEnabled, true);
#if DEBUG            
            _configManager.AddLog($"Serial in {sw.ElapsedMilliseconds - snapshot}ms");
            snapshot = sw.ElapsedMilliseconds;
#endif
            SetEnabled(groupRadio.Controls, _configManager.RadioEnabled, true);
#if DEBUG
            _configManager.AddLog($"Radio in {sw.ElapsedMilliseconds - snapshot}ms");
            snapshot = sw.ElapsedMilliseconds;            
#endif
            SetEnabled(groupSecurity.Controls, _configManager.SecurityEnabled, true);
#if DEBUG
            _configManager.AddLog($"Security in {sw.ElapsedMilliseconds - snapshot}ms");
            snapshot = sw.ElapsedMilliseconds;
#endif
            SetEnabled(groupGPIO.Controls, _configManager.PinEnabled, true);
#if DEBUG
            _configManager.AddLog($"GPIO in {sw.ElapsedMilliseconds - snapshot}ms");
            snapshot = sw.ElapsedMilliseconds;
#endif
            SetEnabled(groupData.Controls, _configManager.DataEnabled, true);
#if DEBUG
            _configManager.AddLog($"Data in {sw.ElapsedMilliseconds - snapshot}ms");
            snapshot = sw.ElapsedMilliseconds;
#endif
            //SetEnabled(groupInfo.Controls, _configManager.InfoEnabled, true);

            btn_LoadSetting.Enabled = _configManager.LoadEnabled;
            btn_SaveSetting.Enabled = _configManager.SaveEnabled;
            btn_LoadFile.Enabled = _configManager.ImportEnabled;
            btn_SaveFile.Enabled = _configManager.ExportEnabled;
            btn_Reset.Enabled = _configManager.ResetEnabled;
            btn_Firmware.Enabled = _configManager.FirmwareEnabled;


#if DEBUG
            _configManager.AddLog($"All Control states updated in {sw.ElapsedMilliseconds}ms");
            sw.Stop();
#endif

        }

        void ProgramFirmware(bool Custom)
        {
           


            //EnableProgrammingControls(false);
            //EnableConfigControls(false, false);

            try
            {
                _configManager.AddLog("Determining modem...");

                RFD.RFD900.RFD900 RFD900 = _configManager.Modem;
                
                if (RFD900 == null)
                {
                    _configManager.AddLog("Unknown modem");
                    MsgBox.CustomMessageBox.Show("Couldn't communicate with modem.  Try power-cycling modem.");
                    
                    _configManager.EndSession();
                }
                else
                {
                    if (Custom)
                    {
                        _configManager.AddLog("Asking user for firmware file");
                    }
                    else
                    {
                        _configManager.AddLog("Getting firmware from internet");
                    }
                    if (getFirmware(RFD900.Board, RFD900, Custom))
                    {
                        _configManager.AddLog("Programming firmware into device");

                        // Set appropriate control states
                        _configManager.FirmwareUpdateInProgress = true;
                        CheckControlStates();

                        if (RFD900.ProgramFirmware(firmwarefile, UpdateStatusCallback))
                        {
                            _configManager.AddLog("Firmware update complete",true);

                            // Reset state to pull new settings?
                            ClearBindings();
                            _configManager.ClearSettings();
                            MsgBox.CustomMessageBox.Show("Firmware update successful", "Success");
                            _configManager.ClearProgress();
                        }
                        else
                        {
                            _configManager.AddLog("Programming failed.  (Try again?)");
                        }

                                          
                        _configManager.EndSession();                        
                    }
                    else
                    {
                        _configManager.AddLog("Firmware file selection cancelled");
                    }
                }
            }
            catch
            {
                try
                {
                    _configManager.AddLog("Programming failed.  (Try again?)");                    
                    _configManager.EndSession();
                }
                catch
                {
                }
            }
            finally
            {
                // Restore expected control states      
                _configManager.FirmwareUpdateInProgress = false;
                CheckControlStates();
            }
            //EnableProgrammingControls(true);
            //EnableConfigControls(true, false);
            //UploadFW(true);
        }

        private void Progressbar_Click(object sender, EventArgs e)
        {
            beta = !beta;
            MsgBox.CustomMessageBox.Show("Beta set to " + beta);
        }

        private void linkLabel_mavlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MAVLINK.SelectedValue = 1;
            MAX_WINDOW.Text = 131.ToString();            
        }

        private void linkLabel_lowlatency_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MAVLINK.SelectedValue = 2;
            MAX_WINDOW.Text = 33.ToString();        }

        public enum mavlink_option
        {
            RawData = 0,
            Mavlink = 1,
            LowLatency = 2
        }

        private enum mavlink_option_simple
        {
            RawData = 0,
            Mavlink = 1,
        }

        private void txt_aeskey_TextChanged(object sender, EventArgs e)
        {

        }


              
                
        string RemoveMultiPointLocalNodeID(string ATCReply)
        {
            ATCReply = ATCReply.Trim();

            if ((ATCReply.Length > 1) && (ATCReply[0] == '[') && ATCReply.Contains(']'))
            {
                int StartIndex = ATCReply.IndexOf(']') + 1;

                return ATCReply.Substring(StartIndex);
            }
            else
            {
                return ATCReply;
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            _configManager.RandomizeEncryptionKey();            
        }

        private void btnCommsLog_Click(object sender, EventArgs e)
        {
            
        }

        void SaveWorkingConfig(TSettings config)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                if (config.SaveToFile(dlgSave.FileName))
                {
                    MessageBox.Show("Saved settings to " + dlgSave.FileName + " OK");
                }
                else
                {
                    MessageBox.Show("Failed to save settings to " + dlgSave.FileName);
                }
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveWorkingConfig(_configManager.Current.Settings);
            //SaveToFile(_LocalSettings, groupBoxLocal, false);
        }

        private async void btn_SaveSetting_Click(object sender, EventArgs e)
        {
            _configManager.ClearProgress();
            var result = await _configManager.Save();
            if (result)
            {
                _configManager.AddLog("Reloading saved settings...");
                var loaded = await _configManager.Load();
                _configManager.CompleteProgress();
                if (loaded)
                {
                    BindControls();
                } else
                {
                    _configManager.AddLog("Settings reload failed...");
                }
                MsgBox.CustomMessageBox.Show("Settings have been saved to the device successfully", "Success");
            }
            _configManager.ClearProgress();
        }
        
        private void LoadConfigFromFile()
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                var loaded = _configManager.Current.Settings.LoadFromFile(dlgOpen.FileName);
                if (loaded == null)
                {
                    System.Windows.Forms.MessageBox.Show("Failed to load settings from " + dlgOpen.FileName);
                    return;
                }
                // Trigger the binding source to update the UI
                configManagerBindingSource.ResetBindings(false);
                
                ShowLoadedSettings(dlgOpen.FileName, loaded);                               
            }
        }

        private void ShowLoadedSettings(string FileName, Dictionary<string, TNameAndValue> x)
        {
            string Temp = "Loaded\n";

            foreach (var kvp in x)
            {
                Temp += kvp.Value.Name + " = " + kvp.Value.Value.ToString() + "\n";
            }

            Temp += "from " + dlgOpen.FileName + " OK";

            System.Windows.Forms.MessageBox.Show(Temp);
        }

        private void btn_LoadFile_Click(object sender, EventArgs e)
        {
            LoadConfigFromFile();
        }

        private void btn_SaveFile_Click(object sender, EventArgs e)
        {
            SaveWorkingConfig(_configManager.Current.Settings);
        }

        private async void btn_Reset_Click(object sender, EventArgs e)
        {
            var msgBoxResult = MsgBox.CustomMessageBox.Show(
                "Are you sure you want to reset to default configuration?  Link with remote may be lost...",
                "Reset Confirmation", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (msgBoxResult != DialogResult.Yes)
                return;

            _configManager.AddLog($"Reset Initiated...");

            await _configManager.ResetDefaults();

            _configManager.AddLog($"Reset Complete");
            MsgBox.CustomMessageBox.Show("Reset to default settings completed", "Success");
        }

        private void btn_Firmware_Click(object sender, EventArgs e)
        {
            ProgramFirmware(true);
        }
        
        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            _configManager.RandomizeEncryptionKey();
        }

        private async void BindControls()
        {
            _configManager.AddLog($"Binding Controls...");

            var sw = Stopwatch.StartNew();
            // Setup Control Bindings
            foreach (var item in _configManager.Local.Settings.Settings)
            {
                if (item.Value == null)
                    continue;
                var ctrl = this.Controls.Find(item.Key.Replace("/", "_"), true).FirstOrDefault();
                if (ctrl == null)
                {
#if DEBUG
                    _configManager.AddLog($"Control not found: {item.Key}");
#endif
                    continue;
                }

                if (!(item.Value is TSetting))
                {
                    var ttext = item.Value as TTextSetting;
                    if (ttext == null)
                        continue;

                    ctrl.DataBindings.Clear();
                    ctrl.DataBindings.Add("Text", configManagerBindingSource, ttext.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                    AddBoundControl(ctrl);
                }
                else if (ctrl is ComboBox)
                {
                    BindSettingOptionsToComboBox(item.Value as TSetting, ctrl as ComboBox);
                }
                else if (ctrl is TextBox)
                {
                    BindSettingToTextBox(item.Value as TSetting, ctrl as TextBox);
                }
                else if (ctrl is CheckBox)
                {
                    BindSettingToCheckBox(item.Value as TSetting, ctrl as CheckBox);
                }
                //_configManager.AddLog($"Loaded {item.Key}: {item.Value.GetValueAsString()}");

                // Not needed as changing Current now performs this?
                UpdateIndicator(item.Key);
            }

            // Static bindings?
            BindGPIO(GPIO0, "GPIO0", _configManager.GPIO0_Items);
            BindGPIO(GPIO1, "GPIO1", _configManager.GPIO1_Items);
            BindGPIO(GPIO2, "GPIO2", _configManager.GPIO2_Items);
            BindGPIO(GPIO3, "GPIO3", _configManager.GPIO3_Items);


            sw.Stop();
            _configManager.AddLog($"Binding completed in {sw.ElapsedMilliseconds}ms");            

            CheckControlStates();

           
        }

        private void BindGPIO(ComboBox comboBox, string bindingPropertyName, BindingList<RFDCommon.ConfigManager.PinFunction> items)
        {
            if (comboBox.DataBindings.Count > 0)
                return;

            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = items;
            
            comboBox.SelectedValue = _configManager.GetPinSetting(items);            

            // Setup Binding
            comboBox.DataBindings.Add("SelectedValue", configManagerBindingSource, bindingPropertyName, false, DataSourceUpdateMode.OnPropertyChanged);
            AddBoundControl(comboBox);
        }
                
        private async void btn_LoadSetting_Click(object sender, EventArgs e)
        {
            _configManager.AddLog("Loading settings...");
            _configManager.ClearProgress();
            var loaded = await _configManager.Load();
            if (!loaded)
            {
                ShowMessageBox("An error occurred while trying to load settings...", "Load Failed");
                return;
            }
            _configManager.CompleteProgress();
            BindControls();
            _configManager.AddLog($"Load settings complete", true);
        }

        private async void Control_Clicked_ShowHelp(object sender, EventArgs e)
        {
            // Get controls group parent
            var ctrl = sender as Control;
            var groupBox = ctrl.Parent?.Parent as GroupBox;
            if (groupBox == null)
                return;
            await DisplayHelp(groupBox);
        }

        private async Task DisplayHelp(GroupBox groupBox)
        {
            if (groupBox == null || groupBox.Controls.Count != 1)
                return;

            // No need to redraw if its the same
            if (groupBox == _helpDisplay)
                return;

            _helpDisplay = groupBox;

            // Clear current help
            richTextHelp.Clear();


            StringBuilder rtf = new StringBuilder();
            await Task.Run(() => {
                // Append the RTF header and document preamble.
                rtf.Append(@"{\rtf1\ansi");

                // Define the color table (if you want to use colors).
                rtf.Append(@"{\colortbl ;\red255\green255\blue255;}"); // Entry 0 is the default; Entry 1 is white.


                // Define a font table if you want specific fonts (optional).
                rtf.Append(@"{\fonttbl {\f0 Microsoft Sans Serif;}}");

                // Start the body group.
                rtf.Append(@"\pard"); // Reset to default paragraph properties.
                var orderedControls = groupBox.Controls[0].Controls.Cast<Control>().OrderBy(c => c.TabIndex);                
                foreach (var item in orderedControls)
                {
                    if (item is Control)
                    {
                        var ic = item as Control;
                        if (ic == null || ic.GetType() == typeof(IconPictureBox))
                            continue;
                        var toolTip = toolTip1.GetToolTip(ic);
                        if (string.IsNullOrWhiteSpace(toolTip))
                            continue;

                        // Add the control and tooltip to help?
                        // Define the heading.
                        rtf.Append($@"\b\f0\fs20\cf1 {ic.Name.Replace("_", " ")}\par"); // Bold, Font Size 20

                        // Reset the font and size for normal text and define the paragraph.
                        rtf.Append($@"\b0\f0\fs16\cf1 {toolTip}.\par\par"); // Not bold, Font 0 (Microsoft Sans Serif), Font Size 16                    
                    }
                }
                // Close the RTF control group.
                rtf.Append(@"}");
            });
            // Get all tooltips, and add then to display help?            
            richTextHelp.Rtf = rtf.ToString();
        }

        private void comboModemSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboModemSelection.SelectedItem == null)
                return;

            _configManager.Current = comboModemSelection.SelectedItem as RFDModem;
            // All indicators need to be updated?

        }

        //private void BUT_SetPPMFailSafe_Click(object sender, EventArgs e)
        //{
        //    _configManager.SetPPMFailSafe("AT&R", "AT&W");
        //}

        //private void btn_Failsafe_Click(object sender, EventArgs e)
        //{
        //    _configManager.SetPPMFailSafe("AT&R", "AT&W");
        //}

        private void GPIO1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            // Was GPIO1 changed?
            if (_configManager.GPIO1 == "GPO1_1R/COUT" || _configManager.GPIO1 == "GPO1_1SBUSOUT")
            {
                // Show PPM failsafe controls?
                lblFailsafe.Visible = true;
                FSFRAMELOSS.Visible = true;
                btn_PPMFailSafe.Visible = true;
                return;
            }

            lblFailsafe.Visible = false;
            FSFRAMELOSS.Visible = false;
            btn_PPMFailSafe.Visible = false;
        }

        private void btn_PPMFailSafe_Click(object sender, EventArgs e)
        {
            _configManager.SetPPMFailSafe();
        }

        private void ENCRYPTION_LEVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerateKey.Enabled = ENCRYPTION_LEVEL.SelectedIndex > 0;
        }
    }
}