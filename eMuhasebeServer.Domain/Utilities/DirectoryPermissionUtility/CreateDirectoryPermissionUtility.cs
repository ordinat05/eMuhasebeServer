using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

namespace eMuhasebeServer.Domain.Utilities.DirectoryPermissionUtility;

public static class CreateDirectoryPermissionUtility
{
    public static void SetFolderPermissions(string folderPath)
    {
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Read-only özelliğini kaldır
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            if ((dirInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                dirInfo.Attributes &= ~FileAttributes.ReadOnly;
            }

            if (OperatingSystem.IsWindows())
            {
                SetPermissionsForWindows(folderPath);
            }
            else
            {
                SetFolderPermissionsNonWindows(folderPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting folder permissions: {ex.Message}");
            throw;
        }
    }

    [SupportedOSPlatform("windows")]
    private static void SetFolderPermissionsWindows(string folderPath)
    {
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            SetPermissionsForWindows(folderPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting folder permissions on Windows: {ex.Message}");
            throw;
        }
    }

    private static void SetFolderPermissionsNonWindows(string folderPath)
    {
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // Linux ve macOS için basit izinler
            if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            {
                System.Diagnostics.Process.Start("chmod", $"755 {folderPath}");
                System.Diagnostics.Process.Start("chmod", $"-R 755 {folderPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting non-Windows permissions: {ex.Message}");
            throw;
        }
    }

    [SupportedOSPlatform("windows")]
    private static void SetPermissionsForWindows(string path)
    {
        var dirInfo = new DirectoryInfo(path);
        var dirSecurity = dirInfo.GetAccessControl();

        // Security Identifiers (SID)
        var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
        var everyoneIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
        var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
        var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
        var administratorsIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

        // NT Accounts
        var iisUsersGroup = new NTAccount("IIS_IUSRS");
        var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");
        var networkService = new NTAccount("NT AUTHORITY\\NETWORK SERVICE");
        var localSystem = new NTAccount("NT AUTHORITY\\SYSTEM");
        var administrators = new NTAccount("BUILTIN\\Administrators");

        // SID izinleri ekle
        AddFileFolderPermission(dirSecurity, usersIdentity);
        AddFileFolderPermission(dirSecurity, everyoneIdentity);
        AddFileFolderPermission(dirSecurity, networkServiceIdentity);
        AddFileFolderPermission(dirSecurity, systemIdentity);
        AddFileFolderPermission(dirSecurity, administratorsIdentity);

        // NT Account izinleri ekle
        AddFileFolderPermissionNT(dirSecurity, iisUsersGroup);
        AddFileFolderPermissionNT(dirSecurity, appPoolIdentity);
        AddFileFolderPermissionNT(dirSecurity, networkService);
        AddFileFolderPermissionNT(dirSecurity, localSystem);
        AddFileFolderPermissionNT(dirSecurity, administrators);

        dirSecurity.SetAccessRuleProtection(false, false);
        dirInfo.SetAccessControl(dirSecurity);

        ApplyPermissionsToSubItemsWindows(path);
    }

    [SupportedOSPlatform("windows")]
    private static void ApplyPermissionsToSubItemsWindows(string path)
    {
        foreach (var dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
        {
            // Alt klasörlerin read-only özelliğini kaldır
            DirectoryInfo subDirInfo = new DirectoryInfo(dir);
            if ((subDirInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                subDirInfo.Attributes &= ~FileAttributes.ReadOnly;
            }
            SetPermissionsForWindows(dir);
        }

        foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
        {
            // Dosyaların read-only özelliğini kaldır
            FileInfo fileInfo = new FileInfo(file);
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                fileInfo.Attributes &= ~FileAttributes.ReadOnly;
            }
            SetFilePermissionsWindows(file);
        }
    }

    [SupportedOSPlatform("windows")]
    private static void SetFilePermissionsWindows(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        var fileSecurity = fileInfo.GetAccessControl();

        var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
        var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
        var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);

        var iisUsersGroup = new NTAccount("IIS_IUSRS");
        var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");

        AddFilePermission(fileSecurity, usersIdentity);
        AddFilePermission(fileSecurity, networkServiceIdentity);
        AddFilePermission(fileSecurity, systemIdentity);

        AddFilePermissionNT(fileSecurity, iisUsersGroup);
        AddFilePermissionNT(fileSecurity, appPoolIdentity);

        fileInfo.SetAccessControl(fileSecurity);
    }

    [SupportedOSPlatform("windows")]
    private static void AddFileFolderPermission(DirectorySecurity security, SecurityIdentifier identity)
    {
        var fileSystemRule = new FileSystemAccessRule(
            identity,
            FileSystemRights.FullControl,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow);

        security.AddAccessRule(fileSystemRule);
    }

    [SupportedOSPlatform("windows")]
    private static void AddFileFolderPermissionNT(DirectorySecurity security, NTAccount account)
    {
        var fileSystemRule = new FileSystemAccessRule(
            account,
            FileSystemRights.FullControl,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow);

        security.AddAccessRule(fileSystemRule);
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

//using System.Runtime.Versioning;
//using System.Security.AccessControl;
//using System.Security.Principal;
//// Directory.CreateDirectory komutu yerine klasör yoksa oluşturmak için; 
//// using eMuhasebeServer.Infrastructure.Utilities.DirectoryPermissionUtility;
//// DirectoryPermissionUtility.CreateDirectoryPermissionUtility



//namespace eMuhasebeServer.Infrastructure.Utilities.DirectoryPermissionUtility;

//[SupportedOSPlatform("windows")]
//public static class CreateDirectoryPermissionUtility
//{
//    /// <summary>
//    /// Belirtilen klasöre gerekli tüm izinleri atar
//    /// </summary>
//    /// <param name="folderPath">İzin verilecek klasör yolu</param>
//    public static void SetFolderPermissions(string folderPath)
//    {
//        try
//        {
//            if (!Directory.Exists(folderPath))
//            {
//                Directory.CreateDirectory(folderPath);
//            }

//            SetPermissions(folderPath);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error setting folder permissions: {ex.Message}");
//            throw;
//        }
//    }

//    private static void SetPermissions(string path)
//    {
//        try
//        {
//            var dirInfo = new DirectoryInfo(path);
//            var dirSecurity = dirInfo.GetAccessControl();

//            // Security Identifiers (SID)
//            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
//            var everyoneIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
//            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
//            var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
//            var administratorsIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

//            // NT Accounts
//            var iisUsersGroup = new NTAccount("IIS_IUSRS");
//            var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");
//            var networkService = new NTAccount("NT AUTHORITY\\NETWORK SERVICE");
//            var localSystem = new NTAccount("NT AUTHORITY\\SYSTEM");
//            var administrators = new NTAccount("BUILTIN\\Administrators");

//            // SID izinleri ekle
//            AddFileFolderPermission(dirSecurity, usersIdentity);
//            AddFileFolderPermission(dirSecurity, everyoneIdentity);
//            AddFileFolderPermission(dirSecurity, networkServiceIdentity);
//            AddFileFolderPermission(dirSecurity, systemIdentity);
//            AddFileFolderPermission(dirSecurity, administratorsIdentity);

//            // NT Account izinleri ekle
//            AddFileFolderPermissionNT(dirSecurity, iisUsersGroup);
//            AddFileFolderPermissionNT(dirSecurity, appPoolIdentity);
//            AddFileFolderPermissionNT(dirSecurity, networkService);
//            AddFileFolderPermissionNT(dirSecurity, localSystem);
//            AddFileFolderPermissionNT(dirSecurity, administrators);

//            // Alt klasör ve dosyalar için inheritance ayarla
//            dirSecurity.SetAccessRuleProtection(false, false);

//            // İzinleri uygula
//            dirInfo.SetAccessControl(dirSecurity);

//            // Alt klasör ve dosyalara da izinleri uygula
//            ApplyPermissionsToSubItems(path);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error in permission configuration: {ex.Message}");
//            throw;
//        }
//    }

//    private static void ApplyPermissionsToSubItems(string path)
//    {
//        try
//        {
//            // Alt klasörlere izinleri uygula
//            foreach (var dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
//            {
//                SetPermissions(dir);
//            }

//            // Dosyalara izinleri uygula
//            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
//            {
//                SetFilePermissions(file);
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error applying permissions to sub-items: {ex.Message}");
//            throw;
//        }
//    }

//    private static void SetFilePermissions(string filePath)
//    {
//        try
//        {
//            var fileInfo = new FileInfo(filePath);
//            var fileSecurity = fileInfo.GetAccessControl();

//            // Security Identifiers (SID)
//            var usersIdentity = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
//            var networkServiceIdentity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
//            var systemIdentity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);

//            // NT Accounts
//            var iisUsersGroup = new NTAccount("IIS_IUSRS");
//            var appPoolIdentity = new NTAccount(@"IIS AppPool\DefaultAppPool");

//            // SID izinleri ekle
//            AddFilePermission(fileSecurity, usersIdentity);
//            AddFilePermission(fileSecurity, networkServiceIdentity);
//            AddFilePermission(fileSecurity, systemIdentity);

//            // NT Account izinleri ekle
//            AddFilePermissionNT(fileSecurity, iisUsersGroup);
//            AddFilePermissionNT(fileSecurity, appPoolIdentity);

//            fileInfo.SetAccessControl(fileSecurity);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error setting file permissions: {ex.Message}");
//            throw;
//        }
//    }

//    private static void AddFileFolderPermission(DirectorySecurity security, SecurityIdentifier identity)
//    {
//        var fileSystemRule = new FileSystemAccessRule(
//            identity,
//            FileSystemRights.FullControl,
//            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
//            PropagationFlags.None,
//            AccessControlType.Allow);

//        security.AddAccessRule(fileSystemRule);
//    }

//    private static void AddFileFolderPermissionNT(DirectorySecurity security, NTAccount account)
//    {
//        var fileSystemRule = new FileSystemAccessRule(
//            account,
//            FileSystemRights.FullControl,
//            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
//            PropagationFlags.None,
//            AccessControlType.Allow);

//        security.AddAccessRule(fileSystemRule);
//    }

//    private static void AddFilePermission(FileSecurity security, SecurityIdentifier identity)
//    {
//        security.AddAccessRule(new FileSystemAccessRule(
//            identity,
//            FileSystemRights.FullControl,
//            AccessControlType.Allow));
//    }

//    private static void AddFilePermissionNT(FileSecurity security, NTAccount account)
//    {
//        security.AddAccessRule(new FileSystemAccessRule(
//            account,
//            FileSystemRights.FullControl,
//            AccessControlType.Allow));
//    }
//}