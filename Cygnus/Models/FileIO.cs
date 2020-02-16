using System;
using System.Collections.Generic;
using System.IO;

namespace Cygnus.Models
{
    /// <summary>
    /// Read and write volunteers and activities data.
    /// </summary>
    class FileIO
    {
        readonly string FilePath;
        readonly string AlternativePath;

        /// <summary>
        /// Creates file path to save file in Documents and current program folders.
        /// </summary>
        /// <param name="Name">Name of the save file.</param>
        public FileIO(string Name)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FilePath = Path.Combine(docPath, Name);

            string currPath = AppDomain.CurrentDomain.BaseDirectory;
            AlternativePath = Path.Combine(currPath, Name);
        }

        /// <summary>
        /// Tries to read save file from Documents folder. If it doesn't exist, reads from current program folder.
        /// </summary>
        /// <remarks>
        /// Save file has following structure:
        /// Number of volunteers
        ///     Name of volunteer
        ///     Birthday of volunteer
        ///     Address of volunteer
        ///     Number of activities
        ///         ID of activity
        ///         Name of activity
        ///         Description of activity
        ///         Number of activity attributes
        ///             Name of attribute
        ///         Location of activity
        ///         Starting date of activity
        ///         Turn of activity
        ///         Type of recurrence of activity
        ///         Period of recurrence of activity
        ///         (Remaining activities from volunteer)
        ///     (Remaining volunteers)
        /// </remarks>
        /// <returns>List of saved volunteers and their activities.</returns>
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
                        string nameActivity = sr.ReadLine();
                        string description = sr.ReadLine();
                        int numAttributes = Convert.ToInt32(sr.ReadLine());
                        List<string> attributes = new List<string>();
                        for (int k = 0; k < numAttributes; k++)
                            attributes.Add(sr.ReadLine());
                        string location = sr.ReadLine();
                        DateTime startDate = Convert.ToDateTime(sr.ReadLine());
                        int turn = Convert.ToInt32(sr.ReadLine());
                        string timeLine = sr.ReadLine();
                        int[] time = Array.ConvertAll(timeLine.Split(','), int.Parse);
                        switch (turn)
                        {
                            case 1:
                                time = new int[] { 7, 0, 9, 0 };
                                break;
                            case 2:
                                time = new int[] { 12, 0, 13, 0 };
                                break;
                            case 3:
                                time = new int[] { 15, 0, 18, 0 };
                                break;
                        }
                        string freqType = sr.ReadLine();
                        string freqPeriod = sr.ReadLine();
                        Frequency frequency = new Frequency(freqType, freqPeriod);

                        activities.Add(new Activity(id, nameActivity, description, attributes, location, startDate, turn, time, frequency));
                    }
                    volunteers.Add(new Volunteer(name, birthDate, address, activities));
                }
            }
            return volunteers;
        }

        /// <summary>
        /// Writes to save file in current folder if it exists. Else, writes to Documents folder.
        /// </summary>
        /// <param name="volunteers">List of volunteers to be written.</param>
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
                    text += activity.Name + "\n";
                    text += activity.Description + "\n";
                    text += activity.Attributes.Count.ToString() + "\n";
                    foreach (string attribute in activity.Attributes)
                    {
                        text += attribute + "\n";
                    }
                    text += activity.Location + "\n";
                    text += activity.StartDate.ToString() + "\n";
                    text += activity.Turn.ToString() + "\n";
                    text += string.Join(", ", activity.Time) + "\n";
                    text += activity.Frequency.Type + "\n";
                    text += activity.Frequency.Period + "\n";
                }
                outputFile.Write(text);
            }
        }
    }
}
