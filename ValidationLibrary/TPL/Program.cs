using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{

    //class Task
    //{
    //    Action _target;

    //    public Task(Action target)
    //    {
    //        this._target = target;
    //    }
    //    public void Start()
    //    {
    //        WaitCallback _targetAddress = new WaitCallback((obj) => { _target.Invoke(); });
    //        ThreadPool.QueueUserWorkItem(_targetAddress);
    //    }
    //}
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Presss any Key to Send Http Request");
                Console.ReadKey();
                SendHttpRequest();
            }
        }
        static void Main_ParentChild()
        {
            Task _task = new Task(Maintask);
            _task.Start();
            _task.Wait();
        }
        static void Main_Cancellation()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task _longRunningTaskReference = new Task(LongRunningTask, cancellationToken, cancellationToken);
            _longRunningTaskReference.Start();
            Thread.Sleep(5000);
            cancellationTokenSource.Cancel();
            Console.WriteLine("Cancellation Requested");
            Console.ReadKey();
            
         }
        static void Main_old(string[] args)
        {
            Task _t1 = new Task(AsyncTaskOne);
            Task<string> _t2 = new Task<string>(AsyncTaskTwo);
            Task _t3 = new Task(AsyncTaskThree, "Hello");
            Task<string> _t4 = new Task<string>(AsyncTaskFour,"Hi");

            _t1.Start();
            _t2.Start();
             _t3.Start();
            _t4.Start();

            //Exception Handling
            //Task.WaitAll(_t1, _t2, _t3, _t4);

            try
            {
                // Task.WaitAll(new Task[] { _t1, _t2, _t3, _t4 });
                 DivideByZeroException obj=new DivideByZeroException("asdfdfds");
                AggregateException _aggregator = new AggregateException(obj);
                throw _aggregator;
               
            }
            catch(AggregateException  ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                var exceptions = ex.InnerExceptions;
                foreach (var exception in exceptions)
                {
                    Console.WriteLine(exception.StackTrace);
                }
            }
            catch(DivideByZeroException ex)
            {

            }
            catch(Exception ex)
            {

            }
            
          //  Console.WriteLine(_t2.Result);
           // Console.WriteLine(_t4.Result);

        
                


        }

        static void AsyncTaskOne() {

            UniversalTask("One");
            throw new InvalidProgramException("AsyncTask One Exception");
        }
        static string AsyncTaskTwo() { UniversalTask("Two"); throw new InvalidProgramException("AsyncTask Two Exception"); ; }
        static void AsyncTaskThree(object arg) { UniversalTask("Three"); throw new InvalidProgramException("AsyncTask Three Exception"); }
        static string AsyncTaskFour(object arg) { UniversalTask("Four"); throw new InvalidProgramException("AsyncTask Four Exception"); }

        static void UniversalTask(string name)

        {

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Task {name} in Execution...... ");
                Thread.Sleep(1000);
            }

        }

        static void LongRunningTask(object token)
        {
           CancellationToken _cancellationTokenRef=( CancellationToken) token;
            // _cancellationTokenRef.Register(() => {

            //     Console.WriteLine("Cancellation requested Callback");
            // });
           

            for (int i = 0; i < 100; i++)
            {
                
                Console.WriteLine("Long Running Task in Excecution");
                Thread.Sleep(1000);
            }
        }

        static void Maintask()
        {
            Console.WriteLine("MainTask Begin");
            Task _childTask = new Task(() => {
                Console.WriteLine("ChildTask Begin");
                Task _gChildTask = new Task(() => {
                    Console.WriteLine("GrandChildTask Begin");
                    Thread.Sleep(4000);
                    Console.WriteLine("GrandChildTask End");

                },TaskCreationOptions.AttachedToParent);
                _gChildTask.Start();
                Thread.Sleep(3000);
                Console.WriteLine("ChildTask End");

            },TaskCreationOptions.AttachedToParent);
            _childTask.Start();

            Task _detachedTask = new Task(()=> {
                Console.WriteLine("DetachedTask Begin");
                Thread.Sleep(10000);
                Console.WriteLine("DetachedTask End");


            });
            _detachedTask.Start();
            Thread.Sleep(2000);
            Console.WriteLine("MainTask End");
        }

        static void SendHttpRequest()
        {

            Task<string> httpRequestTask = new Task<string>(()=> {

                Console.WriteLine("Http Request Sent");
                Thread.Sleep(1000);
                Console.WriteLine("Http Request Completed");
                Random _random = new Random();
               int value= _random.Next(1, 5);
                if (value % 2 == 0)
                {
                    return $"Http Respone Content {value} ";
                }
                throw new Exception($"Server Error {value}");

            });

           Task<string> httpResponseProcessingTask =httpRequestTask.ContinueWith<string>((pt) => {

                Console.WriteLine($"Processing {pt.Result}");
                Thread.Sleep(2000);
                Console.WriteLine($"Processing {pt.Result} Complted");
                return "Extracted Data From Http Response";

            },TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.NotOnFaulted);

            httpRequestTask.ContinueWith((pt) => {

                Console.WriteLine($"Writing Log Content {pt.Exception.InnerException.Message}");
                Thread.Sleep(2000);
                Console.WriteLine($"Log Write Operation Completed");

            },TaskContinuationOptions.OnlyOnFaulted);

            httpRequestTask.Start();
        }
    }
}
