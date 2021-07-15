using CardioClinic.BAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardioClinic.DAL
{
    class AccountDAL
    {

        //update list
        public void updateList(string query, ListBox listBox1)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            con.Open();
            command.CommandText = "SELECT account_id, account_name, account_type FROM account WHERE account_type in (0, 1) AND (account_name LIKE @query OR account_phone LIKE @query) ORDER BY account_type";
            command.Parameters.AddWithValue("@query", query + "%");

            SqlDataReader reader = command.ExecuteReader();

            listBox1.Items.Clear();
            while (reader.Read())
                listBox1.Items.Add(new account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));

            con.Close();
        }
        //Validate Input
        public bool validateInputs(AccountBLL account)
        {
            if (account.Name == "" || account.Password == "" || account.UserName == "")
                return false;

           if (account.Type < 0)
                return false;

            return true;
        }

        //Insert (Add Data)
        public void AddAccount(AccountBLL account)
        {
            if (!validateInputs(account))
            {
                MessageBox.Show("Please check the input fields again!");
                return;
            }

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO [user] (user_username, user_password) VALUES(@username, @password)";
            command.Parameters.AddWithValue("@username", account.UserName);
            command.Parameters.AddWithValue("@password", utils.hashPassword(account.Password));
            con.Open();
            if (command.ExecuteNonQuery() > 0)
            {
                //We created the record in user table
                command.CommandText = "SELECT user_id FROM [user] WHERE user_username = @username";
                int user_id = (int)command.ExecuteScalar();

                command.CommandText = "INSERT INTO account (account_user_id, account_name, account_type, account_notes, account_creation_date) VALUES (@user_id, @name, @type, @notes, @date)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@name", account.Name);
                command.Parameters.AddWithValue("@type", account.Type);
                command.Parameters.AddWithValue("@notes", account.Notes);
                command.Parameters.AddWithValue("@date", DateTime.Now);

                if (command.ExecuteNonQuery() > 0)
                {
                    //All good => Account created!
                    MessageBox.Show("Account was successfully created!");
                }
                else
                    MessageBox.Show("Error while creating the account!");
            }
            else
                MessageBox.Show("Error while creating the account!");
            con.Close();
          
        }
       

        //Delete Account 
        public void DeleteAccount(TextBox textBox7, ListBox listBox1)
        {
            if (textBox7.Text == "")
                return;

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "DELETE FROM [user] WHERE user_username=@username";
            command.Parameters.AddWithValue("@username", textBox7.Text);
            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was deleted!");
            else
                MessageBox.Show("Account was not deleted!");
            con.Close();
            updateList("",listBox1);
        }
        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        public void EditAccount(AccountBLL accountBLL,  DateTimePicker dateTimePicker1, int account_id) {

            if (accountBLL.Name == "")
            {
                MessageBox.Show("Please enter a name!");
                return;
            }

            SqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE account SET account_name = @name, account_dob = @dob, account_notes = @notes, account_phone = @phone WHERE account_id = @account_id";
            command.Parameters.AddWithValue("@name", accountBLL.Name);
            command.Parameters.AddWithValue("@dob", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@phone", accountBLL.Phone);
            command.Parameters.AddWithValue("@notes", accountBLL.Notes);
            command.Parameters.AddWithValue("@account_id", account_id);

            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was updated!");
            else
                MessageBox.Show("Account was not updated!");
            con.Close();
        }



    }
}
