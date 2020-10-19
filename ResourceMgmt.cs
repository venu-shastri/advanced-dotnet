using System;
using System.Collections.Generic;
using System.Text;

namespace GCDemo
{

    public static class Signals
    {

        public static System.Threading.AutoResetEvent _resourceStateWaitHandle = new System.Threading.AutoResetEvent(false);
    }
    public enum ResourceState
    {
        BUSY,FREE
    }
    public class Resource
    {
        private Resource() {

            this.State = ResourceState.FREE;
        }
        public static readonly Resource Instance = new Resource();

        public void UseResource() {

                Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Using  Resource");
        }

        public ResourceState State { get; set; }
    }


    class ResourceWrapper:IDisposable
    {

        public ResourceWrapper()
        {
        }
        public void GrabResource()
        {
            if (Resource.Instance.State == ResourceState.FREE)
            {
                Resource.Instance.State = ResourceState.BUSY;
                Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Aquired Resource");
            }
            else
            {
                Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Awaiting for  Resource");
                Signals._resourceStateWaitHandle.WaitOne();
                Resource.Instance.State = ResourceState.BUSY;
                Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Aquired Resource");
            }

        }
        public void UseResource()
        {
            Resource.Instance.UseResource();
        }

        //Grammer -> Compiler will generate private void Finalize() - MSIL 
        ~ResourceWrapper()
        {
            Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Releasing   Resource using Finalize");
            Resource.Instance.State = ResourceState.FREE;
            Signals._resourceStateWaitHandle.Set();
        }
        

        public void Dispose()
        {
            Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " Releasing   Resource using Dispose");
            Resource.Instance.State = ResourceState.FREE;
            Signals._resourceStateWaitHandle.Set();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {

            new System.Threading.Thread(Client1).Start();
          //  new System.Threading.Thread(Client2).Start();

        }

        static void Client1()
        {
            Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + "Executing Client1");
            ResourceWrapper _rw = new ResourceWrapper();
            _rw.GrabResource();
            try
            {
                _rw.UseResource();
                
            }
            catch (Exception ex)
            {
                
            }

            finally
            {
                if (_rw is IDisposable)
                {
                    //_rw.Dispose();
                }
            }
            _rw = null;
            GC.Collect();

            
          
          
        }
        static void Client2()
        {
            Console.WriteLine("Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + "Executing Client2");
            using (ResourceWrapper _rw = new ResourceWrapper())
            {
                _rw.GrabResource();
                _rw.UseResource();
             
            }
            
            
        }
    }
}
