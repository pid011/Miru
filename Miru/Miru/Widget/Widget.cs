using System;

namespace Miru.Widget
{
    internal abstract class Widget
    {
    }

    internal class ErrorCallbackEventArgs : EventArgs
    {
        public string name { get; set; }
        public string msg { get; set; }
    }
}