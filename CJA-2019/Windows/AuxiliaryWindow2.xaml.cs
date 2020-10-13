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
    /// Логика взаимодействия для AuxiliaryWindow2.xaml
    /// </summary>
    public partial class AuxiliaryWindow2 : Window
    {
        MainForm mainForm;
        public AuxiliaryWindow2(MainForm mainForm, string tempString)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            mainForm.gridBackGround.Background = new SolidColorBrush(Colors.Black);
            mainForm.gridBackGround.Opacity = 0.5;

            txbTempText.Text = tempString;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            mainForm.gridBackGround.Background = null;
            mainForm.gridBackGround.Opacity = 1;

            Close();
        }
    }
}
 