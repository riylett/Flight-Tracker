using Mapsui.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public interface IFlightConverter
    {
        FlightGUI ConvertFlightToGUI(Flight flight, Dictionary<UInt64, Airport> airportDictionary);
    }

    public class FlightConverterAdapter : IFlightConverter
    {
        public FlightGUI ConvertFlightToGUI(Flight flight, Dictionary<UInt64, Airport> airportDictionary)
        {
            Airport origin = airportDictionary[flight.OriginAsID];
            Airport target = airportDictionary[flight.TargetAsID];
            UpdateFlightLocation(flight, origin, target);
            FlightGUI flightGUI = new FlightGUI()
            {
                ID = flight.ID,
                WorldPosition = new WorldPosition(flight.Latitude, flight.Longitude),
                MapCoordRotation = RotationCalculate(flight, origin, target),
            };
            return flightGUI;
        }

        public void UpdateFlightLocation(Flight flight, Airport depart, Airport target)
        {
            DateTime departureTime = DateTime.Parse(flight.TakeoffTime);
            DateTime arrivalTime = DateTime.Parse(flight.LandingTime);
            if (arrivalTime < departureTime)
            {
                arrivalTime = arrivalTime.AddDays(1);
            }

            DateTime currentTime = DateTime.Now;
            double remainingTime = (arrivalTime - currentTime).TotalSeconds;

            if (currentTime >= arrivalTime || currentTime < departureTime)
            {
                flight.Longitude = float.MaxValue;
                flight.Latitude = float.MaxValue;
                return;
            }

            double currentPositionLong = flight.Longitude;
            double currentPositionLat = flight.Latitude;
            double longitudeTarget = target.Longitude;
            double latitudeTarget = target.Latitude;

            double interpolatedLatitude = currentPositionLat +
                                          (latitudeTarget - currentPositionLat) / remainingTime;
            double interpolatedLongitude = currentPositionLong +
                                           (longitudeTarget - currentPositionLong) / remainingTime;

            flight.Longitude = (float)interpolatedLongitude;
            flight.Latitude = (float)interpolatedLatitude;
        }

        public double RotationCalculate(Flight flight, Airport depart, Airport target)
        {
            (double x1, double y1) = SphericalMercator.FromLonLat(flight.Longitude, flight.Latitude);
            (double x2, double y2) = SphericalMercator.FromLonLat(target.Longitude, target.Latitude);

            double deltaX = x2 - x1;
            double deltaY = y2 - y1;

            double angleRadians = Math.Atan2(deltaY, deltaX);

            double mapCoordRotation = Math.PI / 2 - angleRadians;

            return mapCoordRotation;
        }
    }
}