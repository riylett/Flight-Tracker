using NetworkSourceSimulator;
using OOD_project_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public abstract class BitCreator
    {
        public abstract IFlightData Create(Byte[] data);
    }

    public class CrewBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            UInt16 NameLength = BitConverter.ToUInt16(data, 15);
            string Name = Encoding.ASCII.GetString(data, 17, NameLength);
            UInt16 Age = BitConverter.ToUInt16(data, 17 + NameLength);
            string PhoneNumber = Encoding.ASCII.GetString(data, 19 + NameLength, 12);
            UInt16 EmailLength = BitConverter.ToUInt16(data, 31 + NameLength);
            string Email = Encoding.ASCII.GetString(data, 33 + NameLength, EmailLength);
            char Practice = Encoding.ASCII.GetChars(data, 33 + NameLength + EmailLength, 1)[0];
            char Role = Encoding.ASCII.GetChars(data, 35 + NameLength + EmailLength, 1)[0];
            Crew crew = new Crew(ID, Name, Age, PhoneNumber, Email, Practice, Role.ToString());
            Database.CrewDictionary.Add(crew.ID, crew);
            return crew;
        }
    }

    public class PassengerBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            UInt16 NameLength = BitConverter.ToUInt16(data, 15);
            string Name = Encoding.ASCII.GetString(data, 17, NameLength);
            UInt16 Age = BitConverter.ToUInt16(data, 17 + NameLength);
            string PhoneNumber = Encoding.ASCII.GetString(data, 19 + NameLength, 12);
            UInt16 EmailLength = BitConverter.ToUInt16(data, 31 + NameLength);
            string Email = Encoding.ASCII.GetString(data, 33 + NameLength, EmailLength);
            char Class = Encoding.ASCII.GetChars(data, 33 + NameLength + EmailLength, 1)[0];
            UInt64 Miles = BitConverter.ToUInt64(data, 34 + NameLength + EmailLength);
            Passenger passenger = new Passenger(ID, Name, Age, PhoneNumber, Email, Class.ToString(), Miles);
            Database.PassangerDictionary.Add(passenger.ID, passenger);
            return passenger;
        }
    }

    public class CargoBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            Single Weight = BitConverter.ToSingle(data, 15);
            string Code = Encoding.ASCII.GetString(data, 19, 6);
            UInt16 DescriptionLength = BitConverter.ToUInt16(data, 25);
            string Description = Encoding.ASCII.GetString(data, 27, DescriptionLength);
            Cargo cargo = new Cargo(ID, Weight, Code, Description);
            Database.CargoDictionary.Add(cargo.ID, cargo);
            return cargo;
        }
    }

    public class CargoPlaneBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            string Serial = Encoding.ASCII.GetString(data, 15, 10);
            string ISOCountryCode = Encoding.ASCII.GetString(data, 25, 3);
            UInt16 ModelLength = BitConverter.ToUInt16(data, 28);
            string Model = Encoding.ASCII.GetString(data, 30, ModelLength);
            Single MaxLoad = BitConverter.ToSingle(data, 30 + ModelLength);
            CargoPlane cargoPlane = new CargoPlane(ID, Serial, ISOCountryCode, Model, MaxLoad);
            Database.CargoPlaneDictionary.Add(cargoPlane.ID, cargoPlane);
            return cargoPlane;
        }
    }

    public class PassengerPlaneBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            string Serial = Encoding.ASCII.GetString(data, 15, 10);
            string ISOCountryCode = Encoding.ASCII.GetString(data, 25, 3);
            UInt16 ModelLength = BitConverter.ToUInt16(data, 28);
            string Model = Encoding.ASCII.GetString(data, 30, ModelLength);
            UInt16 FirstClassSize = BitConverter.ToUInt16(data, 30 + ModelLength);
            UInt16 BusinessClassSize = BitConverter.ToUInt16(data, 32 + ModelLength);
            UInt16 EconomyClassSize = BitConverter.ToUInt16(data, 34 + ModelLength);
            PassengerPlane passengerPlane = new PassengerPlane(ID, Serial, ISOCountryCode, Model, FirstClassSize,
                BusinessClassSize, EconomyClassSize);
            Database.PassengerPlaneDictionary.Add(passengerPlane.ID, passengerPlane);
            return passengerPlane;
        }
    }

    public class AirportBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            UInt16 NameLength = BitConverter.ToUInt16(data, 15);
            string Name = Encoding.ASCII.GetString(data, 17, NameLength);
            string Code = Encoding.ASCII.GetString(data, 17 + NameLength, 3);
            Single Longitude = BitConverter.ToSingle(data, 20 + NameLength);
            Single Latitude = BitConverter.ToSingle(data, 24 + NameLength);
            Single AMSL = BitConverter.ToSingle(data, 28 + NameLength);
            string ISOCountryCode = Encoding.ASCII.GetString(data, 32 + NameLength, 3);
            Airport airport = new Airport(ID, Name, Code, Longitude, Latitude, AMSL, ISOCountryCode);
            Database.AirportDictionary.Add(airport.ID, airport);
            return airport;
        }
    }

    public class FlightBitCreator : BitCreator
    {
        public override IFlightData Create(byte[] data)
        {
            UInt32 MessageLenght = BitConverter.ToUInt32(data, 3);
            UInt64 ID = BitConverter.ToUInt64(data, 7);
            UInt64 OriginAsID = BitConverter.ToUInt64(data, 15);
            UInt64 TargetAsID = BitConverter.ToUInt64(data, 23);
            Int64 TakeoffTimeMs = BitConverter.ToInt64(data, 31);
            Int64 LandingTimeMs = BitConverter.ToInt64(data, 39);
            UInt64 PlaneID = BitConverter.ToUInt64(data, 47);
            UInt16 CrewCount = BitConverter.ToUInt16(data, 55);
            UInt64[] CrewList = new UInt64[CrewCount];
            int crewOffset = 57;
            for (int i = 0; i < CrewCount; i++)
            {
                CrewList[i] = BitConverter.ToUInt64(data, crewOffset + i * 8);
            }

            UInt16 PassengerCargoCount = BitConverter.ToUInt16(data, crewOffset + CrewCount * 8);
            UInt64[] PassengerCargoList = new UInt64[PassengerCargoCount];
            int passengerCargoOffset = crewOffset + CrewCount * 8 + 2;
            for (int i = 0; i < PassengerCargoCount; i++)
            {
                PassengerCargoList[i] = BitConverter.ToUInt64(data, passengerCargoOffset + i * 8);
            }

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime TakeoffDateTime = epoch.AddMilliseconds(TakeoffTimeMs);
            DateTime LandingDateTime = epoch.AddMilliseconds(LandingTimeMs);
            string TakeoffTime = TakeoffDateTime.ToString("HH:mm");
            string LandingTime = LandingDateTime.ToString("HH:mm");
            Single Longitude = float.MaxValue;
            Single Latitude = float.MaxValue;
            Single AMSL = float.MaxValue;
            Flight flight = new Flight(ID, OriginAsID, TargetAsID, TakeoffTime, LandingTime, Longitude, Latitude, AMSL,
                PlaneID, CrewList, PassengerCargoList);
            Database.FlightDictionary.Add(flight.ID, flight);
            return flight;
        }
    }

    public class BitParseMethods
    {
        public Dictionary<string, BitCreator> Methods { get; }

        public BitParseMethods()
        {
            Methods = new Dictionary<string, BitCreator>();
            Methods.Add("NCR", new CrewBitCreator());
            Methods.Add("NPA", new PassengerBitCreator());
            Methods.Add("NCA", new CargoBitCreator());
            Methods.Add("NCP", new CargoPlaneBitCreator());
            Methods.Add("NPP", new PassengerPlaneBitCreator());
            Methods.Add("NAI", new AirportBitCreator());
            Methods.Add("NFL", new FlightBitCreator());
        }
    }
}