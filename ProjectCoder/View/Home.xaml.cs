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
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        //string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=News;" +
        //    "Integrated Security=True; AttachDbFilename = |DataDirectory|\\News.mdf"; 

        //string newsData = "Select News, NewsDate from News";

        //List<KeyValuePair<string, DateTime>> NewsDateList = new List<KeyValuePair<string, DateTime>>();

        //string newsString;
        //DateTime newsDateTime;

        public Home()
        {
            InitializeComponent();
            ////заполнение новостей
            //DataSet ds1 = new DataSet();
            //using (SqlConnection connection1 = new SqlConnection(connStr))
            //{
            //    connection1.Open();

            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(newsData, connStr);

            //    dataAdapter.Fill(ds1, "News"); //заполнение dataset

            //    foreach(DataRow row in ds1.Tables[0].Rows) //перенос данных в список
            //    {
            //        NewsDateList.Insert(0, new KeyValuePair<string, DateTime>(Convert.ToString(row["News"]), Convert.ToDateTime(row["NewsDate"])));                   
            //    }


            //    if (NewsDateList != null)
            //    {
            //        foreach (var keyValues in NewsDateList) //запонение данными usercontrol
            //        {   
            //            NewsUserControl us = new NewsUserControl();
            //            us.Margin = new Thickness(5, 5, 5, 5);
            //            newsString = keyValues.Key.ToString();
            //            newsDateTime = keyValues.Value;
            //            us.newsTextBlock.Text = newsString;
            //            us.dateTextBlock.Text = newsDateTime.ToString("dd/MM/yyyy");
            //            news.Children.Add(us);
            //        }
            //    }                  
                    
           // } 
        }        
    }
}

