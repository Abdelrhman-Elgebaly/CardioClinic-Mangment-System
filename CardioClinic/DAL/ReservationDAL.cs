using CardioClinic.BAL;
using CardioClinic.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic.DAL
{
    class ReservationDAL
    {
       
        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;
        // Creation of Reservation
        public void updateListOfPatients(string query, ListBox listBox1)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT account_id, account_name, account_type FROM account WHERE account_type = 2 AND (account_name LIKE @query OR account_phone LIKE @query)";
            command.Parameters.AddWithValue("@query", query + "%");

            con.Open();
            listBox1.Items.Clear();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                listBox1.Items.Add(new account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));

            con.Close();
        }
        public void updateSlots(DateTimePicker dateTimePicker1, ComboBox comboBox1)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT reservation_visit_slot FROM reservation WHERE reservation_visit_date=@date";
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            con.Open();

            SqlDataReader reader = command.ExecuteReader();

            Dictionary<int, string> slots = utils.getSlots();

            while (reader.Read())
            {
                slots.Remove(reader.GetInt32(0));
            }
            comboBox1.Items.Clear();
            foreach (object slot in slots.ToArray())
                comboBox1.Items.Add(slot);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            con.Close();
        }
        public void createReservation(ListBox listBox1, ComboBox comboBox1, DateTimePicker dateTimePicker1,int secretary_id)
        {
            //Inputs Validation
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a patient!");
                return;
            }
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a slot!");
                return;
            }

            //Perform the reservation
            int patient_id = ((account)listBox1.SelectedItem).getID();
            int slot = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

            command = con.CreateCommand();
            command.CommandText = "INSERT INTO reservation (reservation_secretary_id, reservation_patient_id, reservation_visit_date, reservation_visit_slot, reservation_date) VALUES (@secretary_id, @patient_id, @visit_date, @visit_slot, @date)";
            command.Parameters.AddWithValue("@secretary_id", secretary_id);
            command.Parameters.AddWithValue("@patient_id", patient_id);
            command.Parameters.AddWithValue("@visit_date", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@visit_slot", slot);
            command.Parameters.AddWithValue("@date", DateTime.Now);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
            {
                //We successfully performed the reservation
                command.CommandText = "SELECT reservation_id FROM reservation WHERE reservation_visit_date=@visit_date AND reservation_visit_slot=@visit_slot";
                int reservation_id = (int)command.ExecuteScalar();

                MessageBox.Show("Reservation was made!");
                MessageBox.Show("Reservation ID:" + reservation_id.ToString());
            }
            else
                MessageBox.Show("Failed to perform the reservation!");

            con.Close();
            updateSlots(dateTimePicker1, comboBox1);
        }


        // view  of Reservation

        public void updateReservationList(RadioButton radioButton1, RadioButton radioButton2, DateTimePicker dateTimePicker1, TextBox textBox1 , ListBox listBox1 )
        {
            command = con.CreateCommand();
            if (radioButton1.Checked)
                command.CommandText = "SELECT reservation_id, reservation_patient_id, patient.account_name, reservation_secretary_id, secretary.account_name, reservation_visit_date, reservation_visit_slot, reservation_date FROM reservation, account as patient, account as secretary WHERE reservation_patient_id = patient.account_id AND reservation_secretary_id = secretary.account_id AND reservation_visit_date=@date";
            else if (radioButton2.Checked)
                command.CommandText = "SELECT reservation_id, reservation_patient_id, patient.account_name, reservation_secretary_id, secretary.account_name, reservation_visit_date, reservation_visit_slot, reservation_date FROM reservation, account as patient, account as secretary WHERE reservation_patient_id = patient.account_id AND reservation_secretary_id = secretary.account_id AND (patient.account_name LIKE @query OR patient.account_phone LIKE @query OR reservation_id LIKE @query)";
            else
                command.CommandText = "SELECT reservation_id, reservation_patient_id, patient.account_name, reservation_secretary_id, secretary.account_name, reservation_visit_date, reservation_visit_slot, reservation_date FROM reservation, account as patient, account as secretary WHERE reservation_patient_id = patient.account_id AND reservation_secretary_id = secretary.account_id AND (patient.account_name LIKE @query OR patient.account_phone LIKE @query OR reservation_id LIKE @query) AND reservation_visit_date=@date";

            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@query", textBox1.Text + "%");

            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int patient_id = reader.GetInt32(1);
                string patient_name = reader.GetString(2);
                int secretary_id = reader.GetInt32(3);
                string secretary_name = reader.GetString(4);
                DateTime visit_date = new DateTime();
                DateTime.TryParse(reader.GetValue(5).ToString(), out visit_date);

                int slot = reader.GetInt32(6);

                DateTime date = new DateTime();
                DateTime.TryParse(reader.GetValue(7).ToString(), out date);

                listBox1.Items.Add(new ReservationBLL(id, patient_id, patient_name, secretary_id, secretary_name, slot, visit_date, date));
            }

            con.Close();
        }

        // Edit Reservations
        public void updateCombo(int visit_slot, DateTimePicker dateTimePicker1 , TextBox textBox1, ComboBox comboBox1 )
        {
            Dictionary<int, string> slots = utils.getSlots();

            command = con.CreateCommand();
            command.CommandText = "SELECT reservation_visit_slot FROM reservation WHERE reservation_visit_date=@date AND reservation_id <> @id";
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@id", textBox1.Text);

            con.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                slots.Remove(reader.GetInt32(0));

            comboBox1.Items.Clear();
            foreach (KeyValuePair<int, string> slot in slots)
            {
                comboBox1.Items.Add(slot);
                if (slot.Key == visit_slot)
                    comboBox1.SelectedItem = slot;
            }
            con.Close();
        }
        public void updateReservation(DateTimePicker dateTimePicker1, TextBox textBox1, ComboBox comboBox1)
        {
            command = con.CreateCommand();
            command.CommandText = "UPDATE reservation SET reservation_visit_date = @visit_date, reservation_visit_slot = @visit_slot WHERE reservation_id = @id";
            command.Parameters.AddWithValue("@id", textBox1.Text);
            command.Parameters.AddWithValue("@visit_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@visit_slot", ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Reservation was updated!");
            else
                MessageBox.Show("Failed to update the reservation!");

            con.Close();
        }
        // Delete Reservations
        public void deleteReservation(DateTimePicker dateTimePicker1, TextBox textBox1, ComboBox comboBox1) {
            command = con.CreateCommand();
            command.CommandText = "DELETE FROM reservation WHERE reservation_id = @id";
            command.Parameters.AddWithValue("@id", textBox1.Text);
            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Reservation was deleted!");
            else
                MessageBox.Show("Failed to delete the reservation!");
            con.Close();

        }

    }
}
