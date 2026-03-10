using AppointmentPlanner.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppointmentPlanner.Infrastructure
{
    public class AppointmentJsonRepository
    {


        private readonly string _dataFolder;
        private readonly string _filePath;

        public AppointmentJsonRepository()
        {
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            _filePath = Path.Combine(_dataFolder, "appointments.json");

            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);

            // ✅ Schrijf "[]" (lege JSON array) in plaats van een leeg bestand
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }



        public List<Appointment> Get()
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Appointment>>(json) ?? new List<Appointment>();

        }

        public void Add(Appointment appointment)
        {
            List<Appointment> appointments = Get();   // Lees bestaande data
            appointments.Add(appointment);               // Voeg toe
            string json = JsonSerializer.Serialize(appointments);
            File.WriteAllText(_filePath, json);          // Sla alles op
        }

        public void Update(Appointment appointment)
        {
            // Stap 1: Lees alle bestaande appointments uit het JSON bestand
            List<Appointment> appointments = Get();

            // Stap 2: Zoek de index van de appointment met dezelfde titel
            // FindIndex geeft -1 terug als er niks gevonden wordt
            int index = appointments.FindIndex(a =>
                a.Title.Equals(appointment.Title, StringComparison.OrdinalIgnoreCase));

            // Stap 3: Als de appointment niet bestaat, gooi een foutmelding
            if (index == -1)
                throw new InvalidOperationException("Appointment niet gevonden.");

            // Stap 4: Vervang de oude appointment door de nieuwe op dezelfde positie
            appointments[index] = appointment;

            // Stap 5: Zet de bijgewerkte lijst terug om naar JSON
            string json = JsonSerializer.Serialize(appointments);

            // Stap 6: Overschrijf het bestand met de nieuwe lijst (met de geüpdatete versie)
            File.WriteAllText(_filePath, json);
        }

 

        public void Delete(Appointment appointment)
        {
            // Stap 1: Lees alle bestaande appointments uit het JSON bestand
            List<Appointment> appointments = Get();


            // Stap 2: Zoek de appointment die overeenkomt met de meegegeven titel
            // FirstOrDefault geeft null terug als er niks gevonden wordt
            Appointment toRemove = appointments.FirstOrDefault(a =>
                a.Title.Equals(appointment.Title, StringComparison.OrdinalIgnoreCase));


            // Stap 3: Als de appointment niet bestaat, gooi een foutmelding
            if (toRemove == null)
                throw new InvalidOperationException("Appointment niet gevonden.");


            // Stap 4: Verwijder de gevonden appointment uit de lijst
            appointments.Remove(toRemove);


            // Stap 5: Zet de bijgewerkte lijst terug om naar JSON
            string json = JsonSerializer.Serialize(appointments);


            // Stap 6: Overschrijf het bestand met de nieuwe lijst (zonder de verwijderde)
            File.WriteAllText(_filePath, json);
        }
    }
}
