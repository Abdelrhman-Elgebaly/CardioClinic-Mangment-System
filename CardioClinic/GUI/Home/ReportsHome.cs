using CardioClinic.GUI.Pages;
using CardioClinic.GUI.Reports;
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
    public partial class ReportsHome : Form
    {
        public ReportsHome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            StaffReport editReservation = new StaffReport();
            editReservation.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            PatientsReport editReservation = new PatientsReport();
            editReservation.ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form110 editReservation = new Form110();
            editReservation.ShowDialog();
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            VisitsReport editReservation = new VisitsReport();
            editReservation.ShowDialog();
            Show();
        }
    }
}
