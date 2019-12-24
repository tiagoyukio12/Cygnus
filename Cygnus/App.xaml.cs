using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Cygnus.Models;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FileIO volunteersIO = new FileIO("volunteers.dat");
            ObservableCollection<Volunteer> volunteers = Volunteers.Instance.ToCollection;
            volunteersIO.WriteVolunteers(new List<Volunteer>(volunteers));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Load volunteers
            FileIO volunteersIO = new FileIO("volunteers.dat");
            List<Volunteer> volunteers = volunteersIO.ReadVolunteers();
            Volunteers.Instance.Add(volunteers);
        }
        
    }
}
