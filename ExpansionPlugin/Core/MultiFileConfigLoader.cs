using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{

    public abstract class MultiFileConfigLoader<TItem> : IConfigLoader where TItem : IDeepCloneable<TItem>, IEquatable<TItem>
    {
        protected readonly string BasePath;

        public string FilePath => BasePath;
        public string FileName => Path.GetFileName(BasePath);

        public List<TItem> Items { get; protected set; } = new();
        public List<TItem> ClonedItems { get; protected set; } = new();

        public bool HasErrors { get; protected set; }
        public List<string> Errors { get; protected set; } = new();

        protected MultiFileConfigLoader(string path)
        {
            BasePath = path;
        }

        public virtual void Load()
        {
            HasErrors = false;
            Errors.Clear();
            Items.Clear();
            ClonedItems.Clear();

            var filePaths = Directory.GetFiles(BasePath, "*.json");

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);

                    OnAfterItemLoad(item, file);

                    Items.Add(item);
                    ClonedItems.Add(item.Clone());
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

            for (int i = Items.Count - 1; i >= 0; i--)
            {
                var item = Items[i];
                var cloned = ClonedItems[i];

                if (!item.Equals(cloned))
                {
                    SaveItem(item);
                    ClonedItems[i] = item.Clone();
                    saved.Add(GetItemFileName(item));
                }

                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    Items.RemoveAt(i);
                    ClonedItems.RemoveAt(i);
                }
            }

            return saved;
        }
        public bool needToSave()
        {
            return false;
        }

        // --- abstract hooks the subclass MUST implement ---

        protected abstract TItem LoadItem(string filePath);
        protected abstract void SaveItem(TItem item);
        protected abstract string GetItemFileName(TItem item);
        protected abstract bool ShouldDelete(TItem item);
        protected abstract void DeleteItemFile(TItem item);

        // --- optional hooks the subclass MAY override ---

        protected virtual void OnAfterItemLoad(TItem item, string path) { }
        protected virtual void OnAfterLoadAll() { }

        protected virtual void HandleItemError(string path, Exception ex)
        {
            Errors.Add($"Error in {Path.GetFileName(path)}: {ex.Message}");
            Console.WriteLine($"[ERROR] {Path.GetFileName(path)}: {ex.Message}");
        }
    }
}
