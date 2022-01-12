using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Common
{
    public interface ICenter
    {
        void ShowMessage(string msg);
        void Start();
        void Stop();
        bool IsDebug { set; get; }
        bool IsNeedShowMessage { set; get; }
    }
    public abstract class CenterThread<TCenter> : VWorker where TCenter : ICenter
    {
        public TCenter? Center { set; get; }
        public TCenter2? GetCenter<TCenter2>() where TCenter2 : class,new()
        {
            if(Center!=null && Center is TCenter2 center) return center;
            return null;
        }
    }
    public abstract class Center<TThread, TCenter> : Messagable, ICenter
       where TThread : CenterThread<TCenter>
       where TCenter : class, ICenter,new()
    {
        public ICrypt Crypt { private set;  get; } = new NoneCrypt();
        public void AddCrypt<TCrypt>() where TCrypt: ICrypt,new()
        {
            Crypt = new TCrypt();
        }
        private readonly List<VWorker> workers = new();
        protected IMapping? Mapping;
        protected List<VWorker> Workers
        {
            get { return workers; }
        }
        protected void AddMapping<TMapping>() where TMapping : IMapping, new()
        {
            this.Mapping = new TMapping();
        }

        protected void AddWorker<TWorker>(Action<TWorker>? action=null) where TWorker : TThread, new()
        {
            var worker = new TWorker { Center = this as TCenter };
            action?.Invoke(worker);
            AddWorker(worker);
        }

        protected void AddWorker<TWorkerConcrete>(TWorkerConcrete worker) where TWorkerConcrete : CenterThread<TCenter>
        {
            worker.Center = this as TCenter;
            worker.Message += msg => ShowMessage(msg);
            workers.Add(worker);
        }

        public void Start() { StartHelper(); OnStart(); }
        protected virtual void OnStart() { StartWorkers(); }

        public void Stop() { OnStop(); StopHelper(); }
        protected virtual void OnStop() { StopWorkers(); }

        protected void StartWorkers()
        {
            workers.ForEach(worker => worker.Start());
        }
        protected void StopWorkers()
        {
            workers.ForEach(worker => worker.Stop());
        }

        protected void StartHelper()
        {
        }

        protected void StopHelper()
        {
        }

        public bool IsDebug { set; get; }

        public bool IsNeedShowMessage { set; get; }
        
    }
}
