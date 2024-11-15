using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.Versioning;

namespace eMuhasebeServer.Domain.Utilities.ProgramCsModuls;

[SupportedOSPlatform("windows")]
public static class DirectoryPermissionHelperDevelopment
{
    public static void EnsureDirectoryWithPermissions(string basePath, string folderName)
    {
        var staticFilesPath = Path.Combine(basePath, "aaaStaticFiles", folderName);

        if (!Directory.Exists(staticFilesPath))
        {
            try
            {
                Directory.CreateDirectory(staticFilesPath);
                SetDirectoryPermissions(staticFilesPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory or setting permissions: {ex.Message}");
            }
        }
    }

    private static void SetDirectoryPermissions(string directoryPath)
    {
        try
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            var directorySecurity = directoryInfo.GetAccessControl();

            // Development ortamında sadece Users grubu için izin
            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            AddDirectoryPermission(directorySecurity, usersIdentity);

            directoryInfo.SetAccessControl(directorySecurity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Windows permission error: {ex.Message}");
        }
    }

    private static void AddDirectoryPermission(DirectorySecurity directorySecurity, SecurityIdentifier identity)
    {
        directorySecurity.AddAccessRule(new FileSystemAccessRule(
            identity,
            FileSystemRights.Modify | FileSystemRights.Read | FileSystemRights.Write,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow));
    }
}