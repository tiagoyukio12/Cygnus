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
            FileIO activitiesIO = new FileIO("activities.txt");
            List<Activity> activities = Activities.Instance.ToList;
            activitiesIO.WriteActivities(activities);
        }
    }
}
