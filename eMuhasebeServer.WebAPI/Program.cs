using eMuhasebeServer.Application;
using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Domain.MailKitMimeKit.Service;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure;
using eMuhasebeServer.Infrastructure.Services;
using eMuhasebeServer.WebAPI.Controllers;
using eMuhasebeServer.WebAPI.HangFire;
using eMuhasebeServer.WebAPI.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using eMuhasebeServer.Domain.Utilities.ProgramCsModuls;




var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlServer")!;


var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomCorsPolicy",
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins(allowedOrigins ?? Array.Empty<string>())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition");
        });
});


builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(connectionString);
});

builder.Services.AddHangfireServer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
    });
});


//builder.Services.AddSignalR();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
});

builder.Services.AddScoped<IDocumentHub, DocumentHub>();
builder.Services.AddScoped<ITxtSender, TxtSender>();

builder.Services.AddTransient<IZipService, ZipService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IWordService, WordService>();


builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IForgotPasswordEmailService, ForgotPasswordEmailService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();
builder.Services.AddDirectoryBrowser();


// SignalR detaylý hata ayýklama için
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
});

// Logging konfigürasyonu
builder.Logging.ClearProviders(); // Mevcut providers'larý temizle
builder.Logging.AddConsole(); // Console logging ekle
builder.Logging.AddDebug();   // Debug logging ekle

// Minimum log seviyesini ayarla
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// SignalR için spesifik log filtreleri
builder.Logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);


// IIS Integration loglarý için
builder.Logging.AddFilter("Microsoft.AspNetCore.Hosting.Diagnostics", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Server.IIS", LogLevel.Debug);

if (OperatingSystem.IsWindows()) // Windows kontrolü
{
    builder.Logging.AddEventLog();// Windows Event Log için
}

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("CustomCorsPolicy");
app.UseHangfireDashboard("/hangfire");
HangFireTestServices.ConfigureScheduledJobs();
app.UseHttpsRedirection();
app.UseStaticFiles();

var fileStoragePath = app.Environment.IsDevelopment()
    ? builder.Configuration.GetValue<string>("FileStorage:BasePath4")
    : builder.Configuration.GetValue<string>("FileStorage:BasePath4");



var basePath5 = builder.Configuration.GetValue<string>("FileStorage:BasePath5");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, basePath5!)),
    RequestPath = "/aaaStaticFiles",
    ServeUnknownFileTypes = true,
    DefaultContentType = "text/plain",
    ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
    {
        { ".txt", "text/plain" },
        { ".lsp", "text/plain" },
        { ".log", "text/plain" },
        { ".ini", "text/plain" },
        { ".csv", "text/csv" },
        { ".cfg", "text/plain" },
        { ".json", "application/json" },
        { ".xml", "application/xml" },
        { ".htm", "text/html" },
        { ".html", "text/html" },

        { ".js", "text/javascript" },
        { ".ts", "text/typescript" },
        { ".css", "text/css" },
        { ".cpp", "text/plain" },
        { ".c", "text/plain" },
        { ".py", "text/plain" },
        { ".java", "text/plain" },
        { ".php", "text/plain" },
        { ".sql", "text/plain" },

        { ".pdf", "application/pdf" },
        { ".doc", "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".ppt", "application/vnd.ms-powerpoint" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".rtf", "application/rtf" },

        { ".png", "image/png" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif", "image/gif" },
        { ".bmp", "image/bmp" },
        { ".ico", "image/x-icon" },
        { ".svg", "image/svg+xml" },
        { ".webp", "image/webp" },

        { ".zip", "application/zip" },
        { ".rar", "application/x-rar-compressed" },
        { ".7z", "application/x-7z-compressed" },
        { ".gz", "application/gzip" },
        { ".tar", "application/x-tar" },

        { ".dwg", "application/acad" },
        { ".dxf", "application/dxf" },
        { ".dgn", "application/x-dgn" },
        { ".rvt", "application/octet-stream" },
        { ".rfa", "application/octet-stream" },
        { ".ifc", "application/x-step" },

        { ".exe", "application/octet-stream" },
        { ".dll", "application/octet-stream" },
        { ".mp3", "audio/mpeg" },
        { ".mp4", "video/mp4" },
        { ".wav", "audio/wav" },
        { ".avi", "video/x-msvideo" },
        { ".woff", "font/woff" },
        { ".woff2", "font/woff2" },
        { ".ttf", "font/ttf" },
        { ".eot", "application/vnd.ms-fontobject" }
    }),
    //OnPrepareResponse = ctx =>
    //{
    //    ctx.Context.Response.Headers.Append(
    //        "Access-Control-Allow-Origin", "*");
    //    ctx.Context.Response.Headers.Append(
    //        "Access-Control-Allow-Headers",
    //        "Origin, X-Requested-With, Content-Type, Accept");
    //}
    OnPrepareResponse = ctx =>
    {
        // Burayý deðiþtirin - wildcard (*) yerine spesifik origin kullanýn
        var origin = ctx.Context.Request.Headers["Origin"].ToString();
        if (allowedOrigins?.Contains(origin) == true)
        {
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
        }

        ctx.Context.Response.Headers.Append(
            "Access-Control-Allow-Headers",
            "Origin, X-Requested-With, Content-Type, Accept");
    }
});


if (OperatingSystem.IsWindows())
{
    if (app.Environment.IsDevelopment())
    {
        DirectoryPermissionArsivDevelopment.ConfigureArsivPermissions();
        StaticFileDirectoryCreatorDevelopment.ConfigureF1Menu7StaticFiles(app, builder.Configuration);
    }
    else
    {
        DirectoryPermissionArsivProduct.ConfigureArsivPermissions();
        StaticFileDirectoryCreatorProduct.ConfigureF1Menu7StaticFiles(app, builder.Configuration);
    }
}
    



//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(builder.Environment.ContentRootPath, "aaaStaticFiles", "f1menu7")),
//    RequestPath = "/StaticFiles/f1menu7"
//});


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(fileStoragePath!),
    RequestPath = "/ExcelFiles",
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
});

app.UseExceptionHandler();
app.MapControllers();
app.MapHub<DocumentHub>("/documentHub");
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/DocumentViewer/SaveDropZoneOneFile"),
    appBuilder =>
    {
        appBuilder.Use((context, next) =>
        {
            var middleware = new RequestRateLimitingMiddleware(next, 250 * 1024);
            return middleware.InvokeAsync(context);
        });
    });
ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
