using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public enum PendingServerAction
    {
        Upload,
        Remove
    }
    public class PendingUploadFile
    {
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public DateTime LastSavedAt { get; set; }
        public PendingServerAction Action { get; set; } = PendingServerAction.Upload;
    }
    public class UploadTrackerService
    {
        private const string RemovePrefix = "File Remove ";

        private readonly string _saveFilePath;
        private readonly Dictionary<string, List<PendingUploadFile>> _pendingFiles;

        public IReadOnlyDictionary<string, List<PendingUploadFile>> PendingFiles => _pendingFiles;

        public UploadTrackerService(string saveFilePath)
        {
            _saveFilePath = saveFilePath;
            _pendingFiles = new Dictionary<string, List<PendingUploadFile>>(StringComparer.OrdinalIgnoreCase);
            Load();
        }

        public void InitializeProjects(IEnumerable<Project> projects)
        {
            if (projects == null)
                return;

            foreach (var project in projects)
            {
                if (project == null || string.IsNullOrWhiteSpace(project.ProjectName))
                    continue;

                EnsureProjectExists(project.ProjectName);
            }

            Save();
        }

        public void MarkForUpload(string projectName, IEnumerable<string> filePaths)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException("Project name cannot be empty.", nameof(projectName));

            if (filePaths == null || filePaths.Count()== 0)
                return;

            EnsureProjectExists(projectName);

            var list = _pendingFiles[projectName];

            foreach (var filePath in filePaths)
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    continue;

                var action = DetermineAction(filePath);
                var displayFileName = Path.GetFileName(filePath);

                var existing = list.FirstOrDefault(x =>
                    string.Equals(x.FullPath, filePath, StringComparison.OrdinalIgnoreCase));

                if (existing != null)
                {
                    existing.FileName = displayFileName;
                    existing.LastSavedAt = DateTime.Now;
                    existing.Action = action;
                }
                else
                {
                    list.Add(new PendingUploadFile
                    {
                        FileName = displayFileName,
                        FullPath = filePath.Replace("File Remove ", ""),
                        ProjectName = projectName,
                        LastSavedAt = DateTime.Now,
                        Action = action
                    });
                }
            }

            Save();
        }
        public void MarkUploaded(string projectName, string filePath)
        {
            if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(filePath))
                return;

            if (!_pendingFiles.TryGetValue(projectName, out var list))
                return;

            list.RemoveAll(x =>
                string.Equals(x.FullPath, filePath, StringComparison.OrdinalIgnoreCase));

            Save();
        }

        public void MarkAllUploaded(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return;

            if (!_pendingFiles.ContainsKey(projectName))
                return;

            _pendingFiles[projectName].Clear();
            Save();
        }

        public IReadOnlyList<PendingUploadFile> GetFilesForProject(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return Array.Empty<PendingUploadFile>();

            if (_pendingFiles.TryGetValue(projectName, out var list))
                return list
                    .OrderBy(x => x.Action)
                    .ThenBy(x => x.FileName)
                    .ToList()
                    .AsReadOnly();

            return Array.Empty<PendingUploadFile>();
        }

        public List<PendingUploadFile> GetAllPendingFiles()
        {
            return _pendingFiles.Values
                .SelectMany(x => x)
                .OrderBy(x => x.ProjectName)
                .ThenBy(x => x.Action)
                .ThenBy(x => x.FileName)
                .ToList();
        }

        public int GetPendingCount(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return 0;

            return _pendingFiles.TryGetValue(projectName, out var list) ? list.Count : 0;
        }

        public bool HasPendingFiles(string projectName)
        {
            return GetPendingCount(projectName) > 0;
        }

        public List<PendingUploadFile> GetFilesToUpload(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return new List<PendingUploadFile>();

            if (!_pendingFiles.TryGetValue(projectName, out var list))
                return new List<PendingUploadFile>();

            return list
                .Where(x => x.Action == PendingServerAction.Upload)
                .OrderBy(x => x.FileName)
                .ToList();
        }

        public List<PendingUploadFile> GetFilesToRemove(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return new List<PendingUploadFile>();

            if (!_pendingFiles.TryGetValue(projectName, out var list))
                return new List<PendingUploadFile>();

            return list
                .Where(x => x.Action == PendingServerAction.Remove)
                .OrderBy(x => x.FileName)
                .ToList();
        }

        public void RemoveMissingFiles()
        {
            foreach (var project in _pendingFiles.Keys.ToList())
            {
                _pendingFiles[project].RemoveAll(x =>
                    x.Action == PendingServerAction.Upload && !File.Exists(x.FullPath));
            }

            Save();
        }

        public string GetPendingFilesReport(string projectName)
        {
            var files = GetFilesForProject(projectName);

            if (files.Count == 0)
                return "No files need server changes.";

            return string.Join(Environment.NewLine,
                files.Select(x => $"{GetActionLabel(x.Action)} {x.FileName} ({x.LastSavedAt:g})"));
        }

        private PendingServerAction DetermineAction(string filePath)
        {
            if (filePath.StartsWith(RemovePrefix, StringComparison.OrdinalIgnoreCase))
                return PendingServerAction.Remove;

            return PendingServerAction.Upload;
        }

        private string GetActionLabel(PendingServerAction action)
        {
            return action switch
            {
                PendingServerAction.Remove => "[REMOVE]",
                _ => "[UPLOAD]"
            };
        }

        private void EnsureProjectExists(string projectName)
        {
            if (!_pendingFiles.ContainsKey(projectName))
            {
                _pendingFiles[projectName] = new List<PendingUploadFile>();
            }
        }

        private void Save()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(_pendingFiles, options);
            File.WriteAllText(_saveFilePath, json);
        }

        private void Load()
        {
            if (!File.Exists(_saveFilePath))
                return;

            try
            {
                var json = File.ReadAllText(_saveFilePath);

                var data = JsonSerializer.Deserialize<Dictionary<string, List<PendingUploadFile>>>(json);

                if (data == null)
                    return;

                _pendingFiles.Clear();

                foreach (var kvp in data)
                {
                    if (string.IsNullOrWhiteSpace(kvp.Key))
                        continue;

                    _pendingFiles[kvp.Key] = kvp.Value ?? new List<PendingUploadFile>();
                }
            }
            catch
            {
                _pendingFiles.Clear();
            }
        }
    }
}