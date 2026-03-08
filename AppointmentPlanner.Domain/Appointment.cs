using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppointmentPlanner.Domain
{
    public class Appointment
    {
        //domain zijn nut is om waardes te krijgen met de get en op te slaan met de set
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ParticipantsCount { get; set; }
        public Room Room { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedAt { get; set; }

        //// Berekende property: geeft automatisch de duur van de afspraak terug (EndTime - StartTime)
        public TimeSpan Duration => EndTime - StartTime;

        public Appointment(string title, DateTime startTime, DateTime endTime, int participantsCount, DateTime createdAt)//regels in stellen
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title mag niet leeg zijn.");

            if (endTime <= startTime)
                throw new ArgumentException("EndTime moet later zijn dan StartTime.");

            if (participantsCount < 1)
                throw new ArgumentException("ParticipantsCount moet >= 1 zijn.");


            //hier wijze we de waarden toe in de get sets hier boven
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            ParticipantsCount = participantsCount;
            CreatedAt = createdAt;
        }
    }
}
