using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public interface IConfigLoader
    {
        public string FileName { get; }
        public string FilePath { get; }

        bool HasErrors { get; }
        public List<string> Errors { get; }

        void Load();
        IEnumerable<string> Save();
        bool needToSave();

    }
    public interface IAdvancedConfigLoader : IConfigLoader
    {
        void Load();
        void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths);
        IEnumerable<string> Save();
        public bool needToSave()
        {
            return false;
        }
    }
}
