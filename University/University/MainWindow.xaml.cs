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
            for (int i = 0; i < Groups.Count; i++)
            {
                cbGroup.Items.Add(Groups[i].Name);
            }
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
                GroupName = cbGroup.SelectedItem.ToString()
            };

            service.AddStudents(students);
            Update(service);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = "";
            tbSurname.Text = "";
            cbGroup.Text = "";
            int stId = (dgStudent.Items[dgStudent.SelectedIndex] as StudentsDTO).Id;

            foreach (var item in Students)
            {
                if (item.Id == stId)
                {
                    service.DeleteStudent(item);
                    break;
                }

            }
            Update(service);
        }

        private void btnUpDate_Click(object sender, RoutedEventArgs e)
        {
            int stId = (dgStudent.Items[dgStudent.SelectedIndex] as StudentsDTO).Id;

            foreach (var item in Students)
            {
                if (item.Id == stId)
                {
                    item.Surname = tbSurname.Text;
                    item.Name = tbName.Text;
                    item.GroupName = cbGroup.SelectedItem.ToString();
                    service.UpdateStudent(item);
                    break;
                }
            }
            Update(service);
        }

        private void dgStudent_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                int stId = (dgStudent.Items[dgStudent.SelectedIndex] as StudentsDTO).Id;

                foreach (var item in Students)
                {
                    if (item.Id == stId)
                    {
                        tbName.Text = item.Name;
                        tbSurname.Text = item.Surname;
                        cbGroup.Text = item.GroupName;
                        break;
                    }
                }
            }
            catch (Exception ex) { };
        }
    }
}
