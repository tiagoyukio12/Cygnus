﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for TabCalendarData.xaml
    /// </summary>
    public partial class TabCalendarData : UserControl
    {
        int currentYear = 2019;
        int currentMonth = 11;

        public TabCalendarData()
        {
            InitializeComponent();
            CreateCalendar();
        }

        /// <summary>
        /// Creates daily columns for monthly calendar, and populates with events for each volunteer
        /// </summary>
        public void CreateCalendar()
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
            foreach (Activity activity in Activities.Instance.ToList)
            {
                DateTime activityDate = activity.StartDate;
                if (activityDate.Year == currentYear && activityDate.Month == currentMonth)
                {
                    int turn = activity.Turn;
                    calendarBinding.Turns[3 * activityDate.Day + turn - 4] = activity.ToString();
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
    }
}
