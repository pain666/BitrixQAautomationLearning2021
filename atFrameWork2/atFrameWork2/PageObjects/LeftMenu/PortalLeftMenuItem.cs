using atFrameWork2.SeleniumFramework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects.LeftMenu
{
    class PortalLeftMenuItem
    {
        WebItem menuItem;

        public PortalLeftMenuItem(string title, WebItem menuItem)
        {
            Title = title;
            this.menuItem = menuItem;
        }

        public string Title { get; }

        public void Select(IWebDriver driver)
        {
            menuItem.Click(driver);
        }
    }
}
