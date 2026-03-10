using System;
using AppointmentPlanner.Domain;
using AppointmentPlanner.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentPlanner.Application
{
    public class PlannerService
    {
        private readonly AppointmentJsonRepository _appointmentjsonrepository;
        private readonly RoomCsvRepository _roomcsvrepository;

        public PlannerService(AppointmentJsonRepository appointmentjsonrepository, RoomCsvRepository roomcsvrepository)
        {
            _appointmentjsonrepository = appointmentjsonrepository;
            _roomcsvrepository = roomcsvrepository;
        }

        // Nieuwe appointment aanmaken en opslaan
        public void AddAppointment(string title, DateTime startTime, DateTime endTime, int participantsCount)
        {
            // Kijken of er al een appointment bestaat met dezelfde titel én starttijd
            bool alreadyExists = _appointmentjsonrepository.Get().Any(a =>
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
            _appointmentjsonrepository.Add(newAppointment);
        }


        // Nieuwe appointment aanmaken en opslaan
        public void AddRoom(string name, int maxCapacity, Guid number)
        {
            // Kijken of er al een appointment bestaat met dezelfde titel én starttijd
            bool alreadyExists = _roomcsvrepository.Get().Any(a =>
                a.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                a.Number == number);

            // Zo ja, foutmelding gooien
            if (alreadyExists)
                throw new InvalidOperationException("Deze room bestaat al.");

            // Nieuw Appointment-object aanmaken met de meegegeven gegevens
            Room newRoom = new Room(
                name,
                maxCapacity,
                number
            );

            // Opslaan in de repository
            _roomcsvrepository.Add(newRoom);
        }


        public void UpdateAppointment(Appointment updatedAppointment)
        {
            _appointmentjsonrepository.Update(updatedAppointment);

        }

        public void CancelAppointment(Appointment cancelAppointment)
        {
            _appointmentjsonrepository.Cancel(cancelAppointment);


        }


        // Alle rooms ophalen uit de repository
        public List<Room> GetAllRooms()
        {
            return _roomcsvrepository.Get();
        }

        // Alle appointments ophalen uit de repository
        public List<Appointment> GetAllAppointments()
        {
            return _appointmentjsonrepository.Get();
        }

    }
}
