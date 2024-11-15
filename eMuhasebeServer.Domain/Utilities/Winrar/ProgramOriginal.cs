// ------------ Winrar Metod sourcePath, targetPath, splitParts , Return START ⭐⭐⭐✳✳✳✨✨✨💫💫💫 ------------------------------------------

//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//class ProgramOriginal
//{
//    static async Task Main(string[] args)
//    {
//        while (true)
//        {
//            Console.WriteLine("1 - WinRAR komutunu çalıştır");
//            Console.WriteLine("Q - Çıkış");
//            Console.Write("Seçiminiz: ");

//            var key = Console.ReadKey(true);
//            Console.WriteLine();

//            if (key.KeyChar == '1')
//            {
//                Console.Write("Kaynak dosya yolu: ");
//                string sourcePath = Console.ReadLine();

//                Console.Write("Hedef dosya yolu: ");
//                string targetPath = Console.ReadLine();

//                Console.Write("Bölünecek parça boyutu (MB): ");
//                if (int.TryParse(Console.ReadLine(), out int splitParts))
//                {
//                    string result = await RunWinRARCommandAsync(sourcePath, targetPath, splitParts);
//                    Console.WriteLine(result);
//                }
//                else
//                {
//                    Console.WriteLine("Geçersiz parça boyutu. Lütfen bir sayı girin.");
//                }
//            }
//            else if (key.Key == ConsoleKey.Q)
//            {
//                break;
//            }
//            else
//            {
//                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
//            }

//            Console.WriteLine();
//        }
//    }

//    static async Task<string> RunWinRARCommandAsync(string sourcePath, string targetPath, int splitParts)
//    {
//        string winRarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
//        string arguments = $@"a -cfg- -ep1 -idcdp -m25 -md4096 -s -v{splitParts}M ""{targetPath}"" ""{sourcePath}""";

//        try
//        {
//            ProcessStartInfo psi = new ProcessStartInfo(winRarPath, arguments)
//            {
//                CreateNoWindow = true,
//                UseShellExecute = false,
//                RedirectStandardOutput = true,
//                RedirectStandardError = true
//            };

//            using (Process process = new Process { StartInfo = psi })
//            {
//                process.Start();

//                string output = await process.StandardOutput.ReadToEndAsync();
//                string error = await process.StandardError.ReadToEndAsync();

//                await process.WaitForExitAsync();

//                if (process.ExitCode == 0)
//                {
//                    return "SIKIŞTIRMA işlemi başarıyla tamamlandı.";
//                }
//                else
//                {
//                    return $"SIKIŞTIRMA işlemi sırasında bir hata oluştu. Hata kodu: {process.ExitCode}\nHata mesajı: {error}";
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            return $"Bir hata oluştu: {ex.Message}";
//        }
//    }
//}

// ------------ Winrar Metod sourcePath, targetPath, splitParts , Return END ✨✨✨ ------------------------------------------




// --------------- Winrar Script Return START ✨✨ ----------------------------------------------------------------------------



//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        while (true)
//        {
//            Console.WriteLine("1 - WinRAR komutunu çalıştır");
//            Console.WriteLine("Q - Çıkış");
//            Console.Write("Seçiminiz: ");

//            var key = Console.ReadKey(true);
//            Console.WriteLine();

//            if (key.KeyChar == '1')
//            {
//                string result = await RunWinRARCommandAsync();
//                Console.WriteLine(result);
//            }
//            else if (key.Key == ConsoleKey.Q)
//            {
//                break;
//            }
//            else
//            {
//                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
//            }

//            Console.WriteLine();
//        }
//    }

//    static async Task<string> RunWinRARCommandAsync()
//    {
//        string winRarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
//        string arguments = @"a -cfg- -ep1 -idcdp -m25 -md4096 -s -v25M C:\zzzmailicinhazir\istanbul.rar C:\zzzsikistirFiles\istanbul.dwg";

//        try
//        {
//            ProcessStartInfo psi = new ProcessStartInfo(winRarPath, arguments)
//            {
//                CreateNoWindow = true,
//                UseShellExecute = false,
//                RedirectStandardOutput = true,
//                RedirectStandardError = true
//            };

//            using (Process process = new Process { StartInfo = psi })
//            {
//                process.Start();

//                string output = await process.StandardOutput.ReadToEndAsync();
//                string error = await process.StandardError.ReadToEndAsync();

//                await process.WaitForExitAsync();

//                if (process.ExitCode == 0)
//                {
//                    return "SIKIŞTIRMA işlemi başarıyla tamamlandı.";
//                }
//                else
//                {
//                    return $"SIKIŞTIRMA işlemi sırasında bir hata oluştu. Hata kodu: {process.ExitCode}\nHata mesajı: {error}";
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            return $"Bir hata oluştu: {ex.Message}";
//        }
//    }
//}

// --------------- Winrar Script Return END ✨✨ ----------------------------------------------------------------------------


// --------------- Winrar Script .net Core Test START ✨✨ ----------------------------------------------------------------------------


//using System;
//using System.Diagnostics;

//class Program
//{
//    static void Main(string[] args)
//    {
//        while (true)
//        {
//            Console.WriteLine("1 - WinRAR komutunu çalıştır");
//            Console.WriteLine("Q - Çıkış");
//            Console.Write("Seçiminiz: ");

//            var key = Console.ReadKey(true);
//            Console.WriteLine();

//            if (key.KeyChar == '1')
//            {
//                RunWinRARCommand();
//            }
//            else if (key.Key == ConsoleKey.Q)
//            {
//                break;
//            }
//            else
//            {
//                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
//            }

//            Console.WriteLine();
//        }
//    }

//    static void RunWinRARCommand()
//    {
//        string winRarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
//        string arguments = @"a -cfg- -ep1 -idcdp -m25 -md4096 -s -v5M C:\zzzmailicinhazir\istanbul.rar C:\zzzsikistirFiles\istanbul.dwg";

//        try
//        {
//            ProcessStartInfo psi = new ProcessStartInfo(winRarPath, arguments)
//            {
//                CreateNoWindow = true,
//                UseShellExecute = false,
//                RedirectStandardOutput = true,
//                RedirectStandardError = true
//            };

//            using (Process process = Process.Start(psi))
//            {
//                string output = process.StandardOutput.ReadToEnd();
//                string error = process.StandardError.ReadToEnd();

//                process.WaitForExit();

//                if (process.ExitCode == 0)
//                {
//                    Console.WriteLine("WinRAR komutu başarıyla çalıştırıldı.");
//                }
//                else
//                {
//                    Console.WriteLine($"WinRAR komutu çalıştırılırken bir hata oluştu. Hata kodu: {process.ExitCode}");
//                    if (!string.IsNullOrEmpty(error))
//                    {
//                        Console.WriteLine($"Hata mesajı: {error}");
//                    }
//                }

//                if (!string.IsNullOrEmpty(output))
//                {
//                    Console.WriteLine($"Çıktı: {output}");
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
//        }
//    }
//}

// --------------- Winrar Script .net Core Test START ✨✨ ----------------------------------------------------------------------------
