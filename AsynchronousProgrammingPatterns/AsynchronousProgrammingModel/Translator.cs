using System;

namespace AsynchronousProgrammingModel
{
    class Translator
    {
        public delegate string AsyncCaller(int integer);

        public IAsyncResult BeginIntToString(int integer, AsyncCallback callback = null)
        {
            AsyncCaller caller = new AsyncCaller(IntToString);

            return caller.BeginInvoke(integer, callback, caller);
        }

        public string EndIntToString(IAsyncResult ar)
        {
            AsyncCaller caller = ar.AsyncState as AsyncCaller;

            return caller.EndInvoke(ar);
        }

        private string IntToString(int integer)
        {
            return integer.ToString();
        }
    }
}
