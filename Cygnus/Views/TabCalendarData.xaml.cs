using Cygnus.Models;
using Cygnus.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Cygnus.Views
{
    /// <summary>
    /// Interaction logic for TabCalendarData.xaml
    /// </summary>
    public partial class TabCalendarData : UserControl
    {
        public TabCalendarData()
        {
            InitializeComponent();
            CreateColumns();
            DataContext = new TabCalendarViewModel();
        }

        public void CreateColumns()
        {
            DateTime _currentMonth = new DateTime(2019, 11, 1);  // TODO: Create current month control
            int daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);

            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Voluntário",
                Binding = new Binding("Name")
            };
            dataGrid.Columns.Add(dataGridTextColumn);

            Style style = new Style();
            style.Setters.Add(new Setter(DataGridCell.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));

            for (int i = 0; i < daysInMonth; i++)
            {
                ColumnDefinition column = new ColumnDefinition
                {
                    Width = new GridLength(240)
                };
                SubGrid.ColumnDefinitions.Add(column);
                var day = new DateTime(_currentMonth.Year, _currentMonth.Month, i + 1);
                var culture = new System.Globalization.CultureInfo("pt-BR");
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
                SubGrid.Children.Add(border);
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
                columnT1.HeaderStyle = style;
                dataGrid.Columns.Add(columnT1);

                columnT2.Header = "T2";
                columnT2.Binding = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i + 1));
                columnT2.CanUserSort = false;
                columnT2.HeaderStyle = style;
                dataGrid.Columns.Add(columnT2);

                columnT3.Header = "T3";
                columnT3.Binding = new Binding(string.Format("Schedule.MonthSchedule[{0}]", 3 * i + 2));
                columnT3.CanUserSort = false;
                columnT3.HeaderStyle = style;
                dataGrid.Columns.Add(columnT3);
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
    }
}
