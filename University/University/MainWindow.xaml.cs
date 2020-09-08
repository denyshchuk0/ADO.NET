using System;
using System.Collections.ObjectModel;
using System.Windows;
using University.BLL.Model;
using University.BLL.Services;

namespace University
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUniversityService service;
        public ObservableCollection<StudentsDTO> Students { get; set; } = new ObservableCollection<StudentsDTO>();
        public ObservableCollection<GroupDTO> Groups { get; set; } = new ObservableCollection<GroupDTO>();
        public MainWindow(IUniversityService universityService)
        {
            InitializeComponent();
            service = universityService;
            Update(universityService);
            dgStudent.DataContext = Students;
            dgGroup.DataContext = Groups;

        }
        private void Update(IUniversityService universityService)
        {
            Students.Clear();
            Groups.Clear();
            var tempStud = universityService.GetStudents();
            var tempGroup = universityService.GetGroup();
            foreach (var item in tempStud)
            {
                Students.Add(item);
            }
            foreach (var item in tempGroup)
            {
                Groups.Add(item);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            StudentsDTO students = new StudentsDTO
            {
                Name = tbName.Text,
                Surname = tbSurname.Text,
                IdGroup = Convert.ToInt32(tbGroup.Text)
            };

            service.AddStudents(students);
            Update(service);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            StudentsDTO students = new StudentsDTO
            {
           
            };
      
        }
    }
}
