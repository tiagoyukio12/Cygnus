using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cygnus
{
    class FileIO
    {
        string FilePath;

        public FileIO(string Name)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(docPath, Name);
            this.FilePath = filePath;
        }

        public List<Activity> ReadActivities()
        {
            List<Activity> activities = new List<Activity>();
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);

                for (int i = 0; i < lines.Length / 4; i++)
                {
                    string id = lines[4 * i];
                    string location = lines[4 * i + 1];
                    DateTime startDate = Convert.ToDateTime(lines[4 * i + 2]);
                    int turn = Convert.ToInt32(lines[4 * i + 3]);
                    string frequency = lines[4 * i + 4];

                    activities.Add(new Activity(id, location, startDate, turn, frequency));
                }
            }
            return activities;
        }

        public List<Volunteer> ReadVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);
                StreamReader sr = new StreamReader(FilePath);

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

        public void WriteActivities(List<Activity> activities)
        {
            using StreamWriter outputFile = new StreamWriter(FilePath);
            foreach (Activity activity in activities)
            {
                string text = "";
                text += activity.Id + "\n";
                text += activity.Location + "\n";
                text += activity.StartDate.ToString() + "\n";
                text += activity.Turn.ToString() + "\n";
                text += activity.Frequency + "\n";
                outputFile.Write(text);
            }
        }

        public void WriteVolunteers(List<Volunteer> volunteers)
        {
            using StreamWriter outputFile = new StreamWriter(FilePath);
            outputFile.Write(volunteers.Count.ToString() + "\n");
            foreach (Volunteer volunteer in volunteers)
            {
                string text = "";
                text +=volunteer.Name + "\n";
                text += volunteer.BirthDate.ToString() + "\n";
                text += volunteer.Address + "\n";
                text += volunteer.Activities.Count.ToString() + "\n";
                foreach (Activity activity in volunteer.Activities)
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
