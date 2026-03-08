using AppointmentPlanner.Domain;

namespace AppointmentPlanner.Infrastructure
{
    /// <summary>
    /// Repository: slaat alle data op in het geheugen (lijsten).
    /// Dit is de "database" van de applicatie.
    /// </summary>
    ///     
    public class Repository
    {
        // Lijst met alle rooms
        private readonly List<Room> _Room = new();

        // Lijst met alle appointments
        private readonly List<Appointment> _Appointment = new();



        // Room toevoegen
        public void AddRoom(Room room) => _Room.Add(room);

        // Alle rooms ophalen
        public List<Room> All() => _Room;



        // Appointment toevoegen
        public void Add(Appointment appointment) => _Appointment.Add(appointment);

        // Alle appointments ophalen
        public List<Appointment> AllAppointments() => _Appointment;
    }
}
