using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for WindowAddVolunteer.xaml
    /// </summary>
    public partial class WindowAddVolunteer : Window
    {
        Volunteer SelectedVolunteer;
        bool isNewVolunteer;

        public WindowAddVolunteer()
        {
            InitializeComponent();
            SelectedVolunteer = new Volunteer();
            isNewVolunteer = true;
        }

        public WindowAddVolunteer(Volunteer volunteer)
        {
            InitializeComponent();
            SelectedVolunteer = volunteer;
            nameText.Text = SelectedVolunteer.Name;
            addressText.Text = SelectedVolunteer.Address;
            birthCalendar.SelectedDate = SelectedVolunteer.BirthDate;
            isNewVolunteer = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SelectedVolunteer.Name = nameText.Text;
            SelectedVolunteer.Address = addressText.Text;
            SelectedVolunteer.BirthDate = (DateTime) birthCalendar.SelectedDate;
            if (isNewVolunteer)
                Volunteers.Instance.Add(SelectedVolunteer);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (!isNewVolunteer)
            {
                Volunteers.Instance.ToList.Remove(SelectedVolunteer);
            }
            Close();
        }
    }
}
