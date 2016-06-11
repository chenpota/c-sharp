using System;

namespace EventBasedAsynchronousPattern
{
    class Translator
    {
        public void IntToStringAsync(CustomEventArgs e)
        {
            e._isComplete = false;

            Func<CustomEventArgs, CustomEventArgs> func = IntToString;

            func.BeginInvoke(e, IntToStringCallback, func);
        }

        private void IntToStringCallback(IAsyncResult ar)
        {
            Func<CustomEventArgs, CustomEventArgs> func = ar.AsyncState as Func<CustomEventArgs, CustomEventArgs>;

            CustomEventArgs e = func.EndInvoke(ar);

            e.NotifyResult(this);
        }

        private CustomEventArgs IntToString(CustomEventArgs e)
        {
            e._result = e.Integer.ToString();

            return e;
        }
    }
}
