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
using System.Windows.Shapes;

namespace CJA_2019.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuxiliaryWindow.xaml
    /// </summary>
    public partial class AuxiliaryWindow : Window
    {
        MainForm mainForm;
        ListView listPanel;
        public AuxiliaryWindow(MainForm mainForm ,string tempString, ListView listPanel)
        {
            this.mainForm = mainForm;
            this.listPanel = listPanel;
            InitializeComponent();

            txbTempText.Text = tempString;
            listPanel.SelectedIndex = -1;

            mainForm.gridBackGround.Background = new SolidColorBrush(Colors.Black);
            mainForm.gridBackGround.Opacity = 0.5;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();

            Login login = new Login(mainForm);
            login.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;

            Close();
        }
    }
}
