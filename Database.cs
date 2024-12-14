using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public static class Database
    {
        public static Dictionary<UInt64, Crew> CrewDictionary = new();
        public static Dictionary<UInt64, Passenger> PassangerDictionary = new();
        public static Dictionary<UInt64, Cargo> CargoDictionary = new();
        public static Dictionary<UInt64, CargoPlane> CargoPlaneDictionary = new();
        public static Dictionary<UInt64, PassengerPlane> PassengerPlaneDictionary = new();
        public static Dictionary<UInt64, Airport> AirportDictionary = new();
        public static Dictionary<UInt64, Flight> FlightDictionary = new();
        public static Dictionary<UInt64, IFlightData> DataDictionary = new();
    }
}