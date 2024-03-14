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
        /// добавление списка тем тестов
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
        public string answer ;
        public int correctAnswerCount = 0;
        public int allAnswerCount = 0;

        /// <summary>
        /// загрузка теста по выбранной теме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
             
                resultTable = new DataTable();              
                using (SqlDataReader reader = relatedQuestions.ExecuteReader())
                {
                    resultTable.Load(reader);
                }   

                testUserControl.questionsData(nameTopic, resultTable, count);
                while (tests.Children.Count > 0)
                {
                    tests.Children.RemoveAt(0);
                }
                tBorder.Background = Brushes.Transparent;
                tests.Children.Add(testUserControl);
                allAnswerCount = resultTable.Rows.Count;                
                forward.Visibility = Visibility.Visible;               
            
                gGrid.Height = 70;
            }
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
      
            MessageBoxResult result = MessageBox.Show("Принять ответ?", "Тестирование",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool res = checkAnswer();
                if (res == true)
                {
                    correctAnswerCount++;
                }

                if (count < resultTable.Rows.Count - 1)
                {
                    count++;
                    testUserControl.questionsData(nameTopic, resultTable, count);
                }
                else
                {
                    using(SqlConnection conn = new SqlConnection(MainWindow.ConnStrA))
                    {
                        conn.Open();
                        string proc = "dbo.addResultTest";
                        SqlCommand command = new SqlCommand(proc, conn);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@login", MainWindow.loginUser));
                        command.Parameters.Add(new SqlParameter("@testName", nameTopic));
                        command.Parameters.Add(new SqlParameter("@result", correctAnswerCount));

                        command.ExecuteScalar();
                    }

                    MessageBox.Show("Тест пройден. Результат " + correctAnswerCount + " из " + allAnswerCount);
                }
            }           
          
        }

        public bool checkAnswer()
        {
            bool result = false;
            answer = resultTable.Rows[count][2].ToString();
            string response = testUserControl.responseForVerification(answer);
            if (response == answer)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

    }
}
