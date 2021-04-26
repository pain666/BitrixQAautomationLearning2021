using atFrameWork2.TestCases;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
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
            Case_Tasks.CreateTask();
        }

        static IWebDriver driver = default;
    }
}
