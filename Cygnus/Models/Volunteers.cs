using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cygnus.Models
{
    /// <summary>
    /// This singleton class represents the collection of volunteers.
    /// </summary>
    class Volunteers
    {
        private static Volunteers instance;
        public static TrulyObservableCollection<Volunteer> CollectionVolunteers { get; set; }

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
            CollectionVolunteers = new TrulyObservableCollection<Volunteer>();
        }

        public void Add(Volunteer volunteer)
        {
            CollectionVolunteers.Add(volunteer);
        }

        public void Add(List<Volunteer> volunteers)
        {
            volunteers.ForEach(CollectionVolunteers.Add);
        }

        public void Add(Activity activity)
        {
            CollectionVolunteers[0].Schedule.Activities.Add(activity);
        }

        public ObservableCollection<Volunteer> ToCollection => CollectionVolunteers;
    }
}