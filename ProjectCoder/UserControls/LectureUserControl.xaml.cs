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

            using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA))
            {
                connection.Open(); 

                string query = "SELECT CASE WHEN StatusLesson = 1 THEN 'Изучено' ELSE 'Завершить?' END AS результат FROM StatusLesson WHERE Lesson = @lesson AND Login = @UserLogin";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserLogin", MainWindow.loginUser);
                    command.Parameters.AddWithValue("@lesson", Courses.nameLecture);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status.Text = reader["результат"].ToString();                           
                        }
                    }
                }
            }
        }

        /// <summary>
        /// добавление статуса изучения материала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void status_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int st = 1;
            MessageBoxResult result = MessageBox.Show("Материал изучен?", "Обучение",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string sqlExpressionEmail = "dbo.addStatusLesson"; 

                using (SqlConnection connection = new SqlConnection(MainWindow.ConnStrA)) 
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpressionEmail, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter ParamLog = new SqlParameter
                    {
                        ParameterName = "@login",
                        Value = MainWindow.loginUser
                    };
                    command.Parameters.Add(ParamLog);
                    SqlParameter ParamLesson = new SqlParameter
                    {
                        ParameterName = "@lesson",
                        Value = nameLectureTextBlock.Text
                    };
                    command.Parameters.Add(ParamLesson);
                    SqlParameter ParamStatus = new SqlParameter
                    {
                        ParameterName = "@statusLesson",
                        Value = st
                    };
                    command.Parameters.Add(ParamStatus);
                    command.ExecuteScalar();
                    status.Text = "Изучено";
                }
            }
            else
            {
                status.Text = "В процессе";
            }
        }
    }
}
