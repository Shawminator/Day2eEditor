using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class PlayerRestrictedFilesConfig : MultiFileConfigLoaderBase<PlayerRestrictedFile>
    {
        public PlayerRestrictedFilesConfig(string basePath) : base(basePath)
        {
        }

        public void Load(string basePath, BindingList<string> presetPaths)
        {
            BasePath = basePath;
            ResetState();

            foreach (var relativePath in presetPaths ?? new BindingList<string>())
            {
                var fullPath = Path.Combine(BasePath, relativePath.Replace('/', Path.DirectorySeparatorChar));

                try
                {
                    var item = LoadItem(fullPath);

                    OnAfterItemLoad(item, fullPath);
                    _clonedItems[GetID(item)] = item.Clone();

                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + item.FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }

                    MutableItems.Add(item);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(fullPath, ex);
                }
            }

            OnAfterLoadAll();
        }

        protected override PlayerRestrictedFile LoadItem(string filePath)
        {
            var data = AppServices.GetRequired<FileService>().LoadOrCreateJson<PlayerRestrictedData>(
                filePath,
                createNew: () => new PlayerRestrictedData(),
                configName: "RestrictedArea",
                useBoolConvertor: false
            );

            var file = new PlayerRestrictedFile(filePath)
            {
                Data = data,
                ModFolder = Path.GetRelativePath(BasePath, Path.GetDirectoryName(filePath) ?? BasePath)
            };

            file.ConvertPositionsToList();
            return file;
        }
        protected override void SaveItem(PlayerRestrictedFile item)
        {
            item.ConvertListToPositions();
            AppServices.GetRequired<FileService>().SaveJson(item.FilePath, item.Data);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(PlayerRestrictedFile item) => item.FileName;
        protected override string GetItemFilePath(PlayerRestrictedFile PlayerRestrictedFile)
            => PlayerRestrictedFile.FilePath;
        protected override Guid GetID(PlayerRestrictedFile item) => item.Id;

        protected override bool ShouldDelete(PlayerRestrictedFile item) => item.ToDelete;

        protected override void DeleteItemFile(PlayerRestrictedFile item)
        {
            if (!string.IsNullOrWhiteSpace(item.FilePath) && File.Exists(item.FilePath))
            {
                File.Delete(item.FilePath);
                Helper.DeleteEmptyFoldersUpToBase(
                    Path.GetDirectoryName(item.FilePath),
                    AppServices.GetRequired<EconomyManager>().basePath);
            }
        }
    }

    public class PlayerRestrictedFile : IDeepCloneable<PlayerRestrictedFile>, IEquatable<PlayerRestrictedFile>
    {
        private readonly string _path;

        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;

        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDirty { get; set; }
        public bool ToDelete { get; set; }
        public string ModFolder { get; set; } = string.Empty;

        public PlayerRestrictedData Data { get; set; } = new();

        [JsonIgnore]
        public BindingList<PRABoxes> BoxesView { get; set; } = new();

        [JsonIgnore]
        public BindingList<PRASafePosition> SafePositionsView { get; set; } = new();

        public PlayerRestrictedFile(string path)
        {
            _path = path;
        }

        public void ConvertPositionsToList()
        {
            if (Data.PRABoxes != null)
            {
                BoxesView = new BindingList<PRABoxes>();
                for (int i = 0; i < Data.PRABoxes.Count; i++)
                {
                    BoxesView.Add(new PRABoxes
                    {
                        HalfExtents = new Vec3(
                            Convert.ToSingle(Data.PRABoxes[i][0][0]),
                            Convert.ToSingle(Data.PRABoxes[i][0][1]),
                            Convert.ToSingle(Data.PRABoxes[i][0][2])),
                        Orientation = new Vec3(
                            Convert.ToSingle(Data.PRABoxes[i][1][0]),
                            Convert.ToSingle(Data.PRABoxes[i][1][1]),
                            Convert.ToSingle(Data.PRABoxes[i][1][2])),
                        Position = new Vec3(
                            Convert.ToSingle(Data.PRABoxes[i][2][0]),
                            Convert.ToSingle(Data.PRABoxes[i][2][1]),
                            Convert.ToSingle(Data.PRABoxes[i][2][2]))
                    });
                }

                Data.PRABoxes = null;
            }

            if (Data.SafePositions3D != null)
            {
                SafePositionsView = new BindingList<PRASafePosition>();
                for (int i = 0; i < Data.SafePositions3D.Count; i++)
                {
                    SafePositionsView.Add(new PRASafePosition
                    {
                        Position = new Vec3(
                            Data.SafePositions3D[i][0],
                            Data.SafePositions3D[i][1],
                            Data.SafePositions3D[i][2])
                    });
                }

                Data.SafePositions3D = null;
            }
        }

        public void ConvertListToPositions()
        {
            if (BoxesView != null)
            {
                Data.PRABoxes = new BindingList<BindingList<BindingList<decimal>>>();

                foreach (var box in BoxesView)
                {
                    var halfExtents = new BindingList<decimal>
                    {
                        Convert.ToDecimal(box.HalfExtents.X),
                        Convert.ToDecimal(box.HalfExtents.Y),
                        Convert.ToDecimal(box.HalfExtents.Z)
                    };

                    var orientation = new BindingList<decimal>
                    {
                        Convert.ToDecimal(box.Orientation.X),
                        Convert.ToDecimal(box.Orientation.Y),
                        Convert.ToDecimal(box.Orientation.Z)
                    };

                    var position = new BindingList<decimal>
                    {
                        Convert.ToDecimal(box.Position.X),
                        Convert.ToDecimal(box.Position.Y),
                        Convert.ToDecimal(box.Position.Z)
                    };

                    Data.PRABoxes.Add(new BindingList<BindingList<decimal>> { halfExtents, orientation, position });
                }
            }
            else
            {
                Data.PRABoxes = null;
            }

            if (SafePositionsView != null)
            {
                Data.SafePositions3D = new BindingList<BindingList<decimal>>();
                foreach (var safePosition in SafePositionsView)
                {
                    Data.SafePositions3D.Add(new BindingList<decimal>(safePosition.Position.getDecimalArray()));
                }
            }
            else
            {
                Data.SafePositions3D = null;
            }
        }

        public PlayerRestrictedFile Clone()
        {
            return new PlayerRestrictedFile(_path)
            {
                Id = Id,
                IsDirty = IsDirty,
                ToDelete = ToDelete,
                ModFolder = ModFolder,
                Data = Data.Clone(),
                BoxesView = new BindingList<PRABoxes>(BoxesView?.Select(x => x.Clone()).ToList() ?? new List<PRABoxes>()),
                SafePositionsView = new BindingList<PRASafePosition>(SafePositionsView?.Select(x => x.Clone()).ToList() ?? new List<PRASafePosition>())
            };
        }

        public bool Equals(PlayerRestrictedFile? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                Id == other.Id &&
                string.Equals(_path, other._path, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(ModFolder, other.ModFolder, StringComparison.OrdinalIgnoreCase) &&
                ToDelete == other.ToDelete &&
                Equals(Data, other.Data) &&
                ListEqual(BoxesView, other.BoxesView) &&
                ListEqual(SafePositionsView, other.SafePositionsView);
        }

        public override bool Equals(object? obj) => Equals(obj as PlayerRestrictedFile);

        private static bool ListEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    public class PlayerRestrictedData : IDeepCloneable<PlayerRestrictedData>, IEquatable<PlayerRestrictedData>
    {
        public string areaName { get; set; } = string.Empty;
        public BindingList<BindingList<BindingList<decimal>>>? PRABoxes { get; set; }
        public BindingList<BindingList<decimal>>? SafePositions3D { get; set; }

        public PlayerRestrictedData Clone()
        {
            return new PlayerRestrictedData
            {
                areaName = areaName,
                PRABoxes = PRABoxes == null
                    ? null
                    : new BindingList<BindingList<BindingList<decimal>>>(
                        PRABoxes.Select(box =>
                            new BindingList<BindingList<decimal>>(
                                box.Select(inner => new BindingList<decimal>(inner.ToList())).ToList()
                            )).ToList()),
                SafePositions3D = SafePositions3D == null
                    ? null
                    : new BindingList<BindingList<decimal>>(
                        SafePositions3D.Select(x => new BindingList<decimal>(x.ToList())).ToList())
            };
        }

        public bool Equals(PlayerRestrictedData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return areaName == other.areaName &&
                   NestedBoxesEqual(PRABoxes, other.PRABoxes) &&
                   NestedListEqual(SafePositions3D, other.SafePositions3D);
        }

        public override bool Equals(object? obj) => Equals(obj as PlayerRestrictedData);

        private static bool NestedBoxesEqual(
            IList<BindingList<BindingList<decimal>>>? a,
            IList<BindingList<BindingList<decimal>>>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!NestedListEqual(a[i], b[i]))
                    return false;
            }

            return true;
        }

        private static bool NestedListEqual(
            IList<BindingList<decimal>>? a,
            IList<BindingList<decimal>>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].SequenceEqual(b[i]))
                    return false;
            }

            return true;
        }
    }

    public class PRABoxes : IDeepCloneable<PRABoxes>, IEquatable<PRABoxes>
    {
        public Vec3 HalfExtents { get; set; } = new Vec3();
        public Vec3 Orientation { get; set; } = new Vec3();
        public Vec3 Position { get; set; } = new Vec3();

        public PRABoxes Clone()
        {
            return new PRABoxes
            {
                HalfExtents = HalfExtents?.Clone() ?? new Vec3(),
                Orientation = Orientation?.Clone() ?? new Vec3(),
                Position = Position?.Clone() ?? new Vec3()
            };
        }

        public bool Equals(PRABoxes? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(HalfExtents, other.HalfExtents) &&
                   Equals(Orientation, other.Orientation) &&
                   Equals(Position, other.Position);
        }

        public override bool Equals(object? obj) => Equals(obj as PRABoxes);
    }

    public class PRASafePosition : IDeepCloneable<PRASafePosition>, IEquatable<PRASafePosition>
    {
        public Vec3 Position { get; set; } = new Vec3();

        public PRASafePosition Clone()
        {
            return new PRASafePosition
            {
                Position = Position?.Clone() ?? new Vec3()
            };
        }

        public bool Equals(PRASafePosition? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Position, other.Position);
        }

        public override bool Equals(object? obj) => Equals(obj as PRASafePosition);
    }
}
