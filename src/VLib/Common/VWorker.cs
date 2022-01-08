using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Common
{
    public abstract class VWorker : Messagable, IDisposable
    {
        private BackgroundWorker? worker = null;

        private bool isRuning = false;

        private TimeSpan interval = TimeSpan.Zero;
        public TimeSpan Interval
        {
            get { return interval; }
            set { interval = value; }
        }
        protected abstract void DoWork();
        public void Start()
        {
            if (isRuning) return;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;

            isRuning = true;
            BeforeStart();
            worker.RunWorkerAsync();
        }


        public void Stop()
        {
            if (!isRuning) return;

            isRuning = false;
            try
            {
                if (worker != null)
                    worker.Dispose();
            }
            catch { }
            finally
            {
                try
                {
                    OnFinish(this);
                    Dispose();
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected virtual void BeforeStart() { }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Prepare process
            OnPrepare(this);

            while (isRuning)
            {
                try
                {
                    DoWork();
                }
                catch (ThreadAbortException)
                {
                    isRuning = false;
                }
                catch (Exception ex)
                {
                   // ShowMessage(ex.Message);
                    Error?.Invoke(this, ex);
                }

                // Sleep
                if (Interval != TimeSpan.Zero)
                    Thread.Sleep(interval);
            }
        }

        private readonly Dictionary<string, Delegate> exceptions = new();
        public void WithThrow<T>(Action<T> func) where T : Exception
        {
            exceptions[typeof(T).FullName] = func;
        }
        public event Action<object>? Prepare;

        protected virtual void OnPrepare(object sender)
        {
            Prepare?.Invoke(sender);
        }

        public event Action<object>? Finish;

        protected virtual void OnFinish(object sender)
        {
            worker = null;
            Finish?.Invoke(sender);
        }
        public event Action<object, Exception>? Error;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
