using Microsoft.Extensions.Configuration;

namespace eMuhasebeServer.Domain.Utilities.FileStorageBasePath;

public static class FileStorageBasePaths
{
    private static IConfiguration Configuration =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .Build();

    public static string BasePath =>
        Configuration.GetValue<string>("FileStorage:BasePath") ?? "Default/Path";

    public static string BasePath2 =>
        Configuration.GetValue<string>("FileStorage:BasePath2") ?? "Default/Path";
    public static string BasePath3 =>
        Configuration.GetValue<string>("FileStorage:BasePath3") ?? "Default/Path";
    public static string BasePath4 =>
        Configuration.GetValue<string>("FileStorage:BasePath4") ?? "Default/Path";
    public static string BasePath5 =>
        Configuration.GetValue<string>("FileStorage:BasePath5") ?? "Default/Path";
    public static string BasePath6 =>
        Configuration.GetValue<string>("FileStorage:BasePath6") ?? "Default/Path";
    public static string BasePath7 =>
        Configuration.GetValue<string>("FileStorage:BasePath7") ?? "Default/Path";
    public static string BasePath8 =>
        Configuration.GetValue<string>("FileStorage:BasePath8") ?? "Default/Path";

   
}