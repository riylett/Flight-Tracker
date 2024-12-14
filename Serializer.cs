using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public abstract class Serializer
    {
        public abstract void SerializetoFile(IEnumerable<IFlightData> objects, string path);
    }

    public class JsonObjectSerializer : Serializer
    {
        public override void SerializetoFile(IEnumerable<IFlightData> objects, string path)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Converters = { new IFlightDataConverter() }
            };
            File.WriteAllText(path, JsonSerializer.Serialize(objects, options));
            Console.WriteLine($"Serialized to {path}.");
        }
    }

    public class IFlightDataConverter : JsonConverter<IFlightData>
    {
        public override IFlightData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IFlightData value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}