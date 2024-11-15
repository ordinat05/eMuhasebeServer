//---------------------------- Winrar işlemini başlatılacak yerde eklenecek KOD START ------------------------------

// WinRAR sıkıştırma işlemini başlat
//using eMuhasebeServer.Domain.Utilies.Winrar;

//var compressionResult = await WinrarMethod.CompressFileAsync();
//Console.WriteLine(compressionResult); // Sıkıştırma sonucunu logla

//---------------------------- Winrar işlemini başlatılacak yerde eklenecek KOD END ------------------------------



//---------------------------- Modelli ve Varsayılan Datalı Metod START ------------------------------

using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.Utilities.Winrar
{
    public class WinrarCompressionModel
    {
        public string SourcePath { get; set; } = @"C:\zzzsikistirFiles\istanbul.dwg";
        public string TargetPath { get; set; } = @"C:\zzzmailicinhazir\istanbul.rar";
        public int SplitParts { get; set; } = 5;
    }

    public class WinrarMethod
    {
        public static async Task<string> CompressFileAsync(WinrarCompressionModel? model = null)
        {
            model ??= new WinrarCompressionModel();

            //"C:\Program Files\WinRAR\WinRAR.exe" a -cfg- -ep1 -idcdp -m25 -md4096 -s -v5M "C:\zzzmailicinhazir\istanbul.rar" "C:\zzzsikistirFiles\istanbul.dwg"

            string winRarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
            string arguments = $@"a -cfg- -ep1 -idcdp -m25 -md4096 -s -v{model.SplitParts}M ""{model.TargetPath}"" ""{model.SourcePath}""";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(winRarPath, arguments)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync();

                    if (process.ExitCode == 0)
                    {
                        return "SIKIŞTIRMA işlemi başarıyla tamamlandı.";
                    }
                    else
                    {
                        return $"SIKIŞTIRMA işlemi sırasında bir hata oluştu. Hata kodu: {process.ExitCode}\nHata mesajı: {error}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Bir hata oluştu: {ex.Message}";
            }
        }
    }
}


//---------------------------- Modelli ve Varsayılan Datalı Metod END ------------------------------

//---------------------------- Modelsiz Metod START ------------------------------

//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace eMuhasebeServer.Domain.Utilies.Winrar
//{
//    public class WinrarMethod
//    {
//        public static async Task<string> CompressFileAsync(string sourcePath, string targetPath, int splitParts)
//        {
//            string winRarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
//            string arguments = $@"a -cfg- -ep1 -idcdp -m25 -md4096 -s -v{splitParts}M ""{targetPath}"" ""{sourcePath}""";

//            try
//            {
//                ProcessStartInfo psi = new ProcessStartInfo(winRarPath, arguments)
//                {
//                    CreateNoWindow = true,
//                    UseShellExecute = false,
//                    RedirectStandardOutput = true,
//                    RedirectStandardError = true
//                };

//                using (Process process = new Process { StartInfo = psi })
//                {
//                    process.Start();

//                    string output = await process.StandardOutput.ReadToEndAsync();
//                    string error = await process.StandardError.ReadToEndAsync();

//                    await process.WaitForExitAsync();

//                    if (process.ExitCode == 0)
//                    {
//                        return "SIKIŞTIRMA işlemi başarıyla tamamlandı.";
//                    }
//                    else
//                    {
//                        return $"SIKIŞTIRMA işlemi sırasında bir hata oluştu. Hata kodu: {process.ExitCode}\nHata mesajı: {error}";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                return $"Bir hata oluştu: {ex.Message}";
//            }
//        }
//    }
//}

//---------------------------- Modelsiz Metod END ------------------------------
