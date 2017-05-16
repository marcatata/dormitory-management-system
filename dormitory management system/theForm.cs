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

            add.Visible = false;
            search.Visible = true;
            home.Visible = true;
        }
        void Browse(object sender)
        {
            ToolStripMenuItem menuButton = (ToolStripMenuItem)sender;
            if (menuButton.Text.Contains("Home"))
            {
                add.Visible = false;
                search.Visible = true;
                home.Location = new Point(9, 98);
                home.Size = new Size(home.Size.Width, ClientRectangle.Height - home.Size.Height - home.Size.Height - 9 * 3 + 2);
                home.Visible = true;
            }
            else if (menuButton.Text.Contains("наематели"))
            {
                add.Visible = true;
                search.Visible = false;
                home.Visible = false;
            }
        }
    }
}
