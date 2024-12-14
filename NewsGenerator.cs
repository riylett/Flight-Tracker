using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public class NewsGenerator
    {
        List<NewsProvider> providers;
        List<IReportable> objects;

        public NewsGenerator(List<NewsProvider> newsProviders, List<IReportable> reportables)
        {
            providers = newsProviders;
            objects = reportables;
        }

        public IEnumerable<string> GenerateNextNews()
        {
            foreach (var provider in providers)
            {
                foreach (var reportable in objects)
                {
                    yield return reportable.GetReport(provider);
                }
            }
        }

        public void GenerateAllNews()
        {
            foreach (var news in GenerateNextNews())
            {
                Console.WriteLine(news.ToString());
            }
        }
    }
}