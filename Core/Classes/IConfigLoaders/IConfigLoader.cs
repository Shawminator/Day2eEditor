using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public interface IConfigLoader
    {
        string FileName { get; }
        string FilePath { get; }
        bool HasErrors { get; }
        IReadOnlyList<string> Errors { get; }
        void Load();
        IEnumerable<string> Save();
        bool NeedToSave();

    }
    public interface IConfigLoader<T> : IConfigLoader
    {
        T Data { get; }
    }
    public interface IMultiFileConfigLoader<TItem> : IConfigLoader
    {
        IReadOnlyList<TItem> Items { get; }
    }
    public interface IParameterizedConfigLoader : IConfigLoader
    {
        void Load(string basePath, string vanillaPath, List<string> modPaths);
    }
    public interface IParameterizedMultiFileConfigLoader<TItem> : IMultiFileConfigLoader<TItem>, IParameterizedConfigLoader
    {
    }
}
