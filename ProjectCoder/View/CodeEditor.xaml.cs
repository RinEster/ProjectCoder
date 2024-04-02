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
            string filePath = "TXTFiles\\NewProject.txt"; 
            string fileContent = File.ReadAllText(filePath); 
            newProject1.Document.Blocks.Clear(); 
            newProject1.AppendText(fileContent);
            newProject2.Document.Blocks.Clear();
            newProject2.AppendText(fileContent);
            newProject3.Document.Blocks.Clear();
            newProject3.AppendText(fileContent);
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            checkCode(newProject1.Document.ContentStart, newProject1.Document.ContentEnd);
           
        }

        void checkCode(TextPointer projectStart, TextPointer projectEnd)
        {
            string filePath = "TXTFiles\\NewProject.txt";
            string fileContent = File.ReadAllText(filePath);
            TextRange textRange = new TextRange(projectStart, projectEnd);
            string richTextBoxContent = textRange.Text;

            if (fileContent == richTextBoxContent)
            {
                MessageBox.Show("Содержимое файла совпадает с текстом в RichTextBox.");
            }
            else
            {
                MessageBox.Show("Содержимое файла не совпадает с текстом в RichTextBox.");
            }
        }
    }
}
