using Cygnus.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cygnus.ViewModels
{
    class WindowAddVolunteerViewModel : ObservableObject
    {
        public WindowAddVolunteerViewModel(Window window, ObservableCollection<Volunteer> observableCollection)
        {
            _window = window;
            _observableCollection = observableCollection;
            _selectedVolunteer = new Volunteer();
            _editedVolunteer = new Volunteer();
            _isNewVolunteer = true;
        }

        public WindowAddVolunteerViewModel(Window window, ObservableCollection<Volunteer> observableCollection, Volunteer selectedVolunteer)
        {
            _window = window;
            _observableCollection = observableCollection;
            ((Button)window.FindName("addButton")).Content = "Editar";
            _selectedVolunteer = selectedVolunteer;
            _editedVolunteer = new Volunteer
            {
                Name = selectedVolunteer.Name,
                Address = selectedVolunteer.Address,
                BirthDate = new DateTime(selectedVolunteer.BirthDate.ToBinary())
            };
            _isNewVolunteer = false;
        }

        private readonly Window _window;

        private ObservableCollection<Volunteer> _observableCollection;
        public ObservableCollection<Volunteer> ObservableCollection
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

        private Volunteer _editedVolunteer;
        public Volunteer EditedVolunteer
        {
            get => _editedVolunteer;
            set
            {
                _editedVolunteer = value;
                RaisePropertyChangedEvent("EditedVolunteer");
            }
        }

        private readonly bool _isNewVolunteer;

        public ICommand AddCommand
        {
            get { return new DelegateCommand(Add); }
        }

        private void Add()
        {
            SelectedVolunteer.Name = EditedVolunteer.Name;
            SelectedVolunteer.Address = EditedVolunteer.Address;
            SelectedVolunteer.BirthDate = EditedVolunteer.BirthDate;
            if (_isNewVolunteer)
                _observableCollection.Add(SelectedVolunteer);
            _window.Close();
        }

        public ICommand CancelCommand
        {
            get { return new DelegateCommand(Cancel); }
        }

        private void Cancel()
        {
            _window.Close();
        }

        public ICommand RemoveCommand
        {
            get { return new DelegateCommand(Remove); }
        }

        private void Remove()
        {
            if (!_isNewVolunteer)
            {
                _observableCollection.Remove(_selectedVolunteer);
            }
            _window.Close();
        }
    }
}
