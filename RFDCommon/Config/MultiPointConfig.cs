using RFD.RFD900;
using RFDCommon.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDCommon.Config
{
    public class MultiPointConfig : IModemConfig
    {
        public void LoadSettings(TSession session, bool remote)
        {
            throw new NotImplementedException();
        }

        public void SaveSettings(TSession session, bool remote)
        {
            throw new NotImplementedException();
        }
    }
}
