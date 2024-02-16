using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class Helper
    {
        private static string RandomizeString(int _length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < _length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        
        public static string RandomLotNumber()
        {
            string _lotNumber = string.Empty;
            string _currDT = System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Year.ToString().Substring(2,2) + 
                System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();
            _lotNumber = "Lot-" + _currDT + "_" + RandomizeString(8);
            return _lotNumber;
        }
    }
}
