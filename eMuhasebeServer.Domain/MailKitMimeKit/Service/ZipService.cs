using eMuhasebeServer.Domain.MailKitMimeKit.Service;
using Ionic.Zip;
using System;
using System.IO;
using eMuhasebeServer.Domain.Utilities.DirectoryPermissionUtility;


public class ZipService : IZipService
{
    public List<string> CreateMultiPartZip(string sourceFolder, string destinationFolder, long maxPartSize)
    {
        List<string> sourceFileNames = new List<string>();
        List<string> zipFileNames = new List<string>();

        try
        {
            if (!Directory.Exists(sourceFolder))
            {
                throw new DirectoryNotFoundException($"Source folder not found: {sourceFolder}");
            }

            // Ensure the destination folder exists
            if (!Directory.Exists(destinationFolder))
            {
                //Directory.CreateDirectory(destinationFolder);
                CreateDirectoryPermissionUtility.SetFolderPermissions(destinationFolder);
            }

            // Get all file names in the source folder
            sourceFileNames = Directory.GetFiles(sourceFolder)
                                       .Select(Path.GetFileName)
                                       .Where(name => name != null) 
                                       .ToList()!;
            string fileNamesString = sourceFileNames != null ? string.Join(" - ", sourceFileNames) : "No files found.";

            int partCounter = 1;
            string mergeName;

            if (fileNamesString.Length > 200)
            {
                // Dosya isimleri 200 karakterden uzunsa
                mergeName = $"{fileNamesString.Substring(0, 200)} Part";
            }
            else
            {
                // Dosya isimleri 200 karakterden kısaysa
                mergeName = $"{fileNamesString} Part";
            }
            //string mergeName = $"{fileNamesString} Part";
            //string zipFileName = Path.Combine(destinationFolder, $"{mergeName}.zip");
            partCounter++;

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(sourceFolder);
                zip.MaxOutputSegmentSize = (int)maxPartSize;
                string zipFileName = Path.Combine(destinationFolder, $"{mergeName}.zip");
                //zip.Save(Path.Combine(destinationFolder, "Archive.zip"));

                zip.Save(zipFileName);
                //BToBeSentZippedFiles klasöründe, Burada oluşturulan dosyaların sayısını alacağız ve Event e gönderip , sırayla Bu kadar adet mail atacaksın diyeceğiz. ---!!!!---!!!!---!!!!---!!!!---!!!!
                // Mailler gönderildikten sonra da dosyaları sileceğiz.
            }
        }
        catch (Exception ex)
        {
            // Log or handle exceptions as needed
            throw new Exception($"An error occurred while creating the zip file: {ex.Message}", ex);
        }
        if (sourceFileNames == null)
        {
            throw new Exception("Source folder contains no files.");
        }
        return sourceFileNames;
    }
}

