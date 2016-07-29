using System;

namespace Miru.View
{
    public class View
    {
    }

    public class ErrorCallbackEventArgs : EventArgs
    {
        public string name { get; set; }
        public string msg { get; set; }
    }
}