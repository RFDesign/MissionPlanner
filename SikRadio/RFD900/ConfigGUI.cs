using System;

namespace RFD.RFD900.Config.GUI
{
    public class TGUIController
    {
        TSession _Session;
        RFDLib.GUI.ConfigArray _ConfigArray;

        public void LoadSettings()
        {
            if (_Session.PutIntoATCommandMode() == TSession.TMode.AT_COMMAND)
            {
                string ATI5 = _Session.ATCClient.DoQuery("ATI5?", true);
                var Settings = _Session.GetSettings(ATI5, _Session.Board);

                string ATI3 = _Session.ATCClient.DoQuery("ATI3", true);

                foreach (var kvp in Settings)
                {
                    //RFD.RFD900.Config.TATDescriptor.GenerateSettingFromRFD900Setting(kvp.Value, _Session.Board, _Session.fr)
                }

                //RFD.RFD900.Config.TATDescriptor.GenerateSettingFromRFD900Setting()
            }
        }
    }
}
