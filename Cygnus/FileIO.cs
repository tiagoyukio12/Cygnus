using System;
using System.Collections.Generic;
using System.IO;
using Cygnus.Models;

namespace Cygnus
{
    class FileIO
    {
        readonly string FilePath;
        readonly string AlternativePath;

        public FileIO(string Name)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FilePath = Path.Combine(docPath, Name);

            string currPath = AppDomain.CurrentDomain.BaseDirectory;
            AlternativePath = Path.Combine(currPath, Name);
        }


        public List<Volunteer> ReadVolunteers()
        {
            string path = FilePath;
            if (File.Exists(AlternativePath))
                path = AlternativePath;

            List<Volunteer> volunteers = new List<Volunteer>();

            using (StreamReader sr = new StreamReader(path))
            {
                int numVolunteers = Convert.ToInt32(sr.ReadLine());
                for (int i = 0; i < numVolunteers; i++)
                {
                    string name = sr.ReadLine();
                    DateTime birthDate = Convert.ToDateTime(sr.ReadLine());
                    string address = sr.ReadLine();
                    List<Activity> activities = new List<Activity>();
                    int numActivities = Convert.ToInt32(sr.ReadLine());
                    for (int j = 0; j < numActivities; j++)
                    {
                        string id = sr.ReadLine();
                        string location = sr.ReadLine();
                        DateTime startDate = Convert.ToDateTime(sr.ReadLine());
                        int turn = Convert.ToInt32(sr.ReadLine());
                        string frequency = sr.ReadLine();

                        activities.Add(new Activity(id, location, startDate, turn, frequency));
                    }
                    volunteers.Add(new Volunteer(name, birthDate, address, activities));
                }
            }
            return volunteers;
        }

        public void WriteVolunteers(List<Volunteer> volunteers)
        {
            string path = FilePath;
            if (File.Exists(AlternativePath))
                path = AlternativePath;
            using StreamWriter outputFile = new StreamWriter(path);

            outputFile.Write(volunteers.Count.ToString() + "\n");
            foreach (Volunteer volunteer in volunteers)
            {
                string text = "";
                text += volunteer.Name + "\n";
                text += volunteer.BirthDate.ToString() + "\n";
                text += volunteer.Address + "\n";
                text += volunteer.Schedule.Activities.Count.ToString() + "\n";
                foreach (Activity activity in volunteer.Schedule.Activities)
                {
                    text += activity.Id + "\n";
                    text += activity.Location + "\n";
                    text += activity.StartDate.ToString() + "\n";
                    text += activity.Turn.ToString() + "\n";
                    text += activity.Frequency + "\n";
                }
                outputFile.Write(text);
            }
        }
    }
}
