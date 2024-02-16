using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public static class JavaScriptUtility 
    {
        public static string getElementTextByXpath(string xpath, string jsProperty)
        {  
            return "function getElementByXpath(xPathValue){return document.evaluate(xPathValue, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;} getElementByXpath(\""+xpath+"\")."+ jsProperty;           
        }
        
        public static string getElementTextById(string xpathId)
        {
           return "document.getElementById(\""+xpathId+"\")"; 
        }
        
        public static string getElementTextById(string xpathId, string jsProperty)
        {
           return "document.getElementById(\""+xpathId+"\")."+ jsProperty; 
        }
        
        public static string clickOnElementById(string id)
        {
            return "document.getElementById('"+id+"').click();"; 
        }
        
        public static string clickOnElementByXpathEvaluate(string xpath)
        {
            return "function getElementByXpath(xPathValue){return document.evaluate(xPathValue, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;} getElementByXpath(\""+xpath+"\").Click()"; ; 
        }
        
    }
}
