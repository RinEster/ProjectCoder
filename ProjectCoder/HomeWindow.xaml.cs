﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ProjectCoder.Utilities;
using ProjectCoder.ViewModel;
using ProjectCoder.View;
namespace ProjectCoder
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Закрыть приложение?", "Завершение работы",
           MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {          

            if (tgButton.IsChecked == true)
            {
                ttHome.Visibility = Visibility.Collapsed;
                ttUser.Visibility = Visibility.Collapsed;
                ttCourses.Visibility = Visibility.Collapsed;
                ttSettings.Visibility = Visibility.Collapsed;
                ttExit.Visibility = Visibility.Collapsed;              
            }
            else
            {
                ttHome.Visibility = Visibility.Visible;
                ttUser.Visibility = Visibility.Visible;
                ttCourses.Visibility = Visibility.Visible;
                ttSettings.Visibility = Visibility.Visible;
                ttExit.Visibility = Visibility.Visible;                
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Width = 535;
            settings.Margin = new Thickness(0, 0, -535, 0);
            Pages.Children.Add(settings);

           
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Width = 535;
            user.Margin = new Thickness(0, 0, -535, 0);
            Pages.Children.Add(user);
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Width = 535;
            home.Margin = new Thickness(0, 0, -535, 0);
            Pages.Children.Add(home);
        }

        private void сoursesButton_Click(object sender, RoutedEventArgs e)
        {
            Courses courses = new Courses();
            courses.Width = 535;
            courses.Margin = new Thickness(0, 0, -535, 0);
            Pages.Children.Add(courses);

        }

        private void testsButton_Click(object sender, RoutedEventArgs e)
        {
            Tests tests = new Tests();
            tests.Width = 535;
            tests.Margin = new Thickness(0, 0, -535, 0);
            Pages.Children.Add(tests);
        }

        private void codeButton_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor codeEditor = new CodeEditor();
            codeEditor.Width = 535;
            codeEditor.Margin = new Thickness(0, 0, -535, 0);
            Pages.Children.Add(codeEditor);
        }
    }
   
    
}
