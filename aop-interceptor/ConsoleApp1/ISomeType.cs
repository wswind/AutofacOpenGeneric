using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface ISomeType
    {
        [Custom(StartLog = true)]
        string Show(string input);
    }
}
