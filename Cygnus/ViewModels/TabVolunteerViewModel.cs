using System.Windows.Input;
using Cygnus.Models;
using Cygnus.Views;

namespace Cygnus.ViewModels
{
    public class TabVolunteerViewModel : ObservableObject
    {

        public TabVolunteerViewModel()
        {
            _observableCollection = Volunteers.CollectionVolunteers;
        }

        private TrulyObservableCollection<Volunteer> _observableCollection;
        public TrulyObservableCollection<Volunteer> ObservableCollection
        {
            get => _observableCollection;
            set
            {
                _observableCollection = value;
                RaisePropertyChangedEvent("ObservableCollection");
            }
        }

        private Volunteer _selectedVolunteer;
        public Volunteer SelectedVolunteer
        {
            get => _selectedVolunteer;
            set
            {
                _selectedVolunteer = value;
                RaisePropertyChangedEvent("SelectedVolunteer");
            }
        }

        public ICommand EditVolunteerCommand
        {
            get { return new DelegateCommand(EditVolunteer); }
        }
        private void EditVolunteer()
        {
            if (_selectedVolunteer != null)
            {
                WindowAddVolunteer windowAddVolunteer = new WindowAddVolunteer(_observableCollection, _selectedVolunteer);
                windowAddVolunteer.Show();
            }
        }

        public ICommand AddVolunteerCommand
        {
            get { return new DelegateCommand(AddVolunteer); }
        }
        private void AddVolunteer()
        {
            WindowAddVolunteer windowAddVolunteer = new WindowAddVolunteer(_observableCollection);
            windowAddVolunteer.Show();
        }
    }
}
