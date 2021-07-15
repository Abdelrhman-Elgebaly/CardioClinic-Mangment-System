using CardioClinic.BLL;
using CardioClinic.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic.GUI.Pages
{
    public partial class EditReservatinsPage : Form
    {
        ReservationDAL reservationDAL = new ReservationDAL();
   
        public EditReservatinsPage(ReservationBLL res)
        {
            InitializeComponent();
            dateTimePicker1.Value = res.visit_date;

            textBox1.Text = res.id.ToString();
            textBox2.Text = res.patient.ToString();
            textBox3.Text = res.secretary.ToString();
            textBox4.Text = res.date.ToString();

            dateTimePicker1.MinDate = DateTime.Today;
            reservationDAL.updateCombo(res.slot,dateTimePicker1,textBox1,comboBox1);
        }

        private void EditReservatinsPage_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            reservationDAL.updateCombo(1, dateTimePicker1, textBox1, comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reservationDAL.updateReservation(dateTimePicker1,textBox1,comboBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reservationDAL.deleteReservation(dateTimePicker1, textBox1, comboBox1);
        }
    }
}
