using System;
using System.Windows.Forms;

namespace LabWakeUp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.LoadServerList();
            LoadDropDown();
        }

        public void LoadDropDown()
        {
            ServerName.DataSource = Program.DataSource;
            ServerName.DisplayMember = "Name";
            ServerName.ValueMember = "Value";
            ServerName.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Shutdown_Server_Click(object sender, EventArgs e)
        {
            Program.ShutDownMachine(ServerName.Text, userName.Text, password.Text);
        }

        private void Start_Server_Click(object sender, EventArgs e)
        {
            Program.WakeUp(ServerName.SelectedValue.ToString());
        }
    }
}

