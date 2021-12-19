using System;

namespace NpcService.Model
{
    public class ProcessEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public DateTime CompletionTime { get; set; }
    }
}