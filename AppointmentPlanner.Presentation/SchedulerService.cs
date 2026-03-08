using AppointmentPlanner.Domain;
using AppointmentPlanner.Infrastructure;
namespace AppointmentPlanner.Presentation
{
    public class SchedulerService
    {
        // Repository beschikbaar maken in deze klasse
        private readonly Repository _repository;

        // Constructor: ontvangt de repository van buitenaf (dependency injection)
        public SchedulerService(Repository repository)
        {
            _repository = repository;
        }

        // Nieuwe appointment aanmaken en opslaan
        public void AddAppointment(string title, DateTime startTime, DateTime endTime, int participantsCount)
        {
            // Kijken of er al een appointment bestaat met dezelfde titel én starttijd
            bool alreadyExists = _repository.AllAppointments().Any(a =>
                a.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                a.StartTime == startTime);

            // Zo ja, foutmelding gooien
            if (alreadyExists)
                throw new InvalidOperationException("Deze appointment bestaat al.");

            // Nieuw Appointment-object aanmaken met de meegegeven gegevens
            Appointment newAppointment = new Appointment(
                title,
                startTime,
                endTime,
                participantsCount,
                DateTime.Now // CreatedAt = huidige tijd
            );

            // Opslaan in de repository
            _repository.Add(newAppointment);
        }



        public void AfspraakAnnuleren(string title,DateTime startTime, DateTime endTime, int participantsCount, bool isCancelled)
        {
            // Zoek de bestaande appointment op
            Appointment? appointment = _repository.AllAppointments()
                .FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            //geeft melding als het niet bestaat
            if (appointment == null)
                throw new InvalidOperationException("Deze appointment bestaat niet.");


            //geeft melding als die all cencalled is
            if (appointment.IsCancelled == false)
                throw new InvalidOperationException("Deze appointment is all geanulleerd");

            if (appointment.StartTime < DateTime.Now)
            {
                throw new InvalidOperationException("Deze appointment is te laat voor anulleering");

            }


            appointment.IsCancelled = true;//hier wijzige we de waarde aan een all bestaande appointment

            
        }

        // Alle rooms ophalen uit de repository
        public List<Room> GetAllRooms()
        {
            return _repository.All();
        }

        // Alle appointments ophalen uit de repository
        public List<Appointment> GetAllAppointments()
        {
            return _repository.AllAppointments();
        }
    }
}
