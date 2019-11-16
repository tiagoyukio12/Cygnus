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
    }
}
