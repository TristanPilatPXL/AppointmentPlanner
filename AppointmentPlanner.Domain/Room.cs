namespace AppointmentPlanner.Domain
{
    public class Room
    {
        public Guid Number { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }

        public Room(string name, int  maxCapacity)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name mag niet leeg zijn.");

            if (maxCapacity >= 0)
                throw new AggregateException("MaxCapacity moet groter zijn dan 0");

            name = Name;
            maxCapacity = MaxCapacity;
        }

        

    }
}
