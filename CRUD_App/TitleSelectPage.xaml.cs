using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using static CRUD_App.EmployeeList;

namespace CRUD_App
{
    public partial class TitleSelectPage : ContentPage
    {
        string _title;
        public TitleSelectPage(string name, string title)
        {
            InitializeComponent();

            _title = title;
            Page.Title = name + ", " + title;

            listView.ItemsSource = GetTitles();

        }
        public List<string> GetTitles()
        {
            List<string> titles = new List<string>();

            foreach (var role in Role.RoleList)
            {
                if(role.Title != _title)
                {
                    titles.Add(role.Title);
                }
            }
            return titles;
        }

        public ListView JobTitles { get { return listView;  } }
    }
}
