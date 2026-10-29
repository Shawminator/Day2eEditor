using Day2eEditor;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FileService;

namespace CaparePlugin
{
    /// <summary>
    /// Single file loader for the Capare json configs.
    /// DayZ writes bools as 0/1 and vectors as [x, y, z], so both converters are used for loading and saving.
    /// </summary>
    public abstract class CapareConfigBase<T> : SingleFileConfigLoaderBase<T> where T : class, IDeepCloneable<T>, IEquatable<T>
    {
        protected CapareConfigBase(string path) : base(path)
        {
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();
            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateJson(
                        _path,
                        createNew: CreateDefaultData,
                        onError: HandleLoadError,
                        configName: FileName,
                        useBoolConvertor: true,
                        useVecConvertor: true
                    );

                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine($"Validation issues in {FileName}:");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    IsDirty = true;
                }

                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }
        }

        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!NeedToSave())
                return Array.Empty<string>();

            OnBeforeSave(Data);
            AppServices.GetRequired<FileService>().SaveJson(_path, Data, true, true);
            ClonedData = CloneData(Data);
            ClearDirty();

            return new[] { _path };
        }

        protected virtual void OnBeforeSave(T data) { }
    }

    /// <summary>
    /// Clone and compare helpers for the Capare data classes.
    /// Everything is compared/cloned through the same json the files are written with,
    /// so a value that would change the saved file is always picked up as a change.
    /// </summary>
    public static class CapareJson
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new BoolConverter(), new Vec3Converter() }
        };

        public static T Clone<T>(T source)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source, _options), _options)!;
        }

        public static bool AreEqual<T>(T a, T b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return JsonSerializer.Serialize(a, _options) == JsonSerializer.Serialize(b, _options);
        }
    }
}
