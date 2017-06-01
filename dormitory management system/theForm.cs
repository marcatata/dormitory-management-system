using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dormitory_management_system
{
    public partial class theForm : Form
    {
        public theForm()
        {
            InitializeComponent();
        }

        private void theForm_Load(object sender, EventArgs e)
        {
            menu.send += new Menu.customHandler(Browse);
            home.edit += new Home.customHandler(edit);

            add.Visible = false;
            search.Visible = true;
            home.Visible = true;
            bills1.Visible = false;
        }
        void Browse(object sender)
        {
            ToolStripMenuItem menuButton = (ToolStripMenuItem)sender;
            if (menuButton.Text.Contains("Home"))
            {
                add.Visible = false;
                search.Visible = true;
                home.Visible = true;
                bills1.Visible = false;
            }
            else if (menuButton.Text.Contains("Наематели"))
            {
                add.Visible = true;
                search.Visible = false;
                home.Visible = false;
                bills1.Visible = false;
            }
            else if (menuButton.Text.Contains("Месечни такси"))
            {
                add.Visible = false;
                search.Visible = false;
                home.Visible = false;
                bills1.Visible = true;
            }
        }

        private void edit(int renterID)
        {
            add.edit(renterID);
            ToolStripMenuItem menuButton = new ToolStripMenuItem();
            menuButton.Text = "Наематели";
            Browse(menuButton);
        }
    }
}
