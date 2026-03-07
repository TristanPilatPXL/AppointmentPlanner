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
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ParticipantsCount { get; set; }
        public Room Room { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedAt { get; set; }

        public TimeSpan Duration => EndTime - StartTime;

        public Appointment(string title, DateTime startTime, DateTime endTime, int participantsCount, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title mag niet leeg zijn.");

            if (endTime <= startTime)
                throw new ArgumentException("EndTime moet later zijn dan StartTime.");

            if (participantsCount < 1)
                throw new ArgumentException("ParticipantsCount moet >= 1 zijn.");

            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            ParticipantsCount = participantsCount;
            CreatedAt = createdAt;
        }
    }
}
