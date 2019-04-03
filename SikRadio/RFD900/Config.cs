using System;
using System.Collections.Generic;
using RFDLib.Config;

namespace RFD.RFD900.Config
{
    public class TATDescriptor
    {
        public readonly string ATName;
        public readonly string ATNumber;

        public TATDescriptor(string ATName, string ATNumber)
        {
            this.ATName = ATName;
            this.ATNumber = ATNumber;
        }

        /*public string GenerateSetCommand(IParam Param)
        {
            return "AT" + ATNumber + "=" + Param.ToString();
        }*/

        public static ISetting GenerateSettingFromRFD900Setting(RFD.RFD900.TSetting Setting,
            uploader.Uploader.Board Board, uploader.Uploader.Frequency Freq, bool Async)
        {
            TSettingDescription Desc = TDesignatorLookupTable.TABLE.GetDescription(Setting.Name);
            if (Desc == null)
            {
                Desc = new TSettingDescription(Setting.Name, Setting.Name, Setting.Name);
            }

            //If there's no range or options...
            if ((Setting.Options == null) && (Setting.Range == null))
            {
                if (Desc is TSettingDescWithID)
                {
                    //This has to be referred back to a lookup table for this modem model in order to generate its setting.
                    var TheMap = Map.TModemSettingMap.GetMapForModem(Board, Freq, Async);
                    return TheMap.GenerateSetting(((TSettingDescWithID)Desc).ID, Setting);
                }
                else
                {
                    TIntegerSettingFromSettingDescriptor ISFSD =
                        new TIntegerSettingFromSettingDescriptor(Desc.Name, Desc.Description, Setting);
                    return ISFSD.CreateNewSetting();
                }
            }
            else
            {
                if ((Setting.Options == null) && (Setting.Range.Min == 0) && (Setting.Range.Max == 1))
                {
                    TBoolSettingFromSettingDescriptor BSFSD =
                        new TBoolSettingFromSettingDescriptor(Desc.Name, Desc.Description, Setting);
                    return BSFSD.CreateNewSetting();
                }
                else
                {
                    TMultiSettingDescriptor MSD = new TMultiSettingDescriptor(Desc.Name, Desc.Description, Setting);
                    return MSD.CreateNewSetting();
                }
            }
        }
    }

    enum TSettingID
    {
        FORMAT,
        SERIAL_SPEED,
        AIR_SPEED,
        NETID,
        TXPOWER,
        ECC,
        MAVLINK,
        OPPRESEND,
        MIN_FREQ,
        MAX_FREQ,
        NUM_CHANNELS,
        DUTY_CYCLE,
        LBT_RSSI,
        RTSCTS,
        MAX_WINDOW,
        ENCRYPTION_LEVEL,
        GPI1_1R_CIN,
        GPO1_1R_COUT,
        GPO1_3SBUSIN,
        GPO1_3SBUSOUT,
        ANT_MODE,
        RATE_FREQBAND,
    }

    class TSettingDescription : RFDLib.Config.TSettingDescription
    {
        public readonly string ATName;

        public TSettingDescription(string ATName, string Name, string Description)
            : base(Name, Description)
        {
            this.ATName = ATName;
        }
    }

    class TSettingDescWithID : TSettingDescription
    {
        public readonly TSettingID ID;

        public TSettingDescWithID(TSettingID ID, string ATName, string Name, string Description)
            : base(ATName, Name, Description)
        {
            this.ID = ID;
        }
    }

    class TDesignatorLookupTable
    {
        Dictionary<TSettingID, TSettingDescription> _Table = new Dictionary<TSettingID, TSettingDescription>();
        public readonly static TDesignatorLookupTable TABLE = new TDesignatorLookupTable();

