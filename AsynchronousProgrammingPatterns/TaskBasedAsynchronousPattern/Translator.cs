using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasedAsynchronousPattern
{
    class Translator
    {
        public Task<string> IntToStringAsync(int number)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Func<int, string> func = integer => integer.ToString();

            func.BeginInvoke(
                number,
                (ar) =>
                {
                    Thread.Sleep(5000);
                    tcs.SetResult(func.EndInvoke(ar));
                },
                null);

            return tcs.Task;
        }
    }
}
