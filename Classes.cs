using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public interface IReportable
    {
        public string GetReport(NewsProvider newsProvider);
    }

    public interface IFlightData
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public void ModifyDictionary(UInt64 newID);
    }

    public class Crew : IFlightData
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }

        public Crew(ulong iD, string name, ulong age, string phone, string email, ushort practice, string role)
        {
            Type = "Crew";
            ID = iD;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.CrewDictionary.Remove(ID);
            ID = newID;
            Database.CrewDictionary.Add(newID, this);
        }
    }

    public class Passenger : IFlightData
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public UInt64 Miles { get; set; }

        public Passenger(ulong iD, string name, ulong age, string phone, string email, string @class, ulong miles)
        {
            Type = "Passenger";
            ID = iD;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Class = @class;
            Miles = miles;
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.PassangerDictionary.Remove(ID);
            ID = newID;
            Database.PassangerDictionary.Add(newID, this);
        }
    }

    public class Cargo : IFlightData
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public Single Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong iD, float weight, string code, string description)
        {
            Type = "Cargo";
            ID = iD;
            Weight = weight;
            Code = code;
            Description = description;
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.CargoDictionary.Remove(ID);
            ID = newID;
            Database.CargoDictionary.Add(newID, this);
        }
    }

    public class CargoPlane : IFlightData, IReportable
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public Single MaxLoad { get; set; }

        public CargoPlane(ulong iD, string serial, string country, string model, float maxLoad)
        {
            Type = "Cargo Plane";
            ID = iD;
            Serial = serial;
            Country = country;
            Model = model;
            MaxLoad = maxLoad;
        }

        public string GetReport(NewsProvider newsProvider)
        {
            return newsProvider.ReportCargoPlane(this);
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.CargoPlaneDictionary.Remove(ID);
            ID = newID;
            Database.CargoPlaneDictionary.Add(newID, this);
        }
    }

    public class PassengerPlane : IFlightData, IReportable
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlane(ulong iD, string serial, string country, string model, ushort firstClassSize,
            ushort businessClassSize, ushort economyClassSize)
        {
            Type = "Passenger Plane";
            ID = iD;
            Serial = serial;
            Country = country;
            Model = model;
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }

        public string GetReport(NewsProvider newsProvider)
        {
            return newsProvider.ReportPassangerPlane(this);
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.PassengerPlaneDictionary.Remove(ID);
            ID = newID;
            Database.PassengerPlaneDictionary.Add(newID, this);
        }
    }

    public class Airport : IFlightData, IReportable
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public string Country { get; set; }

        public Airport(ulong iD, string name, string code, float longitude, float latitude, float aMSL, string country)
        {
            Type = "Airport";
            ID = iD;
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = aMSL;
            Country = country;
        }

        public string GetReport(NewsProvider newsProvider)
        {
            return newsProvider.ReportAirport(this);
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.AirportDictionary.Remove(ID);
            ID = newID;
            Database.AirportDictionary.Add(newID, this);
        }
    }

    public class Flight : IFlightData
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }
        public UInt64 OriginAsID { get; set; }
        public UInt64 TargetAsID { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[] CrewAsIDs { get; set; }
        public UInt64[] LoadAsIDs { get; set; }

        public Flight(ulong iD, ulong originAsID, ulong targetAsID, string takeoffTime, string landingTime,
            float longitude, float latitude, float aMSL, ulong planeID, ulong[] crewAsIDs, ulong[] loadAsIDs)
        {
            Type = "Flight";
            ID = iD;
            OriginAsID = originAsID;
            TargetAsID = targetAsID;
            TakeoffTime = takeoffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = aMSL;
            PlaneID = planeID;
            CrewAsIDs = crewAsIDs;
            LoadAsIDs = loadAsIDs;
        }

        public void ModifyDictionary(UInt64 newID)
        {
            Database.FlightDictionary.Remove(ID);
            ID = newID;
            Database.FlightDictionary.Add(newID, this);
        }
    }
}