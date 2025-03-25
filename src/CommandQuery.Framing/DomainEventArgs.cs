using System;

namespace CommandQuery.Framing
{
    public class DomainEventArgs
        : EventArgs
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}