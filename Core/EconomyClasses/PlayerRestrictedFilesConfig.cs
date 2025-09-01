using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
                preset.convertpositionstolist();
                AllData.Add(preset);
            }
        }
        public IEnumerable<string> Save()
        {
            var savedFiles = new List<string>();

            foreach (var data in AllData.ToList())
            {
                data.convertlisttopositions();
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
        [JsonIgnore]
        public BindingList<PRABoxes> _PRABoxes { get; set; }
        [JsonIgnore]
        public BindingList<PRASafePosition> _SafePositions3D { get; set; }


        public string areaName { get; set; }
        public BindingList<BindingList<BindingList<decimal>>> PRABoxes { get; set; }
        public BindingList<BindingList<decimal>> SafePositions3D { get; set; }

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
        public void convertpositionstolist()
        {
            if(PRABoxes != null)
            {
                _PRABoxes = new BindingList<PRABoxes>();
                for (int i = 0; i < PRABoxes.Count; i++)
                {
                    _PRABoxes.Add(new PRABoxes()
                    {
                        HalfExtents = new Vector3(Convert.ToSingle(PRABoxes[i][0][0]), Convert.ToSingle(PRABoxes[i][0][1]), Convert.ToSingle(PRABoxes[i][0][2])),
                        Orientation = new Vector3(Convert.ToSingle(PRABoxes[i][1][0]), Convert.ToSingle(PRABoxes[i][1][1]), Convert.ToSingle(PRABoxes[i][1][2])),
                        Position = new Vector3(Convert.ToSingle(PRABoxes[i][2][0]), Convert.ToSingle(PRABoxes[i][2][1]), Convert.ToSingle(PRABoxes[i][2][2]))
                    }
                    );
                }
                PRABoxes = null;

            }
            if (SafePositions3D != null)
            {
                _SafePositions3D = new BindingList<PRASafePosition>();
                for (int i = 0; i < SafePositions3D.Count; i++)
                {
                    _SafePositions3D.Add(new PRASafePosition()
                    {
                        X = SafePositions3D[i][0],
                        Y = SafePositions3D[i][1],
                        Z = SafePositions3D[i][2],
                        Name = SafePositions3D[i][0].ToString("0.##") + "," + SafePositions3D[i][1].ToString("0.##") + "," + SafePositions3D[i][2].ToString("0.##")
                    }
                    );
                }
                SafePositions3D = null;
            }
        }
        public void convertlisttopositions()
        {
            if (_SafePositions3D != null)
            {
                SafePositions3D = new BindingList<BindingList<decimal>>();
                foreach (PRASafePosition pos in _SafePositions3D)
                {
                    SafePositions3D.Add(new BindingList<decimal> { pos.X, pos.Y, pos.Z });
                }
            }
            else
            {
                SafePositions3D = null;
            }
        }
    }
    public class PRABoxes
    {
        public Vector3 HalfExtents { get;set; }
        public Vector3 Orientation { get; set; }
        public Vector3 Position { get; set; }
    }
    public class PRASafePosition
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
