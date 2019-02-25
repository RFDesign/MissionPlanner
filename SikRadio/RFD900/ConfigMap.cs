using System;
using System.Collections.Generic;
using RFDLib.Config;

namespace RFD.RFD900.Config.Map
{
    abstract class TModemSettingMapItem
    {
        public readonly TSettingID ID;

        public TModemSettingMapItem(TSettingID ID)
        {
            this.ID = ID;
        }

        public abstract RFDLib.Config.TSettingDescriptor GetSettingDescriptor(RFD.RFD900.TSetting Setting);
    }

    class TBoolSettingMapItem : TModemSettingMapItem
    {
        public TBoolSettingMapItem(TSettingID ID)
            : base(ID)
        {
        }

        public override TSettingDescriptor GetSettingDescriptor(RFD.RFD900.TSetting Setting)
        {
            var Desc = Config.TDesignatorLookupTable.TABLE.GetDescription(ID);

            return new Config.TBoolSettingFromSettingDescriptor(Desc.Name, Desc.Description, Setting);
        }
    }

    class TMultiSettingMapItem : TModemSettingMapItem
    {
        List<RFD.RFD900.TSetting.TOption> _Options = new List<TSetting.TOption>();

        public TMultiSettingMapItem(TSettingID ID)
            : base(ID)
        {
        }

        public TMultiSettingMapItem(TSettingID ID, int[] Values)
            : base(ID)
        {
            foreach (int V in Values)
            {
                _Options.Add(new TSetting.TOption(V, V.ToString()));
            }
        }

        public TMultiSettingMapItem(TSettingID ID, int Min, int Max)
            : base(ID)
        {
            for (int V = Min; V <= Max; V++)
            {
                _Options.Add(new TSetting.TOption(V, V.ToString()));
            }
        }

        public TMultiSettingMapItem(TSettingID ID, int Min, int Max, int Step)
            : base(ID)
        {
            for (int V = Min; V <= Max; V+=Step)
            {
                _Options.Add(new TSetting.TOption(V, V.ToString()));
            }
        }
        protected void AddOption(int Value)
        {
            _Options.Add(new TSetting.TOption(Value, Value.ToString()));
        }

        public override TSettingDescriptor GetSettingDescriptor(TSetting Setting)
        {
            var Desc = Config.TDesignatorLookupTable.TABLE.GetDescription(ID);
            Setting.Options = _Options.ToArray();

            return new Config.TMultiSettingDescriptor(Desc.Name, Desc.Description, Setting);
        }
    }

    class TModemSettingMap
    {
        Dictionary<TSettingID, TModemSettingMapItem> _Map;
        static TModemSettingMap RFD900 = new TRFD900Map();
        static TModemSettingMap RFD900X = new TRFD900XMap();
        static TModemSettingMap RFD900XAsync = new TRFD900XAsyncMap();

        protected void AddBoolItem(TSettingID ID)
        {
            _Map[ID] = new TBoolSettingMapItem(ID);
        }

        protected void AddMultiItem(TSettingID ID, int[] Options)
        {
            _Map[ID] = new TMultiSettingMapItem(ID, Options);
        }

        protected void AddMultiItem(TSettingID ID, int Min, int Max)
        {
            _Map[ID] = new TMultiSettingMapItem(ID, Min, Max);
        }

        protected void AddMultiItem(TSettingID ID, int Min, int Max, int Step)
        {
            _Map[ID] = new TMultiSettingMapItem(ID, Min, Max, Step);
        }

       static public TModemSettingMap GetMapForModem(uploader.Uploader.Board Board, uploader.Uploader.Frequency Freq, bool Async)
        {
            TModemSettingMap Result;

            switch (Board)
            {
                default:
                case uploader.Uploader.Board.DEVICE_ID_HM_TRP:
                case uploader.Uploader.Board.DEVICE_ID_RF50:
                case uploader.Uploader.Board.DEVICE_ID_RFD900:
                case uploader.Uploader.Board.DEVICE_ID_RFD900A:
                case uploader.Uploader.Board.DEVICE_ID_RFD900P:
                    Result = RFD900;
                    break;
                case uploader.Uploader.Board.DEVICE_ID_RFD900X:
                    if (Async)
                    {
                        Result = RFD900XAsync;
                    }
                    else
                    {
                        Result = RFD900X;
                    }
                    break;
                case uploader.Uploader.Board.DEVICE_ID_RFD900U:
                    Result = RFD900;
                    ((TRFD900Map)Result).Set20dBmMax();
                    break;
                case uploader.Uploader.Board.DEVICE_ID_RFD900UX:
                    if (Async)
                    {
                        Result = RFD900XAsync;
                    }
                    else
                    {
                        Result = RFD900X;
                    }
                    ((TRFD900Map)Result).Set20dBmMax();
                    break;
            }

            ((TRFD900Map)Result).SetFrequency(Freq);

            return Result;
        }

