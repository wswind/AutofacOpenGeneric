using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class CustomAttribute : Attribute
    {
        public bool StartLog { get; set; }
    }
}
