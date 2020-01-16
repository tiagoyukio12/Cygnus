using Cygnus.Models;
using Cygnus.Views;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cygnus.ViewModels
{
    class TabCalendarViewModel : ObservableObject
    {
        public TabCalendarViewModel()
        {
            _observableCollection = Volunteers.CollectionVolunteers;
            _currentMonth = new DateTime(2019, 11, 1);
        }

        private DateTime _currentMonth;

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

        private DataGridCellInfo _selectedCell;
        public DataGridCellInfo SelectedCell
        {
            get => _selectedCell;
            set
            {
                _selectedCell = value;
                RaisePropertyChangedEvent("SelectedCell");
            }
        }

        public ICommand EditActivityCommand
        {
            get { return new DelegateCommand(EditActivity); }
        }
        private void EditActivity()
        {
            if (_selectedCell != null)
            {
                var cellContent = _selectedCell.Column.GetCellContent(_selectedCell.Item);
                DataGridCell dataGridCell = (DataGridCell)cellContent.Parent;
                string id = ((TextBlock)dataGridCell.Content).Text;
                Volunteer activityOwner = (Volunteer)_selectedCell.Item;
                Activity selectedActivity = activityOwner.Schedule.FindActivity(id);
                if (selectedActivity != null)
                {
                    WindowEditActivity windowEditActivity = new WindowEditActivity(activityOwner, selectedActivity);
                    windowEditActivity.Show();
                }
            }
        }
    }
}
