using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FlightTrackerGUI;
using Mapsui.Projections;

namespace OOD_project_1
{
    public class FlightTracker
    {
        List<FlightGUI> flightsData = new List<FlightGUI>();
        public IFlightConverter flightDataConverter = new FlightConverterAdapter();

        public void Start()
        {
            Thread thread = new Thread(Runner.Run);
            thread.Start();
            while (true)
            {
                ConvertData();
                Runner.UpdateGUI(new FlightsGUIData(flightsData));
                Thread.Sleep(1000);
            }
        }

        public void ReadData()
        {
            Console.WriteLine("Type 1 to read from the FTR file, 2 to read from the Network");
            string word = "";
            while (word != "1" && word != "2")
            {
                word = Console.ReadLine();
                if (word == "1")
                {
                    FTRreader reader = new FTRreader();
                    reader.Read(new FactoryMethods(), "example_data.ftr");
                }
                else if (word == "2")
                {
                    Server server = new Server();
                    server.StartReading("example_data.ftr");
                    server.BitReaderThread.Join();
                }
                else
                {
                    Console.WriteLine(
                        "Invalid command. Use '1' to read from the FTR file, '2' to read from the network");
                }
            }
        }

        public void ConvertData()
        {
            flightsData.Clear();
            flightsData = new List<FlightGUI>();
            foreach (var flight in Database.FlightDictionary)
            {
                var flightGUI = flightDataConverter.ConvertFlightToGUI(flight.Value, Database.AirportDictionary);

                if (flight.Value.Longitude < float.MaxValue && flight.Value.Latitude < float.MaxValue)
                {
                    flightsData.Add(flightGUI);
                }
            }
        }
    }
}