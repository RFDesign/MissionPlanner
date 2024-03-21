using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RFD.RFD900
{
    public class TSettings
    {        
        Dictionary<string, TBaseSetting> _Settings = new Dictionary<string, TBaseSetting>();
        public Dictionary<string, TBaseSetting> WorkingSettings = new Dictionary<string, TBaseSetting>();

        const string MIN_FREQ = "MIN_FREQ";
        const string MAX_FREQ = "MAX_FREQ";

        public TSettings(Dictionary<string, TBaseSetting> Settings)
        {
            _Settings = Settings;
            foreach (var kvp in _Settings)
            {
                WorkingSettings[kvp.Key] = (TBaseSetting)kvp.Value.Clone();
            }
        }

        public Dictionary<string, TBaseSetting> Settings
        {
            get
            {
                return _Settings;
            }
        }     

        public TSettings Clone()
        {
            Dictionary<string, TBaseSetting> Temp = new Dictionary<string, TBaseSetting>();

            foreach (var kvp in _Settings)
            {
                Temp[kvp.Key] = (TBaseSetting)kvp.Value.Clone();
            }

            return new TSettings(Temp);
        }

        public bool SaveToFile(string Path)
        {
            // NEED TO ENSURE THIS IS THE WORKING CONFIG?
            try
            {
                using (StreamWriter Writer = File.CreateText(Path))
                {
                    //Save in alphabetical order...
                    List<string> Names = new List<string>(WorkingSettings.Keys);
                    Names.Sort();

                    foreach (var N in Names)
                    {
                        Writer.WriteLine(N + " = " + WorkingSettings[N].GetValueAsString());
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        TNameAndValue ParseINILine(string Line)
        {
            if (!Line.Trim().StartsWith(";") && !Line.Trim().StartsWith("#"))
            {
                string[] ContentAndComment = Line.Split(';', '#');

                if (ContentAndComment.Length >= 1)
                {
                    string Content = ContentAndComment[0];

                    if (Content.Contains("="))
                    {
                        string[] NameAndValue = Content.Split('=');

                        if (NameAndValue.Length == 2)
                        {
                            string Name = NameAndValue[0].Trim();

                            return new TNameAndValue(Name, NameAndValue[1].Trim());
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Update these settings from a file.
        /// </summary>
        /// <param name="Path">The path of the settings file.  Must not be null.</param>
        /// <returns>The names and values loaded.  Null if failed.</returns>
        public Dictionary<string, TNameAndValue> LoadFromFile(string Path)
        {
            Dictionary<string, TNameAndValue> Result = new Dictionary<string, TNameAndValue>();

            try
            {
                using (StreamReader Reader = File.OpenText(Path))
                {
                    string Line;

                    while ((Line = Reader.ReadLine()) != null)
                    {
                        var NV = ParseINILine(Line);
                        if (NV != null && WorkingSettings.ContainsKey(NV.Name))
                        {
                            WorkingSettings[NV.Name].SetValueFromString(NV.Value);
                            Result[NV.Name] = NV;
                        }
                    }
                }

                return Result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Check if this set of settings is valid.  Return a list of error descriptions.
        /// If returned array is zero-length, settings are valid.  
        /// </summary>
        /// <returns></returns>
        public string[] CheckValid()
        {
            List<string> Result = new List<string>();

            if (WorkingSettings.ContainsKey(MIN_FREQ) && WorkingSettings.ContainsKey(MAX_FREQ))
            {
                var Min = WorkingSettings[MIN_FREQ];
                var Max = WorkingSettings[MAX_FREQ];

                if (Min is TShortSetting && Max is TShortSetting)
                {
                    if (((TShortSetting)Min).Value > ((TShortSetting)Max).Value)
                    {
                        Result.Add("MIN_FREQ can't be more than MAX_FREQ");
                    }
                }
            }
            if (WorkingSettings.ContainsKey("AESKEY"))
            {
                // Only validate AESKEY if encryption is enabled
                var encLevel = WorkingSettings["ENCRYPTION_LEVEL"];
                if (encLevel.GetValueAsString() != "0")
                {
                    var aesKey = WorkingSettings["AESKEY"];
                    if (!Regex.IsMatch(aesKey.GetValueAsString(), @"\A\b[0-9a-fA-F]+\b\Z"))
                    {
                        Result.Add("Encryption key not valid hex number");
                    }
                }                           
            }
            return Result.ToArray();
        }

        public class TNameAndValue
        {
            public readonly string Name;
            public readonly string Value;

            public TNameAndValue(string Name, string Value)
            {
                this.Name = Name;
                this.Value = Value;
            }
        }
    }

    public abstract class TBaseSetting : ICloneable
    {
        public string Designator;
        public string Name;

        public abstract string GetValueAsString();
        public abstract void SetValueFromString(string Text);
        public abstract object Clone();
    }

    public class TTextSetting : TBaseSetting
    {
        public string Text;

        public override string GetValueAsString()
        {
            return Text;
        }

        public override void SetValueFromString(string Text)
        {
            this.Text = Text;
        }

        public override object Clone()
        {
            TTextSetting Result = new TTextSetting();
            Result.Designator = Designator;
            Result.Name = Name;
            Result.Text = Text;

            return Result;
        }
    }


    public class TShortSetting : TBaseSetting
    {
        public int Value;

        public TShortSetting(string Designator, string Name, int Value)
        {
            this.Designator = Designator;
            this.Name = Name;
            this.Value = Value;
        }

        public override object Clone()
        {
            return new TShortSetting(Designator, Name, Value);
        }

        public override string GetValueAsString()
        {
            return Value.ToString();
        }

        public override void SetValueFromString(string Text)
        {
            int.TryParse(Text, out this.Value);
        }
    }

    public class TSetting : TShortSetting
    {
        /// <summary>
        /// null if range unknown
        /// </summary>
        public TRange Range;
        /// <summary>
        /// null if options unknown
        /// </summary>
        public TOption[] Options;
        public int Increment;

        public TSetting(string Designator, string Name, TRange Range, int Value, TOption[] Options,
            int Increment)
            : base(Designator, Name, Value)
        {
            this.Designator = Designator;
            this.Name = Name;
            this.Range = Range;
            this.Value = Value;
            this.Options = Options;
            this.Increment = Increment;
        }

        public override object Clone()
        {
            return new TSetting(Designator, Name, Range, Value, Options, Increment);
        }

        public string[] GetOptionNames()
        {
            if (Options == null)
            {
                return null;
            }
            else
            {
                return RFDLib.Array.CherryPickArray(Options, (x) => x.OptionName);
            }
        }

        public string GetOptionNameForValue(string Value)
        {
            if (Options != null)
            {
                foreach (var O in Options)
                {
                    if (O.Value.ToString() == Value)
                    {
                        return O.OptionName;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns whether this setting is a flag/boolean.
        /// </summary>
        /// <returns></returns>
        public bool GetIsFlag()
        {
            return (Range != null) && Range.GetOptions().Length == 2 &&
                Range.GetOptions()[0] == 0 && Range.GetOptions()[1] == 1;
        }

        public abstract class TRange
        {
            public abstract int[] GetOptions();

            /// <summary>
            /// Get the list of options and include the option with the given value if it doesnt exist.
            /// </summary>
            /// <param name="Value">The value to include.</param>
            /// <returns>The options.  Never null.</returns>
            public int[] GetOptionsIncludingValue(int Value)
            {
                List<int> Result = new List<int>();
                bool GotValue = false;

                foreach (var n in GetOptions())
                {
                    if (n == Value)
                    {
                        GotValue = true;
                    }
                    if (!GotValue)
                    {
                        if (n > Value)
                        {
                            Result.Add(Value);
                            GotValue = true;
                        }
                    }

                    Result.Add(n);
                }

                if (!GotValue)
                {
                    Result.Add(Value);
                }

                return Result.ToArray();
            }
        }

        public class TSimpleRange : TRange
        {
            public readonly int Min;
            public readonly int Max;
            public readonly int Increment;

            public TSimpleRange(int Min, int Max, int Increment)
            {
                this.Min = Min;
                this.Max = Max;
                this.Increment = Increment;
            }

            public override int[] GetOptions()
            {
                int[] list;
                int index = 0;
                bool GotEnd = false;

                int Min = this.Min;
                int Max = this.Max;

                //Prevent exception for the case of the modem firmware erroneously specifiying a range in which max is less than min.
                if (Max < Min)
                {
                    Max = Min;
                }

                if (Min == Max)
                {
                    list = new int[1];
                }
                else
                {
                    list = new int[((Max - Min - 1) / Increment) + 2];
                }

                for (var a = Min; a <= Max; a += Increment)
                {
                    if (a == Max)
                    {
                        GotEnd = true;
                    }
                    list[index++] = a;
                }

                if (!GotEnd)
                {
                    list[index++] = Max;
                }

                return list;
            }
        }

        public class TMultiRange : TRange
        {
            TSimpleRange[] _SimpleRanges;

            public TMultiRange(TSimpleRange[] SimpleRanges)
            {
                _SimpleRanges = SimpleRanges;
            }

            public override int[] GetOptions()
            {
                int TotalLength = 0;
                int SRIndex;

                for (SRIndex = 0; SRIndex < _SimpleRanges.Length; SRIndex++)
                {
                    TotalLength += _SimpleRanges[SRIndex].GetOptions().Length;
                }

                int[] Result = new int[TotalLength];
                int ResultIndex = 0;

                for (SRIndex = 0; SRIndex < _SimpleRanges.Length; SRIndex++)
                {
                    int[] SubResult = _SimpleRanges[SRIndex].GetOptions();
                    Array.Copy(SubResult, 0, Result, ResultIndex, SubResult.Length);
                    ResultIndex += SubResult.Length;
                }

                return Result;
            }
        }

        public class TOption
        {
            public readonly int Value;
            public readonly string OptionName;

            public TOption(int Value, string OptionName)
            {
                this.Value = Value;
                this.OptionName = OptionName;
            }
        }
    }    
}
