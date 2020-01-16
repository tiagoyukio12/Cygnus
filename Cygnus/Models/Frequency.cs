using Cygnus.ViewModels;
using System;
using System.Collections.Generic;

namespace Cygnus.Models
{
    /// <summary>
    /// Frequency of a recurring activity.
    /// </summary>
    public class Frequency : ObservableObject
    {
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                // TODO: Check duplicate ID
                _type = value;
                RaisePropertyChangedEvent("Type");
            }
        }
        private string _period;
        public string Period
        {
            get => _period;
            set
            {
                // TODO: Check duplicate ID
                _period = value;
                RaisePropertyChangedEvent("Period");
            }
        }

        public Frequency(string type, string period)
        {
            _type = type;
            _period = period;
        }

        public List<DateTime> GetMonthOccurrences(DateTime startDate, DateTime month)
        {
            if (_type == "")
                if (startDate.Month == month.Month && startDate.Year == month.Year)
                    return new List<DateTime>(new DateTime[] { startDate });
            if (_type == "Semanal")
                return GetWeeklyOccurrences(startDate, month);
            if (_type == "Mensal")
                return GetMonthlyOccurrences(startDate, month);
            if (_type == "Anual")
                return GetYearlyOccurrences(startDate, month);
            return null;
        }

        private List<DateTime> GetWeeklyOccurrences(DateTime startDate, DateTime month)
        {
            List<DateTime> occurrences = new List<DateTime>();

            bool[] isDesiredWeekDay = new bool[7];
            for (int i = 0; i < 7; i++)
            {
                isDesiredWeekDay[i] = (_period[i] == 'T');
            }

            DateTime currentDay = startDate;
            if (startDate.Month != month.Month || startDate.Year != month.Year)
                currentDay = month;

            while (currentDay.Month == month.Month)
            {
                DayOfWeek dayOfWeek = currentDay.DayOfWeek;
                if (isDesiredWeekDay[(int)dayOfWeek])
                    occurrences.Add(currentDay);
                currentDay = currentDay.AddDays(1);
            }

            return occurrences;
        }

        private List<DateTime> GetMonthlyOccurrences(DateTime startDate, DateTime month)
        {
            List<DateTime> occurrences = new List<DateTime>();

            if (startDate < month || (startDate.Month == month.Month && startDate.Year == month.Year))
            {
                if (_period[0] == 'D')
                {
                    int day = Int32.Parse(_period.Substring(1));
                    DateTime occurrence = new DateTime(month.Year, month.Month, day);
                    occurrences.Add(occurrence);
                }
                else if (_period[0] == 'W')
                {
                    int week_start = (Int32.Parse(_period[1].ToString()) - 1) * 7 + 1;
                    DateTime occurrence = new DateTime(month.Year, month.Month, week_start);
                    DayOfWeek dayOfWeek = (DayOfWeek) Int32.Parse(_period.Substring(3));
                    occurrence = occurrence.AddDays((dayOfWeek < occurrence.DayOfWeek ? 7 : 0) + dayOfWeek - occurrence.DayOfWeek);
                    occurrences.Add(occurrence);
                }
            }
            return occurrences;
        }

        private List<DateTime> GetYearlyOccurrences(DateTime startDate, DateTime month)
        {
            List<DateTime> occurrences = new List<DateTime>();

            if (startDate < month || (startDate.Month == month.Month && startDate.Year == month.Year))
            {
                DateTime occurrence = DateTime.Parse(_period);
                if (occurrence.Month == month.Month)
                {
                    occurrences.Add(occurrence);
                }
            }

            return occurrences;
        }
    }
}
