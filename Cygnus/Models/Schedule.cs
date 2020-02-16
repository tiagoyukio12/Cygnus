using Cygnus.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cygnus.Models
{
    /// <summary>
    /// Schedule of activities of a volunteer. Contains monthly schedule used as binding for calendar tab.
    /// </summary>
    public class Schedule : ObservableObject
    {
        /// <summary>
        /// Sort activities list into an <c>TrulyObservableCollection</c>.
        /// </summary>
        /// <param name="activities">List of volunteer activities.</param>
        public Schedule(List<Activity> activities)
        {
            _activities = new TrulyObservableCollection<Activity>(activities);
            _activities.OrderBy(i => i.StartDate);
            DateTime now = DateTime.Now;
            _currMonth = new DateTime(now.Year, now.Month, 1);
            _monthSchedule = GetMonthSchedule(_currMonth);

            _activities.CollectionChanged += ActivitiesChanged;
        }

        private TrulyObservableCollection<Activity> _activities;
        public TrulyObservableCollection<Activity> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
                RaisePropertyChangedEvent("Activities");
                _monthSchedule = GetMonthSchedule(_currMonth);
                RaisePropertyChangedEvent("MonthSchedule");
            }
        }

        private void ActivitiesChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            RaisePropertyChangedEvent("Activities");
            _monthSchedule = GetMonthSchedule(_currMonth);
            RaisePropertyChangedEvent("MonthSchedule");
        }

        private DateTime _currMonth;
        public DateTime CurrMonth
        {
            get => _currMonth;
            set
            {
                _currMonth = value;
                RaisePropertyChangedEvent("CurrMonth");
                _monthSchedule = GetMonthSchedule(_currMonth);
                RaisePropertyChangedEvent("MonthSchedule");
            }
        }

        private string[] _monthSchedule;
        public string[] MonthSchedule
        {
            get => _monthSchedule;
            set
            {
                _monthSchedule = value;
                RaisePropertyChangedEvent("MonthSchedule");
            }
        }

        public string[] GetMonthSchedule(DateTime month)
        {
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            string[] monthSchedule = new string[3 * daysInMonth];

            foreach (Activity activity in _activities)
            {
                DateTime activityDate = activity.StartDate;
                if (activityDate > month.AddMonths(1))
                    break;
                else
                {
                    List<DateTime> occurrences = activity.Frequency.GetMonthOccurrences(activity.StartDate, month);
                    foreach (DateTime occurrence in occurrences)
                    {
                        if (!string.IsNullOrEmpty(monthSchedule[3 * occurrence.Day + activity.Turn - 4]))
                            monthSchedule[3 * occurrence.Day + activity.Turn - 4] += "\n";
                        monthSchedule[3 * occurrence.Day + activity.Turn - 4] += activity.ToString();
                    }
                }
            }

            return monthSchedule;
        }

        public void AddActivity(Activity activity)
        {
            int i = 0;
            for (; i < _activities.Count; i++)
            {
                if (_activities[i].StartDate > activity.StartDate)
                    break;
            }
            _activities.Insert(i, activity);
            RaisePropertyChangedEvent("Activities");

            DateTime activityDate = activity.StartDate;
            if (activityDate > _currMonth.AddMonths(1))
                return;
            RaisePropertyChangedEvent("MonthSchedule");
        }

        public Activity FindActivity(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return (Activity)_activities.Single(x => x.Name == name);
            return null;
        }
    }
}
