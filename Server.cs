using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetworkSourceSimulator;


namespace OOD_project_1
{
    public class Server : IObservable
    {
        public List<IObserver> Observers { get; set; } = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers(IDUpdateArgs args)
        {
            foreach (var observer in Observers)
            {
                observer.Update(args);
            }
        }

        public void NotifyObservers(ContactInfoUpdateArgs args)
        {
            foreach (var observer in Observers)
            {
                observer.Update(args);
            }
        }

        public void NotifyObservers(PositionUpdateArgs args)
        {
            foreach (var observer in Observers)
            {
                observer.Update(args);
            }
        }

        public Thread BitReaderThread { get; set; }
        NetworkSourceSimulator.NetworkSourceSimulator Network { get; set; }

        BitParseMethods methods = new BitParseMethods();

        public void Run(string path)
        {
            StartReading(path);
            ConsoleRead();
        }

        public void ConsoleRead()
        {
            while (true)
            {
                string word = Console.ReadLine();

                if (word.ToLower() == "print")
                {
                    var serializer = new JsonObjectSerializer();
                    Dictionary<UInt64, IFlightData>
                        ObjectsToSerialize = new Dictionary<UInt64, IFlightData>(Database.DataDictionary);
                    serializer.SerializetoFile(ObjectsToSerialize.Values,
                        $"snapshot_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.json");
                }

                else if (word.ToLower() == "exit")
                {
                    Console.WriteLine("Closing the application");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine(
                        "Invalid command. Use 'print' to take a snapshot and 'exit' to close the application.");
                }
            }
        }

        public void StartReading(string path)
        {
            Network = new NetworkSourceSimulator.NetworkSourceSimulator(path, 0, 0);
            Network.OnNewDataReady += (sender, args) =>
            {
                var message = Network.GetMessageAt(args.MessageIndex);
                IFlightData obj = methods.Methods[Encoding.ASCII.GetString(message.MessageBytes, 0, 3)]
                    .Create(message.MessageBytes);
                Database.DataDictionary.Add(obj.ID, obj);
            };
            Network.OnContactInfoUpdate += (sender, args) => { NotifyObservers(args); };
            Network.OnIDUpdate += (sender, args) => { NotifyObservers(args); };
            Network.OnPositionUpdate += (sender, args) => { NotifyObservers(args); };
            Console.WriteLine("Started reading...");
            BitReaderThread = new Thread(() =>
            {
                Network.Run();
                OnThreadFinished();
            });
            BitReaderThread.Start();
        }

        public void OnThreadFinished()
        {
            Console.WriteLine("Finished reading.");
            if (Logger.IsLoggerCreated())
                Logger.Instance.LogModification("End Update", "Finished logging.");
        }
    }
}