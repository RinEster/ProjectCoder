using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
      
        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите завершить работу?", "Завершение работы",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }           
        }


        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void regButton_Click(object sender, RoutedEventArgs e)
        {

            bool res = inputValidation(loginRegTextBox.Text, passRegPassBox.Text, passRegRepPassBox.Text);
            if(res==true)
            {
            loginUser = loginRegTextBox.Text;
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
            Close();
            }
            
        }


        private bool inputValidation(string login, string password, string dpassword)
        {
            string patternPass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,20}$"; //В пароле должна быть минимум одна цифра, одна буква(английская), большая буква и любой знак, который не цифра и не буква, максимальная длина пароля 16 символов.(И пробелы нельзя)            
            string patternLog = @"^[a-zA-Z][a-zA-Z0-9_-]{10,50}$"; //первый символ обязательно буква, можно исп латиницу, цифры,девис и подчеркивание
            bool check = true;
            if (login.Length == 0)
            {
                check = false;              
                loginRegTextBox.ToolTip = "Это поле обязательно для заполнения";
                loginRegTextBox.Background = Brushes.Coral;
            }
            else
            {
                loginRegTextBox.Background = Brushes.Transparent;
            }
            if (password.Length == 0)
            {
                check = false;
                passRegPassBox.ToolTip = "Это поле обязательно для заполнения";
                passRegPassBox.Background = Brushes.Coral;
            }
            else
            {
                passRegPassBox.Background = Brushes.Transparent;
            }
            if (dpassword.Length == 0)
            {
                check = false;
                passRegRepPassBox.ToolTip = "Это поле обязательно для заполнения";
                passRegRepPassBox.Background = Brushes.Coral;
            }
            else
            {
                passRegRepPassBox.Background = Brushes.Transparent;
            }


            //Проверка входных данных
            if (!Regex.IsMatch(login, patternLog)) //Проверяю на соответствие шаблону
            {
                check = false;
                loginRegTextBox.ToolTip = "Разрешенные символы: \ncтрочные/заглавные буквы латинского алфавита,\nцифры от 0 до 9 \nдефис и подчеркивания.\n Логин должен быть не менее 10 и не более 50 символов.";
                loginRegTextBox.Background = Brushes.Coral;
            }
            else
            {
                loginRegTextBox.Background = Brushes.Transparent;
            }
            if (!Regex.IsMatch(password, patternPass))
            {
                check = false;
                passRegPassBox.ToolTip = "В пароле должна быть минимум одна цифра,\nодна буква(английская), большая буква и любой знак,\nкоторый не цифра и не буква, максимальная длина пароля 20 символов.\nМинимальная длина пароля 8 символов.\nТак же в пароле не может быть пробелов.";
                passRegPassBox.Background = Brushes.Coral;
            }
            else
            {
                passRegPassBox.Background = Brushes.Transparent;
            }

            if(password!=dpassword)
            {
                check = false;
                passRegPassBox.ToolTip = "Пароли должны совпадать";
                passRegRepPassBox.ToolTip = "Пароли должны совпадать";
                passRegPassBox.Background = Brushes.Coral;
                passRegRepPassBox.Background = Brushes.Coral;
            }
            else
            {
                passRegRepPassBox.Background = Brushes.Transparent;
            }

            if (check == true)
            {
                
                registrationUser(login, password);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void registrationUser(string login, string pass)
        {
          //  bool check = true;
            string sqlExpressionEmail = "dbo.RegistrationCheckForAccountAvailability";

            using (SqlConnection connection = new SqlConnection(ConnStrA)) 
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressionEmail, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Paramlog = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                command.Parameters.Add(Paramlog);

                SqlParameter Parampass = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = pass
                };
                command.Parameters.Add(Parampass);


                SqlParameter resultParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                resultParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(resultParameter);
                command.ExecuteNonQuery();

                string result = resultParameter.Value.ToString();
                MessageBox.Show(result);

            }

        }

        private void exitReg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow1 = new MainWindow();
            mainWindow1.Show();
            this.Close();
        }
    }
}
