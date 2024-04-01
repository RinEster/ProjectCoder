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
            newProject.Document.Blocks.Clear(); 
            newProject.AppendText(fileContent); 
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "TXTFiles\\NewProject.txt";
            string fileContent = File.ReadAllText(filePath);
            TextRange textRange = new TextRange(newProject.Document.ContentStart, newProject.Document.ContentEnd);
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
