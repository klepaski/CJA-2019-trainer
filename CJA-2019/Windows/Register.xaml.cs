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
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        Windows.MainForm mainForm;
        public Register(Windows.MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Login login = new Login(mainForm);
            login.ShowDialog();
        }

        private void btnSumbit_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string password = HashPassword(this.psbPassword1.Password);
            string sqlExpression1 = $"INSERT INTO USERS ([Login], [Password]) VALUES ('{txbLogin.Text}', '{password}')";
            string sqlExpression2 = "SELECT * FROM USERS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    if (txbLogin.Text.ToString() != String.Empty &&
                        psbPassword1.Password != String.Empty && 
                        psbPassword2.Password != String.Empty)
                    {
                        if (psbPassword1.Password.Length < 6)
                        {
                            string tempString = "Пароль должен быть не менее 6 символов";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                            auxiliaryWindow2.ShowDialog();
                            return;
                        }

                        if (psbPassword1.Password != psbPassword2.Password)
                        {
                            string tempString = "Пароли не совпадают!";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                            auxiliaryWindow2.ShowDialog();
                            return;
                        }
                        
                        SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                        SqlDataReader reader = command2.ExecuteReader();

                        Classes.User tempUser = new Classes.User();
                        bool flagPerson = false;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (txbLogin.Text == (string)reader.GetValue(1))
                                {
                                    flagPerson = true;
                                    tempUser.IdUser = reader.GetValue(0);
                                    tempUser.Login = reader.GetValue(1);
                                    tempUser.Password = reader.GetValue(2);
                                    break;
                                }
                            }
                        }
                        reader.Close();


                        if (!flagPerson)
                        {
                            SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                            command1.ExecuteNonQuery();

                            string tempString = "Вы успешно зарегистрированы!";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                            auxiliaryWindow2.ShowDialog();

                            Login login = new Login(mainForm);
                            login.Show();
                            Close();
                        }
                        else
                        {
                            string tempString = "Такой пользователь уже существует!";
                            Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                            auxiliaryWindow2.ShowDialog();

                            txbLogin.Text = "";
                            psbPassword1.Password = "";
                            psbPassword2.Password = "";
                        }
                    }
                    else
                    {
                        Close();
                        string tempString = "Введите корректные данные";
                        Windows.AuxiliaryWindow2 auxiliaryWindow2 = new Windows.AuxiliaryWindow2(mainForm, tempString);
                        auxiliaryWindow2.ShowDialog();

                        txbLogin.Text = "";
                        psbPassword1.Password = "";
                        psbPassword2.Password = "";
                        return;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public static string HashPassword(string password)
        {
            ///идент.пароли не приводят к одному хешу
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            //генерация ключа password с размером соли 16 байт и 1000 итераций
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;              //генерация соли
                buffer2 = bytes.GetBytes(0x20); //возвращает значение ключа с числом генерируемых псевдослучайных байтов ключа
            }
            byte[] dst = new byte[0x31];                    //формируем массив для хешированного пароля
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);        //копируем соль в хешированный пароль по 15 байт
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);  // копируем ключ в хешированный пароль с 16 по 49 байт
                                                            
            return Convert.ToBase64String(dst);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();

            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;
        }
    }
}

