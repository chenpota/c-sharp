using System;

namespace AsynchronousProgrammingModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Translator translator = new Translator();

            #region wait-until-done
            {
                IAsyncResult ar = translator.BeginIntToString(1, null);

                string result = translator.EndIntToString(ar);

                Console.WriteLine("wait-until-done: " + result);
            }
            #endregion

            #region polling
            {
                IAsyncResult ar = translator.BeginIntToString(2, null);

                while (ar.IsCompleted == false) ;
                {
                    // do something
                }

                string result = translator.EndIntToString(ar);

                Console.WriteLine("polling: " + result);
            }
            #endregion

            #region callback
            {
                IAsyncResult ar = translator.BeginIntToString(3, ApmCallback);

                // do something
            }
            #endregion

            Console.ReadKey();
        }

        private static void ApmCallback(IAsyncResult ar)
        {
            Translator.AsyncCaller caller = ar.AsyncState as Translator.AsyncCaller;

            string result = caller.EndInvoke(ar);

            Console.WriteLine("callback: " + result);
        }
    }
}
