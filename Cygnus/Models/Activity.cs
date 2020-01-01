using Cygnus.ViewModels;
using System;

namespace Cygnus.Models
{
    /// <summary>
    /// This class represents an unique or recurrent activity.
    /// </summary>
    public class Activity : ObservableObject
    {
        public Activity(string id, string pos, DateTime date, int turn, string freq)
        {
            _id = id;
            _location = pos;
            _startDate = date;
            _turn = turn;
            _frequency = freq;
        }

        private string _id;
        /// <value>Identifier for activity.</value>
        public string Id
        {
            get => _id;
            set
            {
                // TODO: Check duplicate ID
                _id = value;
                RaisePropertyChangedEvent("Id");
            }
        }

        private string _location;
        /// <value>Geolocation or address of the activity.</value>
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChangedEvent("Location");
            }
        }

        private DateTime _startDate;
        /// <value>
        /// If this is an unique activity, date of the activity.
        /// If this is a recurrent activity, date of the first occurrence.
        /// </value>
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                RaisePropertyChangedEvent("StartDate");
            }
        }

        private int _turn;
        /// <value>Turn (1, 2 or 3) of the activity.</value>
        public int Turn
        {
            get => _turn;
            set
            {
                _turn = value;
                RaisePropertyChangedEvent("Turn");
            }
        }

        private string _frequency;
        /// <value>Frequency of occurrence of recurrent activity.</value>
        public string Frequency
        {
            get => _frequency;
            set
            {
                _frequency = value;
                RaisePropertyChangedEvent("Frequency");
            }
        }



        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}