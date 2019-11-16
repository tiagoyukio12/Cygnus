using System;

namespace Cygnus
{
    public class Activity
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public int Turn { get; set; }
        public string Frequency { get; set; }

        public Activity(string id, string pos, DateTime date, int turn, string freq)
        {
            Id = id;
            Location = pos;
            StartDate = date;
            Turn = turn;
            Frequency = freq;
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}