using System;


namespace RFDLib.Telemetry
{

    public abstract class TValid<T>
    {
        public abstract T Value { get;}
        public abstract bool IsValid { get; }
    }

    public abstract class TGeneralTimestamped<T> : TValid<T>
    {
        public event Action Updated;

        protected void RaiseUpdated()
        {
            if (Updated != null)
            {
                Updated();
            }
        }

        public abstract void Update(T Value);
        public abstract Int64 Timeout { get; }

        /// <summary>
        /// Age of sample in milliseconds, or -1 if never had sample.
        /// </summary>
        public abstract Int64 Age { get; }
    }


    public class TTimestamped<T> : TGeneralTimestamped<T>
    {
        System.Diagnostics.Stopwatch _SW;
        Int64 _Timeout;
        T _Value;

        public TTimestamped(Int64 Timeout)
        {
            _Timeout = Timeout;
        }

        public override void Update(T Value)
        {
            _Value = Value;
            _SW = new System.Diagnostics.Stopwatch();
            _SW.Start();

            RaiseUpdated();
        }

        public override bool IsValid
        {
            get
            {
                return _SW != null && _SW.ElapsedMilliseconds < _Timeout;
            }
        }

        public override T Value
        {
            get
            {
                return _Value;
            }
        }

        public override Int64 Timeout
        {
            get
            {
                return _Timeout;
            }
        }

        /// <summary>
        /// Age of sample in milliseconds, or -1 if never had sample.
        /// </summary>
        public override Int64 Age
        {
            get
            {
                var SW = _SW;
                if (SW == null)
                {
                    return -1;
                }
                else
                {
                    return SW.ElapsedMilliseconds;
                }
            }
        }
    }

    public class TWrappedTimestamped<T> : TGeneralTimestamped<T>
    {
        TGeneralTimestamped<T> _Wrapped;
        Func<TGeneralTimestamped<T>> _GetWrappedFn;

        public TWrappedTimestamped(Func<TGeneralTimestamped<T>> GetWrappedFn)
        {
            _GetWrappedFn = GetWrappedFn;
        }

        public TGeneralTimestamped<T> GetWrapped()
        {
            var x = _GetWrappedFn();
            if (x != _Wrapped)
            {
                _Wrapped = x;
                x.Updated += RaiseUpdated;
            }

            return _Wrapped;
        }

        public override long Age
        {
            get
            {
                return GetWrapped().Age;
            }
        }

        public override bool IsValid
        {
            get
            {
                return GetWrapped().IsValid;
            }
        }

        public override long Timeout
        {
            get
            {
                return _GetWrappedFn().Timeout;
            }
        }

        public override void Update(T Value)
        {
            GetWrapped().Update(Value);
        }

        public override T Value
        {
            get
            {
                return GetWrapped().Value;
            }
        }
    }

    public abstract class TReader<T> : IDisposable
    {
        TGeneralTimestamped<T> _Status;
        System.Threading.Thread _Worker;
        RFDLib.Threading.TReusableWakeableSleep _Sleep = new Threading.TReusableWakeableSleep();
        Int64 _FetchPeriod;
        public bool Enabled = false;
        public event Action TimedOut;

        public TReader(TGeneralTimestamped<T> Status)
        {
            _Status = Status;
            _FetchPeriod = Status.Timeout / 3;

            _Worker = new System.Threading.Thread(Worker);
            _Worker.Start();
        }

        public TGeneralTimestamped<T> Status
        {
            get
            {
                return _Status;
            }
        }

        void Worker()
        {
            while (_Sleep.Sleep((int)_FetchPeriod))
            {
                if (Enabled)
                {
                    Int64 Age = _Status.Age;

                    if (Age < 0 || Age >= _FetchPeriod)
                    {
                        Request();
                    }

                    if (TimedOut != null && !_Status.IsValid)
                    {
                        TimedOut();
                    }
                }
            }
        }

        protected abstract void Request();

        public void Dispose()
        {
            _Sleep.Wake();
        }
    }
}