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
using System.Windows.Shapes;

namespace CJA_2019.WindowsForReminder
{
    public partial class Day : Window
    {
        Windows.MainForm mainForm;
        UserControls.Reminder reminder;
        string time;
        Classes.User user;
        public Day(Windows.MainForm mainForm, UserControls.Reminder reminder, string time, Classes.User user)
        {
            this.mainForm = mainForm;
            this.reminder = reminder;
            this.time = time;
            this.user = user;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;


            List<string> listSelectedDays = new List<string>();
            foreach (CheckBox chB in stpDays.Children)
            {
                if (chB.IsChecked == true)
                {
                    if (chB.Content.ToString() == chbMonday.Content.ToString())
                    {
                        listSelectedDays.Add("Пн");
                    }
                    else if (chB.Content.ToString() == chbThursday.Content.ToString())
                    {
                        listSelectedDays.Add("Чт");
                    }
                    else if (chB.Content.ToString() == chbFriday.Content.ToString())
                    {
                        listSelectedDays.Add("Пт");
                    }
                    else if (chB.Content.ToString() == chbSaturday.Content.ToString())
                    {
                        listSelectedDays.Add("Сб");
                    }
                    else if (chB.Content.ToString() == chbSunday.Content.ToString())
                    {
                        listSelectedDays.Add("Вс");
                    }
                    else
                    {
                        listSelectedDays.Add(chB.Content.ToString().Substring(0, 2));
                    }
                }
            }

            Close();

            TextBlock txbDays = new TextBlock();
            for (int i = 0; i < listSelectedDays.Count; i++)
            {
                txbDays.Text += listSelectedDays[i] + " ";
            }
            txbDays.FontSize = 20;
            txbDays.Margin = new Thickness(30, 110, 0, 0);

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression1 = $"INSERT INTO REMINDER ([IdUser], [DayTreining], [TimeTreining]) VALUES ({user.IdUser}, '{txbDays.Text}', '{time.ToString()}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                    command1.ExecuteNonQuery();

                    string tempString = "Напоминание успешно добавлено!";
                    Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                    auxiliaryWindow2.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            Border border = new Border();
            border.CornerRadius = new CornerRadius(15);
            border.Height = 150;
            border.Margin = new Thickness(10);
            border.Background = new SolidColorBrush(Colors.White);

            Grid grid = new Grid();

            Button button = new Button();
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Background = null;
            button.BorderBrush = null;
            button.Height = 50;
            button.Width = 70;
            button.Margin = new Thickness(0, 0, 5, 0);

            MaterialDesignThemes.Wpf.PackIcon packIcon = new MaterialDesignThemes.Wpf.PackIcon();
            packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowClose;
            packIcon.HorizontalAlignment = HorizontalAlignment.Center;
            packIcon.Height = 30;
            packIcon.Width = 30;

            button.Content = packIcon;

            TextBlock txbTime = new TextBlock();
            txbTime.Text = time;
            txbTime.FontSize = 35;
            txbTime.Margin = new Thickness(30, 10, 0, 0);

            TextBlock txbReapet = new TextBlock();
            txbReapet.Text = "Повторить:";
            txbReapet.FontSize = 35;
            txbReapet.Margin = new Thickness(30, 60, 0, 0);

            grid.Children.Add(txbTime);
            grid.Children.Add(txbReapet);
            grid.Children.Add(txbDays);
            grid.Children.Add(button);

            border.Child = grid;

            reminder.stkPReminder.Children.Add(border);

            mainForm.gridOwner.Children.Clear();
            mainForm.gridOwner.Children.Add(new UserControls.Reminder(mainForm, user));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();

            Time time = new Time(reminder, mainForm, user);
            time.ShowDialog();
        }
    }
}
