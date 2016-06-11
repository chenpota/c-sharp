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
                CustomEventArgs eventArgs = new CustomEventArgs();
                eventArgs.Callback += WaitUntilDone;
                eventArgs.Integer = 1;
                
                lock (_syncObj)
                {
                    translator.IntToStringAsync(eventArgs);

                    Monitor.Wait(_syncObj);
                }
            }
            #endregion

            #region polling
            {
                CustomEventArgs eventArgs = new CustomEventArgs();
                eventArgs.Integer = 2;

                translator.IntToStringAsync(eventArgs);

                while (eventArgs.IsComplete == false)
                {
                    // do something
                }

                Console.WriteLine("polling: " + eventArgs.Result);
            }
            #endregion

            #region callback
            {
                CustomEventArgs eventArgs = new CustomEventArgs();
                eventArgs.Callback += Callback;
                eventArgs.Integer = 3;

                translator.IntToStringAsync(eventArgs);

                // do something
            }
            #endregion

            Console.ReadKey();
        }

        private static object _syncObj = new object();

        private static void Callback(object sender, CustomEventArgs e)
        {
            Console.WriteLine("callback: " + e.Result);
        }

        private static void WaitUntilDone(object sender, CustomEventArgs e)
        {
            Console.WriteLine("wait-until-done: " + e.Result);

            lock (_syncObj)
            {
                Monitor.Pulse(_syncObj);
            }
        }
    }
}
