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
        }
    }
}
