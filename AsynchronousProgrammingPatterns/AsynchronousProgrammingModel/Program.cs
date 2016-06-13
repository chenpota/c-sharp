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

                while (ar.IsCompleted == false)
                {
                    // do something
                }

                string result = translator.EndIntToString(ar);

                Console.WriteLine("polling: " + result);
            }
            #endregion

            #region callback
            {
                IAsyncResult ar = translator.BeginIntToString(3, Callback);

                // do something
            }
            #endregion

            Console.ReadKey();
        }

        private static void Callback(IAsyncResult ar)
        {
            Translator translator = ar.AsyncState as Translator;

            string result = translator.EndIntToString(ar);

            Console.WriteLine("callback: " + result);
        }
    }
}
