using ProjectCoder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCoder.ViewModel
{
     class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public RelayCommand HomeCommand { get; set; }
        public RelayCommand UserCommand { get; set; }
        public RelayCommand CoursesCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand PersonalAreaCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void User(object obj) => CurrentView = new UserVM();
        private void Courses(object obj) => CurrentView = new CoursesVM();
        private void Settings(object obj) => CurrentView = new SettingsVM();
       
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            UserCommand = new RelayCommand(User);
            CoursesCommand = new RelayCommand(Courses);
            SettingsCommand = new RelayCommand(Settings);


            //Первая открытая страница
            CurrentView = new HomeVM();
        }
    }
}
