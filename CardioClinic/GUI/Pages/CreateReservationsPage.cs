using CardioClinic.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic.GUI.Pages
{
    public partial class CreateReservationsPage : Form
    {
        int secretary_id;
        public CreateReservationsPage(int id)
        {
            InitializeComponent();
            secretary_id = id;
        }
      
        ReservationDAL reservationDAL = new  ReservationDAL();
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CreateReservationsPage_Load(object sender, EventArgs e)
        {
            reservationDAL.updateListOfPatients("",listBox1);
            reservationDAL.updateSlots(dateTimePicker1,comboBox1);
            dateTimePicker1.MinDate = DateTime.Today;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            reservationDAL.updateListOfPatients(textBox1.Text, listBox1);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            reservationDAL.updateSlots(dateTimePicker1, comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reservationDAL.createReservation(listBox1, comboBox1, dateTimePicker1, secretary_id);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
