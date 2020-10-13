using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CJA_2019.Windows
{
    public partial class ExercisesGO : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        List<Classes.Exercise> exercises;
        MainForm mainForm;
        Classes.User user;
        string NameTraining;
        public ExercisesGO(List<Classes.Exercise> exercises, MainForm mainForm, Classes.User user, string NameTraining)
        {
            this.exercises = exercises;
            this.mainForm = mainForm;
            this.user = user;
            this.NameTraining = NameTraining;
            InitializeComponent();

            txbExercise.Text = exercises[0].SomeExercise.ToString();
            txbRepeat.Text = exercises[0].Repeat.ToString();

            mainForm.gridBackGround.Background = new SolidColorBrush(Colors.Black);
            mainForm.gridBackGround.Opacity = 0.5;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();

            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;
        }

        DispatcherTimer dispatcherTimer;
        int countExercise = 1;

        int x;
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            int time = 4;
            x = time;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += dtTicker;
            dispatcherTimer.Start();

            if (countExercise == 7)
            {
                dispatcherTimer.Stop();
                string tempString = "Тренировка успешно пройдена. Поздравляю!";
                Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                auxiliaryWindow2.ShowDialog();

               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sqlExpression = $"INSERT INTO REPORT ([IdUser], [NameTraining], [DateTreining]) VALUES ({user.IdUser}, '{NameTraining}', '{DateTime.Now}')";

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }

                Close();

                mainForm.gridBackGround.Background = null;
                mainForm.gridBackGround.Opacity = 1;
            }
        }

        private void dtTicker(object sender, EventArgs e)
        {
            btnNext.IsEnabled = false;
            x--;
            txbExercise.Text = x.ToString();
            txbExercise.FontSize = 50;
            txbRepeat.Visibility = Visibility.Hidden;
            if(x == 0)
            {
                txbExercise.Text = exercises[countExercise].SomeExercise.ToString();
                txbRepeat.Text = exercises[countExercise].Repeat.ToString();

                txbExercise.FontSize = 35;
                txbRepeat.Visibility = Visibility.Visible;

                countExercise++;
                dispatcherTimer.Stop();
                btnNext.IsEnabled = true;
            }           
        }
    }
}
