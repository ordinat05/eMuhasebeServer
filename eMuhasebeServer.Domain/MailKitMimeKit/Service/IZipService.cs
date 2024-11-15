public interface IZipService
{
    List<string> CreateMultiPartZip(string sourceFolder, string destinationFolder, long maxPartSize);

}



