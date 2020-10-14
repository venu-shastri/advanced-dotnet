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
        private DbHandler() { }

        [System.Runtime.CompilerServices.MethodImpl(
          methodImplOptions: System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public void Read() {

            for (int i = 0; i < 30; i++)
            {

                Console.WriteLine($"DB Handler Read Operation.....{Thread.CurrentThread.Name}");
                Thread.Sleep(2000);
            }

        }

        [System.Runtime.CompilerServices.MethodImpl(
            methodImplOptions:System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public void Write() {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"DB Handler Write Operation.....{Thread.CurrentThread.Name}");
                Thread.Sleep(2000);
            }
        }
    }
}
