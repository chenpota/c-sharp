using System;

namespace EventBasedAsynchronousPattern
{
    public class CustomEventArgs : EventArgs
    {
        public int Integer
        {
            get; set;
        }

        public bool IsComplete
        {
            get
            {
                lock (_lockObj)
                {
                    return _isComplete;
                }
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }
        }

        public event EventHandler<CustomEventArgs> Callback;

        internal string _result;
        internal bool _isComplete;

        internal void NotifyResult(Translator translator)
        {
            lock (_lockObj)
            {
                _isComplete = true;
            }

            EventHandler<CustomEventArgs> callback = Callback;

            callback?.Invoke(translator, this);
        }

        private object _lockObj = new object();
    }
}
