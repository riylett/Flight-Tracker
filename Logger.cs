using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public class Logger : IObserver
    {
        private static Logger? instance;

        private Logger()
        {
            LogModification("Start Update", "Logger started.");
        }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        public static bool IsLoggerCreated()
        {
            return instance != null;
        }

        public void Update(IDUpdateArgs args)
        {
            string data = $"Object ID: {args.ObjectID}, New Object ID: {args.NewObjectID}";
            LogModification("ID Update", data);
        }

        public void Update(ContactInfoUpdateArgs args)
        {
            string data =
                $"Object ID: {args.ObjectID}, Phone Number: {args.PhoneNumber}, Email Address: {args.EmailAddress}";
            LogModification("Contact Info Update", data);
        }

        public void Update(PositionUpdateArgs args)
        {
            string data =
                $"Object ID: {args.ObjectID}, Latitude: {args.Latitude}, Longitude: {args.Longitude}, AMSL: {args.AMSL}";
            LogModification("Position Update", data);
        }

        public void LogModification(string updateType, string data)
        {
            string timestamp = DateTime.Now.ToString();
            string date = DateTime.Now.ToString("yyyy_MM_dd");
            string logFileName = $"{date}.txt";
            string logFilePath = Path.Combine("logs", logFileName);

            Directory.CreateDirectory("logs");

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"Timestamp: {timestamp}");
                writer.WriteLine($"Update Type: {updateType}");
                writer.WriteLine(data);
                writer.WriteLine();
            }
        }
    }
}