        public TDesignatorLookupTable()
        {
            AddDesc(TSettingID.FORMAT, "FORMAT", "Format", "");
            AddDesc(TSettingID.SERIAL_SPEED, "SERIAL_SPEED", "Baud", "Serial baud rate in rounded kbps. So 57 means 57600. ");
            AddDesc(TSettingID.AIR_SPEED, "AIR_SPEED", "Air Speed", "AIR_SPEED is the inter-radio data rate in rounded kbps. So 128 means 128kbps. Max is 192, min is 2. I would not recommend values below 16 as the frequency hopping and tdm sync times get too long. ");
            AddDesc(TSettingID.NETID ,"NETID", "Net ID", "NETID is a 16 bit 'network ID'. This is used to seed the frequency hopping sequence and to identify packets as coming from the right radio. Make sure you use a different NETID from anyone else running the same sort of radio in the area. ");
            AddDesc(TSettingID.TXPOWER, "TXPOWER", "Tx Power", "TXPOWER is the transmit power in dBm. 20dBm is 100mW. It is useful to set this to lower levels for short range testing.");
            AddDesc(TSettingID.ECC ,"ECC", "ECC", "ECC is to enable/disable the golay error correcting code. It defaults to off. If you enable it then you packets take twice as many bytes to send, so you lose half your air bandwidth, but it can correct up to 3 bit errors per 12 bits of data. Use this for long range, usually in combination with a lower air data rate. The golay decode takes 20 microsecond per transmitted byte (40 microseconds per user data byte) which means you will also be a bit CPU constrained at the highest air data rates. So you usually use golay at 128kbps or less. ");
            AddDesc(TSettingID.MAVLINK, "MAVLINK", "Mavlink", "");
            AddDesc(TSettingID.OPPRESEND, "OPPRESEND", "Op Resend", "OPPRESEND enables/disables \"opportunistic resend\". When enabled the radio will send a packet twice if the serial input buffer has less than 256 bytes in it. The 2nd send is marked as a resend and discarded by the receiving radio if it got the first packet OK. This makes a big difference to the link quality, especially for uplink commands. ");
            AddDesc(TSettingID.MIN_FREQ, "MIN_FREQ", "Min Freq", "minimum frequency in kHz ");
            AddDesc(TSettingID.MAX_FREQ, "MAX_FREQ", "Max Freq", "maximum frequency in kHz ");
            AddDesc(TSettingID.NUM_CHANNELS, "NUM_CHANNELS", "# of channels", "number of frequency hopping channels ");
            AddDesc(TSettingID.DUTY_CYCLE, "DUTY_CYCLE", "Duty Cycle", "the percentage of time to allow transmit");
            AddDesc(TSettingID.LBT_RSSI, "LBT_RSSI", "LBT Rssi", "Listen Before Talk threshold");
            AddDesc(TSettingID.RTSCTS, "RTSCTS", "RTS CTS", "");
            AddDesc(TSettingID.MAX_WINDOW, "MAX_WINDOW", "Max Windows (ms)", "");
            AddDesc(TSettingID.ENCRYPTION_LEVEL, "ENCRYPTION_LEVEL", "AES Encryption", "");
            AddDesc(TSettingID.GPI1_1R_CIN, "GPI1_1R/CIN", "GPI1_1R/CIN", "GPI1_1R/CIN Sets GPIO 1.1 as R/C(PPM) input");
            AddDesc(TSettingID.GPO1_1R_COUT, "GPO1_1R/COUT", "GPO1_1R/COUT", "GPI1_1R/COUT Sets GPIO 1.1 as R/C(PPM) output");
            AddDesc(TSettingID.GPO1_3SBUSIN, "GPO1_3SBUSIN", "GPO1_3SBUSIN", "");
            AddDesc(TSettingID.GPO1_3SBUSOUT, "GPO1_3SBUSOUT", "GPO1_3SBUSOUT", "");
            AddDesc(TSettingID.ANT_MODE, "ANT_MODE", "Antenna Mode", "");
            AddDesc(TSettingID.RATE_FREQBAND, "RATE/FREQBAND", "Rate/Freq Band", "");
        }

        void AddDesc(TSettingID ID, string ATName, string Name, string Description)
        {
            _Table[ID] = new TSettingDescWithID(ID, ATName, Name, Description);
        }

        public TSettingDescription GetDescription(string ATName)
        {

            foreach (var Value in _Table.Values)
            {
                if (Value.ATName == ATName)
                {
                    return Value;
                }
            }
            return null;
        }

        public TSettingDescription GetDescription(TSettingID ID)
        {
            if (_Table.ContainsKey(ID))
            {
                return _Table[ID];
            }
            else
            {
                return null;
            }
        }
    }

    public abstract class TMultiChoiceSettingDescriptor : RFDLib.Config.TMultiChoiceSettingDescriptor
    {
        public readonly TATDescriptor ATDescriptor;

        public TMultiChoiceSettingDescriptor(string Name, string Descriptor, TATDescriptor ATDescriptor)
            : base(Name, Descriptor)
        {
            this.ATDescriptor = ATDescriptor;
        }
    }

