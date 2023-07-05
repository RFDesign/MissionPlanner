using System;
using System.Collections.Generic;

namespace MissionPlanner.MavLinkVideoService
{
    public class TTCPServer : IDisposable
    {
        System.Net.Sockets.TcpListener _Listener;
        bool _Run = true;
        List<System.Net.Sockets.TcpClient> _Clients = new List<System.Net.Sockets.TcpClient>();
        object _Locker = new object();

        public TTCPServer(UInt16 Port)
        {
            //_Listener = new System.Net.Sockets.TcpListener(Port);
            _Listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Parse("192.168.137.1"), Port);
            _Listener.Start();

            System.Threading.Thread Worker = new System.Threading.Thread(AcceptWorker);
            Worker.Start();
        }

        bool CheckAndAccept()
        {
            lock (_Locker)
            {
                if (_Listener.Pending())
                {
                    var Client = _Listener.AcceptTcpClient();
                    _Clients.RemoveAll((c) => !c.Connected);
                    _Clients.Add(Client);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        void AcceptWorker()
        {
            while (_Run)
            {
                if (!CheckAndAccept())
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        public void Output(byte[] Data, int Offset, int Length)
        {
            lock (_Locker)
            {
                foreach (var c in _Clients)
                {
                    c.GetStream().Write(Data, Offset, Length);
                }
            }
        }

        public void Dispose()
        {
            _Run = false;

            foreach (var c in _Clients)
            {
                try
                {
                    c.Close();
                }
                catch
                {

                }
            }

            _Listener.Stop();
        }

    }


}