using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CJA_2019.WindowsForReminder
{
    public partial class Time : Window
    {
        UserControls.Reminder reminder;
        Windows.MainForm mainForm;
        Classes.User user;
        public Time(UserControls.Reminder reminder, Windows.MainForm mainForm, Classes.User user)
        {
            this.reminder = reminder;
            this.mainForm = mainForm;
            this.user = user;
            InitializeComponent();

            mainForm.gridBackGround.Background = new SolidColorBrush(Colors.Black);
            mainForm.gridBackGround.Opacity = 0.5;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;

            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            string time = clock.Time.TimeOfDay.Hours.ToString() + ":" + clock.Time.TimeOfDay.Minutes.ToString();
            //string timeNow = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
             
            Close();

            Day day = new Day(mainForm, reminder, time, user);
            day.ShowDialog();

        }
    }
}
