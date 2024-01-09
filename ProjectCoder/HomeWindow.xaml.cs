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

namespace ProjectCoder
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {          

            if (tgButton.IsChecked == true)
            {
                ttHome.Visibility = Visibility.Collapsed;
                ttUser.Visibility = Visibility.Collapsed;
                ttCourses.Visibility = Visibility.Collapsed;
                ttSettings.Visibility = Visibility.Collapsed;
                ttExit.Visibility = Visibility.Collapsed;              
            }
            else
            {
                ttHome.Visibility = Visibility.Visible;
                ttUser.Visibility = Visibility.Visible;
                ttCourses.Visibility = Visibility.Visible;
                ttSettings.Visibility = Visibility.Visible;
                ttExit.Visibility = Visibility.Visible;                
            }
        }
    }
}
