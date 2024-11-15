using eMuhasebeServer.Domain.MailKitMimeKit.Service;
using System;
using System.Threading.Tasks;

public class ScheduleService : IScheduleService
{
    public async Task ScheduleEmailAsync(DateTime scheduledTime)
    {
        var currentTime = DateTime.Now;

        if (scheduledTime <= currentTime)
        {
            throw new ArgumentException("Belirtilen tarih ve saat geçmiş bir zamana ait.");
        }

        var delay = scheduledTime - currentTime;

        await Task.Run(async () =>
        {
            await Task.Delay(delay);
            await zamangeldi();
        });
    }

    private Task zamangeldi()
    {
        // zamangeldi() fonksiyonunun içeriğini buraya ekleyin
        Console.WriteLine($"zamangeldi() fonksiyonu çalıştırıldı. Zaman: {DateTime.Now}");
        return Task.CompletedTask;
    }
}
