using atFrameWork2.BaseFramework;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace atFrameWork2.TestCases
{
    class Case_Tasks : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            return new List<TestCase>
            {
                new TestCase("Создание задачи", (driver, homePage) => CreateTask(driver, homePage)),
                new TestCase("Редактирование задачи", (driver, homePage) => throw new NotImplementedException("Заглушка теста редактирования задачи")),
                new TestCase("Удаление задачи", (driver, homePage) => throw new NotImplementedException("Заглушка теста удаления задачи")),
            };
        }

        public static void CreateTask(IWebDriver driver, PortalHomePage homePage)
        {
            homePage.LeftMenu
                .OpenSection(PortalLeftMenu.Tasks);
            
            //TODO переписать на пейджобдждект как сверху
            //
            //в гриде ткнуть доллбавить задачу 
            var btnAddTask = new WebItem("//a[@id='tasks-buttonAdd']", "Кнопка добавления задачи");
            btnAddTask.Click(driver);
            //свичнуться в слайдер
            var sliderFrame = new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм слайдера");
            sliderFrame.SwitchToFrame(driver);
            //ввести тайтл и описание
            var inputTaskTitle = new WebItem("//input[@data-bx-id='task-edit-title']", "Текстбокс названия задачи");
            var task = new Bitrix24Task("testTasks" + DateTime.Now.Ticks) { Description = "Какой то дескрипгш" + +DateTime.Now.Ticks };
            inputTaskTitle.SendKeys(task.Title, driver);
            var editorFrame = new WebItem("//iframe[@class='bx-editor-iframe']", "Фрейм редактора текста");
            editorFrame.SwitchToFrame(driver);
            var body = new WebItem("//body", "Это просто бади какой то");
            body.SendKeys(task.Description, driver);
            driver.SwitchTo().DefaultContent();
            sliderFrame.SwitchToFrame(driver);
            //сохранить 
            var btnSaveTask = new WebItem("//button[@data-bx-id='task-edit-submit' and @class='ui-btn ui-btn-success']", "Кнопка сохранения задачи");
            btnSaveTask.Click(driver);
            driver.SwitchTo().DefaultContent();
            var gridTaskLink = new WebItem($"//a[contains(text(), '{task.Title}') and contains(@class, 'task-title')]", 
                $"Ссылка на задачу '{task.Title}' в гриде");
            gridTaskLink.Click(driver);
            sliderFrame.SwitchToFrame(driver);
            //открыть задачу, ассертнуть тайтл и дескрипшн
            var taskTitleArea = new WebItem($"//div[@class='tasks-iframe-header']//span[@id='pagetitle']",
                "Область заголовка задачи"); 
            taskTitleArea.WaitElementDisplayed(driver, 10);
            taskTitleArea.AssertTextContains(driver, task.Title, "Название задачи отображается неверно");
            var taskDescriptionArea = new WebItem($"//div[@id='task-detail-description']",
                "Область описания задачи");
            taskDescriptionArea.AssertTextContains(driver, task.Description, "Название задачи отображается неверно");
        }
    }
}
