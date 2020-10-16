using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAndAwait
{
    class Program
    {
        static async  Task Main(string[] args)
        {
            // Console.WriteLine($"Main Thread ->  {Thread.CurrentThread.ManagedThreadId}");
            //await  DoWork();
            // Console.WriteLine($"Statement after Dowork -> {Thread.CurrentThread.ManagedThreadId}");
            DoTask();

            while (true)
            {
                Console.WriteLine("Main thread- I am doing Something");
                Thread.Sleep(1000);
            }
        }

        static async void DoTask()
        {
            await Task.Run(() => { });
            Console.WriteLine("After First Await");
            await Task.Run(() => { Console.WriteLine(" First Task"); Thread.Sleep(2000); });
            Console.WriteLine("After Second Await");
            await Task.Run(() => { Console.WriteLine(" Second Task"); Thread.Sleep(10000); });
            Console.WriteLine("After Third Await");
            await Task.Run(() => { Console.WriteLine(" Third Task"); Thread.Sleep(4000); });
            Console.WriteLine("After Last Await");
        }
        //static async Task  DoWork()
        //{
        //    Console.WriteLine($"Episode One -> {Thread.CurrentThread.ManagedThreadId}");
        //    await Task.Run(() => {

        //        for (int i = 0; i < 5; i++)
        //        {
        //            Thread.Sleep(1000);
        //            Console.WriteLine($"Task in Execution.....{Thread.CurrentThread.ManagedThreadId}");
        //        }
        //     });
        //  //  Console.WriteLine($"Episode Two -> {Thread.CurrentThread.ManagedThreadId}");
        //}

        ////Ui thread
        //static async  void Button_Click()
        //{
        //    string result = await Task.Run<string>(() => {
            
        //        for(int i = 0; i < 10; i++)
        //        {
        //            Console.WriteLine("Search.......");
        //            Thread.Sleep(1000);
                    
        //        }
        //        return "Search result";
            
        //    });
        //    Console.WriteLine($"Update {result} to UI ");
        //}
    }
}
