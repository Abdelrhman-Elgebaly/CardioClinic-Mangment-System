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
    public partial class ReservationHome : Form
    {
        int account_id;
        public ReservationHome(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateReservationsPage home = new CreateReservationsPage(account_id);
            home.ShowDialog();
            home.Show();
        }

        private void ReservationHome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ViewReservations viewReservations = new ViewReservations(account_id);
            viewReservations.ShowDialog();
            Show();
        }
    }
}
