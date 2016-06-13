using System;

namespace EventBasedAsynchronousPattern
{
    public class CustomEventArgs : EventArgs
    {
        public string Result
        {
            get;
            internal set;
        }
    }
}
