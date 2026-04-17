using System.Collections.Generic;
using System.IO;

namespace Day2eEditor
{
    public abstract class ParameterizedMultiFileConfigLoaderBase<TItem>
        : MultiFileConfigLoaderBase<TItem>, IParameterizedMultiFileConfigLoader<TItem>
        where TItem : IDeepCloneable<TItem>, IEquatable<TItem>
    {
        protected string VanillaPath = string.Empty;
        protected List<string> ModPaths = new();

        protected ParameterizedMultiFileConfigLoaderBase(string basePath) : base(basePath)
        {
        }

        public virtual void Load(string basePath, string vanillaPath, List<string> modPaths)
        {
            BasePath = basePath;
            VanillaPath = vanillaPath;
            ModPaths = modPaths ?? new List<string>();

            ResetState();
            LoadCore();
        }

        public override void Load()
        {
            ResetState();
            LoadCore();
        }

        protected virtual void LoadCore()
        {
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
    }
}