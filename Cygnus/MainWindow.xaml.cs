using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Calendar tab
            int currentYear = 2019;
            int currentMonth = 10;
            CreateCalendar(currentYear, currentMonth);

            // Volunteer editor tab
            List<CVoluntario> items = new List<CVoluntario>();
            DateTime dataNascimento = new DateTime(2000, 01, 01);
            items.Add(new CVoluntario() { Nome = "John Doe", DataNascimento = dataNascimento, Endereco = "Av. 1" });
            items.Add(new CVoluntario() { Nome = "Jane Doe", DataNascimento = dataNascimento, Endereco = "Av. 1" });
            lvDataBinding.ItemsSource = items;
        }

        /// <summary>
        /// Creates daily columns for monthly calendar, and populates with events for each volunteer
        /// </summary>
        void CreateCalendar(int currentYear, int currentMonth)
        {
            int numDays = DateTime.DaysInMonth(currentYear, currentMonth);

            for (int i = -1; i < numDays; i++)
            {
                DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();

                if (i == -1)
                {
                    dataGridTextColumn.Header = "Voluntário";
                    dataGridTextColumn.Binding = new Binding("Name");
                }
                else
                {
                    var day = new DateTime(currentYear, currentMonth, i + 1);

                    var culture = new System.Globalization.CultureInfo("pt-BR");
                    var week_day = culture.DateTimeFormat.GetDayName(day.DayOfWeek);

                    dataGridTextColumn.Header = week_day + " - " + (i + 1);
                    dataGridTextColumn.Binding = new Binding(string.Format("Days[{0}]", i));
                    dataGridTextColumn.CanUserSort = false;
                }
                dataGrid.Columns.Add(dataGridTextColumn);
            }

            CalendarBinding calendarBinding = new CalendarBinding("Miguel", numDays);
            calendarBinding.Days[2] = "Evento";

            dataGrid.Items.Add(calendarBinding);
        }

        /// <summary>
        /// Data binding class for DataGrid in calendar tab
        /// </summary>
        public class CalendarBinding
        {
            public string Name { get; set; }
            public string[] Days { get; set; }

            public CalendarBinding(string name, int numDays)
            {
                Name = name;
                Days = new string[numDays];
            }
        }
    }
}
