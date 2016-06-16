using System;

namespace EventBasedAsynchronousPattern
{
    public class Translator
    {
        public event EventHandler<CustomEventArgs> IntToStringCompleted;

        public void IntToStringAsync(int number)
        {
            Action<int> action = IntToString;

            action.BeginInvoke(number, null, null);
        }

        private void IntToString(int number)
        {
            CustomEventArgs args = new CustomEventArgs()
            {
                Result = number.ToString(),
            };

            EventHandler<CustomEventArgs> completed = IntToStringCompleted;

            completed?.Invoke(this, args);
        }
    }
}
