using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class StringHandling
    {
        // this takes string like: "Doe, John" and returns {"John", "Doe"} in array
        public static string[] FirstLastName(string _commaSepName)
        {
            string[] _firstLast = _commaSepName.Split(',').Reverse().ToArray();
            return _firstLast;
        }
    }
}
