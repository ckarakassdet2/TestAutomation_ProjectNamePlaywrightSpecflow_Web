using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class RandomStrings
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static string guidString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
