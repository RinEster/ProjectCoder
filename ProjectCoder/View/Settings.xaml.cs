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


        private void changeTheLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string patternPass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,20}$"; //В пароле должна быть минимум одна цифра, одна буква(английская), большая буква и любой знак, который не цифра и не буква, максимальная длина пароля 16 символов.(И пробелы нельзя)            
            string patternLog = @"^[a-zA-Z][a-zA-Z0-9_-]{10,50}$"; //первый символ обязательно буква, можно исп латиницу, цифры,девис и подчеркивание
          
            if (changeLogin.Text!="")
            {
                if(!Regex.IsMatch(changeLogin.Text, patternLog))
                {
                    changeLogin.ToolTip = "В пароле должна быть минимум одна цифра,\nодна буква(английская), большая буква и любой знак,\nкоторый не цифра и не буква, максимальная длина пароля 20 символов.\nМинимальная длина пароля 8 символов.\nТак же в пароле не может быть пробелов.";
                }
                else
                {
                    changeUserName(MainWindow.loginUser, changeLogin.Text);
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
    }
}
