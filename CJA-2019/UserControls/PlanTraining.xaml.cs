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
    public partial class PlanTraining : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        Windows.MainForm mainForm;
        List<Classes.Training> trainings = new List<Classes.Training>();
        Classes.User user;
        public PlanTraining(Windows.MainForm mainForm, Classes.User user)
        {
            this.mainForm = mainForm;
            this.user = user;
            InitializeComponent();
            
            string sqlExpression = "SELECT * FROM TRAINING";
            
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
                            Classes.Training training = new Classes.Training();
                            training.IdTraining = reader.GetValue(0);
                            training.NameTraining = reader.GetValue(1);
                            training.InfoTraining = reader.GetValue(2);
                            trainings.Add(training);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < trainings.Count; i++)
            {
                Border border = new Border();
                border.Height = 155;
                border.Width = 459;
                border.Background = new SolidColorBrush(Colors.Black);
                border.Opacity = 0.8;
                border.CornerRadius = new CornerRadius(20);
                border.Margin = new Thickness(0, 5, 0, 0);

                Grid grid = new Grid();
                grid.Cursor = Cursors.Hand;
                grid.MouseDown += gridTraining1_MouseDown;

                TextBlock txbNameTrainig = new TextBlock();
                txbNameTrainig.Text = trainings[i].NameTraining.ToString();
                txbNameTrainig.FontFamily = new FontFamily("Impact");
                txbNameTrainig.Foreground = new SolidColorBrush(Colors.White);
                txbNameTrainig.FontSize = 36;
                txbNameTrainig.VerticalAlignment = VerticalAlignment.Top;
                txbNameTrainig.HorizontalAlignment = HorizontalAlignment.Center;

                Border border2 = new Border();
                border2.Background = new RadialGradientBrush(Colors.White, Colors.Black);
                border2.CornerRadius = new CornerRadius(15);
                border2.Margin = new Thickness(0, 70, 0, 0);

                TextBlock txbInfoTraining = new TextBlock();
                txbInfoTraining.Text = trainings[i].InfoTraining.ToString();
                txbInfoTraining.FontFamily = new FontFamily("Impact");
                txbInfoTraining.Foreground = new SolidColorBrush(Colors.DarkRed); ///
                txbInfoTraining.FontSize = 24;
                txbInfoTraining.VerticalAlignment = VerticalAlignment.Center;
                txbInfoTraining.HorizontalAlignment = HorizontalAlignment.Center;

                border2.Child = txbInfoTraining;

                grid.Children.Add(txbNameTrainig);
                grid.Children.Add(border2);

                border.Child = grid;

                stkPTraining.Children.Add(border);
            }

        }

        public PlanTraining(Windows.MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            string sqlExpression = "SELECT * FROM TRAINING";
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
                            Classes.Training training = new Classes.Training();
                            training.IdTraining = reader.GetValue(0);
                            training.NameTraining = reader.GetValue(1);
                            training.InfoTraining = reader.GetValue(2);
                            trainings.Add(training);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < trainings.Count; i++)
            {
                Border border = new Border();
                border.Height = 155;
                border.Width = 459;
                border.Background = new SolidColorBrush(Colors.Black);
                border.Opacity = 0.8;
                border.CornerRadius = new CornerRadius(20);
                border.Margin = new Thickness(0, 5, 0, 0);

                Grid grid = new Grid();
                grid.Cursor = Cursors.Hand;
                grid.MouseDown += gridTraining1_MouseDown;

                TextBlock txbNameTrainig = new TextBlock();
                txbNameTrainig.Text = trainings[i].NameTraining.ToString();
                txbNameTrainig.FontFamily = new FontFamily("Impact");
                txbNameTrainig.Foreground = new SolidColorBrush(Colors.White);
                txbNameTrainig.FontSize = 36;
                txbNameTrainig.VerticalAlignment = VerticalAlignment.Top;
                txbNameTrainig.HorizontalAlignment = HorizontalAlignment.Center;

                Border border2 = new Border();
                border2.Background = new RadialGradientBrush(Colors.White, Colors.Black);
                border2.CornerRadius = new CornerRadius(15);
                border2.Margin = new Thickness(0, 70, 0, 0);

                TextBlock txbInfoTraining = new TextBlock();
                txbInfoTraining.Text = trainings[i].InfoTraining.ToString();
                txbInfoTraining.FontFamily = new FontFamily("Impact");
                txbInfoTraining.Foreground = new SolidColorBrush(Colors.DarkRed); ///
                txbInfoTraining.FontSize = 24;
                txbInfoTraining.VerticalAlignment = VerticalAlignment.Center;
                txbInfoTraining.HorizontalAlignment = HorizontalAlignment.Center;

                border2.Child = txbInfoTraining;

                grid.Children.Add(txbNameTrainig);
                grid.Children.Add(border2);

                border.Child = grid;

                stkPTraining.Children.Add(border);                
            }
        }

        private void gridTraining1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;

            
            foreach (var brd in stkPTraining.Children)
            {
                Grid tempGrid = new Grid();

                TextBlock txbNameTraining = new TextBlock();
                TextBlock txbInfoTraining = new TextBlock();
                //получаю grid
                if (brd.GetType() == typeof(Border))
                {
                    Border tempBorder = (Border)brd;
                    var tGrid = tempBorder.Child;
                    tempGrid = (Grid)tGrid;

                    //получаю элементы grid'a
                    foreach (var item in grid.Children)
                    {
                        if (item.GetType() == typeof(TextBlock))
                        {
                            txbNameTraining = (TextBlock)item;
                        }
                        if (item.GetType() == typeof(Border))
                        {
                            Border tempBorder2 = (Border)item;
                            var tBlock = tempBorder2.Child;
                            txbInfoTraining = (TextBlock)tBlock;
                        }
                    }
                }

                if(tempGrid == grid && user != null)
                {
                    mainForm.gridOwner.Children.Clear();
                    mainForm.gridOwner.Children.Add(new UserControls.Exercises(mainForm, txbNameTraining.Text, txbInfoTraining.Text, user));
                }
                else if(tempGrid == grid)
                {
                    mainForm.gridOwner.Children.Clear();
                    mainForm.gridOwner.Children.Add(new UserControls.Exercises(mainForm, txbNameTraining.Text, txbInfoTraining.Text));
                }
            }
        }
    }
}
