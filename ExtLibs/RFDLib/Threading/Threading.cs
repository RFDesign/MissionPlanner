using System;
using System.Collections.Generic;

namespace RFDLib.Threading
{
    /// <summary>
    /// An interface to pass to a function to make its operation
    /// able to be cancelled.
    /// </summary>
    public class TCancel
    {
        System.Threading.ManualResetEvent _MRE = new System.Threading.ManualResetEvent(false);
        bool _IsCancelled = false;
        public event Action Cancelled;

        public bool IsCancelled
        {
            get
            {
                return _IsCancelled;
            }
        }

        /// <summary>
        /// Cancel sleeps.
        /// </summary>
        public void Cancel()
        {
            _IsCancelled = true;
            _MRE.Set();
            if (Cancelled != null)
            {
                Cancelled();
            }
        }

        /// <summary>
        /// Sleep for the given period in milliseconds.  Returns true if slept, false if sleep was interrupted by a cancel.
        /// </summary>
        /// <param name="Period">The period to sleep for, in milliseconds.</param>
        /// <returns>true if slept, false if sleep was interrupted by a cancel.</returns>
        public bool Sleep(int Period)
        {
            return !_MRE.WaitOne(Period);
        }
    }

    /// <summary>
    /// A class which allows a thread to sleep, but also to be woken during the sleep
    /// by another thread.  After being woken once, sleep will always return straight away with
    /// false, so this is a single-use object.
    /// </summary>
    public class TWakeableSleep
    {
        System.Threading.ManualResetEvent _MRE = new System.Threading.ManualResetEvent(false);

        /// <summary>
        /// Sleep for the given period in milliseconds.  Returns true if slept for that time, false if woken
        /// </summary>
        /// <param name="Period">the given period in milliseconds</param>
        /// <returns>true if slept for that time, false if woken</returns>
        public bool Sleep(int Period)
        {
            return !_MRE.WaitOne(Period);
        }

        /// <summary>
        /// Wake the sleep.
        /// </summary>
        public void Wake()
        {
            _MRE.Set();
        }
    }

    public class TReusableWakeableSleep
    {
        System.Threading.AutoResetEvent _ARE = new System.Threading.AutoResetEvent(false);

        /// <summary>
        /// Sleep for the given period in milliseconds.  Returns true if slept for that time, false if woken
        /// </summary>
        /// <param name="Period">the given period in milliseconds</param>
        /// <returns>true if slept for that time, false if woken</returns>
        public bool Sleep(int Period)
        {
            return !_ARE.WaitOne(Period);
        }

        /// <summary>
        /// Wake the sleep.
        /// </summary>
        public void Wake()
        {
            _ARE.Set();
        }
    }

    /// <summary>
    /// For managing multiple tasks happening in parallel from start to finish.
    /// </summary>
    /// <typeparam name="T">The type of the parameter passed to each task.</typeparam>
    /// <typeparam name="U">The type of the result returned by each task.</typeparam>
    public class TParalleliser<T, U>
    {
        object _Locker = new object();
        List<TThreadWrapper> _Running = new List<TThreadWrapper>();
        System.Threading.ManualResetEvent _MRE = new System.Threading.ManualResetEvent(true);
        Action<T, U> _TaskCompletedCallback;

        /// <summary>
        /// Create a new TParalleliser
        /// </summary>
        /// <param name="TaskCompletedCallback">The function to call when a single task has completed,
        /// for each individual task.  Must not be null.</param>
        public TParalleliser(Action<T, U> TaskCompletedCallback)
        {
            _TaskCompletedCallback = TaskCompletedCallback;
        }

        /// <summary>
        /// Internally handles a task being completed.
        /// </summary>
        /// <param name="TW">The thread wrapper for the task.  Must not be null.</param>
        /// <param name="Result">The returned result of the task.</param>
        void TaskCompleteHdlr(TThreadWrapper TW, U Result)
        {
            lock (_Locker)
            {
                _TaskCompletedCallback(TW.Parameter, Result);
                _Running.Remove(TW);
                if (_Running.Count == 0)
                {
                    _MRE.Set();
                }
            }
        }

        /// <summary>
        /// Start an individual task.
        /// </summary>
        /// <param name="Start">The function which performs the task.  Must not be null.</param>
        /// <param name="Parameter">The parameter to pass to the task.</param>
        public void StartTask(Func<T, U> Start, T Parameter)
        {
            lock (_Locker)
            {
                _MRE.Reset();
                TThreadWrapper TW = new TThreadWrapper(Start, Parameter, TaskCompleteHdlr);
                _Running.Add(TW);
                TW.Start();
            }
        }

        /// <summary>
        /// Returns when all of the started tasks are complete.
        /// </summary>
        public void WaitUntilComplete()
        {
            _MRE.WaitOne();
        }

        /// <summary>
        /// Manages and individual task.
        /// </summary>
        class TThreadWrapper
        {
            Func<T, U> _Start;
            T _Parameter;
            Action<TThreadWrapper, U> _TaskCompletedHdlr;

            /// <summary>
            /// Create a TThreadWrapper
            /// </summary>
            /// <param name="Start">The body function.  Must not be null.</param>
            /// <param name="Parameter">The parameter to pass to the body function.</param>
            /// <param name="TaskCompletedHdlr">The task completed handler of the paralleliser.  Must not be null.</param>
            public TThreadWrapper(Func<T, U> Start, T Parameter,
                Action<TThreadWrapper, U> TaskCompletedHdlr)
            {
                _Start = Start;
                _Parameter = Parameter;
                _TaskCompletedHdlr = TaskCompletedHdlr;
            }

