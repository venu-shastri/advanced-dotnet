  class B : IDisposable
    {
        bool isDisposed;

        ~B()
        {
            Dispose(false);
        }

        public void Dispose()
        {
           
            Dispose(true);

        }
        protected void Dispose(bool isDisposing) {

            
            if (!isDisposed)
            {

                if (isDisposing)
                {
                    isDisposed = true;
                    GC.SuppressFinalize(this);
                    Console.WriteLine("Object Clean up Using Dispose");
                    //Dispose
                }
                else
                {
                    //Finalize
                    Console.WriteLine("Object Clean up Using Finalize");
                }


            }
            
        
        }

        public void UseObject()
        {
            if (isDisposed) { throw new ObjectDisposedException("Instance of B Already Disposed"); }
        }
    }
