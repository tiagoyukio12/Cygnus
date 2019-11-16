using System.Collections.Generic;
using System.Windows;

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
            List<Volunteer> volunteers = Volunteers.Instance.ToList;
            volunteersIO.WriteVolunteers(volunteers);
        }
    }
}
