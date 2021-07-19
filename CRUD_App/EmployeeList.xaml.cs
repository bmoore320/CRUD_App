using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CRUD_App
{
    public partial class EmployeeList : ContentPage
    {
        public EmployeeList()
        {
            InitializeComponent();

            Role.RoleList = new List<Role>
            {
                new Role { Title = "Technical Writer" },
                new Role { Title = "Curriculum Manager" },
                new Role { Title =  "Program Manager" },
                new Role { Title = "Content Deveoper" },
                new Role { Title = "Account Manager" },
                new Role { Title = "Research Intern" }
            };

            Employee.NameList = new List<Employee>
            {
                new Employee { ID = 1, Name = "Brandon" },
                new Employee { ID = 2, Name = "Brian" },
                new Employee { ID = 3, Name = "Thomas" },
                new Employee { ID = 4, Name = "Jerry" },
                new Employee { ID = 5, Name = "Gilbert" },
                new Employee { ID = 6, Name = "Sarah" },
                new Employee { ID = 7, Name = "Missy" }
            };

            EmployeeRole_View.ActiveEmployeeList = new ObservableCollection<EmployeeRole_View>
            {
                new EmployeeRole_View { ID = 1, Name = "Brandon", Title =  "Technical Writer" },
                new EmployeeRole_View { ID = 2, Name = "Brian", Title = "Curriculum Manager" },
                new EmployeeRole_View { ID = 3, Name = "Thomas", Title = "Program Manager" },
                new EmployeeRole_View { ID = 4, Name = "Jerry", Title = "Content Deveoper" },
                new EmployeeRole_View { ID = 5, Name = "Gilbert", Title = "Account Manager" },
                new EmployeeRole_View { ID = 6, Name = "Sarah", Title = "Research Intern" },
                new EmployeeRole_View { ID = 7, Name = "Missy", Title =  "Technical Writer" }
            };

            List<Employee> employeeSorted = Employee.NameList.OrderByDescending(o => o.ID).ToList();
            foreach (var n in employeeSorted)
            {
                IDCounter.Count = (n.ID);
                break;
            }
                   
            listView.ItemsSource = EmployeeRole_View.ActiveEmployeeList;
        }

        async void Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            var employee = (sender as MenuItem).CommandParameter as EmployeeRole_View;
            var name = employee.Name;
            var id = employee.ID;

            bool answer = await DisplayAlert("Termination Confirmation", "Terminate " + employee.Name + "?", "Yes", "No");
            if (answer == true)
            {
                EmployeeRole_View.ActiveEmployeeList.Remove(employee);

                foreach (var n in Employee.NameList)
                {
                    if (n.ID == id)
                    {
                        Employee.NameList.Remove(n);
                        break;
                    }
                }
            }
        }

        void Create_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EmployeeCreatePage());
        }

        void listView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var employee = e.SelectedItem as EmployeeRole_View;
            var page = new TitleSelectPage(employee.Name, employee.Title);

            page.JobTitles.ItemSelected += (source, args) =>
            {
                foreach (var emp in EmployeeRole_View.ActiveEmployeeList.Where(x => x.ID == employee.ID))
                {
                    emp.Title = args.SelectedItem.ToString();
                }
                listView.ItemsSource = null;
                listView.ItemsSource = EmployeeRole_View.ActiveEmployeeList;
                Navigation.PopAsync();
            };

            Navigation.PushAsync(page);
            listView.SelectedItem = null;
        }
    }

    public class Role
    {
        public string Title { get; set; }
        public static List<Role> RoleList { get; set; }
    }

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public static List<Employee> NameList { get; set; }
    }

    public class EmployeeRole_View
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public static ObservableCollection<EmployeeRole_View> ActiveEmployeeList { get; set; }
    }

    public class IDCounter
    {
        public static int Count { get; set; }
    }
}