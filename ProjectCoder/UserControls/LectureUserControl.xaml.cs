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
        Courses courses = new Courses();

        public LectureUserControl()
        {
            InitializeComponent();
         
            
            object name = GetCourses().Rows[0][0];
            object lection = GetCourses().Rows[0][1];

            nameLectureTextBlock.Text = name.ToString();

            lecture.Text = lection.ToString();
        }

        public static DataTable GetCourses() // заполнение датасет данными из бд
        {
            SqlConnection connection = new SqlConnection(Courses.ConnStr);
            string sqlCourses = "SELECT TopicDirectory.[Name], BankLessons.Text FROM TopicDirectory, BankLessons where TopicDirectory.ID = BankLessons.TopicID";
            SqlDataAdapter da = new SqlDataAdapter(sqlCourses, connection);
            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                // Заполнить DataSet
                da.Fill(ds, "BankLessons");
                DataTable tempTable = ds.Tables[0];
                dataTable = tempTable.Copy(); 
            }
            finally
            {
                connection.Close();
            }        

            return dataTable;
     

        }

    }
}
