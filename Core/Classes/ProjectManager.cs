using System.Text.Json;

namespace Day2eEditor
{
    public class ProjectManager
    {
        private const string ProjectsFileName = "projects.json";
        private readonly string _projectsFolder;
        private ProjectStore _store = new();

        public IReadOnlyList<Project> Projects => _store.Projects.AsReadOnly();
        public bool ShowChangeLog => _store.ShowChangeLog;
        public Project? CurrentProject => _store.Projects.FirstOrDefault(p => p.ProjectName == _store.ActiveProject);

        public ProjectManager(string projectsFolder)
        {
            _projectsFolder = projectsFolder;
            Directory.CreateDirectory(_projectsFolder);
        }

        public void AddProject(Project project)
        {
            if (!_store.Projects.Any(p => p.ProjectName == project.ProjectName))
                _store.Projects.Add(project);

            if (string.IsNullOrWhiteSpace(_store.ActiveProject))
                _store.ActiveProject = project.ProjectName;
        }

        public void RemoveProject(Project project)
        {
            _store.Projects.Remove(project);
            if (_store.ActiveProject == project.ProjectName)
                _store.ActiveProject = _store.Projects.FirstOrDefault()?.ProjectName ?? string.Empty;
        }

        public void SetCurrentProject(Project project)
        {
            if (_store.Projects.Contains(project))
                _store.ActiveProject = project.ProjectName;
        }

        public void Load()
        {
            Console.WriteLine($"Loading {ProjectsFileName}");
            var path = Path.Combine(_projectsFolder, ProjectsFileName);

            if (!File.Exists(path))
            {
                Console.WriteLine("Projects file not found. Creating a new one.");
                _store = new ProjectStore() { ShowChangeLog = false, ActiveProject = "", Projects = new List<Project>() }; // Create an empty store
                Save(); // Save it immediately
                return;
            }
            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<ProjectStore>(json);
            if (loaded != null)
                _store = loaded;
        }

        public void Save()
        {
            var path = Path.Combine(_projectsFolder, ProjectsFileName);
            var json = JsonSerializer.Serialize(_store, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }
    }


}
