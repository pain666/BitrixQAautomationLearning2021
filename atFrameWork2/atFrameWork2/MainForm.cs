using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atFrameWork2
{
    public partial class MainForm : Form
    {
        public static MainForm form;
        
        Button btnDo = new Button()
        {
            Text = "Нажми меня скорее",
            Size = new Size(200, 100),
        };
        public RichTextBox tb = new RichTextBox() { Size = new Size(150, 150), Location = new Point(200, 0) };

        public MainForm()
        {
            InitializeComponent();
            form = this;
            btnDo.Click += BtnDo_Click;
            this.Controls.Add(btnDo);
            this.Controls.Add(tb);
        }

        private void BtnDo_Click(object sender, EventArgs e)
        {
            string portalUrl = "https://yandex.ru";
            driver = new ChromeDriver();
            var chromeDriver = driver;
            //chromeDriver.Navigate().GoToUrl(portalUrl);
            chromeDriver.Manage().Window.Maximize();
            IWebElement searchInput = chromeDriver.FindElement(By.XPath("//div[contains(@class, 'search')]//input[@id='text']"));
            searchInput.SendKeys("как писать автотесты" + OpenQA.Selenium.Keys.Enter);
            //IWebElement searchSubmitBtn = chromeDriver.FindElement(By.XPath("//button[@type='submit']"));
            //searchSubmitBtn.Click();


            var taskFrame = chromeDriver.FindElement(By.XPath("//iframe[@class='side-panel-iframe']"));
            chromeDriver.SwitchTo().Frame(taskFrame);
            var delBtn = chromeDriver.FindElement(By.XPath("//span[contains(@class,'task-form-field-item-delete')]"));
            delBtn.Click();
            chromeDriver.SwitchTo().DefaultContent();
        }

        static IWebDriver driver = default;
    }
}
