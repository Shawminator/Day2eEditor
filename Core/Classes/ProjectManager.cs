using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class ProjectManager
    {
        private const string ProjectsFileName = "projects.json";
        private readonly string _projectsFolder;
        private ProjectStore _store = new();

        public BindingList<Project> Projects => _store.Projects;
        public bool ShowChangeLog => _store.ShowChangeLog;
        public Project? CurrentProject => _store.Projects.FirstOrDefault(p => p.ProjectName == _store.ActiveProject);

        public ProjectManager(string projectsFolder)
        {
            _projectsFolder = projectsFolder;
            Directory.CreateDirectory(_projectsFolder);
        }
        public void AddProject(Project project)
        {
            if (!_store.Projects.Any(p => p.ProjectName.Equals(project.ProjectName, StringComparison.OrdinalIgnoreCase)))
            {
                _store.Projects.Add(project);
                SortProjects();
            }

            Save();
        }
        public void RemoveProject(Project project)
        {
            _store.Projects.Remove(project);

            if (_store.ActiveProject == project.ProjectName)
                _store.ActiveProject = _store.Projects.FirstOrDefault()?.ProjectName ?? string.Empty;

            Save();
        }
        public void SetCurrentProject(Project project)
        {
            if (_store.Projects.Contains(project))
            {
                _store.ActiveProject = project.ProjectName;
                Console.WriteLine($"The Current Active Project is  {CurrentProject.ProjectName}");
                AppServices.GetRequired<EconomyManager>().SetProject(project);
                Console.WriteLine("Please click the select section to get the pop out menu");
                Save();
            }
        }
        public void Load()
        {
            Console.WriteLine($"Loading {ProjectsFileName}");
            var path = Path.Combine(_projectsFolder, ProjectsFileName);

            if (!File.Exists(path))
            {
                Console.WriteLine("Projects file not found. Creating a new one.");
                _store = new ProjectStore
                {
                    ShowChangeLog = false,
                    ActiveProject = "",
                    Projects = new BindingList<Project>()
                };
                Save();
                return;
            }

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<ProjectStore>(json);

            if (loaded != null)
            {
                // Convert to BindingList and sort
                var sorted = loaded.Projects.OrderBy(p => p.ProjectName).ToList();
                loaded.Projects = new BindingList<Project>(sorted);
                _store = loaded;
            }
            bool anyChanges = false;

            foreach (var p in _store.Projects)
            {
                bool canFix = true;

                // --- Normalize ProjectRoot ---
                var normalizedRoot = p.ProjectRoot.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if (p.ProjectRoot != normalizedRoot)
                {
                    Console.WriteLine($"[INFO] Normalised Project root from : {p.ProjectRoot} to {normalizedRoot} for Project : {p.ProjectName}");
                    p.ProjectRoot = normalizedRoot;
                    anyChanges = true;
                }

                // --- Check ProjectRoot ---
                if (!Directory.Exists(p.ProjectRoot))
                {
                    Console.WriteLine($"[ERROR] Project root missing: {p.ProjectRoot}");
                    canFix = false;
                }

                // --- Check mission folder ---
                var missionDir = Path.Combine(p.ProjectRoot, "mpmissions", p.MpMissionPath);
                if (!Directory.Exists(missionDir))
                {
                    Console.WriteLine($"[ERROR] Mission folder missing: {missionDir}");
                    canFix = false;
                }

                // --- Optional checks ---
                var mapFile = Path.Combine("MapAddons", p.MapPath);
                if (!File.Exists(mapFile))
                {
                    Console.WriteLine($"[WARN] Map file missing: {mapFile}, Please download and Install from Project Manager");
                }

                var profileDir = Path.Combine(p.ProjectRoot, p.ProfileName);
                if (!Directory.Exists(profileDir))
                {
                    Console.WriteLine($"[WARN] Profile directory missing: {profileDir}");
                    canFix = false;
                }

                // --- MapSize validation ---
                var newSize = ShellHelper.Getmapsizefrommissionpath(p.MpMissionPath);
                if (p.MapSize != newSize)
                {
                    p.MapSize = newSize;
                    anyChanges = true;
                }
 
                // --- Ensure ProjectName ---
                if (string.IsNullOrWhiteSpace(p.ProjectName))
                {
                    p.ProjectName = Path.GetFileNameWithoutExtension(p.MpMissionPath);
                    anyChanges = true;
                }

                // --- If not fixable, clear active project ---
                if (!canFix && _store.ActiveProject == p.ProjectName)
                {
                    Console.WriteLine($"[INFO] Clearing active project: {p.ProjectName}");
                    _store.ActiveProject = string.Empty;
                    anyChanges = true;
                }
            }

            if (anyChanges)
            {
                Save();
            }
        }
        public void Save()
        {
            var path = Path.Combine(_projectsFolder, ProjectsFileName);
            var json = JsonSerializer.Serialize(_store, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            File.WriteAllText(path, json);
            Console.WriteLine($"[INFO] Saved Project json.");
        }
        private void SortProjects()
        {
            var sorted = _store.Projects.OrderBy(p => p.ProjectName).ToList();

            _store.Projects.RaiseListChangedEvents = false;
            _store.Projects.Clear();
            foreach (var project in sorted)
                _store.Projects.Add(project);
            _store.Projects.RaiseListChangedEvents = true;
            _store.Projects.ResetBindings();
        }
    }
}
