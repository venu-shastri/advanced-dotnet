using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreaingDemo
{
    public class DbHandler
    {
        public static readonly DbHandler Instance = new DbHandler();
        Object _readSyncObject = new object();
        Object _writeSyncObject = new object();
        private DbHandler() { }

       
        public void Read() {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} waiting ahead of Critical Scetion");
            
            Monitor.Enter(_readSyncObject);
            try
            {
                for (int i = 0; i < 30; i++)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.Name} Enterd Critical Scetion");
                    Console.WriteLine($"DB Handler Read Operation.....{Thread.CurrentThread.Name}");
                    if (i == 5)
                    {
                        return;
                    }
                    Thread.Sleep(2000);
                }
            }

            finally
            {
                Monitor.Exit(_readSyncObject);
                Console.WriteLine($"Thread {Thread.CurrentThread.Name} Exited from  Critical Scetion");
            }
        }

       
        public void Write() {

            lock (_writeSyncObject)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"DB Handler Write Operation.....{Thread.CurrentThread.Name}");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
