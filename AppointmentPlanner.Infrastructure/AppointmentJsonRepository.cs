using AppointmentPlanner.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public AppointmentJsonRepository()//check of folder en bestand bestaat zo nee dan maak het aan
        {
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            _filePath = Path.Combine(_dataFolder, "appointments.json");

            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);

            // ✅ Schrijf "[]" (lege JSON array) in plaats van een leeg bestand
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }



        public List<Appointment> Get()//leest het bestand
        {
            string json = File.ReadAllText(_filePath);//hier maken we duidleijk welk bestand het moet lezen
            return JsonSerializer.Deserialize<List<Appointment>>(json) ?? new List<Appointment>();//hier zetten we json tekst terug om naar c# objecten

        }

        public void Add(Appointment appointment)
        {
            try
            {
                List<Appointment> appointments = Get();   // Lees bestaande data
                appointments.Add(appointment);// Voeg toe
                string json = JsonSerializer.Serialize(appointments, new JsonSerializerOptions { WriteIndented = true });// hier zetten we c# objecten om naar json
                File.WriteAllText(_filePath, json); // Sla alles op
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[AppointmentJsonRepository] Error writing file: " + ex);
                throw;
            }
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
            Appointment toCancel = appointments.FirstOrDefault(a =>
                a.Title.Equals(appointment.Title, StringComparison.OrdinalIgnoreCase));


            // Stap 3: Als de appointment niet bestaat, gooi een foutmelding
            if (toCancel == null)
                throw new InvalidOperationException("Appointment niet gevonden.");


            // Stap 4: Verwijder de gevonden appointment uit de lijst
            appointments.Remove(toCancel);


            // Stap 5: Zet de bijgewerkte lijst terug om naar JSON
            string json = JsonSerializer.Serialize(appointments);


            // Stap 6: Overschrijf het bestand met de nieuwe lijst (zonder de verwijderde)
            File.WriteAllText(_filePath, json);
        }



        public void Cancel(Appointment appointment)
        {
            // Stap 1: Lees alle bestaande appointments uit het JSON bestand
            List<Appointment> appointments = Get();

            // Stap 2: Zoek de appointment die overeenkomt met de meegegeven titel
            // FirstOrDefault geeft null terug als er niks gevonden wordt
            Appointment toCancel = appointments.FirstOrDefault(a =>
                a.Title.Equals(appointment.Title, StringComparison.OrdinalIgnoreCase));

            // Stap 3: Als de appointment niet bestaat, gooi een foutmelding
            if (toCancel == null)
                throw new InvalidOperationException("Appointment niet gevonden.");

            // Stap 4: Markeer de gevonden appointment als geannuleerd (zet IsCancelled op true)
            if (!toCancel.IsCancelled)
            {
                toCancel.IsCancelled = true;
                // (optioneel) toCancel.CancelledAt = DateTime.Now; // als je een timestamp wil bijhouden
            }

            // Stap 5: Zet de bijgewerkte lijst terug om naar JSON (indented voor leesbaarheid)
            string json = JsonSerializer.Serialize(appointments, new JsonSerializerOptions { WriteIndented = true });

            // Stap 6: Overschrijf het bestand met de nieuwe lijst (met de geannuleerde flag)
            File.WriteAllText(_filePath, json);
        }
    }
}
