using System.Collections.Generic;
using System.Windows;

namespace Cygnus.Views
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
    }
}
