
using eMuhasebeServer.Domain.Utilities.ProgramCsModuls;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Runtime.Versioning;

[SupportedOSPlatform("windows")]
public static class StaticFileDirectoryCreatorProduct
{
    public static void ConfigureF1Menu7StaticFiles(WebApplication app, IConfiguration configuration)
    {
        // BasePath8'i configurasyondan al
        var targetBasePath = configuration.GetValue<string>("FileStorage:BasePath8");
        if (string.IsNullOrEmpty(targetBasePath))
        {
            throw new InvalidOperationException("FileStorage:BasePath8 configuration is missing");
        }

        // Klasörün var olduğundan emin ol ve izinleri ayarla
        DirectoryPermissionHelperProduct.EnsureDirectoryWithPermissions(
            Path.GetDirectoryName(targetBasePath)!,  // aaaStaticFiles klasörü
            Path.GetFileName(targetBasePath)!        // f1menu7 klasörü
        );

        // StaticFiles middleware'ini yapılandır
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(targetBasePath),
            RequestPath = "/aaaStaticFiles/f1menu7",
            ServeUnknownFileTypes = true,
            DefaultContentType = "application/octet-stream"
        });
    }
}

