namespace NEWgIT.Tests;

public static class Extensions
{
    public static void ForceDelete(this Repository repo)
    {
        DeleteReadOnlyDirectory(repo.Info.Path);
    }

    private static void DeleteReadOnlyDirectory(string directory)
    {
        foreach (var subdirectory in Directory.EnumerateDirectories(directory))
        {
            DeleteReadOnlyDirectory(subdirectory);
        }
        foreach (var fileName in Directory.EnumerateFiles(directory))
        {
            var fileInfo = new FileInfo(fileName);
            fileInfo.Attributes = FileAttributes.Normal;
            fileInfo.Delete();
        }
        Directory.Delete(directory);
    }
}
