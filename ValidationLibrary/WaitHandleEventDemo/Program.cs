using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitHandleEventDemo
{
    public static class Signals
    {
       public static AutoResetEvent _signalA = new AutoResetEvent(false);//wait state
        public static AutoResetEvent _signalB = new AutoResetEvent(false);//wait state
        public static AutoResetEvent _signalC = new AutoResetEvent(false);//wait state
        //public static AutoResetEvent _initialSignal = new AutoResetEvent(false);//wait state
        public static ManualResetEvent _initialSignal = new ManualResetEvent(false);//wait state

    }
    public class BackGroundTasks
    {
        public void TaskOne()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} Awaiting for Signal From Main");
            Signals._initialSignal.WaitOne();

            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    //Signals._initialSignal.Set();
                    Signals._signalB.Set();
                }
                Console.WriteLine($"Task 1 {i}");

                Thread.Sleep(1000);
            }
        }
        public void TaskTwo()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} Awaiting for Signal From Main");
            Signals._initialSignal.WaitOne();
            for (int i = 0; i < 20; i++)
            {
                if (i == 10)
                {
                  //  Signals._initialSignal.Set();
                    Signals._signalA.Set();
                }
                Console.WriteLine($"Task 2 -->{i} ");
                Thread.Sleep(1000);
            }
        }

        public void TaskThree()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} Awaiting for Signal From Main");
            Signals._initialSignal.WaitOne();
            for (int i = 0; i < 15; i++)
            {
                if (i == 12)
                {
                    //Signals._initialSignal.Set();
                    Signals._signalC.Set(); }
                Console.WriteLine($"Task 3 --- {i}");
                Thread.Sleep(1000);
                
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
      
                BackGroundTasks _tasks = new BackGroundTasks();
            
            
                ThreadPool.QueueUserWorkItem((obj)=> { _tasks.TaskOne(); });
                ThreadPool.QueueUserWorkItem((obj) => { _tasks.TaskTwo(); });
                ThreadPool.QueueUserWorkItem((obj) => { _tasks.TaskThree(); });

            //Signals._signalA.WaitOne(); //Check Handle state ...if handle state is free continue the execution.. else ..wait for signal
            //Signals._signalB.WaitOne();
            //Signals._signalC.WaitOne();
            Console.WriteLine("Press Any Key to Set initiaiSignal");
            Console.ReadKey();
            Signals._initialSignal.Set();

            WaitHandle.WaitAll(new WaitHandle[] {
                Signals._signalA, 
                Signals._signalB, 
                Signals._signalC });

            }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
           
        }
    }
}
