using CardioClinic.BAL;
using CardioClinic.BLL;
using CardioClinic.DAL;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
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
    public partial class PatientPage : Form
    {
        protected FhirClient _client;
        public PatientPage()
        {
            InitializeComponent();
                        //Initialize Fhir client
            FhirClientSettings _settings = new FhirClientSettings()
            {
                PreferredFormat = ResourceFormat.Json,
            };

            _client = new FhirClient("https://fhir.simplifier.net/BioProject", _settings);
            _client.RequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWJkZWxyYWhtYW5lbGdlYmFseSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZTRjYmYwZDgtMjZlMC00ODIwLWFjYzMtNzQwM2Y3NTEzODdjIiwianRpIjoiM2YzNjVkYTItMmIzNC00NWY2LWI4MDUtOTA1OGIxMDVhNjM2IiwiZXhwIjoxNjI2MDMzMDMwLCJpc3MiOiJhcGkuc2ltcGxpZmllci5uZXQiLCJhdWQiOiJhcGkuc2ltcGxpZmllci5uZXQifQ.b3wGJuF_UdZbcoSZ7eIPs8S-EnV4UbDjZelhuGKs-hc");
        }
        AccountBLL accountBLL = new AccountBLL();
        PatientDAL patientDAL = new PatientDAL();
        
        private void PatientPage_Load(object sender, EventArgs e)
        {
            patientDAL.updateList("", listBox1);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            accountBLL.Name = textBox1.Text;
            accountBLL.Phone = textBox2.Text;
            accountBLL.Notes = textBox3.Text;
            patientDAL.AddPatient(accountBLL, listBox1);

            ////////////////////hl7///////////////////////
            //bind information to patientDataModel
            PatientDataModel patientDataModel = new PatientDataModel();
            patientDataModel.FirstName = textBox1.Text;
            patientDataModel.LastName = txt_LastName.Text;
            patientDataModel.Email = txt_Email.Text;
            patientDataModel.FullName = patientDataModel.FirstName + "  " + patientDataModel.LastName;
            patientDataModel.phoneNumber = textBox2.Text;


            //check Is patient already inserted or not based on full name
            //   var result = Find_Patient_By_Name(_client, patientDataModel.FullName);

            //check number of resources

            //fill in HL7 patient Model
            Patient patient = new Patient()
                {
                    Active = true,
                    Name = new List<HumanName>()
                {
                    new HumanName()
                    {

                        Family = patientDataModel.LastName,
                        Text = patientDataModel.FullName,

                        Given = new List<string>()
                        {
                            patientDataModel.FirstName
                        },
                         Use = HumanName.NameUse.Official
                    }
                },
                    Identifier = new List<Identifier>()
                    {
                        new Identifier()
                        {
                            Value = "1032702",
                            Use = Identifier.IdentifierUse.Usual,
                            System="urn:oid:0.1.2.3.4.5.6.7"

                        }
                    },
                    BirthDate = (new FhirDateTime(patientDataModel.Birthdate.Year, patientDataModel.Birthdate.Month, patientDataModel.Birthdate.Day)).ToString(),
                    Gender = patientDataModel.Gender ? AdministrativeGender.Male : AdministrativeGender.Female,
                    Telecom = new List<ContactPoint>()
                {
                    new ContactPoint()
                    {
                        System = ContactPoint.ContactPointSystem.Phone,
                        Value =  patientDataModel.phoneNumber,
                        Use = ContactPoint.ContactPointUse.Mobile
                    },
                    new ContactPoint()
                    {
                        System = ContactPoint.ContactPointSystem.Phone,
                        Value = patientDataModel.phoneNumber,
                        Use = ContactPoint.ContactPointUse.Home
                    },
                     new ContactPoint()
                    {
                        System = ContactPoint.ContactPointSystem.Email,
                        Value = patientDataModel.Email,
                        Use = ContactPoint.ContactPointUse.Home
                    },

                }
                };



                var re = _client.Create<Patient>(patient);

                if (re.Id != null)
                {
                    MessageBox.Show("Patient added");
                    textBox1.Text = "";
                    txt_LastName.Text = "";
                    txt_Email.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";


            }

                //For Better Performance use Async functions //Avoid UI Freezing
                // var re = _client.CreateAsync<Patient>(patient);

                //if (re.Result.Id != null)
                //{
                //    MessageBox.Show("Patient added");
                //    txt_fname.Text="";
                //    txt_LastName.Text="";
                //    txt_Email.Text="";
                //    //Patients_dic.Add(re.Id, patient);
                //}

            







        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command;
            //Inputs Validation
            if (textBox6.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Please check the inputs!");
                return;
            }

            //Editing the account
            command = con.CreateCommand();
            command.CommandText = "UPDATE account SET account_name = @name, account_phone = @phone, account_dob = @dob, account_notes = @notes WHERE account_id = @id";
            command.Parameters.AddWithValue("@name", textBox6.Text);
            command.Parameters.AddWithValue("@phone", textBox7.Text);
            command.Parameters.AddWithValue("@dob", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@notes", textBox8.Text);
            command.Parameters.AddWithValue("@id", textBox5.Text);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was updated!");
            else
                MessageBox.Show("Failed to updated the account!");

            con.Close();
        }
    


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command;
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
                return;
            int account_id = ((account)listBox1.SelectedItem).getID();
            command = con.CreateCommand();
            command.CommandText = "SELECT account_name, account_dob, account_phone, account_notes, account_creation_date FROM account WHERE account_id=@id";
            command.Parameters.AddWithValue("@id", account_id);

            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox5.Text = account_id.ToString();
                textBox6.Text = reader.GetString(0);

                DateTime dob = new DateTime();
                if (DateTime.TryParse(reader.GetValue(1).ToString(), out dob))
                    dateTimePicker1.Value = dob;
                textBox7.Text = reader.GetString(2);
                textBox8.Text = reader.GetString(3);
                textBox9.Text = reader.GetValue(4).ToString();
            }

            con.Close();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            patientDAL.updateList(textBox4.Text, listBox1);
        }
    }
}
