using System.Collections.Generic;

namespace Cygnus
{
    class Volunteers
    {
        private static Volunteers instance;
        private static List<Volunteer> ListVolunteers { get; set; }

        public static Volunteers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Volunteers();
                }
                return instance;
            }
        }

        private Volunteers()
        {
            ListVolunteers = new List<Volunteer>();
        }

        public void Add(Volunteer volunteer)
        {
            ListVolunteers.Add(volunteer);
        }

        public void Add(List<Volunteer> volunteers)
        {
            ListVolunteers.AddRange(volunteers);
        }

        public void Add(Activity activity)
        {
            ListVolunteers[0].Activities.Add(activity);
        }

        public List<Volunteer> ToList => ListVolunteers;
    }
}