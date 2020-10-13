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
    public partial class Exercises : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Windows.MainForm mainForm;
        Classes.User user;
        List<Classes.Exercise> exercises = new List<Classes.Exercise>();
        public Exercises(Windows.MainForm mainForm, string NameTraining, string InfoTraining, Classes.User user)
        {
            this.mainForm = mainForm;
            this.user = user;
            InitializeComponent();

            txbNameTraining.Text = NameTraining;
            txbInfoTraining.Text = InfoTraining;

            string sqlExpression = $@"
SELECT EXCERCISE.IdExcercise, EXCERCISE.IdTraining, EXCERCISE.Excercise, EXCERCISE.Repeat FROM EXCERCISE
JOIN TRAINING ON EXCERCISE.IdTraining = TRAINING.IdTreining
WHERE TRAINING.Treining = '{NameTraining}'";

           
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
                            Classes.Exercise exercise = new Classes.Exercise();
                            exercise.IdExercise = reader.GetValue(0);
                            exercise.IdTraining = reader.GetValue(1);
                            exercise.SomeExercise = reader.GetValue(2);
                            exercise.Repeat = reader.GetValue(3);
                            exercises.Add(exercise);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for(int i = 0; i < exercises.Count; i++)
            {
                switch(i)
                {
                    case 0:
                        txbExercise1.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat1.Text = exercises[i].Repeat.ToString();
                        break;
                    case 1:
                        txbExercise2.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat2.Text = exercises[i].Repeat.ToString();
                        break;
                    case 2:
                        txbExercise3.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat3.Text = exercises[i].Repeat.ToString();
                        break;
                    case 3:
                        txbExercise4.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat4.Text = exercises[i].Repeat.ToString();
                        break;
                    case 4:
                        txbExercise5.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat5.Text = exercises[i].Repeat.ToString();
                        break;
                    case 5:
                        txbExercise6.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat6.Text = exercises[i].Repeat.ToString();
                        break;
                    case 6:
                        txbExercise7.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat7.Text = exercises[i].Repeat.ToString();
                        break;
                    case 7:
                        break;
                }
            }
        }

        public Exercises(Windows.MainForm mainForm, string NameTraining, string InfoTraining)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            btnGoToTheWork.IsEnabled = false;

            txbNameTraining.Text = NameTraining;
            txbInfoTraining.Text = InfoTraining;

            string sqlExpression = $@"
SELECT EXCERCISE.IdExcercise, EXCERCISE.IdTraining, EXCERCISE.Excercise, EXCERCISE.Repeat FROM EXCERCISE
JOIN TRAINING ON EXCERCISE.IdTraining = TRAINING.IdTreining
WHERE TRAINING.Treining = '{NameTraining}'";

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
                            Classes.Exercise exercise = new Classes.Exercise();
                            exercise.IdExercise = reader.GetValue(0);
                            exercise.IdTraining = reader.GetValue(1);
                            exercise.SomeExercise = reader.GetValue(2);
                            exercise.Repeat = reader.GetValue(3);
                            exercises.Add(exercise);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < exercises.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        txbExercise1.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat1.Text = exercises[i].Repeat.ToString();
                        break;
                    case 1:
                        txbExercise2.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat2.Text = exercises[i].Repeat.ToString();
                        break;
                    case 2:
                        txbExercise3.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat3.Text = exercises[i].Repeat.ToString();
                        break;
                    case 3:
                        txbExercise4.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat4.Text = exercises[i].Repeat.ToString();
                        break;
                    case 4:
                        txbExercise5.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat5.Text = exercises[i].Repeat.ToString();
                        break;
                    case 5:
                        txbExercise6.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat6.Text = exercises[i].Repeat.ToString();
                        break;
                    case 6:
                        txbExercise7.Text = exercises[i].SomeExercise.ToString();
                        txbRepeat7.Text = exercises[i].Repeat.ToString();
                        break;
                    case 7:
                        break;
                }
            }
        }

        private void btnGoToTheWork_Click(object sender, RoutedEventArgs e)
        {
            Windows.ExercisesGO exercisesGO = new Windows.ExercisesGO(exercises, mainForm, user, txbNameTraining.Text);
            exercisesGO.ShowDialog();
        }
    }
}
