using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для TestUserControl.xaml
    /// </summary>
    public partial class TestUserControl : UserControl
    {
        public TestUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// выгрузка вопросов и ответов из базы данных в textblock и radiobutton
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="resultTable"></param>
        /// <param name="count"></param>
        public void questionsData(string topicName, DataTable resultTable, int count)
        {
            nameTestTextBlock.Text = topicName;          
            question.Text = resultTable.Rows[count][1].ToString();
            optionOne.Text = resultTable.Rows[count][3].ToString();
            optionTwo.Text = resultTable.Rows[count][4].ToString();
            optionThree.Text = resultTable.Rows[count][5].ToString();
            optionFour.Text = resultTable.Rows[count][6].ToString();
           
        }
        /// <summary>
        ///  для получения выбранного ответа
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string responseForVerification(string response)
        {
            if (oneRadioButton.IsChecked == true) response = optionOne.Text;
            if (twoRadioButton.IsChecked == true) response = optionTwo.Text;
            if (threeRadioButton.IsChecked == true) response = optionThree.Text;
            if (fourRadioButton.IsChecked == true) response = optionFour.Text;
            return response;
        }
    }
}
