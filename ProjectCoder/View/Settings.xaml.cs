﻿using System;
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

namespace ProjectCoder.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }


        private void changeTheLoginEmailButton_Click(object sender, RoutedEventArgs e)
        {
            string patternLog = @"^[a-zA-Z][a-zA-Z0-9_-]{10,50}$"; //первый символ обязательно буква, можно исп латиницу, цифры,девис и подчеркивание
            string patternPass = @"[A-Za-z0-9._%+-]+@(?:yandex|mail|gmail)\.(?:ru|com)";
            if (changeLogin.Text!="")
            {
                if(!Regex.IsMatch(changeLogin.Text, patternLog))
                {
                    changeLogin.ToolTip = "Разрешенные символы: \ncтрочные/заглавные буквы латинского алфавита,\nцифры от 0 до 9 \nдефис и подчеркивания.\n Логин должен быть не менее 10 и не более 50 символов.";
                    changeLogin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                }
                else
                {
                    changeUserName(MainWindow.loginUser, changeLogin.Text);
                    changeLogin.Text = "";
                }
            }
            if (changeEmail.Text != "")
            {
                if (!Regex.IsMatch(changeEmail.Text, patternPass))
                {
                    changeEmail.ToolTip = "Разрешены почты yandex, mail, gmail";
                    changeEmail.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                }
                else
                {
                    changeUserEmail(MainWindow.loginUser, changeEmail.Text);
                    changeEmail.Text = "";
                }                    
            }

        }

        /// <summary>
        /// изменение логина пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="newlogin"></param>
        private void changeUserName(string login, string newlogin)
        {
            string sqlExpressionEmail = "dbo.UpdateUserLogin";

            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                using (SqlCommand command = new SqlCommand(sqlExpressionEmail, connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@login", login));
                    command.Parameters.Add(new SqlParameter("@newLogin", newlogin));

                    SqlParameter resultParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                    resultParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParameter);
                  
                    command.ExecuteNonQuery();

                    string result = resultParameter.Value.ToString();
                    MessageBox.Show(result);
                }

            }
        }

        /// <summary>
        /// изменение почты пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="newlogin"></param>
        private void changeUserEmail(string login, string email)
        {
            string sqlExpressionEmail = "dbo.UpdateEmail";

            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                using (SqlCommand command = new SqlCommand(sqlExpressionEmail, connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@login", login));
                    command.Parameters.Add(new SqlParameter("@newemail", email));

                    SqlParameter resultParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                    resultParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParameter);

                    command.ExecuteNonQuery();

                    string result = resultParameter.Value.ToString();                    
                    MessageBox.Show(result);
                }

            }
        }

        private void changePassButton_Click(object sender, RoutedEventArgs e)
        {
            string patternPass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,20}$";
            bool check = true;
            if (passTextBox.Text=="")
            {
                passTextBox.ToolTip = "Это поле обязательно при изменении пароля";
                passTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                check = false;
            }
            if (newPassTextBox.Text == "")
            {
                newPassTextBox.ToolTip = "Это поле обязательно при изменении пароля";
                newPassTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                check = false;
            }
            if (repNewPassTextBox.Text == "")
            {
                repNewPassTextBox.ToolTip = "Это поле обязательно при изменении пароля";
                repNewPassTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                check = false;
            }
            if (!Regex.IsMatch(newPassTextBox.Text, patternPass))
            {
                newPassTextBox.ToolTip = "В пароле должна быть минимум одна цифра,\nодна буква(английская), большая буква и любой знак,\nкоторый не цифра и не буква, максимальная длина пароля 20 символов.\nМинимальная длина пароля 8 символов.\nТак же в пароле не может быть пробелов.";
                newPassTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                check = false;
            }
            if (newPassTextBox.Text!=repNewPassTextBox.Text)
            {
                newPassTextBox.ToolTip = "Пароли должны совпадать";
                repNewPassTextBox.ToolTip = "Пароли должны совпадать";
                newPassTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                repNewPassTextBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9EB4"));
                check = false;
            }

            if (check == true)
            {
                changePassWord(MainWindow.loginUser, newPassTextBox.Text);
            }
        }

        /// <summary>
        /// изменение пароля пользователем
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        private void changePassWord(string login, string pass)
        {
            string sqlExpressionEmail = "dbo.RegistrationPasswordUpdate";

            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                using (SqlCommand command = new SqlCommand(sqlExpressionEmail, connection))
                {
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@login", login));
                    command.Parameters.Add(new SqlParameter("@password", pass));

                    SqlParameter resultParameter = new SqlParameter("@res", SqlDbType.NVarChar, 50);
                    resultParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParameter);

                    command.ExecuteNonQuery();

                    string result = resultParameter.Value.ToString();
                    MessageBox.Show(result);
                }

            }
        }
    }
}
