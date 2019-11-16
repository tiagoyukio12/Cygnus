using System.Collections.Generic;

namespace Cygnus
{
    public class Activities
    {
        private static Activities instance;
        private static List<Activity> ListActivities { get; set; }

        public static Activities Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Activities();
                }
                return instance;
            }
        }

        private Activities()
        {
            ListActivities = new List<Activity>();
        }

        public void Add(Activity activity)
        {
            ListActivities.Add(activity);
        }

        public void Add(List<Activity> activities)
        {
            ListActivities.AddRange(activities);
        }

        public List<Activity> ToList => ListActivities;
    }
}
