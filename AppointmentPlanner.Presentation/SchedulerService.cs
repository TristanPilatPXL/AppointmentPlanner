using AppointmentPlanner.Domain;
using AppointmentPlanner.Infrastructure;
namespace AppointmentPlanner.Presentation
{
    public class SchedulerService
    {
        private readonly Repository _repository;

        public SchedulerService(Repository repository)
        {
            _repository = repository;
        }

        public void AddAppointment(string Name)
        {
            Appointment newAppointment = new Appointment(Name);

            bool alreadyExists = _repository.All().Any(s =>
                s.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (alreadyExists)
                throw new InvalidOperationException("Deze student bestaat al.");

            _repository.Add(newStudent);
        }

        public List<Room> GetAllRooms()
        {
            return _repository.All();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _repository.All();
        }
    }
}
