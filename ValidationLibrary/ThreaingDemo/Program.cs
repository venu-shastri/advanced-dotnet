using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreaingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Who is executing Main Method : {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            System.Threading.Thread.CurrentThread.Name = "Main"; // Debugging , Conditional Breakpoints , Thread - trace

            System.Threading.Thread _threadOne = new System.Threading.Thread(
                new System.Threading.ThreadStart(Task)
                ) { Name = "Thread1", IsBackground=true };
           _threadOne.Start(); // Asynchrous 

            WaitCallback _backGroundTaskPointer = new WaitCallback(NewBackGroundTask);
            ThreadPool.QueueUserWorkItem(_backGroundTaskPointer);

            Thread.Sleep(2000);
            Console.WriteLine("Statement N");
        }

        static void Task()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(
                    $"Task Executed By" +
                    $" {System.Threading.Thread.CurrentThread.ManagedThreadId}  " +
                    $"{System.Threading.Thread.CurrentThread.Name}" +
                    $" Are u running in Foeground Mode {!Thread.CurrentThread.IsBackground} ");

                Thread.Sleep(1000);
            }

        }

        //WaitCallBack Delegete Signature - void(object)
        static void NewBackGroundTask(object param)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(
                    $"NewBackGroundTask Executed By" +
                    $" {System.Threading.Thread.CurrentThread.ManagedThreadId}  " +
                    $"{System.Threading.Thread.CurrentThread.Name}" +
                    $" Are u running in Foeground Mode {!Thread.CurrentThread.IsBackground}+" +
                    $"Are u from ThreadPool Thread {Thread.CurrentThread.IsThreadPoolThread} ");
                Thread.Sleep(1000);
            }
        }
    }
}
