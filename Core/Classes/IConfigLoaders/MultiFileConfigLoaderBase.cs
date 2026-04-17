using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2eEditor
{
    public abstract class MultiFileConfigLoaderBase<TItem> : IMultiFileConfigLoader<TItem> where TItem : IDeepCloneable<TItem>, IEquatable<TItem>
    {
        protected readonly List<string> _errors = new();
        protected readonly Dictionary<Guid, TItem> _clonedItems = new();

        protected string BasePath;

        public string FilePath => BasePath;
        public string FileName => Path.GetFileName(BasePath);

        public List<TItem> MutableItems { get; protected set; } = new();
        public IReadOnlyList<TItem> Items => MutableItems;

        public bool HasErrors { get; protected set; }
        public IReadOnlyList<string> Errors => _errors;

        protected MultiFileConfigLoaderBase(string path)
        {
            BasePath = path;
        }

        public virtual void Load()
        {
            ResetState();

            if (!Directory.Exists(BasePath))
                return;

            var filePaths = Directory.GetFiles(BasePath, "*.json");

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);

                    OnAfterItemLoad(item, file);

                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine($"Validation issues in {Path.GetFileName(file)}:");
                        foreach (var msg in issues)
                        {
                            Console.WriteLine("- " + msg);
                        }
                    }

                    MutableItems.Add(item);
                    _clonedItems[GetID(item)] = CloneItem(item);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(file, ex);
                }
            }

            OnAfterLoadAll();
        }

        public virtual IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = MutableItems.Count - 1; i >= 0; i--)
            {
                var item = MutableItems[i];
                var id = GetID(item);
                var fileName = GetItemFileName(item);

                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    MutableItems.RemoveAt(i);
                    _clonedItems.Remove(id);
                    saved.Add("File Remove " + fileName);
                    continue;
                }

                if (!_clonedItems.TryGetValue(id, out var baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = CloneItem(item);
                    saved.Add(fileName);
                    continue;
                }

                if (!AreEqual(item, baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = CloneItem(item);
                    saved.Add(fileName);
                }
            }

            return saved;
        }

        public virtual bool NeedToSave()
        {
            foreach (var item in MutableItems)
            {
                var id = GetID(item);

                if (ShouldDelete(item))
                    return true;

                if (!_clonedItems.TryGetValue(id, out var baseline))
                    return true;

                if (!AreEqual(item, baseline))
                    return true;
            }

            return false;
        }

        protected virtual void ResetState()
        {
            HasErrors = false;
            _errors.Clear();
            MutableItems.Clear();
            _clonedItems.Clear();
        }

        protected abstract TItem LoadItem(string filePath);
        protected abstract void SaveItem(TItem item);
        protected abstract string GetItemFileName(TItem item);
        protected abstract Guid GetID(TItem item);
        protected abstract bool ShouldDelete(TItem item);
        protected abstract void DeleteItemFile(TItem item);

        protected virtual void OnAfterItemLoad(TItem item, string path) { }
        protected virtual void OnAfterLoadAll() { }

        protected virtual void HandleItemError(string path, Exception ex)
        {
            var msg = $"Error in {Path.GetFileName(path)}: {ex.Message}";
            _errors.Add(msg);
            Console.WriteLine(msg);
        }
        protected virtual IEnumerable<string> ValidateItem(TItem item) => Enumerable.Empty<string>();
        protected virtual bool AreEqual(TItem a, TItem b)
            => a.Equals(b);

        protected virtual TItem CloneItem(TItem item)
            => item.Clone();
    }
}