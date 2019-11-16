using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for TabVolunteerData.xaml
    /// </summary>
    public partial class TabVolunteerData : UserControl
    {
        public TabVolunteerData()
        {
            InitializeComponent();

            List<Volunteer> items = new List<Volunteer>();
            DateTime dataNascimento = new DateTime(2000, 01, 01);
            items.Add(new Volunteer() { Name = "John Doe", BirthDate = dataNascimento, Address = "Av. 1" });
            items.Add(new Volunteer() { Name = "Jane Doe", BirthDate = dataNascimento, Address = "Av. 1" });
            lvDataBinding.ItemsSource = items;
        }
    }
}
