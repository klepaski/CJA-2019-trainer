using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class Login : Window
    {
        Windows.MainForm mainForm;
        public Login(Windows.MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Register register = new Register(mainForm);
            register.ShowDialog();
        }

        private void btnSingIn_Click(object sender, RoutedEventArgs e)
        {
            ToolsAdmin.Admin admin = new ToolsAdmin.Admin();
            if (txbLogin.Text == admin.login && psbPassword.Password == admin.password)
            {
                Close();
                MainForm mainForm2 = new MainForm(admin);
                mainForm2.Show();
                mainForm.Close();
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlExpression = "SELECT * FROM USERS";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    if (txbLogin.Text != String.Empty && psbPassword.Password != String.Empty)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        Classes.User tempUser = new Classes.User();

                        if (reader.HasRows)
                        {
                            bool flagPerson = false;
                            while (reader.Read())
                            {
                                if (txbLogin.Text == (string)reader.GetValue(1) && VerifyHashedPassword((string)reader.GetValue(2), psbPassword.Password))
                                {
                                    flagPerson = true;
                                    tempUser.IdUser = reader.GetValue(0);
                                    tempUser.Login = reader.GetValue(1);
                                    tempUser.Password = reader.GetValue(2);
                                    tempUser.ImageData = reader.GetValue(6);
                                    break;
                                }
                            }
                            reader.Close();

                            if (flagPerson)
                            {
                                Close();
                                MainForm mainForm2 = new MainForm(tempUser);
                                mainForm2.Show();
                                mainForm.Close();
                            }
                            else
                            {
                                string tempString = "Такого пользователя нет!";
                                Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                                auxiliaryWindow2.ShowDialog();

                                txbLogin.Text = "";
                                psbPassword.Password = "";
                                return;
                            }
                        }
                        else
                        {
                            string tempString = "Такого пользователя нет!";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                            auxiliaryWindow2.ShowDialog();

                            txbLogin.Text = "";
                            psbPassword.Password = "";
                        }
                    }
                    else
                    {
                        Close();
                        string tempString = "Введите корректные данные!";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm ,tempString);
                        auxiliaryWindow2.ShowDialog();
                        
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10); //копируем 0х10 символов из src начиная с 1 байта в dst c 0 байте
            byte[] buffer2 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer2, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer2, buffer);
        }

        private static bool ByteArraysEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++) 
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();

            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;
        }
    }
}
