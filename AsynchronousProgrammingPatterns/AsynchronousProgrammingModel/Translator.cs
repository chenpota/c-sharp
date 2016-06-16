using System;
using System.Runtime.Remoting.Messaging;

namespace AsynchronousProgrammingModel
{
    public class Translator
    {
        public IAsyncResult BeginIntToString(int integer, AsyncCallback callback = null)
        {
            AsyncCaller caller = new AsyncCaller(IntToString);

            return caller.BeginInvoke(integer, callback, this);
        }

        public string EndIntToString(IAsyncResult ar)
        {
            AsyncCaller caller = ((AsyncResult)ar).AsyncDelegate as AsyncCaller;

            return caller.EndInvoke(ar);
        }

        private delegate string AsyncCaller(int integer);

        private string IntToString(int integer)
        {
            return integer.ToString();
        }
    }
}
