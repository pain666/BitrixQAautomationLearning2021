using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects
{
    class PortalHomePage
    {
        IWebDriver driver;
        public PortalLeftMenu LeftMenu => new PortalLeftMenu(driver);

        public PortalHomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
