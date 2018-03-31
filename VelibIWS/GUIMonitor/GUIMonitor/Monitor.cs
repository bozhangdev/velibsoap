using GUIMonitor.MonitorRef;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIMonitor
{
    public partial class Monitor : Form
    {
        Button confirmButton;
        Button returnButton;
        MonitorClient client;
        RadioButton getDelay;
        RadioButton getRequestToVelib;
        RadioButton getRequestFromClient;
        RadioButton getCache;
        TextBox startTime;
        TextBox endTime;
        Panel panel = new Panel();
        int choice = 0;
        string result;
        public Monitor()
        {
            InitializeComponent();
            CreateWelcomePanel();
            this.Height = 400;
            this.Width = 400;
            returnButton = new Button();
            returnButton.Text = "return";
            returnButton.Size = new Size(50, 30);
            returnButton.Location = new Point(0, 0);
            returnButton.Click += new EventHandler(ReturnButtonClick);
            client = new MonitorClient();
            result = "test";
            Controls.Add(InitReturnPanel());
        }

        private Panel InitReturnPanel()
        {
            Panel returnPanel = new Panel();
            returnPanel.Size = new Size(400, 40);
            returnPanel.Location = new Point(0, 0);
            returnPanel.Controls.Add(returnButton);
            return returnPanel;
        }

        private void CreateWelcomePanel()
        {
            choice = 0;
            Panel welcomePanel = new Panel();
            welcomePanel.Size = new Size(400, 300);
            welcomePanel.Location = new Point(0, 40);

            Size radioButtonSize = new Size(200, 40);

            Label welcomeLabel = new Label();
            welcomeLabel.Text = "Welcome to Velib Monitor\nWhat do yo want to do?";
            welcomeLabel.Size = radioButtonSize;
            welcomeLabel.Location = new Point((welcomePanel.Width - welcomeLabel.Width) / 2, (welcomePanel.Height - welcomeLabel.Height) / 2 - 90);

            getDelay = new RadioButton();
            getDelay.Size = radioButtonSize;
            getDelay.Location = new Point((welcomePanel.Width - getDelay.Width) / 2, (welcomePanel.Height - getDelay.Height) / 2 - 60);
            getDelay.Text = "Get the average of delay";

            getRequestToVelib = new RadioButton();
            getRequestToVelib.Size = radioButtonSize;
            getRequestToVelib.Location = new Point((welcomePanel.Width - getRequestToVelib.Width) / 2, (welcomePanel.Height - getRequestToVelib.Height) / 2 - 25);
            getRequestToVelib.Text = "Get the number of requests to Velib server during a period";

            getRequestFromClient = new RadioButton();
            getRequestFromClient.Size = radioButtonSize;
            getRequestFromClient.Location = new Point((welcomePanel.Width - getRequestToVelib.Width) / 2, (welcomePanel.Height - getRequestToVelib.Height) / 2 + 10);
            getRequestFromClient.Text = "Get the number of requests from client during a period";

            getCache = new RadioButton();
            getCache.Size = radioButtonSize;
            getCache.Location = new Point((welcomePanel.Width - getCache.Width) / 2, (welcomePanel.Height - getCache.Height) / 2 + 45);
            getCache.Text = "Get the quantity of cache in IWS";

            confirmButton = new Button();
            confirmButton.Size = new Size(200, 50);
            confirmButton.Location = new Point((welcomePanel.Width - confirmButton.Width) / 2, (welcomePanel.Height - confirmButton.Height) / 2 + 100);
            confirmButton.Text = "Confirm";
            confirmButton.Click += new EventHandler(ConfirmButtonClick);

            welcomePanel.Controls.Add(getDelay);
            welcomePanel.Controls.Add(getRequestToVelib);
            welcomePanel.Controls.Add(confirmButton);
            welcomePanel.Controls.Add(welcomeLabel);
            welcomePanel.Controls.Add(getRequestFromClient);
            welcomePanel.Controls.Add(getCache);

            panel.Visible = false;
            panel = welcomePanel;
            Controls.Add(panel);
            panel.Visible = true;
        }


        private void CreateSearchPanel()
        {
            Panel searchPanel = new Panel();
            searchPanel.Size = new Size(400, 200);
            searchPanel.Location = new Point((this.Width - searchPanel.Width) / 2, (this.Height - searchPanel.Height) / 2);

            startTime = new TextBox();
            startTime.Size = new Size(300, 40);
            startTime.Text = "Please input the start time (yymmddhhmmss)";
            startTime.Location = new Point((searchPanel.Width - startTime.Width) / 2, (searchPanel.Height - startTime.Height) / 2 - 50);

            endTime = new TextBox();
            endTime.Size = new Size(300, 40);
            endTime.Text = "Please input the end time (yymmddhhmmss)";
            endTime.Location = new Point((searchPanel.Width - endTime.Width) / 2, (searchPanel.Height - endTime.Height) / 2 - 20);

            confirmButton = new Button();
            confirmButton.Size = new Size(200, 50);
            confirmButton.Text = "Confirm";
            confirmButton.Click += new EventHandler(ConfirmSearchButtonClick);
            confirmButton.Location = new Point((searchPanel.Width - confirmButton.Width) / 2, (searchPanel.Height - confirmButton.Height) / 2 + 50);
            searchPanel.Controls.Add(startTime);
            searchPanel.Controls.Add(endTime);
            searchPanel.Controls.Add(confirmButton);

            panel.Visible = false;
            panel = searchPanel;
            Controls.Add(panel);
            panel.Visible = true;
        }

        private void CreateResultPanel()
        {
            Panel resultPanel = new Panel();
            resultPanel.Size = new Size(300, 400);
            resultPanel.Location = new Point((this.Width - resultPanel.Width) / 2, (this.Height - resultPanel.Height) / 2);
            Label resultLabel = new Label();
            resultPanel.Controls.Add(resultLabel);
            resultLabel.AutoSize = true;
            resultPanel.AutoSize = false;
            resultPanel.AutoScroll = true;
            resultLabel.MaximumSize = new Size(300, 0);
            resultLabel.Location = new Point(0, 80);
            resultLabel.Text = result;

            panel.Visible = false;
            panel = resultPanel;
            Controls.Add(panel);
            panel.Visible = true;
        }

        private void ConfirmButtonClick(object sender, EventArgs args)
        {
            if (getDelay.Checked)
            {
                result = "The average of delay is: " + client.GetDelay() + "ms";
                CreateResultPanel();
            }
            if (getRequestFromClient.Checked)
            {
                choice = 2;
                CreateSearchPanel();
            }
            if (getRequestToVelib.Checked)
            {
                choice = 1;
                CreateSearchPanel();
            }
            if (getCache.Checked)
            {
                result = "The quantity of cache is: " + client.GetCacheNumber();
                CreateResultPanel();
            }
            
        }

        private void ConfirmSearchButtonClick(object sender, EventArgs args)
        {
            if (startTime.Text == "" || endTime.Text == "")
            {
                MessageBox.Show("Please input a time");
                return;
            }
            if (choice == 2)
            {
                if(startTime.Text == "" || endTime.Text == "")
                {
                    MessageBox.Show("Please input a time");
                    return;
                }
                result = client.GetRequestFromClient(startTime.Text, endTime.Text);
            }
            if (choice == 1)
            {
                result = client.GetRequestToVelib(startTime.Text, endTime.Text);
            }
            CreateResultPanel();
        }

        private void ReturnButtonClick(object sender, EventArgs args)
        {
            CreateWelcomePanel();
        }

    }
}
