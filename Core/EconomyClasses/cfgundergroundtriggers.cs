using System.ComponentModel;

namespace Day2eEditor
{
    public class cfgundergroundtriggersConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public cfgundergroundtriggers Data { get; private set; } = new cfgundergroundtriggers();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgundergroundtriggersConfig(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<cfgundergroundtriggers>(
                _path,
                createNew: () => new cfgundergroundtriggers(),
                onAfterLoad: _ => { },
                onError: ex => LogError("cfgundergroundtriggers", ex),
                configName: "cfgundergroundtriggers",
                useBoolConvertor: false
            );
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }

        public bool needToSave()
        {
            return isDirty;
        }
        private void LogError(string context, Exception ex)
        {
            HasErrors = true;
            string message = $"Error in {context}\n{ex.Message}";
            if (ex.InnerException != null)
                message += $"\n{ex.InnerException.Message}";

            Console.WriteLine(message);
            Errors.Add(message);
        }
    }
    public class cfgundergroundtriggers
    {
        public BindingList<Trigger> Triggers { get; set; }

        public cfgundergroundtriggers()
        {
            Triggers = new BindingList<Trigger>();
        }
    }

    public class Trigger
    {
        public decimal[] Position { get; set; }
        public decimal[] Orientation { get; set; }
        public decimal[] Size { get; set; }
        public decimal EyeAccommodation { get; set; }
        public BindingList<Breadcrumb> Breadcrumbs { get; set; }
        public decimal? InterpolationSpeed { get; set; }

        public Trigger()
        {

        }

        public string gettriggertype()
        {
            if (EyeAccommodation == 1 && Breadcrumbs.Count == 0)
            {
                if (InterpolationSpeed == null)
                    InterpolationSpeed = 1;
                return "Outer";
            }
            else if (EyeAccommodation == 0 && Breadcrumbs.Count == 0)
            {
                if (InterpolationSpeed == null)
                    InterpolationSpeed = 1;
                return "Inner";
            }
            else if (Breadcrumbs.Count == 0)
            {
                InterpolationSpeed = 1;
                return "Transition";
            }
            else
            {
                InterpolationSpeed = null;
                return "Transition";
            }
        }
    }

    public class Breadcrumb
    {
        public decimal[] Position { get; set; }
        public decimal EyeAccommodation { get; set; }
        public int UseRaycast { get; set; }
        public decimal Radius { get; set; }

        public Breadcrumb()
        {
        }
        public string getbreadcrumbtype()
        {
            switch (EyeAccommodation)
            {
                case 0:
                    return "Inner";
                case 1:
                    return "Outer";
                default:
                    return "Transition";
            }
        }
    }
}
