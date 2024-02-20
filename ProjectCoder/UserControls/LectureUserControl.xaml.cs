using ProjectCoder.View;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ProjectCoder.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LectureUserControl.xaml
    /// </summary>
    public partial class LectureUserControl : UserControl
    {
        public LectureUserControl()
        {
            InitializeComponent();      
        }

        private void status_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Материал изучен?", "Обучение",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
               
            }
            else
            {

            }
        }
    }
}
