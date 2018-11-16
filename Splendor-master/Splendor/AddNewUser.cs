using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splendor
{
    public partial class AddNewUser : Form
    {
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void cmdNewUser_Click(object sender, EventArgs e)
        {
            ConnectionDB db = new ConnectionDB();
           
            Player player1 = new Player();



        }
    }
}
