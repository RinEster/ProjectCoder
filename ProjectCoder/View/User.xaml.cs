using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ProjectCoder.View
{

    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {       
        public User()
        {
            InitializeComponent();
            userLogin.Text = MainWindow.loginUser;
            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                connection.Open();

                string sqlQuery = "SELECT CASE WHEN Email IS NOT NULL THEN Email ELSE '' END AS Email FROM Users WHERE Login = @userLogin";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@userLogin", MainWindow.loginUser);

                    string email = (string)command.ExecuteScalar();
                    userEmail.Text = email;
                }
            }
            loadingStudiedLessons();
            testUser();

        }

        /// <summary>
        /// все пройденные пользователем лекции
        /// </summary>
        public void loadingStudiedLessons()
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                connection.Open();

                string sqlQuery = "Select StatusLesson.Lesson From StatusLesson where Login =  @userLogin";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@userLogin", MainWindow.loginUser);
                    SqlDataReader read = command.ExecuteReader();
                    while (read.Read())
                    {
                        studiedLessons.Items.Add(read.GetValue(0).ToString());
                    }
                    
                  
                }
            }       
                              
        }
        /// <summary>
        /// все пройденные пользователем тесты
        /// </summary>
        public void testUser()
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                connection.Open();

                string proc = "dbo.resultUser";
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@login", MainWindow.loginUser));

                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    test.Items.Add(reader.GetValue(0).ToString());
                    testResult.Items.Add(reader.GetValue(1).ToString());
                }

            }

        }
    }
}
