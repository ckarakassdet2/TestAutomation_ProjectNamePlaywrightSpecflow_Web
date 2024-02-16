using Microsoft.Playwright;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;

        public Driver()
        {
            _page = InitializePlaywright();
        }

        public IPage Page => _page.Result; // to be able to use _page we need Result method because it is a Task in CSharp

        public void Dispose() => _browser?.CloseAsync();

        private async Task<IPage> InitializePlaywright()
        {
            // Playwright initition
            var playwright = await Playwright.CreateAsync();

            // browser initiation 
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1500,
                //Channel = "msedge"
            });
            return await _browser.NewPageAsync();
        }
    }
}

