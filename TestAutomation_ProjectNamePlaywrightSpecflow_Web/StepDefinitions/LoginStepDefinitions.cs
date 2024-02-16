using Microsoft.Playwright;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    [Binding]
    public class LoginStepDefinitions 
    {
        private readonly Driver driver;
        private readonly LoginPage loginPage; 
        public LoginStepDefinitions(Driver driver) 
        {
            this.driver = driver;
            loginPage = new LoginPage(this.driver.Page); 
        }

        [Given(@"I navigate to QSight application")]
        public void GivenINavigateToQSightApplication()
        {
            //driver.Page.SetViewportSizeAsync(1920, 1080); 
            driver.Page.GotoAsync("https://www.example.com"); 
        }

        [Given(@"I enter valid username password  and click on login")]
        public async Task GivenIEnterValidUsernamePasswordAndClickOnLogin(Table table)
        {
            dynamic data = table.CreateDynamicInstance(false);
            string password = SymmetricDecryptor.DecryptToString((string)data.password); 
            await loginPage.Login((string)data.username, password);
        }

        [Then(@"I select department")]
        public async Task ThenISelectDepartment()
        {
            await driver.Page.GetByRole(AriaRole.Combobox).SelectOptionAsync(new[] { "General Hosp QA - Catheterization Lab" }); // select department "Cath Lab" 
        }

        [Then(@"I click on Continue button")]
        public async Task ThenIClickOnContinueButton()
        {
            await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        }

        [Then(@"I close training video")]
        public async Task ThenICloseTrainingVideo()
        {
            if (await driver.Page.GetByText("Close Video").IsVisibleAsync())
            {
                await driver.Page.GetByText("Close Video").ClickAsync(); 
            }
        }

        [Then(@"I verify landing page Add Product to Hospital Inventory")]
        public async Task ThenIVerifyLandingPageAddProductToHospitalInventory()
        {          
            Assert.AreEqual("Add Product to Hospital Inventory",await driver.Page.Locator("#page-wrapper > div > div.page-header > h1").InnerTextAsync());
        }

        [Then(@"I click on logout link")]
        public async Task ThenIClickOnLogoutLink()
        {
            await loginPage.ClickLogout(); 
        }

        // This is a copy of above logout method for in cases we need it as when statement
        [When(@"I click on logout link")]
        public async Task WhenIClickOnLogoutLink()
        {
            await loginPage.ClickLogout();
        }

        [Then(@"I verify I am logged out")]
        public async Task ThenIVerifyIAmLoggedOut()
        {
            await driver.Page.WaitForURLAsync("**/?requestFrom=QSight"); 
        }
    }
}
