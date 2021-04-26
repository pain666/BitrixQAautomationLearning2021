using atFrameWork2.BaseFramework;
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
        Button btnDo = new Button()
        {
            Text = "Нажми меня скорее",
            Size = new Size(200, 100),
        };
        public RichTextBox tb = new RichTextBox() { Size = new Size(150, 150), Location = new Point(200, 0) };

        public MainForm()
        {
            InitializeComponent();
            CaseCollectionBuilder.ActivateTestCaseProvidersInstances();
            //TODO в этом месте генерация интерфейса дерева кейсов

            btnDo.Click += BtnDo_Click;
            this.Controls.Add(btnDo);
            this.Controls.Add(tb);
        }

        private void BtnDo_Click(object sender, EventArgs e)
        {
            //TODO тут проверка галок на отмеченность и запуск управляющего ходом тестов потока с отмеченными кейсами
        }
    }
}
