using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2eEditor
{
    public abstract class SingleFileConfigLoaderBase<T> : IConfigLoader<T> where T : IDeepCloneable<T>, IEquatable<T>
    {
        protected readonly string _path;
        protected readonly List<string> _errors = new();

        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;

        public T Data { get; protected set; } = default!;
        protected T? ClonedData { get; set; }

        public bool HasErrors { get; protected set; }
        public IReadOnlyList<string> Errors => _errors;

        public bool IsDirty { get; protected set; }

        protected SingleFileConfigLoaderBase(string path)
        {
            _path = path;
        }

        public virtual void Load()
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
                        useBoolConvertor: true
                    );

                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine($"Validation issues in {FileName}:");
                    foreach (var msg in issues)
                    {
                        Console.WriteLine("- " + msg);
                    }

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

        public virtual IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!NeedToSave())
                return Array.Empty<string>();

            AppServices.GetRequired<FileService>().SaveJson(_path, Data);
            ClonedData = CloneData(Data);
            IsDirty = false;

            return new[] { _path };
        }

        public virtual bool NeedToSave()
        {
            if (Data is null)
                return false;

            if (IsDirty)
                return true;

            if (ClonedData is null)
                return true;

            return !AreEqual(Data, ClonedData);
        }

        protected abstract T CreateDefaultData();

        protected virtual void OnAfterLoad(T loadedData) { }

        protected virtual IEnumerable<string> ValidateData() => Enumerable.Empty<string>();

        protected virtual void HandleLoadError(Exception ex)
        {
            HasErrors = true;

            var msg = ex.InnerException is null
                ? $"Error in {FileName}: {ex.Message}"
                : $"Error in {FileName}: {ex.Message}\n{ex.InnerException.Message}";

            _errors.Add(msg);
            Console.WriteLine(msg);
        }

        protected virtual bool AreEqual(T a, T b)
            => EqualityComparer<T>.Default.Equals(a, b);

        protected virtual T CloneData(T source)
            => source.Clone();

        protected void MarkDirty()
        {
            IsDirty = true;
        }
        protected void ClearDirty()
        {
            IsDirty = false;
        }
    }
}