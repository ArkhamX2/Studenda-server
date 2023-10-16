using CommunityToolkit.Mvvm.ComponentModel;
using Studenda.Core.Model.Common;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Studenda.Core.Client.Views;
using System.Collections.ObjectModel;

namespace Studenda.Core.Client.ViewModels
{
    public partial class GroupSelectorViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();

        [ObservableProperty]
        private Group selectedGroup = new Group();

        [ObservableProperty]
        private ObservableCollection<Course> courses = new ObservableCollection<Course>();

        [ObservableProperty]
        private Course selectedCourse = new Course();

        [ObservableProperty]
        private ObservableCollection<Department> departments = new ObservableCollection<Department>();

        [ObservableProperty]
        private Department selectedDepartment = new Department();

        public GroupSelectorViewModel()
        {

            Groups.Add(new Group() { Name = "Б.ПИН.РИС.2106" });
            Groups.Add(new Group() { Name = "Б.ПИН.РИС.2106" });
            Groups.Add(new Group() { Name = "Б.ПИН.РИС.2106" });
            Groups.Add(new Group() { Name = "Б.ПИН.РИС.2106" });
            SelectedGroup = Groups.First();

            Courses.Add(new Course() { Name = "1 курс" });
            Courses.Add(new Course() { Name = "2 курс" });
            Courses.Add(new Course() { Name = "3 курс" });
            Courses.Add(new Course() { Name = "4 курс" });
            SelectedCourse = Courses.First();

            Departments.Add(new Department() { Name = "ФИТ" });
            Departments.Add(new Department() { Name = "ФУСК" });
            Departments.Add(new Department() { Name = "ФПИЭ" });
            Departments.Add(new Department() { Name = "МСФ" });
            Departments.Add(new Department() { Name = "ХТФ" });
            SelectedDepartment = Departments.First();

        }
    }
}
