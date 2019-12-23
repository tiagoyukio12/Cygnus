using Cygnus.Models;
using Cygnus.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
            WindowAddVolunteer windowAddVolunteer = new WindowAddVolunteer(_observableCollection, _selectedVolunteer);
            windowAddVolunteer.Show();
            windowAddVolunteer.Closed += (s, eventarg) =>
            {
                Volunteer foo = new Volunteer();
                ObservableCollection.Add(foo);
                ObservableCollection.Remove(foo);
            };
        }

        public ICommand AddVolunteerCommand
        {
            get { return new DelegateCommand(AddVolunteer); }
        }
        private void AddVolunteer()
        {
            WindowAddVolunteer windowAddVolunteer = new WindowAddVolunteer(_observableCollection);
            windowAddVolunteer.Show();
            windowAddVolunteer.Closed += (s, eventarg) =>
            {
                //ObservableCollection = new ObservableCollection<Volunteer>();
                ObservableCollection.Add(new Volunteer("foo", new System.DateTime(), "foo1", new List<Activity>()));
            };
        }
    }
}
