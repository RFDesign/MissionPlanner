using MissionPlanner.Comms;
using RFD.RFD900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFDCommon.Interface
{
    public interface IModemComms
    {
        string DoCommand(string cmd, bool multiLineResponce = false, int level = 0);
        string DoQueryWithRetry(string query, bool waitForTerminator = false);
        TSession GetSession();
        void EndSession();
        TSession.TMode PutIntoATCommandMode();
        TSession.TMode PutIntoTransparentMode();
        void DiscardInBuffer();
        void Reconnect();
        bool Connect();
        bool Disconnect();
        bool IsConnected();

        event EventHandler<EventArgs> ConnectionStateChanged;              
    }
}
