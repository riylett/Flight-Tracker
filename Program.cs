// See https://aka.ms/new-console-template for more information

using Avalonia.Rendering;
using OOD_project_1;

namespace OOD_project1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FlightTracker tracker = new FlightTracker();
            FTRreader reader = new FTRreader();
            Server server = new Server();
            server.AddObserver(new UpdateManager());
            server.AddObserver(Logger.Instance);
            reader.Read(new FactoryMethods(), "example_data.ftr");
            Thread thread = new Thread(tracker.Start);
            thread.Start();
            Thread.Sleep(8000);
            server.StartReading("example.ftre");
            Menu menu = new Menu();
            menu.ConsoleRead();
        }
    }
}