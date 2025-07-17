using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Recycli.Model;
using System.Reflection;

namespace Recycli
{
    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        private string versionText = string.Empty;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private List<RecycleFile> allRecycleBinFiles;

        [ObservableProperty]
        private List<RecycleFile> visibleRecycleBinFiles;

        [ObservableProperty]
        private List<RecycleFile> selectedFiles;

        private readonly ILogger? logger = App.Logger;

        public ViewModel()
        {
            VersionText = $"Version: {GetAppVersion()}";
            var files = RecycleManager.GetRecycleBinFiles();
            AllRecycleBinFiles = new List<RecycleFile>(files);
            VisibleRecycleBinFiles = new List<RecycleFile>(files);
            SelectedFiles = new List<RecycleFile>();
        }

        private void RefreshStatus()
        {
            VisibleRecycleBinFiles = RecycleManager.GetRecycleBinFiles();
            SelectedFiles.Clear();
        }

        [RelayCommand]
        private void RestoreFile()
        {
            if (SelectedFiles.Count != 0)
            {
                foreach (var file in SelectedFiles)
                {
                    logger.LogInformation("Recycli: Restoring file: {FileName}", file.Name);
                    RecycleManager.RestoreFile(file);
                }
            }
            RefreshStatus();
        }

        [RelayCommand]
        private void DeleteFile()
        {
            if (SelectedFiles.Count != 0)
            {
                foreach (var file in SelectedFiles)
                {
                    logger.LogInformation("Recycli: Deleting file: {FileName}", file.Name);
                    RecycleManager.DeleteFile(file);
                }
            }
            RefreshStatus();
        }

        // Thanks to the MVVM Toolkit, this method will be called automatically when the SearchText property changes.
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                VisibleRecycleBinFiles = new List<RecycleFile>(AllRecycleBinFiles);
            }
            else
            {
                logger.LogInformation("Recycli: OnSearchTextChanged: {value}", value);
                VisibleRecycleBinFiles = new List<RecycleFile>(
                    AllRecycleBinFiles.Where(f => f.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase))
                );
            }
        }

        private static string GetAppVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var informationalVersion = assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            if (!string.IsNullOrEmpty(informationalVersion))
            {
                // Remove everything after '+' if present
                var plusIndex = informationalVersion.IndexOf('+');
                if (plusIndex >= 0)
                    return informationalVersion.Substring(0, plusIndex);
                return informationalVersion;
            }

            var version = assembly.GetName().Version?.ToString();
            return version ?? "unknown";
        }
    }
}
