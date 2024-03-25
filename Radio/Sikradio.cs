using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using log4net;
using MissionPlanner.Comms;
using MissionPlanner.Controls;
using MissionPlanner.MsgBox;
using MissionPlanner.Radio;
using MissionPlanner.Utilities;
using uploader;
using Microsoft.VisualBasic;
using RFDCommon.Interface;
using static RFD.RFD900.TSettings;
using RFD.RFD900;
using RFDCommon.Config;
using RFDCommon;
using RFDCommon.Radio;
using System.Runtime.CompilerServices;
using RFDLib;
using RFDCommon.RFDLib;
using System.Threading.Tasks;

namespace MissionPlanner.Radio
{
    public partial class Sikradio : UserControl, IRFDConfigForm
    {
        public delegate void LogEventHandler(string message, int level = 0);

        public delegate void ProgressEventHandler(double completed);

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool beta;
        private bool _started = false;
        private GroupBox _helpDisplay = null;

        private string firmwarefile = Path.GetTempFileName();
        private Dictionary<Control, bool> _DefaultLocalEnabled = new Dictionary<Control, bool>();
        private Dictionary<ComboBox, object> _DefaultCBObjects = new Dictionary<ComboBox, object>();
        RFD.RFD900.TSession _Session;
        ExtraParamControlsSet _LocalExtraParams;
        ExtraParamControlsSet _RemoteExtraParams;

        RFDLib.GUI.Settings.TDynamicLabelEditorPairRegister _DynamicLabelEditorPairRegister;

        RFDLib.GUI.Settings.TLabelEditorPairRegister _LocalLabelEditorPairs = new RFDLib.GUI.Settings.TLabelEditorPairRegister();
        RFDLib.GUI.Settings.TLabelEditorPairRegister _RemoteLabelEditorPairs = new RFDLib.GUI.Settings.TLabelEditorPairRegister();
        Dictionary<string, string> _KnownNameDescriptions = new Dictionary<string, string>()
        {
            {"RSSI_IN_DBM", "RSSI in dBm"},
            {"AUXSER_SPEED", "Aux Baud"},
            {"AIR_FRAMELEN", "Air Frame Length"},
        };

        // Added a property to hold ICommsSerial to avoid need to access a hard coded parent control?
        //private ICommsSerial _comPort;

        // Property for interacting with the modem connection and commands
        //private IModemComms _modemComms;

        // Added a working config set for databinding approach
        //RFD.RFD900.TSettings _LocalSettings, _LocalWorking, _RemoteSettings, _RemoteWorking;
        private ConfigManager _configManager;// = new ConfigManager();
        private IModemComms _comms;


        //MultiPointConfig _multiPointSettings;
        //AsyncConfig _asyncSettings;


        //public event Action DoDisconnectReconnect;

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

            //_configManager = configManager;
            //// Handle events coming from _configManager
            //_configManager.ShowMessageBox += (sender, args) => MsgBox.CustomMessageBox.Show(args.Text, args.Title);
            //_configManager._configManager.AddLog += (sender, args) => _configManager.AddLog(args.Text);

            // hide advanced view
            //SPLIT_local.Panel2Collapsed = true;
            //SPLIT_remote.Panel2Collapsed = true;

            _LocalExtraParams = new ExtraParamControlsSet(lblNODEID, NODEID,
                lblDESTID, DESTID, lblTX_ENCAP_METHOD, TX_ENCAP_METHOD, lblRX_ENCAP_METHOD, RX_ENCAP_METHOD,
                lblMAX_DATA, MAX_DATA, lblMAX_RETRIES, MAX_RETRIES,
                new Control[] {
                lblGLOBAL_RETRIES, GLOBAL_RETRIES, lblSER_BRK_DETMS, SER_BRK_DETMS}, false);

            

            // Set sync mode to AUTO (Sync all recommended)
            comboSyncMode.SelectedIndex = 0;

            // setup netid
            NETID.DataSource = Enumerable.Range(0, 500).ToArray();
            
            MAVLINK.DisplayMember = "Value";
            MAVLINK.ValueMember = "Key";
            SetupComboForMavlink(MAVLINK, false);
            
            MAX_WINDOW.DataSource = Enumerable.Range(33, 131 - 32).ToArray();
            
            // Disable all children, instead of being selective?
            //SetEnabled(this.Controls, false, true);
            //foreach (Control C in groupBoxLocal.Controls)
            //{
            //    _DefaultLocalEnabled[C] = C.Enabled;
            //}
            //foreach (Control C in groupBoxRemote.Controls)
            //{
            //    _DefaultLocalEnabled[C] = C.Enabled;
            //}

            SaveDefaultCBObjects(SERIAL_SPEED);
            
            SaveDefaultCBObjects(AIR_SPEED);
            
            SaveDefaultCBObjects(NETID);
            
            SaveDefaultCBObjects(NUM_CHANNELS);
            
