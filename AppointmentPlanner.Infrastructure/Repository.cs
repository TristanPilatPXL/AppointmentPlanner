using AppointmentPlanner.Domain;

namespace AppointmentPlanner.Infrastructure
{
    public class Repository
    {
        private readonly List<Room> _Room = new();

        public void Add(Room room) => _Room.Add(room);

        public List<Room> All() => _Room;
        

    }
}
