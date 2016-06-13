using System;
using System.Threading;

namespace EventBasedAsynchronousPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Translator translator = new Translator();

            #region wait-until-done
            {
                translator.IntToStringCompleted += WaitUntilDone;

                lock (_syncObj)
                {
                    translator.IntToStringAsync(1);

                    Monitor.Wait(_syncObj);
                }
            }
            #endregion

            #region callback
            {
                translator.IntToStringCompleted += Callback;

                translator.IntToStringAsync(2);

                // do something
            }
            #endregion

            Console.ReadKey();
        }

        private static object _syncObj = new object();

        private static void Callback(object sender, CustomEventArgs e)
        {
            Translator translator = sender as Translator;

            translator.IntToStringCompleted -= Callback;

            Console.WriteLine("callback: " + e.Result);
        }

        private static void WaitUntilDone(object sender, CustomEventArgs e)
        {
            Translator translator = sender as Translator;

            translator.IntToStringCompleted -= WaitUntilDone;

            Console.WriteLine("wait-until-done: " + e.Result);

            lock (_syncObj)
            {
                Monitor.Pulse(_syncObj);
            }
        }
    }
}
