using CardioClinic.GUI.Pages;
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
    public partial class SecretaryHome : Form
    {
        int account_id;
        public SecretaryHome(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        private void SecretaryHome_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            EditAccountPage editProfile = new EditAccountPage(account_id);
            editProfile.ShowDialog();
            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            PatientPage patientProfiles = new PatientPage();
            patientProfiles.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            ReservationHome Reservation = new ReservationHome(account_id);
            Reservation.ShowDialog();
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            Form editReservation = new Form1();
            editReservation.ShowDialog();
            // reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
            Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Hide();
            ReportsHome editReservation = new ReportsHome();
            editReservation.ShowDialog();
            Show();
        }
    }
}
