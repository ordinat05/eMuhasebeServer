using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Runtime.Versioning;

namespace eMuhasebeServer.Domain.Utilities.ProgramCsModuls;

[SupportedOSPlatform("windows")]
public static class StaticFileDirectoryCreatorDevelopment
{
    public static void ConfigureF1Menu7StaticFiles(WebApplication app, IConfiguration configuration)
    {
        // basePath7 Kaynak Klasör Path sourceBasePath
        // basePath8 Hedef Klasör Path targetBasePath
        string basePath = app.Environment.ContentRootPath;
        string folderName = "f1menu7";

        DirectoryPermissionHelperDevelopment.EnsureDirectoryWithPermissions(basePath, folderName);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(basePath, "aaaStaticFiles", folderName)),
            RequestPath = "/StaticFiles/f1menu7"
        });
    }
}