using System.Collections.Generic;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public static class ExtractedValue
    {
        public static void SetExtractedValue(string qkey, string? qvalue)
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
        public static string GetExtractedValue(string qkey)
        { 
            return qdata.ContainsKey(qkey)? qdata[qkey] : "There is no stored key for \"" + qkey + "\""; 
        }
        static Dictionary<string, string> qdata= new();        
    }
}