            SaveDefaultCBObjects(MAX_WINDOW);
            
            RFDLib.GUI.Settings.TDynamicLabelEditorPair SBUSIN = new RFDLib.GUI.Settings.TDynamicLabelEditorPair(lblSBUSIN, GPO1_3SBUSIN,
                new RFDLib.GUI.Settings.TDynamicLabelEditorPair.TSettingNameLabelTextPair[]
                {
                    new RFDLib.GUI.Settings.TDynamicLabelEditorPair.TSettingNameLabelTextPair("GPO1_3SBUSIN", "GPO1_3SBUSIN"),
                    new RFDLib.GUI.Settings.TDynamicLabelEditorPair.TSettingNameLabelTextPair("GPO1_1SBUSIN", "GPO1_1SBUSIN"),
                });
            
            RFDLib.GUI.Settings.TDynamicLabelEditorPair SBUSOUT = new RFDLib.GUI.Settings.TDynamicLabelEditorPair(lblSBUSOUT, GPO1_3SBUSOUT,
                new RFDLib.GUI.Settings.TDynamicLabelEditorPair.TSettingNameLabelTextPair[]
                {
                    new RFDLib.GUI.Settings.TDynamicLabelEditorPair.TSettingNameLabelTextPair("GPO1_3SBUSOUT", "GPO1_3SBUSOUT"),
                    new RFDLib.GUI.Settings.TDynamicLabelEditorPair.TSettingNameLabelTextPair("GPO1_1SBUSOUT", "GPO1_1SBUSOUT"),
                });
            
            _DynamicLabelEditorPairRegister = new RFDLib.GUI.Settings.TDynamicLabelEditorPairRegister(new RFDLib.GUI.Settings.TDynamicLabelEditorPair[]
                {
                    SBUSIN, SBUSOUT,
                });

            _LocalLabelEditorPairs.Add(lblNODEID, NODEID, toolTip1);
            _LocalLabelEditorPairs.Add(lblDESTID, DESTID, toolTip1);
            _LocalLabelEditorPairs.Add(lblTX_ENCAP_METHOD, TX_ENCAP_METHOD, toolTip1);
            _LocalLabelEditorPairs.Add(lblRX_ENCAP_METHOD, RX_ENCAP_METHOD, toolTip1);
            _LocalLabelEditorPairs.Add(lblMAX_DATA, MAX_DATA, toolTip1);
            _LocalLabelEditorPairs.Add(lblMAX_RETRIES, MAX_RETRIES, toolTip1);
            _LocalLabelEditorPairs.Add(lblGLOBAL_RETRIES, GLOBAL_RETRIES, toolTip1);
            _LocalLabelEditorPairs.Add(lblSER_BRK_DETMS, SER_BRK_DETMS, toolTip1);
            _LocalLabelEditorPairs.Add(label54, FSFRAMELOSS, toolTip1);
            _LocalLabelEditorPairs.Add(lblNETID, NETID, toolTip1);
            _LocalLabelEditorPairs.Add(lblTXPOWER, TXPOWER, toolTip1);
            _LocalLabelEditorPairs.Add(lblMAVLINK, MAVLINK, toolTip1);
            _LocalLabelEditorPairs.Add(lblOPPRESEND, OPPRESEND, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPI1_1R_CIN, GPI1_1R_CIN, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPO1_1R_COUT, GPO1_1R_COUT, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPO1_3STATLED, GPO1_3STATLED, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPI1_2AUXIN, GPI1_2AUXIN, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPO1_3AUXOUT, GPO1_3AUXOUT, toolTip1);
            _LocalLabelEditorPairs.Add(lblMIN_FREQ, MIN_FREQ, toolTip1);
            _LocalLabelEditorPairs.Add(lblMAX_FREQ, MAX_FREQ, toolTip1);
            _LocalLabelEditorPairs.Add(lblNUM_CHANNELS, NUM_CHANNELS, toolTip1);
            _LocalLabelEditorPairs.Add(lblDUTY_CYCLE, DUTY_CYCLE, toolTip1);
            _LocalLabelEditorPairs.Add(lblLBT_RSSI, LBT_RSSI, toolTip1);
            _LocalLabelEditorPairs.Add(lblRTSCTS, RTSCTS, toolTip1);
            _LocalLabelEditorPairs.Add(lblMAX_WINDOW, MAX_WINDOW, toolTip1);
            _LocalLabelEditorPairs.Add(lblENCRYPTION_LEVEL, ENCRYPTION_LEVEL, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPO1_0TXEN485, GPO1_0TXEN485, toolTip1);
            _LocalLabelEditorPairs.Add(lblGPIO1_1FUNC, GPIO1_1FUNC, toolTip1);

            //this.Disposed += DisposedEvtHdlr;

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
                        
            // Have just connected, enable the form?
            //SetEnabled(this.Controls, true, true);

