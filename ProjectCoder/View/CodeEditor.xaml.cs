using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ProjectCoder.View
{
    /// <summary>
    /// Логика взаимодействия для CodeEditor.xaml
    /// </summary>
    public partial class CodeEditor : UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();
            loadCodeEditor();
        }

        /// <summary>
        /// отображение примера кода в элементе RichTextBox
        /// </summary>
        void loadCodeEditor()
        {
            string filePath = "TXTFiles\\NewProject.txt";
            string fileContent = File.ReadAllText(filePath);
            newProject.Document.Blocks.Clear();
            newProject.AppendText(fileContent);
        }

        /// <summary>
        /// кнопка проверки решения задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_Click(object sender, RoutedEventArgs e)
        {
            switch (task)
            {
                case "1":

                    string task1 = "TXTFiles\\task1.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task1);
                    break;
                case "2":
                    string task2 = "TXTFiles\\task2.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task2); 
                    break;
                case "3":
                    string task3 = "TXTFiles\\task3.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task3); 
                    break;
                case "4":
                    string task4 = "TXTFiles\\task4.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task4);
                    break;
                case "5":
                    string task5 = "TXTFiles\\task5.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task5);
                    break;
                case "6":
                    string task6 = "TXTFiles\\task6.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task6);
                    break;
                case "7":
                    string task7 = "TXTFiles\\task7.txt";
                    checkCode(newProject.Document.ContentStart, newProject.Document.ContentEnd, task7);
                    break;
            }           
        }

        /// <summary>
        /// метод, проверяющий решение задачи
        /// </summary>
        /// <param name="projectStart">начальная точка документа</param>
        /// <param name="projectEnd">конечная точка документа</param>
        /// <param name="filePath">путь к документу</param>
        void checkCode(TextPointer projectStart, TextPointer projectEnd,string filePath)
        {            
            string fileContent = File.ReadAllText(filePath);
            TextRange textRange = new TextRange(projectStart, projectEnd);
            string richTextBoxContent = textRange.Text;

            if (fileContent == richTextBoxContent)
            {
                MessageBox.Show("Верно");
            }
            
        }
      
        /// <summary>
        /// чекбокс, отвечающий за отображение задач
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTask_Checked(object sender, RoutedEventArgs e)
        { 
            taskListView.Visibility = Visibility.Visible;          
        }

        /// <summary>
        /// чекбокс, отвечающий за скрытие задач
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTask_Unchecked(object sender, RoutedEventArgs e)
        {
            taskListView.Visibility = Visibility.Hidden;
            taskGrid.Height = 0;
            richGrid.Height = 430;
        }

        /// <summary>
        /// номер задачи
        /// </summary>
        public string task;

        /// <summary>
        /// текст задач
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ListView).SelectedItem;
            task = selected.ToString();
            int firstSpaceIndex = task.IndexOf(' '); // Индекс первого пробела
            int secondSpaceIndex = task.IndexOf(' ', firstSpaceIndex + 1); // Индекс второго пробела
            task = task.Substring(secondSpaceIndex + 1);
            switch (task)
            {
                case "1":
                    taskTextBlock.Text = "Напишите консольную программу, в которую пользователь вводит с клавиатуры два числа. А программа должна выводить на консоль сумму двух введенных чисел.";
                    break;
                case "2":
                    taskTextBlock.Text = "Напишите консольную программу, в которую пользователь вводит с клавиатуры два числа. А программа должна выводить на консоль большее число (в программе использовать if-else)";
                    break;
                case "3":
                    taskTextBlock.Text = "Напишите консольную программу, в которую пользователь вводит с клавиатуры два числа - a и b. А программа должна выводить на консоль все числа начиная от a, и заканчивая b (в программе использовать for)";
                    break;
                case "4":
                    taskTextBlock.Text = "Напишите программу, которая запрашивает число у пользователя и сообщает, является ли оно четным или нечетным.";
                    break;
                case "5":
                    taskTextBlock.Text = "Напишите программу, которая запрашивает длину a и ширину b прямоугольника, а затем выводит его площадь.";
                    break;
                case "6":
                    taskTextBlock.Text = "Напишите консольную программу, которая запрашивает у пользователя целые числа до тех пор, пока пользователь не введет число 0. После этого программа должна вывести сумму всех введенных чисел.";
                    break;
                case "7":
                    taskTextBlock.Text = "Напишите программу, которая сортирует массив целых чисел { 1, 12, 31, 5, 23 } по возрастанию (наиболее простым способом и вывод массива с помощью foreach)";
                    break;               

            }
            taskGrid.Height = 60;
            richGrid.Height = 370;            
        }
    }
}
