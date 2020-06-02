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

namespace Rekenmachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbInput.Text += ((Button) sender).Content;
        }

        private void bDivide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bMultiply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bSubtract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
