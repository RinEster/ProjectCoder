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
using System.Windows.Threading;

namespace ProjectCoder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string ConnStrA = "Data Source = DESKTOP-E6HKS9J\\SQLEXPRESS;Initial Catalog = \"CodeVerseUsers\"; Integrated Security = True"; //строка одключения бд
        public static string loginUser;


        public MainWindow()
        {
            InitializeComponent();
        }
        DispatcherTimer timer = new DispatcherTimer();

        private void authenticationButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            string password = passwordPassBox.Password.Trim();         
            if (login != "" && password != "")
            {
                Entrance(login, password);             
            }
            if (login.Length == 0)
            {
                loginTextBox.ToolTip = "Введите логин";
                loginTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
            }
            if (passwordPassBox.Password.ToString() == "")
            {
                passwordPassBox.ToolTip = "Введите пароль";
                passwordPassBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
            }
        
            
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private int Entrance(string login, string pass) 
        {    
            string sqlExpression = "dbo.Entrance";
            using (SqlConnection connection = new SqlConnection(ConnStrA))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Paramlog = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                command.Parameters.Add(Paramlog);
                SqlParameter Parampas = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = pass
                };
                command.Parameters.Add(Parampas);
                var reader = (Int32)command.ExecuteScalar();
                if (reader != 0)
                {
                    loginUser = login;
                    //timer.Interval = TimeSpan.FromSeconds(0.5);
                    //timer.Tick += Timer_Tick;
                    //timer.Start();
                    HomeWindow homeWindow = new HomeWindow();
                    homeWindow.Show();
                    Close();
                }
                else 
                {
                    loginTextBox.ToolTip = "Неверно введенные данные";
                    loginTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                    passwordPassBox.ToolTip = "Неверно введенные данные";
                    passwordPassBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                }
                return 0;
            }

        }

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    timer.Stop();
        //    HomeWindow homeWindow = new HomeWindow();
        //    homeWindow.Show();
        //    Close();
        //}

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            Close();
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите завершить работу?", "Завершение работы",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }           
        }
    }
}
