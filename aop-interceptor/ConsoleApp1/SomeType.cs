using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    //[Intercept(typeof(CallLogger))]
    [Custom(StartLog = true)]
    public class SomeType : ISomeType
    {
        
        public string Show(string input)
        {
            Console.WriteLine($"showdemo");
            return "resultdemo";
        }
    }
}
