using System;
using System.Threading.Tasks;

namespace TaskBasedAsynchronousPattern
{
    public class Translator
    {
        public Task<string> IntToStringAsync(int number)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Func<int, string> func = integer => integer.ToString();

            func.BeginInvoke(
                number,
                (ar) =>
                {
                    tcs.SetResult(func.EndInvoke(ar));
                },
                null);

            return tcs.Task;
        }
    }
}
