using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSourceSimulator;

namespace OOD_project_1
{
    public class UpdateManager : IObserver
    {
        public void Update(IDUpdateArgs args)
        {
            if (Database.DataDictionary.TryGetValue(args.ObjectID, out var data))
            {
                if (Database.DataDictionary.ContainsKey(args.NewObjectID))
                {
                    Console.WriteLine($"Object with ID {args.NewObjectID} already exists.");
                    if (Logger.IsLoggerCreated())
                        Logger.Instance.LogModification("ID Update",
                            $"ERROR. Object with ID {args.NewObjectID} already exists.");
                }
                else
                {
                    Database.DataDictionary.Remove(args.ObjectID);
                    Database.DataDictionary.Add(args.NewObjectID, data);
                    data.ModifyDictionary(args.NewObjectID);
                }
            }
            else
            {
                Console.WriteLine($"Object with ID {args.ObjectID} not found.");
                if (Logger.IsLoggerCreated())
                    Logger.Instance.LogModification("ID Update", $"ERROR. Object with ID {args.ObjectID} not found.");
            }
        }

        public void Update(ContactInfoUpdateArgs args)
        {
            if (Database.CrewDictionary.TryGetValue(args.ObjectID, out var data))
            {
                data.Phone = args.PhoneNumber;
                data.Email = args.EmailAddress;
            }
            else if (Database.PassangerDictionary.TryGetValue(args.ObjectID, out var data2))
            {
                data2.Phone = args.PhoneNumber;
                data2.Email = args.EmailAddress;
            }
            else
            {
                Console.WriteLine($"Object with ID {args.ObjectID} not found.");
                if (Logger.IsLoggerCreated())
                    Logger.Instance.LogModification("Contact Info Update",
                        $"ERROR. Object with ID {args.ObjectID} not found.");
            }
        }

        public void Update(PositionUpdateArgs args)
        {
            if (Database.FlightDictionary.TryGetValue(args.ObjectID, out var data))
            {
                data.Latitude = args.Latitude;
                data.Longitude = args.Longitude;
                data.AMSL = args.AMSL;
            }
            else if (Database.AirportDictionary.TryGetValue(args.ObjectID, out var data2))
            {
                data2.Latitude = args.Latitude;
                data2.Longitude = args.Longitude;
                data2.AMSL = args.AMSL;
            }
            else
            {
                Console.WriteLine($"Object with ID {args.ObjectID} not found.");
                if (Logger.IsLoggerCreated())
                    Logger.Instance.LogModification("Position Update",
                        $"ERROR. Object with ID {args.ObjectID} not found.");
            }
        }
    }
}