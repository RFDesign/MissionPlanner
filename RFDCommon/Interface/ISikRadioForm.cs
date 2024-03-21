using MissionPlanner.Comms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDCommon.Interface
{
    public interface ISikRadioForm : IDisposable
    {
        void Connect(IModemComms modemComms);
        void Disconnect();
        void Show();
        bool Enabled { get; set; }
        string Header { get; }
    }

    public interface IRFDConfigForm
    {        
        void Start(IModemComms modemComms);
        void Stop();
    }
}
