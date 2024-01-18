using ProjectCoder.UserControls;
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

namespace ProjectCoder.View
{
    /// <summary>
    /// Логика взаимодействия для Courses.xaml
    /// </summary>
    public partial class Courses : UserControl
    {
        public static string ConnStr = "Data Source = DESKTOP-E6HKS9J\\SQLEXPRESS;Initial Catalog = \"CodeVerseLeesons\"; Integrated Security = True"; //строка одключения бд

        public string nameLecture;

        public object name;
        public object lection;

        public Courses()
        {
            InitializeComponent();
            FillParetChildTree();

           

        }

       


        /// <summary>
        /// метод для заполнения treeview
        /// </summary>
        public void FillParetChildTree()
        {
            DataSet ds = GetCources();
            var Items = new List<ItemInfo>();
            var query = from ID in ds.Tables["TopicDirectory"].AsEnumerable()
                        from ChildCategoryID in ds.Tables["ParentChildTree"].AsEnumerable()
                        where (int)ID["ID"] == (int)ChildCategoryID["ChildID"]
                        select new { Child = ChildCategoryID["ChildID"], Name = ID["Name"], Parent = ChildCategoryID["ParentID"] };

            foreach (var item in query)
            {
                var items = new ItemInfo() { ChildId = Convert.ToInt32(item.Child), Name = Convert.ToString(item.Name), ParentId = Convert.ToInt32(item.Parent) };

                Items.Add(items);
            }

            FillNode(Items, null);
        }

        /// <summary>
        /// Рекурсивный метод для получения родителей-детей
        /// </summary>
        /// <param name="items"></param>
        /// <param name="node"></param>
        public void FillNode(List<ItemInfo> items, TreeViewItem node)
        {
            var parentId = node != null ? (int)node.Tag : 0;
            var nodesCollection = node != null ? node.Items : treeView.Items;
            foreach (var item in items.Where(i => i.ParentId == parentId))
            {
                var newNode = new TreeViewItem { Header = item.Name, Tag = item.ChildId };
                nodesCollection.Add(newNode);
                FillNode(items, newNode);
            }
        }

        public class ItemInfo
        {
            public int ChildId;
            public int ParentId;
            public string Name;
        }
        /// <summary>
        /// Вспомогательный метод, возвращающий DataSet для двух таблиц TopicDirectory и ParentChildTree
        /// </summary>
        /// <returns></returns>
        public DataSet GetCources()
        {
            SqlConnection connection = new SqlConnection(ConnStr);
            string sqlCat = "SELECT * FROM TopicDirectory";
            string sqlTrees = "SELECT * FROM ParentChildTree";
            SqlDataAdapter da = new SqlDataAdapter(sqlCat, connection);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                da.Fill(ds, "TopicDirectory");              
                da.SelectCommand.CommandText = sqlTrees;
                da.Fill(ds, "ParentChildTree");
            }
            finally
            {
                connection.Close();
            }

            DataRelation relat = new DataRelation("CourcesTrees",
            ds.Tables["ParentChildTree"].Columns["ChildId"],
            ds.Tables["TopicDirectory"].Columns["ID"]);

            ds.Relations.Add(relat);

            return ds;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedNode = (TreeViewItem)treeView.SelectedItem;

            if (selectedNode.Items.Count == 0)
            {
                while (courses.Children.Count > 0)
                {
                    courses.Children.RemoveAt(0);
                }
                LectureUserControl lecture = new LectureUserControl();
                nameLecture = selectedNode.Header.ToString();
                courses.Children.Add(lecture);
              
            }
        }

       
    }
}
