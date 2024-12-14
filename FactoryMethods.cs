using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public abstract class Creator
    {
        public abstract IFlightData Create(params string[] args);
    }

    public class CrewCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 7)
                throw new ArgumentException("Invalid number of arguments for Crew.");
            Crew crew = new Crew(ulong.Parse(args[0]), args[1], ulong.Parse(args[2]), args[3], args[4],
                ushort.Parse(args[5]), args[6]);
            Database.CrewDictionary.Add(crew.ID, crew);
            return crew;
        }
    }

    public class PassengerCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 7)
                throw new ArgumentException("Invalid number of arguments for Passenger.");
            Passenger passenger = new Passenger(ulong.Parse(args[0]), args[1], ulong.Parse(args[2]), args[3], args[4],
                args[5], ulong.Parse(args[6]));
            Database.PassangerDictionary.Add(passenger.ID, passenger);
            return passenger;
        }
    }

    public class CargoCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 4)
                throw new ArgumentException("Invalid number of arguments for Cargo.");
            Cargo cargo = new Cargo(ulong.Parse(args[0]), float.Parse(args[1]), args[2], args[3]);
            Database.CargoDictionary.Add(cargo.ID, cargo);
            return cargo;
        }
    }

    public class CargoPlaneCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 5)
                throw new ArgumentException("Invalid number of arguments for CargoPlane.");
            CargoPlane cargoPlane = new CargoPlane(ulong.Parse(args[0]), args[1], args[2], args[3],
                float.Parse(args[4]));
            Database.CargoPlaneDictionary.Add(cargoPlane.ID, cargoPlane);
            return cargoPlane;
        }
    }

    public class PassengerPlaneCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 7)
                throw new ArgumentException("Invalid number of arguments for PassengerPlane.");
            PassengerPlane passengerPlane = new PassengerPlane(ulong.Parse(args[0]), args[1], args[2], args[3],
                ushort.Parse(args[4]), ushort.Parse(args[5]), ushort.Parse(args[6]));
            Database.PassengerPlaneDictionary.Add(passengerPlane.ID, passengerPlane);
            return passengerPlane;
        }
    }

    public class AirportCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 7)
                throw new ArgumentException("Invalid number of arguments for Airport.");
            Airport airport = new Airport(ulong.Parse(args[0]), args[1], args[2], float.Parse(args[3]),
                float.Parse(args[4]), float.Parse(args[5]), args[6]);
            Database.AirportDictionary.Add(airport.ID, airport);
            return airport;
        }
    }

    public class FlightCreator : Creator
    {
        public override IFlightData Create(string[] args)
        {
            if (args.Length != 11)
                throw new ArgumentException("Invalid number of arguments for Flight.");
            Flight flight = new Flight(ulong.Parse(args[0]), ulong.Parse(args[1]), ulong.Parse(args[2]), args[3],
                args[4], float.Parse(args[5]), float.Parse(args[6]), float.Parse(args[7]), ulong.Parse(args[8]),
                StringtoArray(args[9]), StringtoArray(args[10]));
            Database.FlightDictionary.Add(flight.ID, flight);
            return flight;
        }

        public UInt64[] StringtoArray(string args)
        {
            var IDs = new List<UInt64>();
            string[] IDstrings = args.Trim('[', ']').Split(';');
            ulong[] result = IDstrings.Select(element => ulong.Parse(element)).ToArray();
            return result;
        }
    }

    public class FactoryMethods
    {
        public Dictionary<string, Creator> Methods { get; }

        public FactoryMethods()
        {
            Methods = new Dictionary<string, Creator>();
            Methods.Add("C", new CrewCreator());
            Methods.Add("P", new PassengerCreator());
            Methods.Add("CA", new CargoCreator());
            Methods.Add("CP", new CargoPlaneCreator());
            Methods.Add("PP", new PassengerPlaneCreator());
            Methods.Add("AI", new AirportCreator());
            Methods.Add("FL", new FlightCreator());
        }
    }
}