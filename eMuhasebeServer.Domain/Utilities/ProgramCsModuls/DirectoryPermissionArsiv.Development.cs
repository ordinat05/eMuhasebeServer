//using System.Runtime.Versioning;
//using System.Security.AccessControl;
//using System.Security.Principal;

//namespace eMuhasebeServer.Infrastructure.Utilities.useStaticFiles;

//public static class DirectoryPermissionArsivDevelopment
//{
//    [SupportedOSPlatform("windows")]
//    public static void ConfigureArsivPermissions()
//    {
//        if (!OperatingSystem.IsWindows())
//        {
//            Console.WriteLine("This operation is only supported on Windows.");
//            return;
//        }

//        string basePath = @"C:\ArsivDevelopment";
//        EnsureDirectoryWithPermissions(basePath);
//    }

//    private static void EnsureDirectoryWithPermissions(string directoryPath)
//    {
//        try
//        {
//            if (!Directory.Exists(directoryPath))
//            {
//                Directory.CreateDirectory(directoryPath);
//            }

//            if (OperatingSystem.IsWindows())
//            {
//                var allDirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);
//                var allPaths = new List<string> { directoryPath }.Concat(allDirectories);

//                foreach (var path in allPaths)
//                {
//                    SetDirectoryPermissions(path);
//                }

//                foreach (var file in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
//                {
//                    SetFilePermissions(file);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error in development permissions: {ex.Message}");
//        }
//    }

//    [SupportedOSPlatform("windows")]
//    private static void SetDirectoryPermissions(string path)
//    {
//        try
//        {
//            var dirInfo = new DirectoryInfo(path);
//            var security = dirInfo.GetAccessControl();

//            var everyoneIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
//            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

//            AddDirectoryPermission(security, everyoneIdentity);
//            AddDirectoryPermission(security, usersIdentity);

//            dirInfo.SetAccessControl(security);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error setting directory permissions for {path}: {ex.Message}");
//        }
//    }

//    [SupportedOSPlatform("windows")]
//    private static void SetFilePermissions(string path)
//    {
//        try
//        {
//            var fileInfo = new FileInfo(path);
//            var security = fileInfo.GetAccessControl();

//            var everyoneIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
//            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

//            AddFilePermission(security, everyoneIdentity);
//            AddFilePermission(security, usersIdentity);

//            fileInfo.SetAccessControl(security);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error setting file permissions for {path}: {ex.Message}");
//        }
//    }

//    [SupportedOSPlatform("windows")]
//    private static void AddDirectoryPermission(DirectorySecurity security, SecurityIdentifier identity)
//    {
//        var rights = FileSystemRights.FullControl;
//        var inheritanceFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
//        var propagationFlags = PropagationFlags.None;
//        var type = AccessControlType.Allow;

//        security.AddAccessRule(new FileSystemAccessRule(identity, rights, inheritanceFlags, propagationFlags, type));
//    }

//    [SupportedOSPlatform("windows")]
//    private static void AddFilePermission(FileSecurity security, SecurityIdentifier identity)
//    {
//        security.AddAccessRule(new FileSystemAccessRule(
//            identity,
//            FileSystemRights.FullControl,
//            AccessControlType.Allow));
//    }
//}
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

namespace eMuhasebeServer.Domain.Utilities.ProgramCsModuls;

public static class DirectoryPermissionArsivDevelopment
{
    [SupportedOSPlatform("windows")]
    public static void ConfigureArsivPermissions()
    {
        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("This operation is only supported on Windows.");
            return;
        }

        string basePath = @"C:\ArsivDevelopment";
        EnsureDirectoryWithPermissions(basePath);
    }

    private static void EnsureDirectoryWithPermissions(string directoryPath)
    {
        try
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (OperatingSystem.IsWindows())
            {
                var allDirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);
                var allPaths = new List<string> { directoryPath }.Concat(allDirectories);

                foreach (var path in allPaths)
                {
                    SetDirectoryPermissions(path);
                }

                foreach (var file in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
                {
                    SetFilePermissions(file);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in development permissions: {ex.Message}");
        }
    }

    [SupportedOSPlatform("windows")]
    private static void SetDirectoryPermissions(string path)
    {
        try
        {
            var dirInfo = new DirectoryInfo(path);
            var security = dirInfo.GetAccessControl();

            // Tüm kullanıcılar için izinler
            var everyoneIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            // Built-in Users grubu
            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            // Administrators grubu
            var adminIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            // SYSTEM hesabı
            var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
            // Network Service
            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
            // IIS_IUSRS grubu
            var iisUsersGroup = new NTAccount("IIS_IUSRS");

            AddDirectoryPermission(security, everyoneIdentity);
            AddDirectoryPermission(security, usersIdentity);
            AddDirectoryPermission(security, adminIdentity);
            AddDirectoryPermission(security, systemIdentity);
            AddDirectoryPermission(security, networkServiceIdentity);
            AddDirectoryPermission(security, iisUsersGroup);

            dirInfo.SetAccessControl(security);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting directory permissions for {path}: {ex.Message}");
        }
    }

    [SupportedOSPlatform("windows")]
    private static void SetFilePermissions(string path)
    {
        try
        {
            var fileInfo = new FileInfo(path);
            var security = fileInfo.GetAccessControl();

            var everyoneIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            var adminIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
            var iisUsersGroup = new NTAccount("IIS_IUSRS");

            AddFilePermission(security, everyoneIdentity);
            AddFilePermission(security, usersIdentity);
            AddFilePermission(security, adminIdentity);
            AddFilePermission(security, systemIdentity);
            AddFilePermission(security, networkServiceIdentity);
            AddFilePermission(security, iisUsersGroup);

            fileInfo.SetAccessControl(security);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting file permissions for {path}: {ex.Message}");
        }
    }

    [SupportedOSPlatform("windows")]
    private static void AddDirectoryPermission(DirectorySecurity security, SecurityIdentifier identity)
    {
        var rights = FileSystemRights.FullControl;
        var inheritanceFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
        var propagationFlags = PropagationFlags.None;
        var type = AccessControlType.Allow;

        security.AddAccessRule(new FileSystemAccessRule(identity, rights, inheritanceFlags, propagationFlags, type));
    }

    [SupportedOSPlatform("windows")]
    private static void AddDirectoryPermission(DirectorySecurity security, NTAccount account)
    {
        var rights = FileSystemRights.FullControl;
        var inheritanceFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
        var propagationFlags = PropagationFlags.None;
        var type = AccessControlType.Allow;

        security.AddAccessRule(new FileSystemAccessRule(account, rights, inheritanceFlags, propagationFlags, type));
    }

    [SupportedOSPlatform("windows")]
    private static void AddFilePermission(FileSecurity security, SecurityIdentifier identity)
    {
        security.AddAccessRule(new FileSystemAccessRule(
            identity,
            FileSystemRights.FullControl,
            AccessControlType.Allow));
    }

    [SupportedOSPlatform("windows")]
    private static void AddFilePermission(FileSecurity security, NTAccount account)
    {
        security.AddAccessRule(new FileSystemAccessRule(
            account,
            FileSystemRights.FullControl,
            AccessControlType.Allow));
    }
}