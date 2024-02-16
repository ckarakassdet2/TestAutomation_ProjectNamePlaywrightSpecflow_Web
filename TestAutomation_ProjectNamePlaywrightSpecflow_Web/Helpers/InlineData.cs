using System.Collections.Generic;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public static class InlineData
    {
        public static void SetInlineData(string qkey, string? qvalue)
        {
            if (!qdata.ContainsKey(qkey))
            {
                qdata.Add(qkey, qvalue);
            }
            else
            {
                qdata[qkey] = qvalue; 
            }
        }
        public static string GetInlineData(string qkey)
        { 
            return qdata.ContainsKey(qkey)? qdata[qkey] : "There is no stored key for \"" + qkey + "\""; 
        }
        static Dictionary<string, string> qdata= new();        
    }
}
