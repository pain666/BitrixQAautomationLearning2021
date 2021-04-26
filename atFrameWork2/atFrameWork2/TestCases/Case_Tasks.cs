using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace atFrameWork2.TestCases
{
    class Case_Tasks
    {
        public static void CreateTask()
        {
            var driver = DriverActions.GetNewDriver();
            var admin = new User() { Login = "qa-at-learning@mail.bx24.net", Password = "^YHN7ujm*IK<" };
            var portal = new PortalInfo(new Uri("https://bxprod77.bitrix24.ru/"), admin);
            //залогинится
            DriverActions.OpenUri(driver, portal.PortalUri);
            var loginField = new WebItem("//input[@id='login']", "Поле для ввода логина");
            var pwdField = new WebItem("//input[@id='password']", "Поле для ввода пароля");
            loginField.SendKeys(admin.Login, driver);
            Thread.Sleep(2000);
            loginField.SendKeys(Keys.Enter, driver);
            pwdField.SendKeys(admin.Password + Keys.Enter, driver);
            var leftMenuItemTask = new WebItem("//li[@id='bx_left_menu_menu_tasks']", "Пункт левого меню 'Задачи'");
            leftMenuItemTask.Click(driver);
            var btnAddTask = new WebItem("//a[@id='tasks-buttonAdd']", "Кнопка добавления задачи");
            btnAddTask.Click(driver);
            var sliderFrame = new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм слайдера");
            sliderFrame.SwitchToFrame(driver);
            var inputTaskTitle = new WebItem("//input[@data-bx-id='task-edit-title']", "Текстбокс названия задачи");
            var task = new Bitrix24Task("testTasks" + DateTime.Now.Ticks) { Description = "Какой то дескрипгш" + +DateTime.Now.Ticks };
            inputTaskTitle.SendKeys(task.Title, driver);
            var editorFrame = new WebItem("//iframe[@class='bx-editor-iframe']", "Фрейм редактора текста");
            editorFrame.SwitchToFrame(driver);
            var body = new WebItem("//body", "Это просто бади какой то");
            body.SendKeys(task.Description, driver);
            driver.SwitchTo().DefaultContent();//TODO добавить метод свича в дефолтконтент в драйверАшнс
            sliderFrame.SwitchToFrame(driver);

            //в меню ткнуть в задачи
            //в гриде ткнуть доллбавить задачу 
            //свичнуться в слайдер
            //ввести тайтл и описание
            //сохранить, закрыть слайдер, рефрешнуть,
            //открыть задачу, ассертнуть тайтл и дескрипшн
        }
    }
}
