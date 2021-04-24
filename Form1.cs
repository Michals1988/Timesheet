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

namespace Timesheet
{
    public partial class Form1 : Form
    {
        FirestoreDb database;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            //database = FirestoreDb.Create("timesheet-3225e");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = dateTimePickerFrom.Value;
            DateTime dtTo = dateTimePickerTo.Value;
            labelTotalHours.Text = totalHoursCount(dtFrom, dtTo);
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = dateTimePickerFrom.Value;
            DateTime dtTo = dateTimePickerTo.Value;
            labelTotalHours.Text = totalHoursCount(dtFrom, dtTo);
        }


        private void buttonSubmit_Click(object sender, EventArgs e)
        {

            DateTime dtFrom = dateTimePickerFrom.Value;
            DateTime dtTo = dateTimePickerTo.Value;
            if (totalHoursCount(dtFrom, dtTo).Equals("ERROR-błędnie podany czas- ujemny czas pracy") ^ (textBox1.Text.Equals("")) ^ (textBox2.Text.Equals("")))
            {
                MessageBox.Show("Check the form - not sumbitted");
            }

            else
            {
                MessageBox.Show(totalHoursCount(dtFrom, dtTo) + " hours sumbitted!");
            }



        }


        public string totalHoursCount(DateTime dt1, DateTime dt2)
        {
            if (dt2 < dt1)
            {
                return "ERROR-błędnie podany czas- ujemny czas pracy";
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
            CollectionReference coll = database.Collection("");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Description", textBox2.Text },
                {"Task", textBox1.Text },
                {"StartWork", dateTimePickerFrom},
                {"EndWork", dateTimePickerTo }
            };

            coll.AddAsync(data);
        }
    }
}
