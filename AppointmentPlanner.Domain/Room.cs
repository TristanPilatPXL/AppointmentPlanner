namespace AppointmentPlanner.Domain
{
    public class Room
    {

        //hier terug waardes in krijgen en terug in opslaan
        public Guid Number { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }

        public Room(string name, int maxCapacity)// teryg regels toepassen
        {
            if (string.IsNullOrWhiteSpace(name))          // was: Name (property ipv parameter)
                throw new ArgumentException("Name mag niet leeg zijn.");

            if (maxCapacity <= 0)                          // was: >= 0 (omgekeerde logica)
                throw new ArgumentException("MaxCapacity moet groter zijn dan 0"); // was: AggregateException

            Name = name;                                   // was: name = Name (omgekeerd)
            MaxCapacity = maxCapacity;                      // was: maxCapacity = MaxCapacity (omgekeerd)
            Number = Guid.NewGuid();
        }
    }
}
