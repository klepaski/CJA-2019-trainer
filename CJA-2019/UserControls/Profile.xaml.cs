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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CJA_2019.UserControls
{
    public partial class Profile : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        Windows.MainForm mainForm;
        Classes.User user;
        public Profile(Windows.MainForm mainForm, Classes.User user)
        {
            this.mainForm = mainForm;
            this.user = user;
            InitializeComponent();

            txbLogin.Text = user.Login.ToString();

            string sqlExpression = $"SELECT * FROM USERS WHERE IdUser = {user.IdUser}";

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
                            txbHeight.Text = reader.GetValue(3).ToString();
                            txbWidth.Text = reader.GetValue(4).ToString();
                            txbInformation.Text = reader.GetValue(5).ToString();
                            if (reader.GetValue(6).ToString() != String.Empty)
                            {
                                image.ImageSource = new BitmapImage(new Uri(reader.GetValue(6).ToString()));
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string path;
        private void btnUpdateImage_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Image (*.jpg)|*.jpg";

                if (openFile.ShowDialog() == true)
                {
                    path = openFile.FileName;

                    image.ImageSource = new BitmapImage(new Uri(openFile.FileName));
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand();
                            command.Connection = connection;
                            command.CommandText = $@"UPDATE USERS SET ImageDate = '{openFile.FileName}' WHERE IdUser = {user.IdUser}";

                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    command.CommandText = $@"UPDATE USERS SET Height = {txbHeight.Text}, Width = {txbWidth.Text}, Information = '{txbInformation.Text}' WHERE IdUser = {user.IdUser}";

                    command.ExecuteNonQuery();

                    string tempString = "Данные успешно сохранены!";
                    Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                    auxiliaryWindow2.ShowDialog();

                    string sqlExpression2 = $"SELECT * FROM USERS WHERE IdUser = {user.IdUser}";
                    SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader = command2.ExecuteReader();

                    while (reader.Read())
                    {
                        mainForm.image.ImageSource = new BitmapImage(new Uri(reader.GetValue(6).ToString()));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }                    
    }
}
