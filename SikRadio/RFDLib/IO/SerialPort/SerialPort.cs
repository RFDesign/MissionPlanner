using System;

namespace RFDLib.IO
{
    public abstract class TSerialPort
    {
        public abstract void Write(string Text);
        public abstract string ReadExisting();
        public abstract int ReadTimeout { get; set; }
        public abstract int ReadChar();
        public abstract string ReadLine();
        public abstract void DiscardInBuffer();

        /// <summary>
        /// Query a serial port.
        /// </summary>
        /// <param name="Port">The port.  Must not be null.</param>
        /// <param name="Query">The query to send, without the terminator.  Must not be null.</param>
        /// <param name="Terminator">The terminator.  Can be null if none.</param>
        /// <param name="MaxWait">The maximum period to wait in milliseconds.</param>
        /// <returns>The data returned.  Never null.</returns>
        public static string Query(TSerialPort Port,
            string Query, string Terminator, int MaxWait)
        {
            Port.DiscardInBuffer();
            Query = Query + (Terminator == null ? "" : Terminator);
            Port.Write(Query);
            string Reply;
            WaitForToken(Port, Terminator, MaxWait, out Reply);
            return Reply;
        }

        /// <summary>
        /// Wait for the given token or timeout on the given serial port.
        /// </summary>
        /// <param name="Port">The port.  Must not be null.</param>
        /// <param name="Token">The token to wait for.  Can be null if not waiting for a token.</param>
        /// <param name="MaxWait">The maximum amount of time to wait for a response, in milliseconds.</param>
        /// <returns>true if token was received, false if not.</returns>
        public static bool WaitForToken(TSerialPort Port,
            string Token, int MaxWait)
        {
            string Temp;
            return WaitForToken(Port, Token, MaxWait, out Temp);
        }

        /// <summary>
        /// Wait for the given token or timeout on the given serial port
        /// </summary>
        /// <param name="Port">The port.  Must not be null.</param>
        /// <param name="Token">The token to wait for.  Can be null if not waiting for a token, but waiting for timeout instead</param>
        /// <param name="MaxWait">The maximum amount of time to wait for a response, in milliseconds.</param>
        /// <param name="Result">The data returned.  Never null.</param>
        /// <returns>true if token was received, false if not.</returns>
        public static bool WaitForToken(TSerialPort Port,
            string Token, int MaxWait, out string Result)
        {
            System.Diagnostics.Stopwatch SW = new System.Diagnostics.Stopwatch();
            string Temp = "";
            int Timeout = Port.ReadTimeout;
            Port.ReadTimeout = MaxWait;
            SW.Start();

            while (SW.ElapsedMilliseconds < MaxWait)
            {
                //System.Diagnostics.Stopwatch StWa = new System.Diagnostics.Stopwatch();
                //SW.Start();
                string x = Port.ReadExisting();
                //if (StWa.ElapsedMilliseconds > 10)
                //{
                //    System.Diagnostics.Debug.WriteLine("Read existing slow\n");
                //}
                Temp += x;

                if ((Token != null) && Temp.Contains(Token))
                {
                    Port.ReadTimeout = Timeout;
                    Result = Temp;
                    return true;
                }

                System.Threading.Thread.Sleep(2);
            }
            Port.ReadTimeout = Timeout;
            Result = Temp;
            return false;
        }

        /// <summary>
        /// Wait for any of these tokens to be received on the port.  
        /// Wait up to MaxWait milliseconds.  If a token was received, return the array index of
        /// the token.  If no tokens received, return -1.
        /// </summary>
        /// <param name="Tokens">The tokens to wait for.  Must not be null.</param>
        /// <param name="MaxWait">The max wait time in milliseconds.</param>
        /// <returns>The token array index of the token received, or -1 is none of the tokens received.</returns>
        public static int WaitForAnyOfTheseTokens(TSerialPort Port, string[] Tokens, int MaxWait, out string RxData)
        {
            System.Diagnostics.Stopwatch SW = new System.Diagnostics.Stopwatch();
            RxData = "";
            int Timeout = Port.ReadTimeout;
            Port.ReadTimeout = MaxWait;
            SW.Start();

            while (SW.ElapsedMilliseconds < MaxWait)
            {
                try
                {
                    int Byte;
                    while (((Byte = Port.ReadChar()) != -1) && (SW.ElapsedMilliseconds < MaxWait))
                    {
                        //string x = _Port.ReadExisting();
                        RxData += ((char)Byte);

                        for (int n = 0; n < Tokens.Length; n++)
                        {
                            if (RxData.Contains(Tokens[n]))
                            {
                                Port.ReadTimeout = Timeout;
                                return n;
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(50);
                }
                catch (Exception e)
                {
                    //System.Diagnostics.Debug.WriteLine(e.Message);
                    //System.Diagnostics.Debug.WriteLine("No tokens, got " + Temp);

                    return -1;
                }
            }

            //Console.WriteLine("Failed to get token, got this instead:  " + Temp);

            Port.ReadTimeout = Timeout;
            return -1;
        }

    }

    public class TSystemIOPortsSerialPort : TSerialPort
    {
        System.IO.Ports.SerialPort _Port;

        public TSystemIOPortsSerialPort(System.IO.Ports.SerialPort Port)
        {
            _Port = Port;
        }

        public override void DiscardInBuffer()
        {
            _Port.DiscardInBuffer();
        }

        public override int ReadChar()
        {
            return _Port.ReadChar();
        }

        public override void Write(string Text)
        {
            _Port.Write(Text);
        }

        public override string ReadLine()
        {
            return _Port.ReadLine();
        }

        public override string ReadExisting()
        {
            return _Port.ReadExisting();
        }

        public override int ReadTimeout
        {
            get
            {
                return _Port.ReadTimeout;
            }
            set
            {
                _Port.ReadTimeout = value;
            }
        }
    }

}