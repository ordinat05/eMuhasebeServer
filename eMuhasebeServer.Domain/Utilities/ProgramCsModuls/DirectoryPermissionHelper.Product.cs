using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.Versioning;

namespace eMuhasebeServer.Domain.Utilities.ProgramCsModuls;

[SupportedOSPlatform("windows")]
public static class DirectoryPermissionHelperProduct
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

            // IIS_IUSRS için izinler
            var iisUsersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            AddDirectoryPermission(directorySecurity, iisUsersIdentity);

            // NETWORK SERVICE için izinler
            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
            AddDirectoryPermission(directorySecurity, networkServiceIdentity);

            // IIS_IUSRS grubu için izinler
            var iisUsersGroup = new NTAccount("IIS_IUSRS");
            AddDirectoryPermission(directorySecurity, iisUsersGroup);

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

    private static void AddDirectoryPermission(DirectorySecurity directorySecurity, NTAccount account)
    {
        directorySecurity.AddAccessRule(new FileSystemAccessRule(
            account,
            FileSystemRights.Modify | FileSystemRights.Read | FileSystemRights.Write,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow));
    }
}