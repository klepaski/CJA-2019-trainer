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

namespace CJA_2019.ToolsAdmin
{
    public partial class Tranings : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        Windows.MainForm mainForm;
        public Tranings(Windows.MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            mainForm.gridBackGround.Background = new SolidColorBrush(Colors.Black);
            mainForm.gridBackGround.Opacity = 0.5;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    string sqlExpression2 = $"SELECT * FROM TRAINING";

                    SqlCommand sqlCommand = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    List<Classes.Training> trainings = new List<Classes.Training>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Classes.Training training = new Classes.Training();
                            training.IdTraining = reader.GetValue(0);
                            training.NameTraining = reader.GetValue(1);
                            training.InfoTraining = reader.GetValue(2);
                            trainings.Add(training);
                        }
                    }
                    reader.Close();
                    listTraining.ItemsSource = trainings;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Classes.Training training = (Classes.Training)listTraining.SelectedItem;

            string sqlExpression1 = $"DELETE EXCERCISE WHERE IdTraining = {training.IdTraining}";
            string sqlExpression2 = $"DELETE TRAINING WHERE Treining = '{training.NameTraining.ToString()}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                    command1.ExecuteNonQuery();
                    SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                    command2.ExecuteNonQuery();

                    string tempString = "Удаление тренировки прошло успешно!";
                    Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                    auxiliaryWindow2.ShowDialog();

                    mainForm.gridBackGround.Background = null;
                    mainForm.gridBackGround.Opacity = 1;
                    
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
