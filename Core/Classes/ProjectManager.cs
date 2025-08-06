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

            if (string.IsNullOrWhiteSpace(_store.ActiveProject))
                _store.ActiveProject = project.ProjectName;

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
