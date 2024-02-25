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
                }
                else
                {
                    changeUserName(MainWindow.loginUser, changeLogin.Text);
                }
            }
            if (changeEmail.Text != "")
            {
                if (!Regex.IsMatch(changeEmail.Text, patternPass))
                {
                    changeEmail.ToolTip = "Разрешены почты yandex, mail, gmail";
                }
                else
                {
                    changeUserEmail(MainWindow.loginUser, changeEmail.Text);
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

        }
    }
}
