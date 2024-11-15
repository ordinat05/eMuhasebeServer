using System.IO;

namespace eMuhasebeServer.WebAPI.HangFire
{
    public class TxtSender : ITxtSender
    {
        public async Task Sender(string msg)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var file = $"{workingDirectory}\\hangfire.txt";
            if (!File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file))
                {
                    await sw.WriteLineAsync(msg);
                }
            }
            else
            {
                StreamWriter sw = File.AppendText(file);
                await sw.WriteLineAsync(msg);
                sw.Close();
            }
        }
    }
}
