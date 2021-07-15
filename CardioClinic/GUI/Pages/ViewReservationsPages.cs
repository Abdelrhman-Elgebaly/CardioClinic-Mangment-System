using CardioClinic.BAL;
using CardioClinic.BLL;
using CardioClinic.DAL;
using CardioClinic.GUI.Pages;
using Microsoft.Reporting.WinForms;
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

namespace CardioClinic.GUI.test
{
    public partial class ViewReservations : Form
    {
        int account_id, account_type;
        ReservationDAL reservationDAL = new ReservationDAL();
        public ViewReservations(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;

      
        private void ViewReservations_Load(object sender, EventArgs e)
        {
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
           
            command = con.CreateCommand();
            command.CommandText = "SELECT account_type FROM account WHERE account_id = @id";
            command.Parameters.AddWithValue("@id", account_id);
            con.Open();
            account_type = (int)command.ExecuteScalar();
            con.Close();
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a reservation!");
                return;
            }

           // reservation res = (reservation)listBox1.SelectedItem;

            Hide();
           // EditReservation editReservation = new EditReservation(res);
           // editReservation.ShowDialog();
            //  updateList();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a reservation!");
                return;
            }

            ReservationBLL res = (ReservationBLL)listBox1.SelectedItem;
            Hide();
            VisitsPage visits = new VisitsPage(account_id, res.patient.Key, res.id);
            visits.ShowDialog();
            Show();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a reservation!");
                return;
            }

            ReservationBLL res = (ReservationBLL)listBox1.SelectedItem;

            Hide();
            EditReservatinsPage editReservation = new EditReservatinsPage(res);
             editReservation.ShowDialog();
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
            Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a reservation!");
                return;
            }

            ReservationBLL res = (ReservationBLL)listBox1.SelectedItem;
            Hide();
            VisitsPage visits = new VisitsPage(account_id, res.patient.Key, res.id);
            visits.ShowDialog();
            Show();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            updateForm();
        }

        private void groupBox2_Enter_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            


            Hide();
            Form1 editReservation = new Form1();
            editReservation.ShowDialog();
           // reservationDAL.updateReservationList(radioButton1, radioButton2, dateTimePicker1, textBox1, listBox1);
            Show();
        }

        private void updateForm()
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a reservation!");
                return;
            }

           ReservationBLL  res = (ReservationBLL)listBox1.SelectedItem;
            textBox2.Text = res.id.ToString();
            textBox3.Text = res.patient.ToString();
            textBox4.Text = res.secretary.ToString();
            textBox5.Text = res.visit_date.Date.ToString();
            textBox6.Text = utils.getSlots()[res.slot];
            textBox7.Text = res.date.ToString();

            if (account_type == 0 && res.visit_date >= DateTime.Today)
                button1.Enabled = true;
            else
                button1.Enabled = false;

            if (account_type == 1)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }
    }
}

