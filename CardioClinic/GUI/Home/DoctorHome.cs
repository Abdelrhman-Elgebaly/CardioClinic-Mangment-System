using CardioClinic.GUI.Pages;
using CardioClinic.GUI.test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic.GUI.Home
{
    public partial class DoctorHome : Form
    {
        int account_id;
        public DoctorHome(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        private void DoctorHome_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            EditAccountPage editProfile = new EditAccountPage(account_id);
            editProfile.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            ViewReservations viewReservations = new ViewReservations(account_id);
            viewReservations.ShowDialog();
            Show();
        }
    }
}
