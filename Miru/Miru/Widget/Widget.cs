using System;

namespace Miru.Widget
{
    public class Widget
    {
    }

    public class ErrorCallbackEventArgs : EventArgs
    {
        public string name { get; set; }
        public string msg { get; set; }
    }
}