            /// <summary>
            /// The parameter passed to the body function.
            /// </summary>
            public T Parameter
            {
                get
                {
                    return _Parameter;
                }
            }

            /// <summary>
            /// Start the task.
            /// </summary>
            public void Start()
            {
                System.Threading.Thread Thread = new System.Threading.Thread(DoTask);
                Thread.Start();
            }

            /// <summary>
            /// Does the actual task and then calls the task complete handler.
            /// </summary>
            void DoTask()
            {
                U Result = _Start(_Parameter);
                _TaskCompletedHdlr(this, Result);
            }
        }
    }

    public abstract class TGeneralBackgroundWorker
    {
        public abstract void WaitUnitFinished();
        public abstract void CancelIfStillRunning();
    }

    /// <summary>
    /// A class for automating the creation of a separate thread to run a function in the background,
    /// and store the value it returns, for later use.
    /// </summary>
    /// <typeparam name="TResult">The type of data the function returns</typeparam>
    public class TBackgroundWorker<TResult> : TGeneralBackgroundWorker
    {
        System.Threading.AutoResetEvent _ARE = new System.Threading.AutoResetEvent(false);
        Func<TCancel, TResult> _DoStepFn;
        TResult _Result;
        bool _Done = false;
        TCancel _ParentCancel;
        TCancel _Cancel = new TCancel();

        /// <summary>
        /// Create a new TBackgroundWorker, and launch the background thread.
        /// </summary>
        /// <param name="DoStepFn">The function to run in the background.  Must not be null.</param>
        /// <param name="ParentCancel">The cancel object used by the program.  Must not be null.</param>
        public TBackgroundWorker(Func<TCancel, TResult> DoStepFn, TCancel ParentCancel)
        {
            _DoStepFn = DoStepFn;
            _ParentCancel = ParentCancel;
            _ParentCancel.Cancelled += ParentCancelHdlr;
            StartBackgroundThread();
        }

        /// <summary>
        /// Start the background thread.
        /// </summary>
        void StartBackgroundThread()
        {
            System.Threading.Thread BackgroundThread = new System.Threading.Thread(Background);
            BackgroundThread.Start();
        }

        /// <summary>
        /// The wrapper function run by the background thread.
        /// </summary>
        void Background()
        {
            _Result = _DoStepFn(_Cancel);
            if (!_Cancel.IsCancelled)
            {
                _Done = true;
            }
            _ARE.Set();
        }

        /// <summary>
        /// Handles the parent cancelling event.
        /// </summary>
        void ParentCancelHdlr()
        {
            _Cancel.Cancel();
        }

        /// <summary>
        /// The result returned by the function
        /// </summary>
        public TResult Result
        {
            get
            {
                return _Result;
            }
        }

        /// <summary>
        /// Returns whether the function completed.
        /// </summary>
        public bool Done
        {
            get
            {
                return _Done;
            }
        }

        /// <summary>
        /// Wait until the function has finished running in the background.
        /// </summary>
        public override void WaitUnitFinished()
        {
            _ARE.WaitOne();
        }

        /// <summary>
        /// Cancel the function being run in the background if it is still running.
        /// </summary>
        public override void CancelIfStillRunning()
        {
            if (!_Done)
            {
                _Cancel.Cancel();
            }
        }
    }

    /// <summary>
    /// Automates waiting for any of a number of background workers running to stop.
    /// </summary>
    class TWaitForAny
    {
        System.Threading.AutoResetEvent _ARE = new System.Threading.AutoResetEvent(false);
        List<TWaiter> _Waiters = new List<TWaiter>();

        /// <summary>
        /// Specify an addition background worker to wait for any of.
        /// </summary>
        /// <param name="BackgroundWorker"></param>
        public void WaitForThis(TGeneralBackgroundWorker BackgroundWorker)
        {
            TWaiter W = new TWaiter(BackgroundWorker, FinishedHdlr);
            _Waiters.Add(W);
        }

        /// <summary>
        /// Wait for any of the background workers to finish.
        /// </summary>
        public void WaitForAny()
        {
            _ARE.WaitOne();
            foreach (var W in _Waiters)
            {
                W.BackgroundWorker.CancelIfStillRunning();
            }
        }

        /// <summary>
        /// Handles a background worker finishing.
        /// </summary>
        void FinishedHdlr()
        {
            _ARE.Set();
        }

        /// <summary>
        /// A class which automates waiting for a single background worker to finish.
        /// </summary>
        class TWaiter
        {
            Action _Finished;
            TGeneralBackgroundWorker _BackgroundWorker;

            public TWaiter(TGeneralBackgroundWorker BackgroundWorker, Action Finished)
            {
                _BackgroundWorker = BackgroundWorker;
                _Finished = Finished;
                System.Threading.Thread WaiterThread = new System.Threading.Thread(Wait);
                WaiterThread.Start();
            }

            void Wait()
            {
                _BackgroundWorker.WaitUnitFinished();
                _Finished();
            }

            public TGeneralBackgroundWorker BackgroundWorker
            {
                get
                {
                    return _BackgroundWorker;
                }
            }
        }
    }
}