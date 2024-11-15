//using System.Runtime.Versioning;
//using System.Security.AccessControl;
//using System.Security.Principal;

//namespace eMuhasebeServer.Infrastructure.Utilities.useStaticFiles;



//public static class DirectoryPermissionArsivProduct
//{
//    [SupportedOSPlatform("windows")]
//    public static void ConfigureArsivPermissions()
//    {
//        if (!OperatingSystem.IsWindows())
//        {
//            Console.WriteLine("This operation is only supported on Windows.");
//            return;
//        }

//        string basePath = @"C:\ArsivProduct";
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
//            Console.WriteLine($"Error in production permissions: {ex.Message}");
//        }
//    }

//    [SupportedOSPlatform("windows")]
//    private static void SetDirectoryPermissions(string path)
//    {
//        try
//        {
//            var dirInfo = new DirectoryInfo(path);
//            var security = dirInfo.GetAccessControl();

//            var iisUsersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
//            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);

//            var iisUsersGroup = new NTAccount("IIS_IUSRS");
//            var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");

//            AddDirectoryPermission(security, iisUsersIdentity);
//            AddDirectoryPermission(security, networkServiceIdentity);
//            AddDirectoryPermissionNT(security, iisUsersGroup);
//            AddDirectoryPermissionNT(security, appPoolIdentity);

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

//            var iisUsersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
//            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
//            var iisUsersGroup = new NTAccount("IIS_IUSRS");
//            var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");

//            AddFilePermission(security, iisUsersIdentity);
//            AddFilePermission(security, networkServiceIdentity);
//            AddFilePermissionNT(security, iisUsersGroup);
//            AddFilePermissionNT(security, appPoolIdentity);

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
//    private static void AddDirectoryPermissionNT(DirectorySecurity security, NTAccount account)
//    {
//        var rights = FileSystemRights.FullControl;
//        var inheritanceFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
//        var propagationFlags = PropagationFlags.None;
//        var type = AccessControlType.Allow;

//        security.AddAccessRule(new FileSystemAccessRule(account, rights, inheritanceFlags, propagationFlags, type));
//    }

//    [SupportedOSPlatform("windows")]
//    private static void AddFilePermission(FileSecurity security, SecurityIdentifier identity)
//    {
//        security.AddAccessRule(new FileSystemAccessRule(
//            identity,
//            FileSystemRights.FullControl,
//            AccessControlType.Allow));
//    }

//    [SupportedOSPlatform("windows")]
//    private static void AddFilePermissionNT(FileSecurity security, NTAccount account)
//    {
//        security.AddAccessRule(new FileSystemAccessRule(
//            account,
//            FileSystemRights.FullControl,
//            AccessControlType.Allow));
//    }
//}

using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

namespace eMuhasebeServer.Domain.Utilities.ProgramCsModuls;

public static class DirectoryPermissionArsivProduct
{
    [SupportedOSPlatform("windows")]
    public static void ConfigureArsivPermissions()
    {
        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("This operation is only supported on Windows.");
            return;
        }

        string basePath = @"C:\ArsivProduct";
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
            Console.WriteLine($"Error in production permissions: {ex.Message}");
        }
    }

    [SupportedOSPlatform("windows")]
    private static void SetDirectoryPermissions(string path)
    {
        try
        {
            var dirInfo = new DirectoryInfo(path);
            var security = dirInfo.GetAccessControl();

            // Security Identifiers (SID) için izinler
            var iisUsersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
            var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
            var administratorsIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var authenticatedUsersIdentity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

            // NT Accounts için izinler
            var iisUsersGroup = new NTAccount("IIS_IUSRS");
            var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");
            var networkService = new NTAccount("NT AUTHORITY\\NETWORK SERVICE");
            var localSystem = new NTAccount("NT AUTHORITY\\SYSTEM");
            var administrators = new NTAccount("BUILTIN\\Administrators");

            // SID bazlı izinler
            AddDirectoryPermission(security, iisUsersIdentity);
            AddDirectoryPermission(security, networkServiceIdentity);
            AddDirectoryPermission(security, systemIdentity);
            AddDirectoryPermission(security, administratorsIdentity);
            AddDirectoryPermission(security, authenticatedUsersIdentity);

            // NT Account bazlı izinler
            AddDirectoryPermissionNT(security, iisUsersGroup);
            AddDirectoryPermissionNT(security, appPoolIdentity);
            AddDirectoryPermissionNT(security, networkService);
            AddDirectoryPermissionNT(security, localSystem);
            AddDirectoryPermissionNT(security, administrators);

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

            // Security Identifiers (SID) için izinler
            var iisUsersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
            var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
            var administratorsIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var authenticatedUsersIdentity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

            // NT Accounts için izinler
            var iisUsersGroup = new NTAccount("IIS_IUSRS");
            var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");
            var networkService = new NTAccount("NT AUTHORITY\\NETWORK SERVICE");
            var localSystem = new NTAccount("NT AUTHORITY\\SYSTEM");
            var administrators = new NTAccount("BUILTIN\\Administrators");

            // SID bazlı izinler
            AddFilePermission(security, iisUsersIdentity);
            AddFilePermission(security, networkServiceIdentity);
            AddFilePermission(security, systemIdentity);
            AddFilePermission(security, administratorsIdentity);
            AddFilePermission(security, authenticatedUsersIdentity);

            // NT Account bazlı izinler
            AddFilePermissionNT(security, iisUsersGroup);
            AddFilePermissionNT(security, appPoolIdentity);
            AddFilePermissionNT(security, networkService);
            AddFilePermissionNT(security, localSystem);
            AddFilePermissionNT(security, administrators);

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
    private static void AddDirectoryPermissionNT(DirectorySecurity security, NTAccount account)
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
    private static void AddFilePermissionNT(FileSecurity security, NTAccount account)
    {
        security.AddAccessRule(new FileSystemAccessRule(
            account,
            FileSystemRights.FullControl,
            AccessControlType.Allow));
    }
}
