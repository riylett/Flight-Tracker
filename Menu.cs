using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public class Menu
    {
        NewsGenerator newsGenerator;

        public void AddData(List<IReportable> reportables, List<NewsProvider> providers)
        {
            reportables.AddRange(Database.PassengerPlaneDictionary.Values);
            reportables.AddRange(Database.CargoPlaneDictionary.Values);
            reportables.AddRange(Database.AirportDictionary.Values);
            providers.Add(new Television("Abelian Television"));
            providers.Add(new Television("Channel TV-Tensor"));
            providers.Add(new Radio("Quantifier radio"));
            providers.Add(new Radio("Shmem radio"));
            providers.Add(new Newspaper("Categories Journal"));
            providers.Add(new Newspaper("Polytechnical Gazette"));
        }

        public void ConsoleRead()
        {
            while (true)
            {
                string word = Console.ReadLine();

                if (word.ToLower() == "report")
                {
                    List<IReportable> reportables = new List<IReportable>();
                    List<NewsProvider> providers = new List<NewsProvider>();
                    AddData(reportables, providers);
                    newsGenerator = new NewsGenerator(providers, reportables);
                    newsGenerator.GenerateAllNews();
                }

                else if (word.ToLower() == "exit")
                {
                    Console.WriteLine("Closing the application");
                    Environment.Exit(0);
                }

                else
                {
                    Console.WriteLine(
                        "Invalid command. Use 'report' to print out a news overview and 'exit' to close the application.");
                }
            }
        }
    }
}