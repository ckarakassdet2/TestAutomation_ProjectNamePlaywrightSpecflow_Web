using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class LoginPage
    {
        private IPage ipage;

        public LoginPage(IPage page) => ipage = page;

        private ILocator _txtUsername => ipage.Locator("#UserName");
        private ILocator _txtPassword => ipage.Locator("#Password");

        private ILocator _btnLogin => ipage.Locator("#btnSubmit");

        private ILocator _linkLogout => ipage.Locator("//*[@title='Log Out' or @alt='log out']");

        private ILocator _linkLogoutAdmin => ipage.Locator("xpath=//*[@id='headerBar']/nav/ul/li[2]/a");

        private ILocator _linkForgotPassword => ipage.Locator("#lnkForgetPassword"); 

        public async Task ClickLogin() => await _btnLogin.ClickAsync();

        public async Task Login(string username, string password)
        {
            await _txtUsername.FillAsync(username); 
            await _txtPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
        }

        public async Task ClickLogout() => await _linkLogout.ClickAsync();

        public async Task ClickLogoutAdmin() => await _linkLogoutAdmin.ClickAsync();

        public async Task VerifyLoggedout()
        {
            await _linkForgotPassword.WaitForAsync(); 
            await _linkForgotPassword.IsVisibleAsync();
        }


    }
}
