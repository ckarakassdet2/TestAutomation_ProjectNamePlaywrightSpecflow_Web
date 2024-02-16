using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    [Binding]
    public class MenuStepDefinitions
    {
        private readonly Driver driver;
        private readonly LoginPage loginPage;
        private readonly MenuPage menuPage;
        private readonly ScenarioContext _scenarioContext;

        [Obsolete]
        public MenuStepDefinitions(Driver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            loginPage = new LoginPage(this.driver.Page);
            menuPage = new MenuPage(this.driver.Page);
            _scenarioContext = scenarioContext;
            var tags = ScenarioContext.Current.ScenarioInfo.Tags;
            Console.Write("Scenario Tags: ");
            foreach (var tag in tags)
            {
                Console.Write(tag + ", ");
            }
            Console.WriteLine("");
        }

        [Given(@"I navigated to features page for chosen environment hospital and department")]
        public async void GivenINavigatedToFeaturesPageForChosenEnvironmentHospitalAndDepartment(TechTalk.SpecFlow.Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            Console.WriteLine("Started: " + DateTime.Now);
            ExcelUtility.readExcelDataFileName("DataFile", "Stage");
            // setting default timeout to 90 seconds, it is 30 by default
            driver.Page.SetDefaultTimeout(90000);
            await driver.Page.SetViewportSizeAsync(1620, 780);   
            if (data.environment.Contains("Test"))
            {
                await driver.Page.GotoAsync(ExtractedValue.GetExtractedValue("baseUrl_Test"));
            }
            else if (data.environment.Contains("Stage"))
            {
                await driver.Page.GotoAsync(ExtractedValue.GetExtractedValue("baseUrl_Stage"));
            }
            // enter valid username and password 
            await loginPage.Login("Automation-SiteAdmin", SymmetricDecryptor.DecryptToString(ExtractedValue.GetExtractedValue("Password")));

            if (await driver.Page.Locator("//*[@id='lblModalHeading']").IsVisibleAsync())
            {   
                await driver.Page.Locator("//*[@id='modalLaunch']/div/div[3]/button").ClickAsync(); // clicking to close hello new customer modal
            }
            // select hospital/dept
            await driver.Page.GetByRole(AriaRole.Combobox).SelectOptionAsync(new[] { (string)data.hospital + " - " + (string)data.department }); // select department "Cath Lab"
            
            await driver.Page.WaitForTimeoutAsync(500);
            // click on continue button 
            await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            await driver.Page.WaitForTimeoutAsync(1500);
            if ((string)data.branch == "Alerts")
            {
                await driver.Page.GetByRole(AriaRole.Link, new() { Name = "Alerts" }).ClickAsync();
            }
            else
            {
                await driver.Page.WaitForTimeoutAsync(500);
                if ((string)data.root != "x")
                {
                    await driver.Page.Locator("xpath=//a[@class='nav-link']/span[contains(text(),'" + (string)data.root + "')]").ClickAsync();
                    await driver.Page.WaitForTimeoutAsync(1500);
                }
                if ((string)data.branch != "x")
                {                    
                    await driver.Page.Locator("xpath=//a[@class='dropdown-toggle nav-link']/span[contains(text(),'" + (string)data.branch + "')]").ClickAsync();
                    await driver.Page.WaitForTimeoutAsync(1500);
                }
                if ((string)data.leaf != "x")
                {
                    await driver.Page.Locator("xpath=//a[@class='dropdown-item nav-link']//span[text()='" + (string)data.leaf + "']").ClickAsync();
                    await driver.Page.WaitForTimeoutAsync(1500);
                }
            }
            await driver.Page.WaitForTimeoutAsync(1000);
        }

        [Then(@"navigate via leaf menu option to chosen page and verify header label")]
        public async Task ThenNavigateViaLeafMenuOptionToChosenPageAndVerifyHeaderLabel(TechTalk.SpecFlow.Table table)
        {
            // create a table object to pass variables from feature file
            dynamic data = table.CreateDynamicInstance();

            if ((string)data.branch == "Alerts")
            {
                await driver.Page.GetByRole(AriaRole.Link, new() { Name = "Alerts" }).ClickAsync();
            }
            else
            {
                await driver.Page.WaitForTimeoutAsync(500);
                await driver.Page.Locator("xpath=//span[contains(text(),'" + (string)data.root + "')]").Nth(0).ClickAsync();
                await driver.Page.WaitForTimeoutAsync(500);
                await driver.Page.Locator("xpath=//span[contains(text(),'" + (string)data.branch + "')]").Nth(0).ClickAsync();
                await driver.Page.WaitForTimeoutAsync(500);
                string _tempstr = (string)data.leaf;
                string _leaf = _tempstr.Trim();
                // Fix: 8-25-2023
                await driver.Page.GetByRole(AriaRole.Link, new() { Name = $"{(string)data.leaf}" }).ClickAsync();
                
            }
            await driver.Page.WaitForTimeoutAsync(1000);
            // Verify Header [Label].           
            string _htmltype = data.htmltype;
            string _label = data.label;
            string _locator = "//" + _htmltype + "[contains(text(), '" + _label + "')]";
            await driver.Page.Locator(_locator).Nth(0).IsVisibleAsync();
        }

        [Given(@"I navigated to features page for chosen environment hospital and department start with inventory zero for product non inspection non dynamic values")]
        public async void GivenINavigatedToFeaturesPageForChosenEnvironmentHospitalAndDepartmentStartWithInventoryZeroForProductNonInspectionNonDynamicValues(TechTalk.SpecFlow.Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            string root = data.root;
            string branch = data.branch;
            string leaf = data.leaf; 
            string catalogNoOrUpn = data.catalogNoOrUpn;
            string lotNumber = data.lotNumber;
            string serialNo = data.serialNo;
            string location = data.location;
            string rfid = data.rfid;

            string quantityOnProductSearch = string.Empty;

            Console.WriteLine("Started: " + DateTime.Now);
            ExcelUtility.readExcelDataFileName("DataFile", "Stage");
            // setting default timeout to 90 seconds, it is 30 by default
            driver.Page.SetDefaultTimeout(90000);
            await driver.Page.SetViewportSizeAsync(1620, 780);
            if (data.environment.Contains("Test"))
            {
                await driver.Page.GotoAsync(ExtractedValue.GetExtractedValue("baseUrl_Test"));
            }
            else if (data.environment.Contains("Stage"))
            {
                await driver.Page.GotoAsync(ExtractedValue.GetExtractedValue("baseUrl_Stage"));
            }
            // enter valid username and password 
            await loginPage.Login("Automation-SiteAdmin", SymmetricDecryptor.DecryptToString(ExtractedValue.GetExtractedValue("Password")));

            if (await driver.Page.Locator("//*[@id='lblModalHeading']").IsVisibleAsync())
            {
                await driver.Page.Locator("//*[@id='modalLaunch']/div/div[3]/button").ClickAsync(); // clicking to close hello new customer modal
            }
            // select hospital/dept
            await driver.Page.GetByRole(AriaRole.Combobox).SelectOptionAsync(new[] { (string)data.hospital + " - " + (string)data.department });

            await driver.Page.WaitForTimeoutAsync(500);
            // click on continue button 
            await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            await driver.Page.WaitForTimeoutAsync(1500);
            // search for product and verify it is zero quantity in inventory, if not, remove from inventory
            await driver.Page.GetByPlaceholder("Search").ClickAsync();
            await driver.Page.GetByPlaceholder("Search").FillAsync(catalogNoOrUpn);
            await driver.Page.Keyboard.PressAsync("Enter");
            await driver.Page.WaitForURLAsync(new Regex("qsight.net/product_search.aspx?"));
            quantityOnProductSearch = await driver.Page.Locator("//*[@id='rptHSR_ctl01_lblProductQuantity']").InnerTextAsync();
            int iQuantityOnProductSearch = Convert.ToInt32(quantityOnProductSearch);
            if (iQuantityOnProductSearch != 0)
            {
                await driver.Page.Locator("//*[@id='menu-collapse']//span[text()='Products']").WaitForAsync();
                await driver.Page.Locator("//*[@id='menu-collapse']//span[text()='Products']").ClickAsync();
                await driver.Page.GetByRole(AriaRole.Link, new() { Name = "Inventory " }).WaitForAsync();
                await driver.Page.GetByRole(AriaRole.Link, new() { Name = "Inventory " }).ClickAsync();
                await driver.Page.Locator("//*[@id='menu-collapse']//span[text()='Remove']").WaitForAsync();
                await driver.Page.Locator("//*[@id='menu-collapse']//span[text()='Remove']").ClickAsync();
                await driver.Page.WaitForTimeoutAsync(500);
                await driver.Page.Locator("//input[@id='upn']").WaitForAsync();
                await driver.Page.Locator("//input[@id='upn']").FillAsync(catalogNoOrUpn);
                await driver.Page.Locator("//input[@id='upn']").PressAsync("Tab");
                if (lotNumber != "x")
                {
                    await driver.Page.Locator("//input[@id='Lot']").FillAsync(lotNumber);
                    await driver.Page.Locator("//input[@id='Lot']").PressAsync("Tab");
                }
                await driver.Page.WaitForTimeoutAsync(250);
                if (serialNo != "x")
                {
                    await driver.Page.Locator("#txtSerial").FillAsync(serialNo);
                    //await driver.Page.Locator("#txtSerial").PressAsync("Tab");
                }
                await driver.Page.WaitForTimeoutAsync(250);
                if (location != "x")
                {                   
                    await driver.Page.Locator("//select[@id='locid']").SelectOptionAsync($"{location}"); 
                }
                await driver.Page.WaitForTimeoutAsync(250);

                if (rfid != "x")
                {
                    await driver.Page.Locator("#rfid").FillAsync(rfid);
                    await driver.Page.Locator("#rfid").PressAsync("Tab");
                }
                await driver.Page.WaitForTimeoutAsync(250);
                await driver.Page.Locator("//input[@id='qty']").FillAsync(quantityOnProductSearch);
                await driver.Page.Locator("//input[@id='RemoveButton']").ClickAsync();
                void page_Dialog_EventHandler(object sender, IDialog dialog)
                {
                    Console.WriteLine($"Dialog message: {dialog.Message}");
                    dialog.AcceptAsync();
                    driver.Page.Dialog -= page_Dialog_EventHandler;
                }
                driver.Page.Dialog += page_Dialog_EventHandler;
                await driver.Page.Locator("//input[@id='RemoveButton']").ClickAsync();
                await driver.Page.WaitForURLAsync(new Regex("qsight.net/hospital_remove_scan_entry.asp?"));
                await driver.Page.Locator("//h3[contains(text(),'Successfully Removed')]").IsVisibleAsync();
                Console.WriteLine($">>> Removed product with catalog or upn no: {catalogNoOrUpn} in amount of {quantityOnProductSearch} from inventory."); 
            }

            if (branch == "Alerts")
            {
                await driver.Page.GetByRole(AriaRole.Link, new() { Name = "Alerts" }).ClickAsync();
            }
            else
            {
                await driver.Page.WaitForTimeoutAsync(500);
                if ((string)data.root != "x")
                {
                    await driver.Page.Locator($"//*[@class='nav-link']//*[text()='{root}']").ClickAsync();
                    await driver.Page.WaitForTimeoutAsync(1500);
                }
                if ((string)data.branch != "x")
                {
                    await driver.Page.Locator($"//*[@class='dropdown-toggle nav-link']//*[text()='{branch}']").ClickAsync();
                    await driver.Page.WaitForTimeoutAsync(1500);
                }
                if ((string)data.leaf != "x")
                {
                    await driver.Page.Locator($"//*[@class='dropdown-item nav-link']//*[text()='{leaf}']").ClickAsync();
                    await driver.Page.WaitForTimeoutAsync(1500);
                }
            }
            await driver.Page.WaitForTimeoutAsync(1000);
        }

    }
}