using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public abstract class FileReader
    {
        public abstract void Read(FactoryMethods methods, string filepath);
    }

    public class FTRreader : FileReader
    {
        public override void Read(FactoryMethods methods, string filepath)
        {
            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    var data = line.Split(",");
                    string name = data[0];
                    var data_ = data.Skip(1).ToArray();
                    IFlightData obj = methods.Methods[name].Create(data_);
                    Database.DataDictionary.Add(obj.ID, obj);
                }
            }

            Console.WriteLine($"Read {Database.DataDictionary.Count} objects from {filepath}.");
        }
    }
}