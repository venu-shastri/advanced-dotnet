using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreaingDemo
{
    public class SyncOne { public static readonly SyncOne Instance = new SyncOne(); }
    public class SyncTwo { public static readonly SyncTwo Instance = new SyncTwo(); }
    public class DeadLock
    {
        

        public  void Foo() {

            lock (SyncOne.Instance)
            {
                lock (SyncTwo.Instance)
                {

                }

            }
        
        }

        public void Fun() {

            lock (SyncTwo.Instance)
            {
                lock (SyncOne.Instance)
                {

                }

            }
        }
    }
}