    public class TMultiSettingDescriptor : TMultiChoiceSettingDescriptor
    {
        int _RFD900Setting;
        List<TOption> _Options = new List<TOption>();

        public TMultiSettingDescriptor(string Name, string Descriptor, RFD.RFD900.TSetting Setting)
            : base(Name, Descriptor, new TATDescriptor(Setting.Name, Setting.Designator))
        {
            _RFD900Setting = Setting.Value;
            if (Setting.Options == null)
            {
                for (int n = Setting.Range.Min; n <= Setting.Range.Max; n++)
                {
                    _Options.Add(new TIntOption(n.ToString(), n));
                }
            }
            else
            {
                foreach (var O in Setting.Options)
                {
                    _Options.Add(new TIntOption(O.OptionName, O.Value));
                }
            }
        }

        public override ISetting CreateNewSetting()
        {
            return new TMultiSetting(this, _RFD900Setting);
        }

        public override IEnumerable<TOption> GetOptions()
        {
            return _Options;
        }

        public class TIntOption : TMultiSettingDescriptor.TOption
        {
            string _Name;
            public readonly int Value;

            public TIntOption(string Name, int Value)
            {
                _Name = Name;
                this.Value = Value;
            }

            public override string Name
            {
                get
                {
                    return _Name;
                }
            }

            public override bool IsSameAs(TOption Other)
            {
                return Value == ((TIntOption)Other).Value;
            }
        }
    }

    public class TMultiSetting : TSetting<int>
    {
        public TMultiSetting(TMultiSettingDescriptor Descriptor, int Default)
            : base(Descriptor, Default)
        {

        }
    }

    public abstract class TBoolSettingDescriptor : RFDLib.Config.TBoolSettingDescriptor
    {
        public readonly TATDescriptor ATDescriptor;

        public TBoolSettingDescriptor(string Name, string Descriptor, TATDescriptor ATDescriptor)
            : base(Name, Descriptor)
        {
            this.ATDescriptor = ATDescriptor;
        }
    }

    public class TBoolSettingFromSettingDescriptor : TBoolSettingDescriptor
    {
        RFD.RFD900.TSetting _RFD900Setting;

        public TBoolSettingFromSettingDescriptor(string Name, string Descriptor, RFD.RFD900.TSetting Setting)
            : base(Name, Descriptor, new TATDescriptor(Setting.Name, Setting.Designator))
        {
            _RFD900Setting = Setting;
        }

        public override ISetting CreateNewSetting()
        {
            return new TBoolSetting(this, _RFD900Setting.Value != 0);
        }
    }

    public class TBoolSetting : TSetting<bool>
    {
        public TBoolSetting(TBoolSettingFromSettingDescriptor Descriptor, bool Default)
            : base(Descriptor, Default)
        {
        }
    }

    public abstract class TIntegerSettingDescriptor : RFDLib.Config.TIntegerSettingDescriptor
    {
        public readonly TATDescriptor ATDescriptor;

        public TIntegerSettingDescriptor(string Name, string Descriptor, TATDescriptor ATDescriptor)
            : base(Name, Descriptor)
        {
            this.ATDescriptor = ATDescriptor;
        }
    }

    public class TIntegerSettingFromSettingDescriptor : TIntegerSettingDescriptor
    {
        RFD.RFD900.TSetting _RFD900Setting;

        public TIntegerSettingFromSettingDescriptor(string Name, string Descriptor, RFD.RFD900.TSetting Setting)
            : base(Name, Descriptor, new TATDescriptor(Setting.Name, Setting.Designator))
        {
            _RFD900Setting = Setting;
        }

        public override ISetting CreateNewSetting()
        {
            return new TIntegerSetting(this, _RFD900Setting.Value);
        }
    }

    public class TIntegerSetting : TSetting<int>
    {
        public TIntegerSetting(TIntegerSettingFromSettingDescriptor Descriptor, int Default)
            : base(Descriptor, Default)
        {
        }
    }

    /*public abstract class TParamDescriptor : RFDLib.Config.TSettingDescriptor
    {

        public TParamDescriptor(string Name, string Description, string ATName, string ATNumber)
            : base(Name, Description)
        {
            this.ATName = ATName;
            this.ATNumber = ATNumber;
        }

        
    }

    public interface IParam
    {
        
    }

    public abstract class TParam<T> : RFDLib.Config.TSetting<T>, IParam
    {
        public TParam(TParamDescriptor Descriptor, T Default)
            : base(Descriptor, Default)
        {
        }
    }*/
}