using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public abstract class NewsProvider
    {
        public string Name { get; set; }
        public abstract string ReportAirport(Airport airport);
        public abstract string ReportCargoPlane(CargoPlane cargoPlane);
        public abstract string ReportPassangerPlane(PassengerPlane passengerPlane);
    }

    public class Television : NewsProvider
    {
        public string Name { get; set; }

        public Television(string name)
        {
            Name = name;
        }

        public override string ReportAirport(Airport airport)
        {
            return $"<An image of {airport.Name} airport>";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return $"<An image of {cargoPlane.Serial} cargo plane>";
        }

        public override string ReportPassangerPlane(PassengerPlane passengerPlane)
        {
            return $"<An image of {passengerPlane.Serial} passanger plane>";
        }
    }

    public class Radio : NewsProvider
    {
        public string Name { get; set; }

        public Radio(string name)
        {
            Name = name;
        }

        public override string ReportAirport(Airport airport)
        {
            return $"Reporting for {Name}, Ladies and Gentlemen, we are at the {airport.Name} airport.";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return
                $"Reporting for {Name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        public override string ReportPassangerPlane(PassengerPlane passengerPlane)
        {
            return
                $"Reporting for {Name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.Serial} take off.";
        }
    }

    public class Newspaper : NewsProvider
    {
        public string Name { get; set; }

        public Newspaper(string name)
        {
            Name = name;
        }

        public override string ReportAirport(Airport airport)
        {
            return $"{Name} - A report from the {airport.Name} airport, {airport.Country}.";
        }

        public override string ReportCargoPlane(CargoPlane cargoPlane)
        {
            return $"{Name} - An interview with the crew of {cargoPlane.Serial}.";
        }

        public override string ReportPassangerPlane(PassengerPlane passengerPlane)
        {
            return
                $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
        }
    }
}