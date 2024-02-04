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
using ProjectCoder.UserControls;

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
        public TestUserControl testUserControl = new TestUserControl();
        public string nameTopic;
        public DataTable resultTable;
        public int count = 0;
        public int max = 0;
        private void test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedListItem = (sender as ListBox).SelectedItem;
            nameTopic = selectedListItem.ToString();
            string proc = "dbo.RelatedQuestions";
            using(SqlConnection procConnection = new SqlConnection(Courses.ConnStr))
            {
                procConnection.Open();
                SqlCommand relatedQuestions = new SqlCommand(proc, procConnection);
                relatedQuestions.CommandType = CommandType.StoredProcedure;

                SqlParameter topicName = new SqlParameter
                {
                    ParameterName = "@topicName",
                    Value = selectedListItem
                };
                relatedQuestions.Parameters.Add(topicName);
                // Создание объекта DataTable для хранения результата
                resultTable = new DataTable();

                // Создание объекта SqlDataReader для чтения результата
                using (SqlDataReader reader = relatedQuestions.ExecuteReader())
                {
                    // Заполнение DataTable данными из результата
                    resultTable.Load(reader);
                }
           

                //// Отображение содержимого DataTable в MessageBox
                //StringBuilder message = new StringBuilder();
                //foreach (DataRow row in resultTable.Rows)
                //{
                //    foreach (DataColumn col in resultTable.Columns)
                //    {
                //        message.Append(row[col].ToString() + "\t");
                //    }
                //    message.AppendLine();
                //}
                //MessageBox.Show(message.ToString());
              
                testUserControl.questionsData(nameTopic, resultTable, count);



                tests.Children.Add(testUserControl);
            }

        }

        private void dalee_Click(object sender, RoutedEventArgs e)
        {
            if (count < resultTable.Rows.Count-1)
            {
                count++; testUserControl.questionsData(nameTopic, resultTable, count);
            }
          
        }

        private void naz_Click(object sender, RoutedEventArgs e)
        {
            if (count > 0) { count--; testUserControl.questionsData(nameTopic, resultTable, count); }
        }
    }
}
