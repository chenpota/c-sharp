using System;
using System.Threading.Tasks;

namespace TaskBasedAsynchronousPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Translator translator = new Translator();

            #region wait-until-done
            {
                Task<string> task = translator.IntToStringAsync(1);

                task.Wait();
                
                Console.WriteLine("wait-until-done: " + task.Result);
            }
            #endregion

            #region polling
            {
                Task<string> task = translator.IntToStringAsync(2);

                while(task.IsCompleted==false)
                {
                    // do something
                }

                Console.WriteLine("polling: " + task.Result);
            }
            #endregion

            #region callback
            {
                Task<string> task = translator.IntToStringAsync(3);

                task.ContinueWith(Callback);

                // do something
            }
            #endregion

            Console.ReadKey();
        }

        private static void Callback(Task<string> task)
        {
            Console.WriteLine("Callback: " + task.Result);
        }
    }
}
