using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWeatherPlugin
{
    public interface IDeepCloneable<T>
    {
        T Clone();
    }
    public abstract class DynamicWeatherBaseIConfigLoader<T> : IConfigLoader where T : IDeepCloneable<T>, IEquatable<T>
    {
        protected readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public T Data { get; protected set; }
        public T ClonedData { get; protected set; }
        public bool HasErrors { get; protected set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public DynamicWeatherBaseIConfigLoader(string path)
        {
            _path = path;
        }
        public virtual void Load()
        {
            try
            {
                var DynamicList = AppServices.GetRequired<FileService>().LoadOrCreateJson<BindingList<WeatherDynamic>>(
                        _path,
                        createNew: () =>new BindingList<WeatherDynamic>(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: FileName
                    );
                DynamicWeatherSettings dws = new DynamicWeatherSettings()
                { 
                    m_Dynamics = DynamicList
                };

                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }

        }
        protected abstract T CreateDefaultData();
        protected virtual void OnAfterLoad(T loadedData) { }
        protected virtual IEnumerable<string> ValidateData() => Enumerable.Empty<string>();
        protected virtual void HandleLoadError(Exception ex)
        {
            HasErrors = true;
            var msg = $"Error in {FileName}: {ex.Message}\n{ex.InnerException?.Message}";
            Errors.Add(msg);
            Console.WriteLine(msg);
        }
        public virtual IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || isDirty == true)
            {
                isDirty = false;
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected virtual bool AreEqual(T a, T b)
            => EqualityComparer<T>.Default.Equals(a, b);
        public bool needToSave()
        {
            return isDirty;
        }
        protected virtual T CloneData(T source) => source.Clone();

    }
}

