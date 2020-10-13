using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CJA_2019.UserControls
{
    public partial class Reminder : UserControl
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Windows.MainForm mainForm;
        Classes.User user;

        List<Classes.Reminder> reminders = new List<Classes.Reminder>();

        public Reminder(Windows.MainForm mainForm, Classes.User user)
        {
            this.mainForm = mainForm;
            this.user = user;
            InitializeComponent();

            string sqlExpression1 = $"SELECT * FROM REMINDER WHERE IdUser = {user.IdUser}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                    SqlDataReader reader = command1.ExecuteReader();

                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Classes.Reminder reminder = new Classes.Reminder();
                            reminder.IdReminder = reader.GetValue(0);
                            reminder.IdUser = reader.GetValue(1);
                            reminder.DayTraining = reader.GetValue(2);
                            reminder.TimeTraining = reader.GetValue(3);
                            reminders.Add(reminder);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            for (int i = 0; i < reminders.Count; i++)
            {              
                Border border = new Border();
                border.CornerRadius = new CornerRadius(15);
                border.Height = 150;
                border.Margin = new Thickness(10);
                border.Background = new SolidColorBrush(Colors.White);

                Grid grid = new Grid();
                
                TextBlock txbDays = new TextBlock();
                txbDays.Text = reminders[i].DayTraining.ToString();
                txbDays.FontSize = 20;
                txbDays.Margin = new Thickness(30, 110, 0, 0);

                TextBlock txbTime = new TextBlock();
                txbTime.Text = reminders[i].TimeTraining.ToString();
                txbTime.FontSize = 35;
                txbTime.Margin = new Thickness(30, 10, 0, 0);

                TextBlock txbReapet = new TextBlock();
                txbReapet.Text = "Повторить:";
                txbReapet.FontSize = 35;
                txbReapet.Margin = new Thickness(30, 60, 0, 0);
    
                grid.Children.Add(txbTime);
                grid.Children.Add(txbReapet);
                grid.Children.Add(txbDays);

                border.Child = grid;
                stkPReminder.Children.Add(border);
            }

            

        }

        private void btnAddReminder_Click(object sender, RoutedEventArgs e)
        {          
            WindowsForReminder.Time time = new WindowsForReminder.Time(this, mainForm, user);
            time.ShowDialog();
        }
    }
}
