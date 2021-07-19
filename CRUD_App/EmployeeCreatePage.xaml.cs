using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static CRUD_App.EmployeeList;

namespace CRUD_App
{
    public partial class EmployeeCreatePage : ContentPage
    {
        bool roleExists;

        public EmployeeCreatePage()
        {
            InitializeComponent();
        }

        void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            IDCounter.Count++;
            int i = IDCounter.Count;
            Console.WriteLine(i);

            //add new FTE
            Employee.NameList.Add(new Employee { ID = i, Name = entryName.Text.ToString() });

            //add role, if role does not exist
            foreach (var role in Role.RoleList)
            {
                if(role.Title == entryTitle.Text.ToString())
                {
                    roleExists = true;
                    break;
                }
            }
            if (roleExists != true)
                Role.RoleList.Add(new Role { Title = entryTitle.Text.ToString() });

            //add employee
            EmployeeRole_View.ActiveEmployeeList.Add(new EmployeeRole_View { ID = i, Name = entryName.Text.ToString(), Title = entryTitle.Text.ToString() });
            Console.WriteLine(i);

            Navigation.PopAsync();
        }
    }
}
