using Cygnus.Models;
using Cygnus.Views;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Cygnus.ViewModels
{
    class TabCalendarViewModel : ObservableObject
    {
        public TabCalendarViewModel(DataGrid dataGrid, Grid subGrid)
        {
            _monthText = DateTime.Now.Month.ToString();
            _yearText = DateTime.Now.Year.ToString();
            _dataGrid = dataGrid;
            _subGrid = subGrid;
            _observableCollection = Volunteers.CollectionVolunteers;
            int month = Int32.Parse(_monthText);
            int year = Int32.Parse(_yearText);
            _currentMonth = new DateTime(year, month, 1);
            CreateColumns();
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

        private string _monthText;
        public string MonthText
        {
            get => _monthText;
            set
            {
                _monthText = value;
                RaisePropertyChangedEvent("MonthText");
            }
        }

        private string _yearText;
        public string YearText
        {
            get => _yearText;
            set
            {
                _yearText = value;
                RaisePropertyChangedEvent("YearText");
            }
        }

        public ICommand UpdateMonthCommand
        {
            get { return new DelegateCommand(UpdateMonth); }
        }
        private void UpdateMonth()
        {
            int month = Int32.Parse(_monthText);
            int year = Int32.Parse(_yearText);
            _currentMonth = new DateTime(year, month, 1);
            foreach (Volunteer volunteer in _observableCollection)
            {
                volunteer.Schedule.CurrMonth = _currentMonth;
            }
            CreateColumns();
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
                string id = ((TextBlock)dataGridCell.Content).Text.Substring(0, 4);
                Volunteer activityOwner = (Volunteer)_selectedCell.Item;
                Activity selectedActivity = activityOwner.Schedule.FindActivity(id);
                if (selectedActivity != null)
                {
                    WindowEditActivity windowEditActivity = new WindowEditActivity(activityOwner, selectedActivity);
                    windowEditActivity.Show();
                }
            }
        }

        private DataGrid _dataGrid;
        public DataGrid DataGrid
        {
            get => _dataGrid;
            set
            {
                _dataGrid = value;
                RaisePropertyChangedEvent("DataGrid");
            }
        }

        private Grid _subGrid;
        public Grid SubGrid
        {
            get => _subGrid;
            set
            {
                _subGrid = value;
                RaisePropertyChangedEvent("SubGrid");
            }
        }

        public void CreateColumns()
        {
            _dataGrid.Columns.Clear();
            _subGrid.ColumnDefinitions.Clear();
            _subGrid.Children.Clear();

            ColumnDefinition firstColumn = new ColumnDefinition
            {
                Width = new GridLength(85)
            };
            _subGrid.ColumnDefinitions.Add(firstColumn);

            int daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);

            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Voluntário",
                Binding = new Binding("Name")
            };
            _dataGrid.Columns.Add(dataGridTextColumn);

            Style headerStyle = new Style();
            Style cellStyle = new Style();
            headerStyle.Setters.Add(new Setter
            {
                Property = DataGridCell.HorizontalContentAlignmentProperty, 
                Value = HorizontalAlignment.Center
            });
            var converter = new BackGroundConverter();
            for (int i = 0; i < daysInMonth; i++)
            {
                ColumnDefinition column = new ColumnDefinition
                {
                    Width = new GridLength(240)
                };
                _subGrid.ColumnDefinitions.Add(column);
                var day = new DateTime(_currentMonth.Year, _currentMonth.Month, i + 1);
                var culture = new CultureInfo("pt-BR");
                var week_day = culture.DateTimeFormat.GetDayName(day.DayOfWeek).Substring(0, 3);
                TextBlock textBlock = new TextBlock
                {
                    Text = week_day + " - D" + (i + 1),
                    TextAlignment = TextAlignment.Center
                };
                Border border = new Border
                {
                    Child = textBlock
                };
                _subGrid.Children.Add(border);
                Grid.SetColumn(border, i + 1);

                DataGridTextColumn columnT1 = new DataGridTextColumn
                {
                    Width = new DataGridLength(80)
                };
                DataGridTextColumn columnT2 = new DataGridTextColumn
                {
                    Width = new DataGridLength(80)
                };
                DataGridTextColumn columnT3 = new DataGridTextColumn
                {
                    Width = new DataGridLength(80)
                };

                columnT1.Header = "T1";
                columnT1.Binding = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i));
                columnT1.CanUserSort = false;
                columnT1.HeaderStyle = headerStyle;
                columnT1.CellStyle = new Style(typeof(DataGridCell));
                columnT1.CellStyle.Setters.Add(new Setter
                {
                    Property = DataGridCell.BackgroundProperty,
                    Value = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i)) { Converter = converter }
                });
                _dataGrid.Columns.Add(columnT1);

                columnT2.Header = "T2";
                columnT2.Binding = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i + 1));
                columnT2.CanUserSort = false;
                columnT2.HeaderStyle = headerStyle;
                columnT2.CellStyle = new Style(typeof(DataGridCell));
                columnT2.CellStyle.Setters.Add(new Setter
                {
                    Property = DataGridCell.BackgroundProperty,
                    Value = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i + 1)) { Converter = converter }
                });
                _dataGrid.Columns.Add(columnT2);

                columnT3.Header = "T3";
                columnT3.Binding = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i + 2));
                columnT3.CanUserSort = false;
                columnT3.HeaderStyle = headerStyle;
                columnT3.CellStyle = new Style(typeof(DataGridCell));
                columnT3.CellStyle.Setters.Add(new Setter
                {
                    Property = DataGridCell.BackgroundProperty,
                    Value = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i + 2)) { Converter = converter }
                });
                _dataGrid.Columns.Add(columnT3);
            }
        }

        /// <summary>
        /// Data binding class for DataGrid in calendar tab
        /// </summary>
        public class CalendarBinding
        {
            public string Name { get; set; }
            public string[] Turns { get; set; }
            public Volunteer Owner { get; set; }

            public CalendarBinding(string name, int numDays, Volunteer owner)
            {
                Name = name;
                Turns = new string[3 * numDays];
                Owner = owner;
            }
        }

        public class BackGroundConverter : IValueConverter
        {

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string daySchedule)
                {
                    // TODO: Change daySchedule from string to new class for time checking
                    if (daySchedule.Length > 20)
                        return new SolidColorBrush(Colors.LightSalmon);
                    if (daySchedule.Length > 3)
                        return new SolidColorBrush(Colors.LightGreen);
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
