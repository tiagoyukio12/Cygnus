using Cygnus.ViewModels;
using System;
using System.Collections.Generic;

namespace Cygnus.Models
{
    public class Volunteer : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                RaisePropertyChangedEvent("BirthDate");
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChangedEvent("Address");
            }
        }

        private Schedule _schedule;
        public Schedule Schedule
        {
            get => _schedule;
            set
            {
                _schedule = value;
                RaisePropertyChangedEvent("Schedule");
            }
        }

        public Volunteer()
        {

        }

        public Volunteer(string name, DateTime birthDate, string address, List<Activity> activities)
        {
            Name = name;
            BirthDate = birthDate;
            Address = address;
            _schedule = new Schedule(activities);
        }

        public override string ToString()
        {
            return this.Name + " - " + this.BirthDate.ToString("MM/dd/yyyy") + " - " + this.Address;
        }
    }
}
