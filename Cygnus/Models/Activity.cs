using System;
using Cygnus.ViewModels;

namespace Cygnus.Models
{
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
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChangedEvent("Id");
            }
        }

        private string _location;
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