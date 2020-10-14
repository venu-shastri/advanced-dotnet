using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreaingDemo
{
    [System.Runtime.Remoting.Contexts.Synchronization]
   public class FileHandler:ContextBoundObject
    {
        public static readonly FileHandler Instance = new FileHandler();
        
        private FileHandler() { }
        public void Open() {
        
            for(int i = 0; i < 10; i++)
            {
                
                Console.WriteLine($"FileHandler in Use {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
            }
        
        }
        public void Read() {
            for (int i = 0; i < 10; i++)
            {
                
                Console.WriteLine($"FileHandler- Read in Use {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
            }
        }

        public void Write() {

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"FileHandler - Write  in Use {Thread.CurrentThread.Name} ");
                Thread.Sleep(1000);
            }
        }


    }
}
