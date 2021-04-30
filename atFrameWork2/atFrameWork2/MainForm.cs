using atFrameWork2.BaseFramework;
using atFrameWork2.TestCases;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atFrameWork2
{
    public partial class MainForm : Form
    {
        const string settingsFilePath = "settings.cfg";
        const string configSeparator = " >>>!=!=!=!>>> ";
        Button btnStart;
        RadioButton rbRemoteDriver;
        TextBox tbSelenoidHubUri;
        TextBox tbPortalUri = new TextBox();
        TextBox tbPortalLogin = new TextBox();
        TextBox tbPortalPassword = new TextBox();
        Dictionary<string, TextBox> settingsTextboxes;

        public MainForm()
        {
            InitializeComponent();
            CaseCollectionBuilder.ActivateTestCaseProvidersInstances();
            const int xPadding_px = 20;
            const int yPadding_px = 10;

            var caseTree = new TreeView()
            {
                Size = new Size(225, 240),
                Location = new Point(0 + xPadding_px, 0 + yPadding_px),
                CheckBoxes = true
            };

            CaseCollectionBuilder.CaseCollection.ForEach(x => caseTree.Nodes.Add(x.Node));

            btnStart = new Button()
            {
                Text = "Стартуем!",
                Size = new Size(280, 30),
                Location = new Point(caseTree.Location.X + caseTree.Width + xPadding_px, 0 + yPadding_px)
            };

            btnStart.Click += StartTests;

            var gbDriverType = new GroupBox()
            {
                Text = "Тип драйвера",
                Size = new Size(btnStart.Width, 100),
                Location = new Point(btnStart.Location.X, btnStart.Location.Y + btnStart.Height + yPadding_px)
            };

            RadioButton rbLocalDriver = new RadioButton()
            {
                Text = "Local",
                Size = new Size(gbDriverType.Width / 2 - xPadding_px - 5, 20),
                Location = new Point(xPadding_px, yPadding_px * 2),
                Checked = true,
            };

            rbRemoteDriver = new RadioButton()
            {
                Text = "Selenoid",
                Size = rbLocalDriver.Size,
                Location = new Point(rbLocalDriver.Location.X + rbLocalDriver.Width + xPadding_px, rbLocalDriver.Location.Y)
            };

            Label lbSelenoidHost = new Label()
            {
                Text = "Урл хаба селеноида:",
                Size = new Size(150, 20),
                Location = new Point(rbLocalDriver.Location.X, rbLocalDriver.Location.Y + rbLocalDriver.Height + yPadding_px)
            };

            tbSelenoidHubUri = new TextBox()
            {
                Size = new Size(gbDriverType.Width - xPadding_px * 2, 20),
                Location = new Point(xPadding_px, lbSelenoidHost.Location.Y + lbSelenoidHost.Height),
            };

            settingsTextboxes = new Dictionary<string, TextBox>
            {
                { "Хаб селеноида", tbSelenoidHubUri },
                { "Адрес портала", tbPortalUri},
                { "Логин портала", tbPortalLogin},
                { "Пароль портала", tbPortalPassword},
            };

            gbDriverType.Controls.Add(rbLocalDriver);
            gbDriverType.Controls.Add(rbRemoteDriver);
            gbDriverType.Controls.Add(lbSelenoidHost);
            gbDriverType.Controls.Add(tbSelenoidHubUri);
            //MainForm
            Controls.Add(caseTree);
            Controls.Add(btnStart);
            Controls.Add(gbDriverType);
            Height = (Size.Height - ClientSize.Height) + caseTree.Location.Y + caseTree.Height + yPadding_px;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Width = btnStart.Location.X + btnStart.Width + xPadding_px * 2;
            //Portal settings
            int lbOffsetY = yPadding_px;
            int rowHeight = 20;
            int lbWidth = 100;

            foreach (var settingTb in settingsTextboxes)
            {
                if (settingTb.Value != tbSelenoidHubUri)
                {
                    var lb = new Label()
                    {
                        Text = settingTb.Key,
                        Size = new Size(lbWidth, rowHeight),
                        Location = new Point(btnStart.Location.X, gbDriverType.Location.Y + gbDriverType.Height + lbOffsetY)
                    };

                    settingTb.Value.Width = btnStart.Width - lb.Width;
                    settingTb.Value.Height = lb.Height;
                    settingTb.Value.Location = new Point(lb.Location.X + lb.Width, lb.Location.Y);
                    Controls.Add(lb);
                    Controls.Add(settingTb.Value);
                    lbOffsetY += rowHeight + 5;
                }
            }

            ReadSettingsFromFile();
            foreach (var settingTb in settingsTextboxes)
                settingTb.Value.TextChanged += SaveSettingsToFile;
        }

        private void ReadSettingsFromFile()
        {
            if (File.Exists(settingsFilePath))
            {
                var configLines = File.ReadAllLines(settingsFilePath);

                foreach (var configLine in configLines)
                {
                    int separatorIndex = configLine.IndexOf(configSeparator);

                    if(separatorIndex > 0)
                    {
                        var settings = configLine.Split(configSeparator).ToList();
                        if(settings.Count > 1 && settingsTextboxes.ContainsKey(settings[0]))
                            settingsTextboxes[settings[0]].Text = settings[1];
                    }
                }
            }
        }

        private void SaveSettingsToFile(object sender, EventArgs e)
        {
            File.WriteAllText(settingsFilePath, "");
            foreach (var settingTb in settingsTextboxes)
                File.AppendAllText(settingsFilePath, settingTb.Key + configSeparator + settingTb.Value.Text + "\r\n");
        }

        private void StartTests(object sender, EventArgs e)
        {
            Uri.TryCreate(tbSelenoidHubUri.Text, UriKind.Absolute, out Uri selenoidHubUri);

            if (rbRemoteDriver.Checked && selenoidHubUri == default)
                MessageBox.Show(this, "Некорректный урл хаба селеноида", "Внимательнее будь, падаван", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if(settingsTextboxes.Any(x => string.IsNullOrEmpty(x.Value.Text)))
                    MessageBox.Show(this, "Надо заполнить все поля для ввода", "Что же ты творишь, падаван", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Uri.TryCreate(tbPortalUri.Text, UriKind.Absolute, out Uri portalUri);

                    if(portalUri == default)
                        MessageBox.Show(this, "Невалидный адрес портала", "Возьми себя в руки, падаван", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        var admin = new User() { Login = tbPortalLogin.Text, Password = tbPortalPassword.Text };
                        var testPortal = new PortalInfo(portalUri, admin);
                        var selectedCases = CaseCollectionBuilder.CaseCollection.FindAll(x => x.Node.Checked);

                        var mainTask = Task.Run(() =>
                        {
                            foreach (var caseToRun in selectedCases)
                                caseToRun.Execute(rbRemoteDriver.Checked ? selenoidHubUri : default, testPortal);
                            BeginInvoke(new MethodInvoker(() => btnStart.Enabled = true));
                        });

                        btnStart.Enabled = false;
                    }
                }
            }
        }
    }
}
