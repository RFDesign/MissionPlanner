using MissionPlanner;
using MissionPlanner.Comms;
using RFD.RFD900;
using RFDCommon.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFDCommon
{
    public class ModemComms : IModemComms
    {
        public event EventHandler<ConsoleEventArgs> WriteConsole;
        protected virtual void WriteToConsole(string text)
        {
            WriteConsole?.Invoke(this, new ConsoleEventArgs(text));
        }

        private ICommsSerial _comPort;

        private bool _connected = false;
        //private int _baud;

        private TSession session;

        public ModemComms()
        {
            
        }



        /// <summary>
        /// Send a command to the radio, wait for a response.
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="cmd">The command</param>
        /// <param name="multiLineResponce"></param>
        /// <param name="level"></param>
        /// <returns>The response</returns>
        public string DoCommand(string cmd, bool multiLineResponce = false, int level = 0)
        {
            var comPort = GetSession().Port;
            if (!comPort.IsOpen)
                return "";

            comPort.DiscardInBuffer();

            WriteToConsole("Doing Command: " + cmd);
            //log.Info("Doing Command " + cmd);
            comPort.ReadTimeout = 1000;

            comPort.Write("\r\n");
            Serial_ReadLine(comPort);
            Thread.Sleep(50);
            comPort.Write(cmd + "\r\n");

            comPort.ReadTimeout = 1000;

            // command echo
            var cmdecho = Serial_ReadLine(comPort);

            if (cmdecho.Contains(cmd))
            {
                var value = "";

                if (multiLineResponce)
                {
                    var deadline = DateTime.Now.AddMilliseconds(1000);
                    while (comPort.BytesToRead > 0 || DateTime.Now < deadline)
                    {
                        try
                        {
                            value = value + Serial_ReadLine(comPort);
                        }
                        catch
                        {
                            value = value + comPort.ReadExisting();
                        }
                    }
                }
                else
                {
                    value = Serial_ReadLine(comPort);

                    if (value == "" && level == 0)
                    {
                        return DoCommand(cmd, multiLineResponce, 1);
                    }
                }

                //log.Info(value.Replace('\0', ' '));

                return value;
            }

            comPort.DiscardInBuffer();

            // try again
            if (level == 0)
                return DoCommand(cmd, multiLineResponce, 1);

            return "";
        }

        private string Serial_ReadLine(ICommsSerial comPort)
        {
            var sb = new StringBuilder();
            var Deadline = DateTime.Now.AddMilliseconds(comPort.ReadTimeout);

            while (DateTime.Now < Deadline)
            {
                if (comPort.BytesToRead > 0)
                {
                    var data = (byte)comPort.ReadByte();
                    sb.Append((char)data);
                    if (data == '\n')
                        break;
                }
            }

            return sb.ToString();
        }


        public string DoQueryWithRetry(string query, bool waitForTerminator)
        {
            string Result = GetSession().ATCClient.DoQuery(query, waitForTerminator);
            if (Result == "")
            {
                return GetSession().ATCClient.DoQuery(query, waitForTerminator);
            }
            else
            {
                return Result;
            }
        }

        public TSession GetSession()
        {
            if (session == null)
            {
                try
                {
                    if (_comPort != null)
                    {
                        session = new RFD.RFD900.TSession(_comPort, MainV2.comPort.BaseStream.BaudRate);
                    }
                }
                catch
                {
                    //MsgBox.CustomMessageBox.Show("Invalid ComPort or in use");
                    return null;
                }
            }
            else if (session.Port.BaudRate != MainV2.comPort.BaseStream.BaudRate ||
                (MainV2.comPort.BaseStream.PortName != "TCP" && (session.Port.PortName != MainV2.comPort.BaseStream.PortName)))
            {
                session.Dispose();
                session = null;
                GetSession();
            }
            return session;
        }
        

        public TSession.TMode PutIntoATCommandMode()
        {
            return GetSession().PutIntoATCommandMode();
        }

        public TSession.TMode PutIntoTransparentMode()
        {
            return GetSession().PutIntoTransparentMode();
        }

        public void DiscardInBuffer()
        {
            GetSession().Port.DiscardInBuffer();
        }



        public void EndSession()
        {
            if (session != null)
            {
                session.Dispose();
                session = null;
            }
            Reconnect();
        }

        public void Reconnect()
        {
            Disconnect();
            Connect();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            return _connected;
        }
    }
}
