using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Firebase.Auth;
using Firebase.Database;

namespace Timesheet
{
    public partial class Form1 : Form
    {
        FirestoreDb database;
        DateTime dtFrom;
        DateTime dtTo;
        string eMail;
        string apiKey = "AIzaSyDcIMvSJebyKUzkTRpVZk1WHFbYKVea83I";
        string jSonDirect = "timesheet-3225e-firebase-adminsdk-zbp9u-101586edf3.json";



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @jSonDirect;

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("timesheet-3225e");
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            dtFrom = dateTimePickerFrom.Value;
            dtTo = dateTimePickerTo.Value;
            labelTotalHours.Text = totalHoursCount(dtFrom, dtTo);
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            dtFrom = dateTimePickerFrom.Value;
            dtTo = dateTimePickerTo.Value;
            labelTotalHours.Text = totalHoursCount(dtFrom, dtTo);
        }


        private void buttonSubmit_Click(object sender, EventArgs e)
        {

            dtFrom = dateTimePickerFrom.Value;
            dtTo = dateTimePickerTo.Value;


            if (eMail.Equals(textBox3.Text.ToLower().ToString()))
            {
                if (totalHoursCount(dtFrom, dtTo).Equals("ERROR-negative working time ") | (String.IsNullOrEmpty(textBox1.Text.ToString())) | (String.IsNullOrEmpty(textBox1.Text.ToString())))
                {
                    MessageBox.Show("Check the form - not sumbitted");
                }

                else
                {
                    Add_Document();
                    MessageBox.Show(totalHoursCount(dtFrom, dtTo) + " hours sumbitted!");
                }

            }
            else {
                MessageBox.Show("Please check your email address and log in again");
            }
        }


        public string totalHoursCount(DateTime dt1, DateTime dt2)
        {
            if (dt2 < dt1)
            {
                return "ERROR-negative working time";
            }
            else
            {
                TimeSpan tspan = dt2 - dt1;
                String timeDiff = tspan.Hours.ToString() + ":" + tspan.Minutes.ToString() + ":" + tspan.Seconds.ToString();
                return timeDiff;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFrom.Value = dateTimePicker1.Value;
            dateTimePickerTo.Value = dateTimePicker1.Value;
        }


        void Add_Document() {

                CollectionReference coll = database.Collection("Timesheet").Document(eMail).Collection("WorkTime");
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"Task", textBox1.Text.ToString() },
                    {"Description", textBox2.Text.ToString() },
                    {"EndWork", dtTo.ToString()},
                    {"StartWork", dtFrom.ToString()}
                };
                coll.AddAsync(data);          
        }

        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                FirebaseAuthProvider mAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));

                var mAuthLink = await mAuthProvider.SignInWithEmailAndPasswordAsync(email, password);

                label3.BackColor = Color.FromName("Green");
                label3.ForeColor = Color.FromName("White");
                label3.Text = "Logged in";
                eMail = mAuthLink.User.Email.ToString();
                return true;
            }
            catch
            {
                label3.BackColor= Color.FromName("Red");
                label3.ForeColor = Color.FromName("White");
                label3.Text = "Incorrect login details.";
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task<bool> loguj = SignIn(textBox3.Text.ToLower().ToString(), textBox4.Text.ToString()) ;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
