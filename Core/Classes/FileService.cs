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
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public T LoadOrCreateJson<T>(
        string path,
        Func<T> createNew,
        Action<T>? onAfterLoad = null,
        Func<T, bool>? checkVersionAndUpdate = null,
        Action<Exception>? onError = null,
        string configName = "Configuration",
        bool useBoolConvertor = false
    )
    {
        if (File.Exists(path))
        {
            Console.Write($"[Load] Loading {configName} ({Path.GetFileName(path)})... ");
            try
            {
                var options = new JsonSerializerOptions(_jsonOptions);
                if (useBoolConvertor)
                {
                    options.Converters.Add(new BoolConverter());
                }
                T? data = JsonSerializer.Deserialize<T>(File.ReadAllText(path), options);

                if (data == null)
                    throw new Exception("Deserialized object is null.");

                if (checkVersionAndUpdate?.Invoke(data) == true)
                {
                    SaveJson(path, data);
                    Console.WriteLine($"{configName} version updated.");
                }

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
            SaveJson(path, newData);
            return newData;
        }
    }

    public void SaveJson<T>(string path, T data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data, _jsonOptions);
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
