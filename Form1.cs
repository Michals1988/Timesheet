using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timesheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

                     
            MessageBox.Show(totalHoursCount(dtFrom, dtTo) + " hours sumbitted!");



        }

        
        public string totalHoursCount(DateTime dt1, DateTime dt2)
        {
            TimeSpan tspan = dt2 - dt1;
            String timeDiff = tspan.Hours.ToString();
            return timeDiff;
        }
    }
}
