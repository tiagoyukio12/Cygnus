using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Cygnus.ViewModels;

namespace Cygnus.Models
{
    public class Schedule : ObservableObject
    {
        public Schedule(List<Activity> activities)
        {
            _activities = new TrulyObservableCollection<Activity>(activities);
            _activities.OrderBy(i => i.StartDate);
            _currMonth = new DateTime(2019, 11, 1);
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

            foreach(Activity activity in _activities)
            {
                DateTime activityDate = activity.StartDate;
                if (activityDate > month.AddMonths(1))
                    break;
                if (activityDate.Month == month.Month && activityDate.Year == month.Year)
                {
                    monthSchedule[3 * activityDate.Day + activity.Turn - 4] += activity.ToString();
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
            
            if (activity.StartDate.Month == _currMonth.Month && activity.StartDate.Year == _currMonth.Year)
            {
                _monthSchedule = GetMonthSchedule(_currMonth);
                RaisePropertyChangedEvent("MonthSchedule");
            }
        }

        public Activity FindActivity(string id)
        {
            return (Activity) _activities.Single(x => x.Id == id);
        }
    }
}
