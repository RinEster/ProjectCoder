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

namespace ProjectCoder.View
{
    /// <summary>
    /// Логика взаимодействия для Tests.xaml
    /// </summary>
    public partial class Tests : UserControl
    {
        public Tests()
        {
            InitializeComponent();
            TestList();
        }

        /// <summary>
        /// добавление списка тестов
        /// </summary>
        public void TestList()
        {
            SqlConnection con = new SqlConnection(Courses.ConnStr);
            SqlCommand command = new SqlCommand("Select Distinct  TopicDirectory.[Name],BankQuestions.TopicID From TopicDirectory inner join  " +
                "BankQuestions on BankQuestions.TopicID = TopicDirectory.ID", con);
            con.Open(); //Открываем соединение
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                test.Items.Add(read.GetValue(0).ToString());
            }
            con.Close();
        }
      
    }
}
