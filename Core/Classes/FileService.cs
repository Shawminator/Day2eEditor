using Day2eEditor;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

public class FileService
{
    public class BoolConverter : JsonConverter<bool>
    {
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
            writer.WriteNumberValue(value ? 1 : 0);

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    return reader.TryGetInt64(out long l) ? Convert.ToBoolean(l) : reader.TryGetDouble(out double d) ? Convert.ToBoolean(d) : false;
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
            }
            return false;
        }
    }
    public class Vec3Converter : JsonConverter<Vec3>
    {
        public override Vec3 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Expecting: [ float, float, float ]
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected start of array for Vec3");

            reader.Read();
            float x = reader.GetSingle();
            reader.Read();
            float y = reader.GetSingle();
            reader.Read();
            float z = reader.GetSingle();
            reader.Read(); // EndArray

            return new Vec3(x, y, z);
        }

        public override void Write(Utf8JsonWriter writer, Vec3 value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.X);
            writer.WriteNumberValue(value.Y);
            writer.WriteNumberValue(value.Z);
            writer.WriteEndArray();
        }
    }

    private readonly JsonSerializerOptions _defaultOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    private JsonSerializerOptions BuildOptions(bool useBool, bool useVec)
    {
        var options = new JsonSerializerOptions(_defaultOptions);

        if (useBool) options.Converters.Add(new BoolConverter());
        if (useVec) options.Converters.Add(new Vec3Converter());

        return options;
    }
    public T LoadOrCreateJson<T>(
        string path,
        Func<T> createNew,
        Action<Exception>? onError = null,
        string configName = "Configuration",
        bool useBoolConvertor = false,
        bool useVecConvertor = false
    )
    {
        if (File.Exists(path))
        {
            Console.Write($"[Load] Loading {configName} ({Path.GetFileName(path)})... ");
            try
            {
                var options = BuildOptions(useBoolConvertor, useVecConvertor);
                T? data = JsonSerializer.Deserialize<T>(File.ReadAllText(path), options);

                if (data == null)
                    throw new Exception("Deserialized object is null.");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
                Console.ForegroundColor = ConsoleColor.Gray;

                return data;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed");
                Console.ForegroundColor = ConsoleColor.Gray;

                onError?.Invoke(ex);

                return createNew();
            }
        }
        else
        {
            Console.WriteLine($"{Path.GetFileName(path)} not found. Creating new {configName}...");
            var newData = createNew();
            SaveJson(path, newData, useBoolConvertor, useVecConvertor);
            return newData;
        }
    }

    public void SaveJson<T>(string path, T data, bool useBoolConvertor = false,  bool useVecConvertor = false)
    {
        var options = BuildOptions(useBoolConvertor, useVecConvertor);
        try
        {
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SaveJson] Failed to write JSON: {ex.Message}");
        }
    }

    public T LoadOrCreateXml<T>(
        string path,
        Func<T> createNew,
        Action<T>? onAfterLoad = null,
        Action<Exception>? onError = null,
        string configName = "Configuration"
    )
    {
        if (File.Exists(path))
        {
            Console.Write($"[Load] Loading {configName} ({Path.GetFileName(path)})... ");
            try
            {
                using var stream = File.OpenRead(path);
                var serializer = new XmlSerializer(typeof(T));
                var data = (T)serializer.Deserialize(stream)!;

                onAfterLoad?.Invoke(data);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
                Console.ForegroundColor = ConsoleColor.Gray;

                return data;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed");
                Console.ForegroundColor = ConsoleColor.Gray;

                onError?.Invoke(ex);

                return createNew();
            }
        }
        else
        {
            Console.WriteLine($"{Path.GetFileName(path)} not found. Creating new {configName}...");
            var newData = createNew();
            SaveXml(path, newData);
            return newData;
        }
    }
    public void SaveXml<T>(string path, T data)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", ""); // no prefix, no namespace

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = false,
                OmitXmlDeclaration = false
            };

            using var writer = XmlWriter.Create(path, settings);
            serializer.Serialize(writer, data, ns);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SaveXml] Failed to write XML: {ex.Message}");
        }
    }
}
