using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingBasics
{
    class Program
    {
        static void SequenceOne()
        {
            //Task 
            for(int i = 0; i < 10; i++)
            {

                Console.WriteLine($" SequenceOne Execution Status {i} and Executed By {System.Threading.Thread.CurrentThread.ManagedThreadId} and IsBackgroundMode {System.Threading.Thread.CurrentThread.IsBackground}");
                System.Threading.Thread.Sleep(1000);

            }
        }
        static void SequenceTwo(object initialState)
        {
            for (int i = 0; i < 15; i++)
            {

                Console.WriteLine($" SequenceTwo Execution Status {i} and Executed By {System.Threading.Thread.CurrentThread.ManagedThreadId} and IsBackgroundMode {System.Threading.Thread.CurrentThread.IsBackground}");
                System.Threading.Thread.Sleep(1000);

            }
        }
        static void Main_old(string[] args)
        {
            //Sequence Of Code
            Console.WriteLine($" Main Code  Executed By {System.Threading.Thread.CurrentThread.ManagedThreadId} IsBackgroundMode {System.Threading.Thread.CurrentThread.IsBackground}");
            System.Threading.Thread _mainExecutionPath=System.Threading.Thread.CurrentThread;
            System.Threading.ThreadStart _sequenceOneStartPoint = new System.Threading.ThreadStart(Program.SequenceOne);
            System.Threading.Thread _sequenceOne = new System.Threading.Thread(_sequenceOneStartPoint);
            _sequenceOne.Start();

            //Request Thread Pool Thread
            System.Threading.WaitCallback _sequenceTwoAddress =
                new System.Threading.WaitCallback(Program.SequenceTwo);
            System.Threading.ThreadPool.QueueUserWorkItem(_sequenceTwoAddress);
            

            

        }

        static void Main()
        {
            string[] keys = { "key1","No key", "key2", "key3" };
            
            for(int i = 0; i < keys.Length; i++)
            {
                SearchButton_Click(keys[i]);
            }
            Console.WriteLine("End OF Main");
            
        }
        
        static void SearchButton_Click(string key)
        {
            SearchService _serviceRef = new SearchService();
           string result= _serviceRef.Search(key);
            Console.WriteLine(result);


        }
    }
}
