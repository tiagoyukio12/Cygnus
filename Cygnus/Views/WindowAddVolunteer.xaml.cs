using Cygnus.Models;
using Cygnus.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace Cygnus.Views
{
    /// <summary>
    /// Interaction logic for WindowAddVolunteer.xaml
    /// </summary>
    public partial class WindowAddVolunteer : Window
    {

        public WindowAddVolunteer(ObservableCollection<Volunteer> observableCollection)
        {
            InitializeComponent();
            DataContext = new WindowAddVolunteerViewModel(this, observableCollection);
        }

        public WindowAddVolunteer(ObservableCollection<Volunteer> observableCollection, Volunteer volunteer)
        {
            InitializeComponent();
            DataContext = new WindowAddVolunteerViewModel(this, observableCollection, volunteer);
        }
    }
}
