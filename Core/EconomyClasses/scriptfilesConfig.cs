using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core
{
    public class scriptfilesConfig : MultiFileConfigLoaderBase<EnfusionScriptfile>
    {
        public readonly string _weaponInclude;
        public readonly string _xyzInclude;
        public readonly string WeaponAttachDump = $"\t//<AUTO_DUMP_ATTACH_START>\r\n\tDumpAttach();\r\n\t//<AUTO_DUMP_ATTACH_END>";
        public readonly string GetXYZ = $"\t//<AUTO_GET_XYZ_MAP_START>\r\n\tGetXYZMap();\r\n\t//<AUTO_GET_XYZ_MAP_END>";
        public scriptfilesConfig(string basePath) : base(basePath)
        {
            var missionPath = AppServices.GetRequired<ProjectManager>().CurrentProject.MpMissionPath;

            _weaponInclude = $"#include \"$CurrentDir:mpmissions\\\\{missionPath}\\\\WeaponAttchmentDump.c\"";
            _xyzInclude = $"#include \"$CurrentDir:mpmissions\\\\{missionPath}\\\\XYZMapper.c\"";
        }
        public override void Load()
        {
            ResetState();

            var filePaths = Directory.GetFiles(BasePath, "*.c", SearchOption.AllDirectories);

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);
                    item.HasWeaponAttachInclude = false;
                    item.HasXYZInclude = false;

                    if (Path.GetFileName(file).Equals("INIT.c", StringComparison.OrdinalIgnoreCase))
                    {
                        item.HasWeaponAttachInclude = item.Data.Contains(_weaponInclude);
                        item.HasXYZInclude = item.Data.Contains(_xyzInclude);
                    }
                    OnAfterItemLoad(item, file);
                    _clonedItems.Add(GetID(item), item.Clone());
                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }
                    MutableItems.Add(item);

                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(file, ex);
                }
            }

            OnAfterLoadAll();
        }
        protected override EnfusionScriptfile LoadItem(string filePath)
        {
            var scriptFile = new EnfusionScriptfile()
            {
                Data = File.ReadAllText(filePath)
            };
            scriptFile.SetPath(filePath);
            scriptFile.SetGuid(Guid.NewGuid());
            return scriptFile;
        }
        public override IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = Items.Count - 1; i >= 0; i--)
            {
                var item = Items[i];
                var id = GetID(item);
                var fileName = GetItemFileName(item);
                var fullfielName = item.FilePath;
                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    MutableItems.RemoveAt(i);
                    _clonedItems.Remove(id);
                    saved.Add("File Remove " + fullfielName);
                    continue;
                }
                //new file, needs to be written to disk and cloned
                if (!_clonedItems.TryGetValue(id, out var baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                    continue;
                }
                //edit to existing file, needs to be recloned
                if (!item.Equals(baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                    continue;
                }
                //check external file for edits
                item.Data = File.ReadAllText(item._path);
                if (!item.Equals(baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add($"External edit;{fullfielName}");
                }
            }
            return saved;
        }
        protected override void SaveItem(EnfusionScriptfile item)
        {
            File.WriteAllText(item._path, item.Data);
        }
        protected override string GetItemFileName(EnfusionScriptfile EnfusionScriptfile)
            => EnfusionScriptfile.FileName;
        protected override string GetItemFilePath(EnfusionScriptfile EnfusionScriptfile)
            => EnfusionScriptfile.FilePath;
        protected override bool ShouldDelete(EnfusionScriptfile EnfusionScriptfile)
            => EnfusionScriptfile.ToDelete;
        protected override Guid GetID(EnfusionScriptfile EnfusionScriptfile)
            => EnfusionScriptfile.Id;
        protected override void DeleteItemFile(EnfusionScriptfile EnfusionScriptfile)
        {
            if (!string.IsNullOrWhiteSpace(EnfusionScriptfile._path) && File.Exists(EnfusionScriptfile._path))
            {
                File.Delete(EnfusionScriptfile._path);
            }
        }

        public EnfusionScriptfile addnewScript(string filename, string data)
        {
            EnfusionScriptfile newEnfusionScriptfile = new EnfusionScriptfile()
            {
                Data = data
            };
            newEnfusionScriptfile.SetPath(filename);
            newEnfusionScriptfile.SetGuid(Guid.NewGuid());
            MutableItems.Add(newEnfusionScriptfile);
            return newEnfusionScriptfile;
        }
        public void removeScript(EnfusionScriptfile EnfusionScriptfile)
        {
            EnfusionScriptfile.ToDelete = true;
        }
    }
    public class EnfusionScriptfile : IDeepCloneable<EnfusionScriptfile>, IEquatable<EnfusionScriptfile>
    {
        public string _path { get; private set; }
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public Guid Id { get; set; }
        public bool IsDirty { get; set; }
        public bool ToDelete { get; set; }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;

        public bool HasWeaponAttachInclude { get; set; }
        public bool HasXYZInclude { get; set; }

        public string Data { get; set;  }

        public EnfusionScriptfile Clone()
        {
            EnfusionScriptfile clone = new EnfusionScriptfile()
            {
                Data = Data,
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        public override bool Equals(object? obj) => Equals(obj as EnfusionScriptfile);
        public bool Equals(EnfusionScriptfile? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id != other.Id) return false;

            return _path == other._path
                && Data == other.Data;
        }
    }
}
