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
        int currentYear = 2019;
        int currentMonth = 11;
        List<CAtividade> listActivities;

        public MainWindow()
        {
            InitializeComponent();

            // Calendar tab
            listActivities = new List<CAtividade>();
            CAtividade activity = new CAtividade("0001", "Rua 1", new DateTime(currentYear, currentMonth, 4), 2, "Freq");
            listActivities.Add(activity);
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
            // Remove old items
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();

            int numDays = DateTime.DaysInMonth(currentYear, currentMonth);

            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
            dataGridTextColumn.Header = "Voluntário";
            dataGridTextColumn.Binding = new Binding("Name");
            dataGrid.Columns.Add(dataGridTextColumn);

            for (int i = 0; i < numDays; i++)
            {
                var day = new DateTime(currentYear, currentMonth, i + 1);

                var culture = new System.Globalization.CultureInfo("pt-BR");
                var week_day = culture.DateTimeFormat.GetDayName(day.DayOfWeek).Substring(0, 3);

                DataGridTextColumn columnT1 = new DataGridTextColumn();
                DataGridTextColumn columnT2 = new DataGridTextColumn();
                DataGridTextColumn columnT3 = new DataGridTextColumn();

                columnT1.Header = week_day + " - D" + (i + 1) + " - T1";
                columnT1.Binding = new Binding(string.Format("Turns[{0}]", 3 * i));
                columnT1.CanUserSort = false;
                dataGrid.Columns.Add(columnT1);

                columnT2.Header = week_day + " - D" + (i + 1) + " - T2";
                columnT2.Binding = new Binding(string.Format("Turns[{0}]", 3 * i + 1));
                columnT2.CanUserSort = false;
                dataGrid.Columns.Add(columnT2);

                columnT3.Header = week_day + " - D" + (i + 1) + " - T3";
                columnT3.Binding = new Binding(string.Format("Turns[{0}]", 3 * i + 2));
                columnT3.CanUserSort = false;
                dataGrid.Columns.Add(columnT3);
            }

            CalendarBinding calendarBinding = new CalendarBinding("Miguel", numDays);
            foreach (CAtividade activity in listActivities)
            {
                DateTime activityDate = activity.DataInicio;
                if (activityDate.Year == currentYear && activityDate.Month == currentMonth)
                {
                    int turn = activity.Turno;
                    calendarBinding.Turns[3*activityDate.Day + turn - 4] = activity.ToString();
                }
            }
            dataGrid.Items.Add(calendarBinding);
        }

        /// <summary>
        /// Data binding class for DataGrid in calendar tab
        /// </summary>
        public class CalendarBinding
        {
            public string Name { get; set; }
            public string[] Turns { get; set; }

            public CalendarBinding(string name, int numDays)
            {
                Name = name;
                Turns = new string[3 * numDays];
            }
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            string id = idText.Text;
            string pos = posText.Text;
            DateTime date = (DateTime) dateCalendar.SelectedDate;
            int turn = -1;
            if ((bool)turnOneRadio.IsChecked)
                turn = 1;
            else if ((bool)turnTwoRadio.IsChecked)
                turn = 2;
            else if ((bool)turnThreeRadio.IsChecked)
                turn = 3;
            string freq = freqText.Text;

            CAtividade activity = new CAtividade(id, pos, date, turn, freq);
            listActivities.Add(activity);
            CreateCalendar(currentYear, currentMonth);
        }
    }
}
