using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class PlayerRestrictedFilesConfig
    {
        public string _basepath { get; set; }
        public List<PlayerRestrictedFiles> AllData { get; private set; } = new List<PlayerRestrictedFiles>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load(string basePath, BindingList<string> PresetPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();

            foreach (string filename in PresetPaths)
            {
                var fullPath = Path.Combine(_basepath, filename.Replace('/', Path.DirectorySeparatorChar));
                var relativepath = Path.GetRelativePath(_basepath, fullPath).Replace(Path.DirectorySeparatorChar, '/');

                var preset = AppServices.GetRequired<FileService>().LoadOrCreateJson<PlayerRestrictedFiles>(
                     fullPath,
                     createNew: () => new PlayerRestrictedFiles(),
                     configName: "RestrictedArea",
                     useBoolConvertor: false
                 );
                preset.setpath(fullPath);
                preset.ModFolder = Path.GetRelativePath(_basepath, Path.GetDirectoryName(fullPath));
                AllData.Add(preset);
            }
        }
        public IEnumerable<string> Save()
        {
            var savedFiles = new List<string>();

            foreach (var data in AllData.ToList())
            {
                var result = data.Save();
                savedFiles.AddRange(result);

                if (data.ToDelete)
                {
                    AllData.Remove(data); // cleanup after deleting
                }
            }

            return savedFiles;
        }
    }
    public class PlayerRestrictedFiles
    {
        [JsonIgnore]
        private string _path;
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        [JsonIgnore]
        public string FilePath => _path;
        [JsonIgnore]
        public bool isDirty { get; set; }
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public string ModFolder { get; set; }

        public string areaName { get; set; }
        public BindingList<BindingList<BindingList<double>>> PRABoxes { get; set; }
        public BindingList<BindingList<double>> SafePositions3D { get; set; }

        public void setpath(string path)
        {
            _path = path;
        }
        internal IEnumerable<string> Save()
        {
            if (ToDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    // Delete empty directories if needed
                    ShellHelper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
                    return new[] { FileName + " (deleted)" };
                }
                return Array.Empty<string>();
            }

            else if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, this);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }
    }
}