            // AutoLoad?
            //_configManager.Load(S);
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
            }
        }
                

        private void SaveDefaultCBObjects(ComboBox CB)
        {
            if (CB.DataSource == null)
            {
                List<object> LO = new List<object>();
                foreach (var O in CB.Items)
                {
                    LO.Add(O);
                }
                _DefaultCBObjects[CB] = LO;
            }
            else
            {
                _DefaultCBObjects[CB] = CB.DataSource;
            }
        }

        private void SetEnabled(ControlCollection controls, bool setState, bool recursive)
        {            
            foreach (Control c in controls)
            {                
                c.Enabled = setState;
                if (c.Controls.Count > 0)
                {                    
                    if (recursive)
                        SetEnabled(c.Controls, setState, recursive);
                }
            }            
        }

        private void RestoreAllDefaultCBObjects()
        {
            foreach (var kvp in _DefaultCBObjects)
            {
                if (kvp.Value is List<object>)
                {
                    kvp.Key.DataSource = null;
                    kvp.Key.Items.Clear();
                    List<object> LO = (List<object>)kvp.Value;
                    foreach (var O in LO)
                    {
                        kvp.Key.Items.Add(O);
                    }
                }
                else
                {
                    kvp.Key.DataSource = kvp.Value;
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

        private void BUT_upload_Click(object sender, EventArgs e)
        {
            ProgramFirmware(false);
        }

        private void iHex_ProgressEvent(double completed)
        {
            try
            {
                Progressbar.Value = (int) (completed*100);
                Application.DoEvents();
            }
            catch
            {
            }
        }

        private void uploader_LogEvent(string message, int level = 0)
        {
            try
            {
                if (level == 0)
                {
                    Console.Write(message);
                    lbl_status.Text = message;
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
                    lbl_status.Text = message;
                    Console.WriteLine(message);
                    log.Info(message);
                    Application.DoEvents();
                }
            }
            catch
            {
            }
        }

        private void uploader_ProgressEvent(double completed)
        {
            try
            {
                Progressbar.Value = (int)Math.Min (completed*100,100);
                Application.DoEvents();
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

        int GetValueFromControl(Control control)
        {
            if (control.GetType() == typeof(CheckBox))
            {
                return ((CheckBox)control).Checked ? 1 : 0;
            }
            else if (control is ComboBox)
            {
                string CBValue = GetCBValue((ComboBox)control);

                int Result;

                if (int.TryParse(CBValue, out Result))
                {
                    return Result;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
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

        TBaseSetting GetSettingFromControl(Control control)
        {
            if (control.GetType() == typeof(CheckBox))
            {
                return new TSetting<int>(((CheckBox)control).Checked ? 1 : 0);
            }
            else if (control is TextBox)
            {
                return new TSetting<string>(control.Text);
            }
            else if (control is ComboBox)
            {
                int x;

                if (int.TryParse(GetCBValue((ComboBox)control), out x))
                {
                    return new TSetting<int>(x);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns whether x is different to y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool GetIsDifferent(RFD.RFD900.TBaseSetting x, TBaseSetting y)
        {
            if (y is TSetting<int>)
            {
                return x is RFD.RFD900.TSetting && ((RFD.RFD900.TSetting)x).Value != ((TSetting<int>)y).Value;
            }
            else if (y is TSetting<string>)
            {
                return x is RFD.RFD900.TTextSetting && ((RFD.RFD900.TTextSetting)x).Text != ((TSetting<string>)y).Value;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Make a clone of the given original setting.  Update it's value with Value, and return it.
        /// </summary>
        /// <param name="Orig">The original setting.  Must not be null.</param>
        /// <param name="Value">The new value for the setting.  Must not be null.</param>
        /// <returns>The new cloned setting with updated value.  Never null.</returns>
        RFD.RFD900.TBaseSetting UpdateSetting(RFD.RFD900.TBaseSetting Orig, TBaseSetting Value)
        {
            if (Value is TSetting<int>)
            {
                RFD.RFD900.TSetting y = (RFD.RFD900.TSetting)Orig.Clone();
                y.Value = ((TSetting<int>)Value).Value;
                return y;
            }
            else
            {
                RFD.RFD900.TTextSetting y = (RFD.RFD900.TTextSetting)Orig.Clone();
                y.Text = ((TSetting<string>)Value).Value;
                return y;
            }
        }

        /// <summary>
        /// Figure out which settings in the GUI have different values to the given original settings.
        /// Return a list of the settings which have changed, and their new values.
        /// </summary>
        /// <param name="Orig">The original settings and their values.  Must not be null.</param>
        /// <param name="GB">The relevant groupbox.  Must not be null.</param>
        /// <param name="Remote">true if the remote modem, false if the local modem.</param>
        /// <returns></returns>
        Dictionary<string, RFD.RFD900.TBaseSetting> GetUpdatedSettingsFromGroupBox(
            Dictionary<string, RFD.RFD900.TBaseSetting> Orig, GroupBox GB, 
            bool Remote)
        {
            Dictionary<string, RFD.RFD900.TBaseSetting> Result = new Dictionary<string, RFD.RFD900.TBaseSetting>();

            foreach (var kvp in Orig)
            {
                var control = FindControlInGroupBox(GB, (Remote ? "R" : "") + kvp.Key);

                if (control != null)
                {
                    var S = GetSettingFromControl(control);

                    if (S != null)
                    {
                        if (GetIsDifferent(kvp.Value, S))
                        {
                            Result[kvp.Key] = UpdateSetting(kvp.Value, S);
                        }
                    }
                }
            }

            return Result;
        }
        
        
        private void ShowMessageBox(string message, string caption)
        {

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
            } 
            else if (setting.Range != null)
            {
                comboBox.DataBindings.Clear();
                comboBox.DataSource = setting.Range.GetOptionsIncludingValue(setting.Value);                
                comboBox.Tag = null;
                comboBox.DataBindings.Add("SelectedItem", configManagerBindingSource, setting.Name, false, DataSourceUpdateMode.OnPropertyChanged);
            }            
        }

        public void BindSettingToTextBox(TSetting setting, TextBox textBox)
        {
            textBox.DataBindings.Clear();
            textBox.DataBindings.Add("Text", configManagerBindingSource, setting.Name, false,  DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetupCBWithDefaultEncryptionOptions(ComboBox CB)
        {
            CB.Tag = null;
            CB.DataSource = Range(0, 1, 1);
            CB.Text = "0";
        }

        private string GetCBValue(ComboBox CB)
        {
            if (CB.Tag != null)
            {
                RFD.RFD900.TSetting Setting = (RFD.RFD900.TSetting)CB.Tag;
                foreach (var O in Setting.Options)
                {
                    if (O.OptionName == CB.Text)
                    {
                        return O.Value.ToString();
                    }
                }
            }

            //If got to here, and it's a MAVLink setting, revert back to the old method...
            if (CB.Name.Contains("MAVLINK"))
            {
                var Value = CB.SelectedValue;
                if (Value != null)
                {
                    return Value.ToString();
                }
            }

            //If got here, just return the text.
            return CB.Text;
        }

        void SetupComboForMavlink(ComboBox CB,  bool Simple)
        {
            Dictionary<int, string> dict;
            if (Simple)
            {
                dict = Enum.GetValues(typeof(mavlink_option_simple))
                    .Cast<mavlink_option>()
                    .ToDictionary(t => (int)t, t => t.ToString());
            }
            else
            {
                dict = Enum.GetValues(typeof(mavlink_option))
                    .Cast<mavlink_option>()
                    .ToDictionary(t => (int)t, t => t.ToString());
            }

            CB.DataSource = dict.ToArray();
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

        Control FindControlInGroupBox(GroupBox GB, string Name)
        {
            Control Result = _DynamicLabelEditorPairRegister.FindAndSetUpEditorWithSettingName(Name);
            if (Result == null)
            {
                var Array = GB.Controls.Find(Name, true);

                if (Array.Length == 0)
                {
                    return null;
                }
                else
                {
                    return Array[0];
                }
            }
            else
            {
                return Result;
            }
        }

        /// <summary>
        /// Returns whether it can be determined that the setting (from the given Settings) with the given
        /// SettingName only has one value/option available (i.e. setting can't be changed).
        /// </summary>
        /// <param name="Settings">The dictionary of settings for the modem.  Must not be null.</param>
        /// <param name="SettingName">The setting name.  Must not be null.</param>
        /// <returns>Returns true if it can be determined that the setting can only have one value, otherwise false.</returns>
        bool GetDoesCheckboxHaveOnlyOneOption(Dictionary<string, RFD.RFD900.TBaseSetting> Settings, string SettingName)
        {
            if (Settings.ContainsKey(SettingName))
            {
                var BaseSetting = Settings[SettingName];

                if (BaseSetting is RFD.RFD900.TSetting)
                {
                    RFD.RFD900.TSetting Setting = (RFD.RFD900.TSetting)BaseSetting;

                    if (Setting.Options != null)
                    {
                        if (Setting.Options.Length == 1)
                        {
                            return true;
                        }
                    }
                    if (Setting.Range != null)
                    {
                        if (Setting.Range.GetOptions().Length == 1)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Configure and return a spare editor if one is available.
        /// </summary>
        /// <param name="ThisSetting">The name of the setting to get the editor for.  Must not be null.</param>
        /// <param name="Setting">The setting to get the editor for.  Must not be null.</param>
        /// <param name="Remote">Whether it is the remote set of settings.</param>
        /// <returns>A control to use, or null if none available.</returns>
        Control GetSpareEditor(string ThisSetting, RFD.RFD900.TSetting Setting, bool Remote)
        {
            RFDLib.GUI.Settings.TLabelEditorPairRegister Reg = Remote ? _RemoteLabelEditorPairs : _LocalLabelEditorPairs;
            Control Result = null;
            string Description = Remote ? ThisSetting.Substring(1) : ThisSetting;

            if (_KnownNameDescriptions.ContainsKey(Description))
            {
                Description = _KnownNameDescriptions[Description];
            }

            if (Setting.GetIsFlag())
            {
                Result = Reg.GetSpareCheckbox(ThisSetting, Description);
            }

            if (Result == null)
            {
                Result = Reg.GetSpareComboBox(ThisSetting, Description);
            }

            return Result;
        }
        
        /// <summary>
        /// Load settings button evt hdlr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BUT_getcurrent_Click(object sender, EventArgs e)
        {
            _AlreadyInEncCheckChangedEvtHdlr = true;

            // Disable Action Buttons
            //SetEnabled(flowLayoutActions.Controls, false, recursive: false);
            // Disable device settings groups
            //SetEnabled(flowLayoutSettings.Controls, false, recursive: false);
            //SetEnabled(flowLayoutMain.Controls, false, recursive: false);

            _configManager.AddLog("Loading settings...");
            
            var loaded = await _configManager.Load();
            if (!loaded)
            {
                ShowMessageBox("An error occured while trying to load settings...", "Load Failed");
                return;
            }

            // Setup Control Bindings
            foreach (var item in _configManager.Local.Settings.Settings)
            {
                if (item.Value == null)
                    continue;
                var ctrl = this.Controls.Find(item.Key.Replace("/", "_"), true).FirstOrDefault();
                if (ctrl == null)
                {
                    _configManager.AddLog($"Control not found: {item.Key}");
                    continue;
                }

                if (!(item.Value is TSetting))
                {
                    var ttext = item.Value as TTextSetting;
                    if (ttext == null)
                        continue;

                    ctrl.DataBindings.Clear();
                    ctrl.DataBindings.Add("Text", configManagerBindingSource, ttext.Name, false, DataSourceUpdateMode.OnPropertyChanged);
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
                _configManager.AddLog($"Setting: {item.Key}: {item.Value.GetValueAsString()}");
            }

            _AlreadyInEncCheckChangedEvtHdlr = false;

            UpdateSetPPMFailSafeButtons();

            // Disable Action Buttons
            //SetEnabled(flowLayoutActions.Controls, true, recursive: false);
            // Disable all settings groups
            //SetEnabled(flowLayoutSettings.Controls, true, recursive: false);
            //SetEnabled(flowLayoutMain.Controls, true, recursive: false);
            //BUT_getcurrent.Focus();           
        }

        

        void UpdateSetPPMFailSafeButtons()
        {
            BUT_SetPPMFailSafe.Enabled = GPO1_1R_COUT.Enabled && GPO1_1R_COUT.Checked;
            //BUT_SetPPMFailSafeRemote.Enabled = RGPO1_1R_COUT.Enabled && RGPO1_1R_COUT.Checked;
        }

        

        
        ///// <summary>
        ///// Tries to put the radio into AT command mode.
        ///// </summary>
        ///// <param name="comPort"></param>
        ///// <returns></returns>
        //public bool doConnect(ICommsSerial comPort)
        //{
        //    try
        //    {
        //        Console.WriteLine("doConnect");

        //        var trys = 1;

        //        // setup a known enviroment
        //        comPort.Write("ATO\r\n");

        //        retry:

        //        // wait
        //        Sleep(1500, comPort);
        //        comPort.DiscardInBuffer();
        //        // send config string
        //        comPort.Write("+");
        //        Sleep(200, comPort);
        //        comPort.Write("+");
        //        Sleep(200, comPort);
        //        comPort.Write("+");
        //        Sleep(1500, comPort);
        //        // check for config response "OK"
        //        log.Info("Connect btr " + comPort.BytesToRead + " baud " + comPort.BaudRate);
        //        // allow time for data/response

        //        if (comPort.BytesToRead == 0 && trys <= 3)
        //        {
        //            trys++;
        //            log.Info("doConnect retry");
        //            goto retry;
        //        }

        //        var buffer = new byte[20];
        //        var len = comPort.Read(buffer, 0, buffer.Length);
        //        var conn = Encoding.ASCII.GetString(buffer, 0, len);
        //        log.Info("Connect first response " + conn.Replace('\0', ' ') + " " + conn.Length);
        //        if (conn.Contains("OK"))
        //        {
        //            //return true;
        //        }
        //        else
        //        {
        //            // cleanup incase we are already in cmd mode
        //            comPort.Write("\r\n");
        //        }

        //        doCommand(comPort, "AT&T", false, 1);

        //        var version = doCommand(comPort, "ATI");

        //        log.Info("Connect Version: " + version.Trim() + "\n");

        //        var regex = new Regex(@"SiK\s+(.*)\s+on\s+(.*)");

        //        if (regex.IsMatch(version))
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        private void BUT_Syncoptions_Click(object sender, EventArgs e)
        {
            // TODO: Sync via ConfigMAnager, not UI controls

            //RAIR_SPEED.Text = AIR_SPEED.Text;
            //RNETID.Text = NETID.Text;
            //RECC.Checked = ECC.Checked;
            //RMAVLINK.Text = MAVLINK.Text;
            //RMIN_FREQ.Text = MIN_FREQ.Text;
            //RMAX_FREQ.Text = MAX_FREQ.Text;
            //RNUM_CHANNELS.Text = NUM_CHANNELS.Text;
            //RMAX_WINDOW.Text = MAX_WINDOW.Text;
            //RENCRYPTION_LEVEL.SelectedIndex = ENCRYPTION_LEVEL.SelectedIndex;
            //RAESKEY.Text = AESKEY.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MsgBox.CustomMessageBox.Show(@"The Sik Radios have 2 status LEDs, one red and one green.
green LED blinking - searching for another radio 
green LED solid - link is established with another radio 
red LED flashing - transmitting data 
red LED solid - in firmware update mode");
        }

        

        private async void BUT_resettodefault_Click(object sender, EventArgs e)
        {   
            _configManager.AddLog($"Initiating Config Reset");

            await _configManager.ResetDefaults();
        }
                
        void UpdateStatusCallback(string Status, double Progress)
        {
            if (Status != null)
            {
                _configManager.AddLog(Status);
            }
            if (!double.IsNaN(Progress))
            {
                ProgressEvtHdlr(Progress);
            }
        }

        void CheckControlStates()
        {
            //groupData.Enabled = _configManager.DataEnabled;
            //groupFirmware.Enabled = _configManager.DeviceGroupEnabled;
            //groupRadio.Enabled = _configManager.RadioEnabled;
            //groupSerial.Enabled = _configManager.SerialEnabled;
            //groupSecurity.Enabled = _configManager.SecurityEnabled;
            //groupGPIO.Enabled = _configManager.PinEnabled;
            
            // Apparently the order is important inside a flow layout /sigh
            groupFirmware.Visible = _configManager.DeviceGroupEnabled;
            groupSerial.Visible = _configManager.SerialEnabled;
            groupRadio.Visible = _configManager.RadioEnabled;            
            groupSecurity.Visible = _configManager.SecurityEnabled;
            groupGPIO.Visible = _configManager.PinEnabled;
            groupData.Visible = _configManager.DataEnabled;
            groupInfo.Visible = _configManager.InfoEnabled;

            SetEnabled(groupFirmware.Controls, _configManager.DeviceGroupEnabled, true);
            SetEnabled(groupSerial.Controls, _configManager.SerialEnabled, true);
            SetEnabled(groupRadio.Controls, _configManager.RadioEnabled, true);
            SetEnabled(groupSecurity.Controls, _configManager.SecurityEnabled, true);
            SetEnabled(groupGPIO.Controls, _configManager.PinEnabled, true);
            SetEnabled(groupData.Controls, _configManager.DataEnabled, true);
            SetEnabled(groupInfo.Controls, _configManager.InfoEnabled, true);

            btn_LoadSetting.Enabled = _configManager.LoadEnabled;
            btn_SaveSetting.Enabled = _configManager.SaveEnabled;
            btn_LoadFile.Enabled = _configManager.ImportEnabled;
            btn_SaveFile.Enabled = _configManager.ExportEnabled;
            btn_Reset.Enabled = _configManager.ResetEnabled;
            btn_Firmware.Enabled = _configManager.FirmwareEnabled;
            btn_Reboot.Enabled = _configManager.ResetEnabled;
        }

        public static void ResetAllControls(Control form)
        {
            {
                foreach (Control control in form.Controls)
                {
                    control.Enabled = false;
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        textBox.Text = null;
                    }

                    if (control is ComboBox)
                    {
                        ComboBox comboBox = (ComboBox)control;
                        if (comboBox.Items.Count > 0)
                            comboBox.SelectedIndex = 0;
                    }

                    if (control is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)control;
                        checkBox.Checked = false;
                    }

                    if (control is ListBox)
                    {
                        ListBox listBox = (ListBox)control;
                        listBox.ClearSelected();
                    }
                }
            }
        }        

        private void BUT_loadcustom_Click(object sender, EventArgs e)
        {
            ProgramFirmware(true);
        }

        void ProgramFirmware(bool Custom)
        {
            //EnableProgrammingControls(false);
            //EnableConfigControls(false, false);

            try
            {
                _configManager.AddLog("Determining mode...");
                _configManager.AddLog("Mode is " + _configManager.Local.Mode.ToString());
                
                RFD.RFD900.RFD900 RFD900 = _Session.GetModemObject();

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
                        if (RFD900.ProgramFirmware(firmwarefile, UpdateStatusCallback))
                        {
                            _configManager.AddLog("Programmed firmware into device");
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
            //EnableProgrammingControls(true);
            //EnableConfigControls(true, false);
            //UploadFW(true);
        }

        void ProgressEvtHdlr(double Completed)
        {
            try
            {
                Progressbar.Minimum = 0;
                Progressbar.Maximum = 100;
                Progressbar.Value = Math.Min((int)(Completed * 100F), 100);
                Application.DoEvents();
                
            }
            catch
            {
                //Console.WriteLine("Failed");
            }
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

            //RMAVLINK.SelectedValue = 1;
            //RMAX_WINDOW.Text = 131.ToString();
        }

        private void linkLabel_lowlatency_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MAVLINK.SelectedValue = 2;
            MAX_WINDOW.Text = 33.ToString();

            //RMAVLINK.SelectedValue = 2;
            //RMAX_WINDOW.Text = 33.ToString();
        }

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


        private void BUT_SetPPMFailSafe_Click(object sender, EventArgs e)
        {
            _configManager.SetPPMFailSafe("AT&R", "AT&W");
        }

        //TSession GetSession()
        //{
        //    if (_Session == null)
        //    {
        //        try
        //        {                    
        //            if (_comPort != null)
        //            {
        //                _Session = new RFD.RFD900.TSession(_comPort, MainV2.comPort.BaseStream.BaudRate);
        //            }
        //        }
        //        catch
        //        {
        //            MsgBox.CustomMessageBox.Show("Invalid ComPort or in use");
        //            return null;
        //        }
        //    }
        //    else if (_Session.Port.BaudRate != MainV2.comPort.BaseStream.BaudRate ||
        //        (MainV2.comPort.BaseStream.PortName != "TCP" && (_Session.Port.PortName != MainV2.comPort.BaseStream.PortName)))
        //    {
        //        _Session.Dispose();
        //        _Session = null;
        //        GetSession();
        //    }
        //    return _Session;
        //}

        
        //bool SetSetting(string Designator, int Value, bool Remote)
        //{
        //    var Session = GetSession();

        //    if (Session == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        var answer = _configManager..doCommand(Session.Port, (Remote ? "RT" : "AT")+Designator+"="+Value.ToString(), false);
        //        return answer.Contains("OK");
        //    }
        //}

        /// <summary>
        /// Handles a change of the local encryption level check box.
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">ignored</param>
        //private void ENCRYPTION_LEVEL_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ENCRYPTION_LEVEL.Enabled)
        //    {
        //        EncryptionCheckChangedEvtHdlr(ENCRYPTION_LEVEL, "ATI5", "AT&E?", AESKEY, false, "ATI5");
        //    }
        //    btnRandom.Enabled = GetIsEncryptionEnabled(ENCRYPTION_LEVEL);

        //    // TODO: Sync to all remotes?

        //}

        bool _AlreadyInEncCheckChangedEvtHdlr = false;

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

        /// <summary>
        /// Handles a change of an encryption level check box.
        /// </summary>
        /// <param name="CB">The checkbox which was changed.  Must not be null.</param>
        /// <param name="ATCommand">The AT command to use to get the settings
        /// from the relevant modem.  Must not be null.</param>
        //void EncryptionCheckChangedEvtHdlr(ComboBox CB, string ATCommand, string EncKeyQuery,
        //    TextBox EncKeyTextBox, bool Remote, string ATI5Command)
        //{
        //    if (_AlreadyInEncCheckChangedEvtHdlr)
        //    {
        //        return;
        //    }
        //    _AlreadyInEncCheckChangedEvtHdlr = true;
        //    try
        //    {
        //        //Write setting to radio now.
        //        var Session = GetSession();

        //        if (Session == null)
        //        {
        //            return;
        //        }
        //        Session.PutIntoATCommandMode();
        //        var ATI5answer = doCommand(Session.Port, ATI5Command, true);

        //        bool Junk;

        //        var Settings = Session.GetSettings(Remote, Session.Board, ATI5answer, null, out Junk);
        //        if (Settings.ContainsKey("ENCRYPTION_LEVEL"))
        //        {
        //            var Setting = Settings["ENCRYPTION_LEVEL"];
        //            if (!SetSetting(Setting.Designator, GetEncryptionLevelValue(CB), Remote))
        //            {
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Something wrong here");
        //        }

        //        //Read AES key back out of modem and display it.  
        //        //BUT_getcurrent_Click(this, null);
        //        //txt_aeskey.Text = doCommand(Session.Port, "AT&E?").Trim();
        //        EncKeyTextBox.Text = RemoveMultiPointLocalNodeID(doCommand(Session.Port, EncKeyQuery).Trim()).Trim();
        //        lbl_status.Text = "Done.";
        //    }
        //    finally
        //    {
        //        _AlreadyInEncCheckChangedEvtHdlr = false;
        //    }
        //}

        

        private void btnRandom_Click(object sender, EventArgs e)
        {
            _configManager.RandomizeEncryptionKey();            
        }

        private void btnCommsLog_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Save settings from GUI to file.  
        /// </summary>
        /// <param name="S">The settings read from the modem.  Must not be null.</param>
        /// <param name="GB">The relevant GUI groupbox.</param>
        /// <param name="Remote">true if remote modem, false if local modem.</param>
        void SaveToFile(RFD.RFD900.TSettings S, GroupBox GB,
            bool Remote)
        {
            //Get the settings which have changed in the GUI, and their values.
            var Updated = GetUpdatedSettingsFromGroupBox(
                RFDLib.Collections.Translate(S.Settings, (x) => (RFD.RFD900.TBaseSetting)x),
                GB, Remote);

            //Include the settings which haven't changed.
            foreach (var kvp in S.Settings)
            {
                if (!Updated.ContainsKey(kvp.Key))
                {
                    Updated[kvp.Key] = kvp.Value;
                }
            }

            //Save to file...
            RFD.RFD900.TSettings ToSave = new RFD.RFD900.TSettings(Updated);

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                if (ToSave.SaveToFile(dlgSave.FileName))
                {
                    System.Windows.Forms.MessageBox.Show("Saved settings to " + dlgSave.FileName + " OK");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Failed to save settings to " + dlgSave.FileName);
                }
            }
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
            await _configManager.Save();
        }

        /// <summary>
        /// Load settings from file into the GUI.
        /// </summary>
        /// <param name="S">The settings loaded from the modem.  These aren't modified by this function.  Must not be null.</param>
        /// <param name="GB">The relevant GUI groupbox.  Must not be null.</param>
        /// <param name="Remote">true if for the remote modem, false if for the local modem.</param>
        private void LoadFromFile(RFD.RFD900.TSettings S, GroupBox GB, bool Remote)
        {
            throw new NotImplementedException();
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                S = S.Clone();

                var x = S.LoadFromFile(dlgOpen.FileName);

                if (x == null)
                {
                    System.Windows.Forms.MessageBox.Show("Failed to load settings from " + dlgOpen.FileName);
                }
                else
                {
                   

                    //UpdateControlsWithValues(GB, Remote, S.Settings);

                    string Temp = "Loaded\n";

                    foreach (var kvp in x)
                    {
                        Temp += kvp.Value.Name + " = " + kvp.Value.Value.ToString() + "\n";
                    }

                    Temp += "from " + dlgOpen.FileName + " OK";

                    System.Windows.Forms.MessageBox.Show(Temp);
                }
            }
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


        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            LoadConfigFromFile();
            //LoadFromFile(_LocalSettings, groupBoxLocal, false);
        }      

        public string Header
        {
            get
            {
                return "Settings";
            }
        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            _configManager.ANT_MODE = 1;
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
            _configManager.AddLog($"Initiating Config Reset");

            await _configManager.ResetDefaults();
        }

        private void btn_Firmware_Click(object sender, EventArgs e)
        {
            ProgramFirmware(true);
        }
        
        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            _configManager.RandomizeEncryptionKey();
        }

        private async void btnReboot_Click(object sender, EventArgs e)
        {
            // TODO: Do restart?
            await _configManager.RestartModem();
        }

        private async void btn_LoadSetting_Click(object sender, EventArgs e)
        {
            _configManager.AddLog("Loading settings...");
            
            var loaded = await _configManager.Load();
            if (!loaded)
            {
                ShowMessageBox("An error occured while trying to load settings...", "Load Failed");
                return;
            }

            // Setup Control Bindings
            foreach (var item in _configManager.Local.Settings.Settings)
            {
                if (item.Value == null)
                    continue;
                var ctrl = this.Controls.Find(item.Key.Replace("/", "_"), true).FirstOrDefault();
                if (ctrl == null)
                {
                    _configManager.AddLog($"Control not found: {item.Key}");
                    continue;
                }

                if (!(item.Value is TSetting))
                {
                    var ttext = item.Value as TTextSetting;
                    if (ttext == null)
                        continue;

                    ctrl.DataBindings.Clear();
                    ctrl.DataBindings.Add("Text", configManagerBindingSource, ttext.Name, false, DataSourceUpdateMode.OnPropertyChanged);
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
                _configManager.AddLog($"Setting: {item.Key}: {item.Value.GetValueAsString()}");
            }

            CheckControlStates();                       
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
                foreach (var item in groupBox.Controls[0].Controls)
                {
                    if (item is Control)
                    {
                        var ic = item as Control;
                        if (ic == null)
                            continue;
                        var toolTip = toolTip1.GetToolTip(ic);
                        if (string.IsNullOrWhiteSpace(toolTip))
                            continue;

                        // Add the control and tooltip to help?
                        // Define the heading.
                        rtf.Append($@"\b\f0\fs24\cf1 {ic.Name}\par"); // Bold, Font Size 24

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

        
    }
}