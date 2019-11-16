using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Load volunteers
            FileIO volunteersIO = new FileIO("volunteers.dat");
            List<Volunteer> volunteers = volunteersIO.ReadVolunteers();
            Volunteers.Instance.Add(volunteers);
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabCalendarData.CreateCalendar();
                TabVolunteerData.CreateVolunteerGrid();
            }
        }
    }
}
