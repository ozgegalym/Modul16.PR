using System;
using System.IO;

class ChangeTracker
{
    private string directoryPath;
    private string logFilePath;
    private FileSystemWatcher fileSystemWatcher;

    public void Configure(string directoryPath, string logFilePath)
    {
        this.directoryPath = directoryPath;
        this.logFilePath = logFilePath;
    }

    public void Start()
    {
        if (string.IsNullOrEmpty(directoryPath) || string.IsNullOrEmpty(logFilePath))
        {
            Console.WriteLine("Конфигурация не завершена. Пожалуйста, выполните настройку отслеживания.");
            return;
        }

        fileSystemWatcher = new FileSystemWatcher(directoryPath);
        fileSystemWatcher.IncludeSubdirectories = true;
        fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;

        fileSystemWatcher.Created += OnChanged;
        fileSystemWatcher.Deleted += OnChanged;
        fileSystemWatcher.Renamed += OnRenamed;

        fileSystemWatcher.EnableRaisingEvents = true;
    }

    public void Stop()
    {
        if (fileSystemWatcher != null)
        {
            fileSystemWatcher.EnableRaisingEvents = false;
            fileSystemWatcher.Dispose();
        }
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        LogChange($"[{DateTime.Now}] {e.ChangeType}: {e.FullPath}");
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        LogChange($"[{DateTime.Now}] {e.ChangeType}: {e.OldFullPath} -> {e.FullPath}");
    }

    private void LogChange(string changeInfo)
    {
        try
        {
            File.AppendAllText(logFilePath, $"{changeInfo}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка логирования: {ex.Message}");
        }
    }
}
