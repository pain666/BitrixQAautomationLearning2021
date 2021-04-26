using atFrameWork2.PageObjects.LeftMenu;
using atFrameWork2.SeleniumFramework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects
{
    class PortalLeftMenu
    {
        IWebDriver driver;

        public PortalLeftMenu(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static PortalLeftMenuItem Tasks =>
            new PortalLeftMenuItem("Задачи", new WebItem("//li[@id='bx_left_menu_menu_tasks']", "Пункт левого меню 'Задачи'"));

        public void OpenSection(PortalLeftMenuItem menuItem)
        {
            menuItem.Select(driver);
        }
    }
}
