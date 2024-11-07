using Core.Enums;
using System;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:07:28

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChatTypeAttribute : Attribute
    {
        public ChatType Type { get; set; }
    }
}