        public RFDLib.Config.ISetting GenerateSetting(TSettingID Id, TSetting Setting)
        {
            return _Map[Id].GetSettingDescriptor(Setting).CreateNewSetting();
        }
    }

    class TRFD900Map : TModemSettingMap
    {
        public TRFD900Map()
        {
            AddBoolItem(TSettingID.ECC);
            AddBoolItem(TSettingID.GPI1_1R_CIN);
            AddBoolItem(TSettingID.GPO1_1R_COUT);
            AddBoolItem(TSettingID.GPO1_3SBUSIN);
            AddBoolItem(TSettingID.GPO1_3SBUSOUT);
            AddBoolItem(TSettingID.OPPRESEND);
            AddBoolItem(TSettingID.RTSCTS);
            AddMultiItem(TSettingID.SERIAL_SPEED, new int[] { 115, 111, 57, 38, 19, 9, 4, 2, 1 });
            AddMultiItem(TSettingID.AIR_SPEED, new int[] { 250, 192, 128, 96, 64, 48, 32, 24, 19, 16, 8, 4, 2 });
            AddMultiItem(TSettingID.NETID, 0, 500);
            AddMultiItem(TSettingID.TXPOWER, 0, 20);
            AddMultiItem(TSettingID.MIN_FREQ, new int[] { 902000, 907500, 915000, 921000, 928000, 433050, 434040, 434790, 435000 });
            AddMultiItem(TSettingID.MAX_FREQ, new int[] { 902000, 907500, 915000, 921000, 928000, 433050, 434040, 434790, 435000 });
            AddMultiItem(TSettingID.NUM_CHANNELS, new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 30, 40, 50 });
            AddMultiItem(TSettingID.DUTY_CYCLE, 10, 100, 10);
            AddBoolItem(TSettingID.LBT_RSSI);
            AddMultiItem(TSettingID.MAX_WINDOW, 33, 33+99);
        }

        public void SetFrequency(uploader.Uploader.Frequency Freq)
        {
            switch (Freq)
            {
                case uploader.Uploader.Frequency.FREQ_915:
                    AddMultiItem(TSettingID.MIN_FREQ, 895000, 935000, 1000);
                    AddMultiItem(TSettingID.MAX_FREQ, 895000, 935000, 1000);
                    break;
                case uploader.Uploader.Frequency.FREQ_433:
                    AddMultiItem(TSettingID.MIN_FREQ, 414000, 460000, 50);
                    AddMultiItem(TSettingID.MAX_FREQ, 414000, 460000, 50);
                    break;
                case uploader.Uploader.Frequency.FREQ_868:
                    AddMultiItem(TSettingID.MIN_FREQ, 849000, 889000, 1000);
                    AddMultiItem(TSettingID.MAX_FREQ, 849000, 889000, 1000);
                    break;
            }
        }

        public void Set20dBmMax()
        {
            AddMultiItem(TSettingID.TXPOWER, 0, 20);
        }
    }

    class TRFD900XMap : TRFD900Map
    {
        public TRFD900XMap()
        {
            AddMultiItem(TSettingID.SERIAL_SPEED, new int[] { 1, 2, 4, 9, 19, 38, 57, 115, 230, 460 });
            AddMultiItem(TSettingID.AIR_SPEED, new int[] { 4, 64, 125, 250, 500 });
            AddMultiItem(TSettingID.NETID, 0, 65535);
            AddMultiItem(TSettingID.MIN_FREQ, 902000, 927000, 1000);
            AddMultiItem(TSettingID.MAX_FREQ, 902000, 927000, 1000);
            AddMultiItem(TSettingID.NUM_CHANNELS, 1, 50);
            AddMultiItem(TSettingID.MAX_WINDOW, 20, 400);
            AddMultiItem(TSettingID.LBT_RSSI, 0, 220, 25);
        }
    }

    class TRFD900XAsyncMap : TRFD900XMap
    {
        public TRFD900XAsyncMap()
        {
            AddMultiItem(TSettingID.NETID, 0, 255);
        }
    }
}