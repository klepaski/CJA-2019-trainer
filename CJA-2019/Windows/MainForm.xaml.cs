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

namespace CJA_2019.Windows
{
    public partial class MainForm : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Classes.User user;
        ToolsAdmin.Admin admin;
        bool isAdmin = false;
        public MainForm()
        {
            InitializeComponent();
            
            listPanel.SelectedIndex = -1;

            ellReminder.Visibility = Visibility.Hidden;
            txbCountReminder.Visibility = Visibility.Hidden;
            btnExitToProfile.Visibility = Visibility.Hidden;

            gridOwner.Children.Clear();
            gridOwner.Children.Add(new UserControls.Home("пользователь"));
        }

        public MainForm(ToolsAdmin.Admin admin)
        {
            this.admin = admin;
            InitializeComponent();
            isAdmin = true;

            txbLogin.Text = admin.login;
            ellReminder.Visibility = Visibility.Hidden;
            txbCountReminder.Visibility = Visibility.Hidden;
            btnExitToProfile.Visibility = Visibility.Visible;
        }

        public MainForm(Classes.User user)
        {
            this.user = user;
            InitializeComponent();

            if (user.ImageData.ToString() != String.Empty)
            {
                image.ImageSource = new BitmapImage(new Uri(user.ImageData.ToString()));
            }

            ellReminder.Visibility = Visibility.Hidden;
            txbCountReminder.Text = "";
            txbCountReminder.Visibility = Visibility.Hidden;

            btnExitToProfile.Visibility = Visibility.Visible;

            string sqlExpression1 = $"SELECT * FROM REMINDER WHERE IdUser = {user.IdUser}";

            List<Classes.Reminder> reminders = new List<Classes.Reminder>();
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

                for (int i = 0; i < reminders.Count; i++)
                {
                    if (reminders[i].DayTraining.ToString().Contains("Пн") ||
                        reminders[i].DayTraining.ToString().Contains("Вт") ||
                        reminders[i].DayTraining.ToString().Contains("Ср") ||
                        reminders[i].DayTraining.ToString().Contains("Чт") ||
                        reminders[i].DayTraining.ToString().Contains("Пт") ||
                        reminders[i].DayTraining.ToString().Contains("Сб") ||
                        reminders[i].DayTraining.ToString().Contains("Вс"))
                    {
                        string timeNow = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                        
                        if (reminders[i].TimeTraining.ToString().CompareTo(timeNow).ToString() == (-1).ToString())
                        {
                            ellReminder.Visibility = Visibility.Visible;
                            txbCountReminder.Text = "!";
                            txbCountReminder.Visibility = Visibility.Visible;
                        }
                    }
                }
            }

            txbLogin.Text = user.Login.ToString();
            gridOwner.Children.Clear();
            gridOwner.Children.Add(new UserControls.Home(user.Login.ToString()));
        }


        private void gridTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnGoToProfile_Click(object sender, RoutedEventArgs e)
        {
            if (this.txbLogin.Text == "Аккаунт")
            {
                string tempString = "Для просмотра профиля, необходимо авторизироваться!";
                Windows.AuxiliaryWindow auxiliaryWindow = new AuxiliaryWindow(this, tempString, listPanel);
                auxiliaryWindow.ShowDialog();
            }
            
            else if (txbLogin.Text == "admin")
            {
                string tempString = "Эта вкладка для авторизированного пользователя!";
                Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(this, tempString);
                auxiliaryWindow2.ShowDialog();
                return;
            }
            else if (txbLogin.Text == user.Login.ToString())
            {
                gridOwner.Children.Clear();
                gridOwner.Children.Add(new UserControls.Profile(this, user));
            }
        }

        private void btnExitToProfile_Click(object sender, RoutedEventArgs e)
        {
            txbLogin.Text = "Аккаунт";
            btnExitToProfile.Visibility = Visibility.Hidden;

            image.ImageSource = null;
            gridOwner.Children.Clear();
            gridOwner.Children.Add(new UserControls.Home("пользователь"));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listPanel.SelectedIndex;

            switch (index)
            {
                case 0:
                    if (txbLogin.Text == "Аккаунт")
                    {
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new UserControls.Home("пользователь"));
                    }
                    else if (txbLogin.Text == "admin")
                    {
                        string tempString = "Эта вкладка для авторизированного пользователя!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(this, tempString);
                        auxiliaryWindow2.ShowDialog();
                        break;
                    }
                    else if(txbLogin.Text == user.Login.ToString())
                    {
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new UserControls.Home(user.Login.ToString()));
                    }                  
                    break;

                case 1:
                    if(txbLogin.Text == "admin")
                    {
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new ToolsAdmin.CreateTraining(this));
                        break;
                    }
                    else if(txbLogin.Text == "Аккаунт")
                    {
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new UserControls.PlanTraining(this));
                        break;
                    }
                    else if (txbLogin.Text == user.Login.ToString())
                    {
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new UserControls.PlanTraining(this, user));
                        break;
                    }
                    break;

                case 2:
                    if (txbLogin.Text == "Аккаунт")
                    {
                        string tempString = "Для просмотра отчёта необходимо авторизироваться!";
                        Windows.AuxiliaryWindow auxiliaryWindow = new AuxiliaryWindow(this, tempString, listPanel);
                        auxiliaryWindow.ShowDialog();
                    }
                    else if (txbLogin.Text == "admin")
                    {
                        string tempString = "Эта вкладка для авторизированного пользователя!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(this, tempString);
                        auxiliaryWindow2.ShowDialog();
                        break;
                    }
                    else if (txbLogin.Text == user.Login.ToString())
                    {
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new UserControls.Report(user));
                    }
                    
                    break;

                case 3:
                    if (txbLogin.Text == "Аккаунт")
                    {
                        string tempString = "Для просмотра напоминаний необходимо авторизироваться!";
                        Windows.AuxiliaryWindow auxiliaryWindow = new AuxiliaryWindow(this, tempString, listPanel);
                        auxiliaryWindow.ShowDialog();
                    }
                    else if (txbLogin.Text == "admin")
                    {
                        string tempString = "Эта вкладка для авторизированного пользователя!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(this, tempString);
                        auxiliaryWindow2.ShowDialog();
                        break; 
                    }
                    else if (txbLogin.Text == user.Login.ToString())
                    {
                        txbCountReminder.Text = "";
                        txbCountReminder.Visibility = Visibility.Hidden;
                        ellReminder.Visibility = Visibility.Hidden;
                        
                        gridOwner.Children.Clear();
                        gridOwner.Children.Add(new UserControls.Reminder(this, user));
                    }
                    
                    break;

                case 4:
                    if (txbLogin.Text == "Аккаунт")
                    {
                        string tempString = "Для сброса результатов необходимо авторизироваться!";
                        Windows.AuxiliaryWindow auxiliaryWindow = new AuxiliaryWindow(this, tempString, listPanel);
                        auxiliaryWindow.ShowDialog();
                    }
                    else if (txbLogin.Text == "admin")
                    {
                        string tempString = "Эта вкладка для авторизированного пользователя!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(this, tempString);
                        auxiliaryWindow2.ShowDialog();
                        break;
                    }
                    else if (txbLogin.Text == user.Login.ToString())
                    {
                        string sqlExpression = $"DELETE FROM REPORT WHERE IdUser = {user.IdUser}";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            command.ExecuteNonQuery();

                            string tempString = "Результаты сброшены!";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(this, tempString);
                            auxiliaryWindow2.ShowDialog();
                        }
                    }
                    
                    break;
            }
        }
    }
}
