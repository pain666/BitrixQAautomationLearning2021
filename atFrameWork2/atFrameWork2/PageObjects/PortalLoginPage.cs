using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace atFrameWork2.PageObjects
{
    class PortalLoginPage
    {
        IWebDriver driver;
        PortalInfo portalInfo;

        public PortalLoginPage(IWebDriver webDriver, PortalInfo portal)
        {
            this.driver = webDriver;
            portalInfo = portal;
        }

        public PortalHomePage Login(User admin)
        {
            DriverActions.OpenUri(driver, portalInfo.PortalUri);
            var loginField = new WebItem("//input[@id='login']", "Поле для ввода логина");
            var pwdField = new WebItem("//input[@id='password']", "Поле для ввода пароля");
            loginField.SendKeys(admin.Login, driver);
            Thread.Sleep(1000);
            loginField.SendKeys(Keys.Enter, driver);
            pwdField.SendKeys(admin.Password + Keys.Enter, driver);
            Thread.Sleep(1000);
            pwdField.SendKeys(Keys.Enter, driver);
            return new PortalHomePage(driver);
        }
    }
}
