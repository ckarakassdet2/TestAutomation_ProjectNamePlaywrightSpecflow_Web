using System;
using System.Collections.Generic;
using System.Linq;
// for openxml 
using ClosedXML;
using ClosedXML.Excel;
using NUnit.Framework;


namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    
    public class ExcelUtility
    {
        //[Test]
        public void runTest()
        {
            readExcelDataFileName("DataFile", "STAGE"); 
        }

        public static void readExcelDataFileName(string fileName, string sheetName)
        {
            string currDir = Environment.CurrentDirectory;
            string pathToDataFile = string.Empty; 
            Console.WriteLine("************* currDir: " + currDir);    
            if (currDir.Contains("debug"))
            {
                pathToDataFile = currDir.Replace("\\bin\\debug\\net6.0", "\\Data\\" + fileName + ".xlsx");
            }
            if (currDir.Contains("Debug"))
            {
                pathToDataFile = currDir.Replace("\\bin\\Debug\\net6.0", "\\Data\\" + fileName + ".xlsx");
            }
            Console.WriteLine($"Path to Data file: {pathToDataFile} >> Reading Sheet: {sheetName}");
            using (var workbook = new XLWorkbook(pathToDataFile))
            {
                var nonEmptyDataRows = workbook.Worksheet(sheetName.ToUpper()).RowsUsed().Skip(1);
                foreach (var dataRow in nonEmptyDataRows)
                {
                    var key = dataRow.Cell(1).Value;
                    var value = dataRow.Cell(2).Value; 
                    //Console.WriteLine($"key: {key} value: {value}");
                    ExtractedValue.SetExtractedValue((string)dataRow.Cell(1).Value, (string)dataRow.Cell(2).Value); 
                }
            }
        }// readExcelDataFileName(string fileName, string sheetName)

        public static void readExcelDataFilePath(string filePath, string sheetName)
        {          
            Console.WriteLine($"Path to Data file: {filePath} >> Reading Sheet: {sheetName}");
            using (var workbook = new XLWorkbook(filePath))
            {
                var nonEmptyDataRows = workbook.Worksheet(sheetName).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    var key = dataRow.Cell(1).Value;
                    var value = dataRow.Cell(2).Value;
                    //Console.WriteLine($"key: {key} value: {value}");
                    ExtractedValue.SetExtractedValue((string)dataRow.Cell(1).Value, (string)dataRow.Cell(2).Value);
                }
            }
        } // readExcelDataFilePath(string filePath, string sheetName)



    }
}
