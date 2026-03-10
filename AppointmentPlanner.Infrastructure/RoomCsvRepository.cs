using AppointmentPlanner.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentPlanner.Infrastructure
{
    public class RoomCsvRepository
    {
        


        private readonly string _dataFolder;
        private readonly string _filePath;

        public RoomCsvRepository()
        {
            //pad voor alles
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Room");
            _filePath = Path.Combine(_dataFolder, "Room.csv");

            //Map aanmaken als die niet bestaat
            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);

            //bestand aanmaken ALLEEN als het nog NIET bestaat
            if (!File.Exists(_filePath))
                File.Create(_filePath).Close();
        }


        public void Add(Room room)
        {
            string line = $"{room.Name};{room.Number};{room.MaxCapacity}";

            using (StreamWriter writer = new StreamWriter(_filePath, append: true))
            {
                writer.WriteLine(line);
            }
        }

        public List<Room> Get()
        {
            List<Room> rooms = new List<Room>();  // ✅ List<Room>, niet Room

            using (StreamReader reader = new StreamReader(_filePath))  // ✅ _filePath, niet "students.csv"
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] values = line.Split(';');
                    //Parse Number en MaxCapacity naar int
                    Room room = new Room(values[0], int.Parse(values[1]), System.Guid.Parse(values[2]));
                    rooms.Add(room);
                }
            }

            return rooms; 
        }
    }
}
