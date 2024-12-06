using System;

//CLR: 4.0.30319.42000
//USER: GL
//DATE: 15.08.2024 20:14:50

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; set; }
        public int CommandId { get; set; }
        public string CommandDescription { get; set; }
    }
}
