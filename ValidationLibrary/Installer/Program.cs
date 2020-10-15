using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isCreatedNew;
            Mutex _mutexInstance = new Mutex(true, "PICMR", out isCreatedNew);
            if (isCreatedNew)
            {

                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine("Running Installer.......");
                    Thread.Sleep(1000);
                }
                _mutexInstance.ReleaseMutex();
            }
            else
            {
                Console.WriteLine("Another Instance is In Excecution........");
                Thread.Sleep(2000);
                System.Environment.Exit(0);
            }
        }
    }
}
