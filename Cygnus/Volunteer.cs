using System;

namespace Cygnus
{
    public class Volunteer
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return this.Name + " - " + this.BirthDate.ToString("MM/dd/yyyy") + " - " + this.Address;
        }
    }
}
