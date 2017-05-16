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
    public partial class Menu : UserControl
    {
        public delegate void customHandler(object sender);
        public event customHandler send;

        public Menu()
        {
            InitializeComponent();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        protected void MenuItem_Click(object sender, EventArgs e)
        {
            send(sender);
        }
    }
}
