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
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=News;" +
            "Integrated Security=True; AttachDbFilename = |DataDirectory|\\News.mdf";

        string newsData = "Select News, NewsDate from News";

        List<KeyValuePair<string, DateTime>> NewsDateList = new List<KeyValuePair<string, DateTime>>();

        string newsString;
        DateTime newsDateTime;

        public Home()
        {
            InitializeComponent();

           // NewsUserControl newsUserControl = new NewsUserControl();
            DataSet ds1 = new DataSet();
            using (SqlConnection connection1 = new SqlConnection(connStr))
            {
                connection1.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(newsData, connStr);

                dataAdapter.Fill(ds1, "News");

                foreach(DataRow row in ds1.Tables[0].Rows)
                {
                    NewsDateList.Insert(0, new KeyValuePair<string, DateTime>(Convert.ToString(row["News"]), Convert.ToDateTime(row["NewsDate"])));                   
                }


                if (NewsDateList != null)
                {
                    foreach (var keyValues in NewsDateList)
                    {
                        NewsUserControl us = new NewsUserControl();
                        newsString = keyValues.Key.ToString();
                        newsDateTime = keyValues.Value;
                        us.newsTextBlock.Text = newsString;
                        us.dateTextBlock.Text = newsDateTime.ToString();
                        news.Children.Add(us);
                    }
                }
                   
                    
            }
            //var dbBasket = new SqlCommand("[BasketLogin]", connection1);
            //dbBasket.CommandType = CommandType.StoredProcedure;
            //dbBasket.Parameters.AddWithValue("@loginProc", );
            //SqlDataAdapter dataAdapter = new SqlDataAdapter();
            //dataAdapter.SelectCommand = dbBasket;
            //dataAdapter.Fill(ds1);
            //foreach (DataRow row in ds1.Tables[0].Rows) //занесение результата хранимой  процедуры в список
            //{
            //    TitlePrice.Insert(0, new KeyValuePair<string, int>(Convert.ToString(row["title"]), Convert.ToInt32(row["price"])));
            //    sum += Convert.ToInt32(row["price"]);
            //}
            //if (TitlePrice != null)
            //{
            //    foreach (var keyValues in TitlePrice)
            //    {
            //        UserControls.Product us = new UserControls.Product();
            //        us.VerticalAlignment = VerticalAlignment.Top;
            //        title = keyValues.Key.ToString();
            //        price = keyValues.Value;
            //        us.FillData(title, price);
            //        panelTov.Children.Add(us);
            //    }
            //}
          
        }
          
            
        }
    }

