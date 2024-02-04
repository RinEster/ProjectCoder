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
      
        
    

        public void questionsData(string topicName, DataTable resultTable, int count)
        {         
                        
            nameTestTextBlock.Text = topicName;
            string answer = resultTable.Rows[count][2].ToString();
            question.Text = resultTable.Rows[count][1].ToString();
            optionOne.Content = resultTable.Rows[count][3].ToString();
            optionTwo.Content = resultTable.Rows[count][4].ToString();
            optionThree.Content = resultTable.Rows[count][5].ToString();
            optionFour.Content = resultTable.Rows[count][6].ToString();    
            
                  

        }

      
    }
}
