using System;
using System.Collections.Generic;
using Cygnus.ViewModels;

namespace Cygnus
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
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public List<Activity> Activities { get; set; }

        public Volunteer()
        {
            Activities = new List<Activity>();
        }

        public Volunteer(string name, DateTime birthDate, string address, List<Activity> activities)
        {
            Name = name;
            BirthDate = birthDate;
            Address = address;
            Activities = activities;
        }

        public Activity FindActivity(string id)
        {
            return Activities.Find(x => x.Id == id);
        }

        public override string ToString()
        {
            return this.Name + " - " + this.BirthDate.ToString("MM/dd/yyyy") + " - " + this.Address;
        }
    }
}
