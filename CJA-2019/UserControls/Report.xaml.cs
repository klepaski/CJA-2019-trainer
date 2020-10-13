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
    public partial class Report : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Classes.User user;
        public Report(Classes.User user)
        {
            this.user = user;
            InitializeComponent();
            string sqlExpression = $"SELECT * FROM REPORT WHERE IdUser = {user.IdUser}";

            List<Classes.Report> reports = new List<Classes.Report>();
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
                            Classes.Report report = new Classes.Report();
                            report.IdReport = reader.GetValue(0);
                            report.IdUser = reader.GetValue(1);
                            report.NameTraining = reader.GetValue(2);
                            report.DateTraining = reader.GetValue(3);
                            reports.Add(report);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < reports.Count; i++)
            {
                Border border = new Border();
                border.Height = 155;
                border.Width = 456;
                border.Background = new SolidColorBrush(Colors.Black);
                border.Opacity = 0.8;
                border.CornerRadius = new CornerRadius(20);
                border.Margin = new Thickness(0, 5, 0, 0);

                Grid grid = new Grid();

                TextBlock txbNameTrainig = new TextBlock();
                txbNameTrainig.Text = reports[i].NameTraining.ToString();
                txbNameTrainig.FontFamily = new FontFamily("Impact");
                txbNameTrainig.Foreground = new SolidColorBrush(Colors.White);
                txbNameTrainig.FontSize = 36;
                txbNameTrainig.VerticalAlignment = VerticalAlignment.Top;
                txbNameTrainig.HorizontalAlignment = HorizontalAlignment.Center;

                Border border2 = new Border();
                border2.Background = new RadialGradientBrush(Colors.White, Colors.Black);
                border2.CornerRadius = new CornerRadius(15);
                border2.Margin = new Thickness(0, 70, 0, 0);

                TextBlock txbDateTraining = new TextBlock();
                txbDateTraining.Text = reports[i].DateTraining.ToString();
                txbDateTraining.FontFamily = new FontFamily("Impact");
                txbDateTraining.Foreground = new SolidColorBrush(Colors.DarkRed);
                txbDateTraining.FontSize = 36;
                txbDateTraining.VerticalAlignment = VerticalAlignment.Center;
                txbDateTraining.HorizontalAlignment = HorizontalAlignment.Center;

                border2.Child = txbDateTraining;

                grid.Children.Add(txbNameTrainig);
                grid.Children.Add(border2);

                border.Child = grid;

                stpReport.Children.Add(border);
            }
        }
    }
}
