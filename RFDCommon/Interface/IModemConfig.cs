using RFD.RFD900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDCommon.Interface
{
    public interface IModemConfig
    {
        void LoadSettings(TSession session, bool remote);
        void SaveSettings(TSession session, bool remote);
    }
}
