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

namespace CJA_2019.ToolsAdmin
{
    public partial class CreateTraining : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        Windows.MainForm mainForm;
        public CreateTraining(Windows.MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {  
            string sqlExpression = $"SELECT * FROM TRAINING WHERE Treining = '{cmbNameTraining.SelectedItem.ToString()}'";
            Classes.Training training = new Classes.Training();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            training.IdTraining = reader.GetValue(0);
                            training.NameTraining = reader.GetValue(1);
                            training.InfoTraining = reader.GetValue(2);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (cmbNameTraining.Text != String.Empty &&
               txbExercises1.Text != String.Empty && txbExercises2.Text != String.Empty && txbExercises3.Text != String.Empty &&
               txbExercises4.Text != String.Empty && txbExercises5.Text != String.Empty && txbExercises6.Text != String.Empty &&
               txbExercises7.Text != String.Empty &&
               txbRepeat1.Text != String.Empty && txbRepeat2.Text != String.Empty && txbRepeat3.Text != String.Empty &&
               txbRepeat4.Text != String.Empty && txbRepeat5.Text != String.Empty && txbRepeat6.Text != String.Empty &&
               txbRepeat7.Text != String.Empty)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            SqlCommand command = connection.CreateCommand();
                            command.Transaction = transaction;

                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises1.Text}', '{txbRepeat1.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises2.Text}', '{txbRepeat2.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises3.Text}', '{txbRepeat3.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises4.Text}', '{txbRepeat4.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises5.Text}', '{txbRepeat5.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises6.Text}', '{txbRepeat6.Text}')";
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO EXCERCISE ([IdTraining], [Excercise], [Repeat]) VALUES ({training.IdTraining}, '{txbExercises7.Text}', '{txbRepeat7.Text}')";
                            command.ExecuteNonQuery();

                            transaction.Commit();

                            string tempString = "Тренировка в полном комплекте!";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                            auxiliaryWindow2.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            transaction.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                string tempString = "Заполните все поля!";
                Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                auxiliaryWindow2.ShowDialog();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string sqlExpression = $"INSERT INTO TRAINING ([Treining], [Information]) VALUES ('{txbNameTraining.Text}', '{txbInfoTraining.Text}')";


            if (txbNameTraining.Text != String.Empty && txbInfoTraining.Text != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    bool flagTraining = false;
                    try
                    {
                        string sqlExpression2 = $"SELECT * FROM TRAINING";

                        SqlCommand sqlCommand = new SqlCommand(sqlExpression2, connection);
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (txbNameTraining.Text == reader.GetValue(1).ToString())
                                {
                                    flagTraining = true;
                                    break;
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    if (!flagTraining)
                    {
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();

                        string tempString = "Тренировка создана!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                        auxiliaryWindow2.ShowDialog();

                        cmbNameTraining.Items.Add(txbNameTraining.Text);

                        txbNameTraining.Text = "";
                        txbInfoTraining.Text = "";
                    }
                    else
                    {
                        string tempString = "Такая тренировка уже существует!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                        auxiliaryWindow2.ShowDialog();
                    }
                }
            }

            else
            {
                string tempString = "Заполните все поля!";
                Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                auxiliaryWindow2.ShowDialog();
            }
        }


        private void btnLook_Click(object sender, RoutedEventArgs e)
        {
            Tranings tranings = new Tranings(mainForm);
            tranings.ShowDialog();
        }
    }
}
