﻿using System;
using System.Collections.Generic;

namespace Cygnus
{
    public class Volunteer
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public List<Activity> Activities { get; set; }

        public Volunteer(string name, DateTime birthDate, string address, List<Activity> activities)
        {
            Name = name;
            BirthDate = birthDate;
            Address = address;
            Activities = activities;
        }

        public override string ToString()
        {
            return this.Name + " - " + this.BirthDate.ToString("MM/dd/yyyy") + " - " + this.Address;
        }
    }
}