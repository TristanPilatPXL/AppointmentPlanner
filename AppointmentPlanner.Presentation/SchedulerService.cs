using AppointmentPlanner.Domain;
using AppointmentPlanner.Infrastructure;
namespace AppointmentPlanner.Presentation
{
    public class SchedulerService
    {
        private readonly Repository _repository;//even in de klasse hier in bruikbaar maken
        public SchedulerService(Repository repository)
        {
            _repository = repository;
        }



        public void AddAppointment(string title, DateTime startTime, DateTime endTime, int participantsCount)
        {
            // Check of er al een appointment bestaat met dezelfde titel en starttijd
            bool alreadyExists = _repository.AllAppointments().Any(a =>
                a.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                a.StartTime == startTime);

            if (alreadyExists)
                throw new InvalidOperationException("Deze appointment bestaat al.");

            Appointment newAppointment = new Appointment(
                title,
                startTime,
                endTime,
                participantsCount,
                DateTime.Now
            );

            _repository.Add(newAppointment);
        }

        public List<Room> GetAllRooms()
        {
            return _repository.All();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _repository.AllAppointments();
        }
    }
}
