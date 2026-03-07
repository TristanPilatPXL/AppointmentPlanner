using AppointmentPlanner.Domain;

namespace AppointmentPlanner.Infrastructure
{
    public class Repository
    {
        private readonly List<Room> _Room = new();
        private readonly List<Appointment> _Appointment = new();

        public void AddRoom(Room room) => _Room.Add(room);
        public List<Room> All() => _Room;

        public void Add(Appointment appointment) => _Appointment.Add(appointment);
        public List<Appointment> AllAppointments() => _Appointment;


    }
}
