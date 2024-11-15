using eMuhasebeServer.Domain.ModelDto.Hangfire;
using Hangfire;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace eMuhasebeServer.WebAPI.HangFire
{
    public class MyHangFire
    {
        #region Enqueue Tek işlem gönder başarılı.    
        public static void MyTxtJobs_Enqueue(string msg)
        {
            Hangfire.BackgroundJob.Enqueue<ITxtSender>(a => a.Sender(msg));

        }
        #endregion


        #region Schedule Belirlediğimiz (Zamandan sonra çalışacak) / (Zamanda çalışacak).
        public static void MyTxtJobs_Schedule(string msg)
        {
            Hangfire.BackgroundJob.Schedule<ITxtSender>(a => a.Sender(msg), System.TimeSpan.FromSeconds(15));
        }
        #endregion


        #region ContinueJobWith Referans aldığı metod çalışınca çalışır.VAGON
        public static void MyTxtJobs_ContinueJobWith(string msg)
        {
            var jobsId = Hangfire.BackgroundJob.Schedule<ITxtSender>(a => a.Sender("Schedule Tren Görev "), System.TimeSpan.FromSeconds(15));
            //Aşağıdakini Test edebilmek için yukarıdakini yazdık. BuradaId si alınabilir.
            Hangfire.BackgroundJob.ContinueJobWith<ITxtSender>(jobsId, a => a.Sender("ContinueJobWith Vagon"));
        }
        #endregion


        #region RecurringJob.AddOrUpdate  Belirlediğimiz aralıklar içerisinde çalışıyor.
        public static void MyTxtJobs_RecurringJob_AddOrUpdate(string msg)
        {
            Hangfire.RecurringJob.AddOrUpdate<ITxtSender>(
                    recurringJobId: "UniqueRecurringJobId", // Benzersiz bir iş kimliği
                    methodCall: a => a.Sender("RecurringJob Tekrarlı Görev"),
                    //cronExpression: Cron.Minutely,
                     cronExpression: "*/1 * * * * *", // Her 5 saniyede bir çalışır
                    options: new RecurringJobOptions()
            );
        }
        #endregion


        #region PlanliTekrarliGorevMetod (Schedule + ContinueJobWith + RecurringJob_AddOrUpdate)
        public static void PlanliTekrarliGorevMetod(string jobId, DateTime startDate, TimeSpan interval, string message)
        {
            //BackgroundJob.Schedule<ITxtSender>(a => a.Sender(message), TimeSpan.FromSeconds(15));
            var scheduledJobId = BackgroundJob.Schedule<ITxtSender>(
          a => a.Sender(message),
          startDate
      );
            Console.WriteLine($"//MyHangFire.cs//PlanliTekrarliGorevMetod Job ID: {jobId}, Başlangıç Tarihi: {startDate}, Mesaj: {message}");

            // "Scheduler Treni" 'ne "ContinueJobWith Vagon" ile "RecurringJob" bağlantısı oluşturuluyor.
            // Vagon içine RecurringJob.AddOrUpdate. Tekrarlı işi ekliyor.
            BackgroundJob.ContinueJobWith(
                scheduledJobId,
                () => SetupRecurringJob(jobId, interval, message)
            );
        }

        public static void SetupRecurringJob(string jobId, TimeSpan interval, string message)
        {
            // Cron ifadesi oluştur 
            string cronExpression = $"*/{(int)interval.TotalMinutes} * * * *";

            // Tekrarlanan işi oluştur veya güncelle
            RecurringJob.AddOrUpdate<ITxtSender>(
                jobId,
                txtSender => txtSender.Sender($"PlanliTekrarliGorevMetod : {DateTime.Now:U} - Mesaj: {message}"),
                cronExpression
            );
            Console.WriteLine($"Tekrarlı iş EKLENDİ Job ID: {jobId}, Interval: {interval.TotalMinutes} minutes");

        }

        #endregion


        #region PlanliTekrarliGorevMetodReturnJobId (Schedule + ContinueJobWith + RecurringJob_AddOrUpdate)
        public static JobIdResponse PlanliTekrarliGorevMetodReturnJobId(string jobId, DateTime startDate, TimeSpan interval, string message)
        {
            var response = new JobIdResponse();
            // Schedule job
            var scheduledJobId = BackgroundJob.Schedule<ITxtSender>(
                a => a.Sender(message),
                startDate
            );
            response.ScheduledJobId = scheduledJobId;
            Console.WriteLine($"//MyHangFire.cs//PlanliTekrarliGorevMetod Scheduled Job ID: {scheduledJobId}, Başlangıç Tarihi: {startDate}, Mesaj: {message}");
            // Continue job with
            var continuationJobId = BackgroundJob.ContinueJobWith(
                scheduledJobId,
                () => SetupRecurringJobReturnJobId(jobId, interval, message)
            );
            response.ContinuationJobId = continuationJobId;
            Console.WriteLine($"Continuation Job ID: {continuationJobId}");
            // RecurringJob ID is the same as the provided jobId
            response.RecurringJobId = jobId;
            return response;
        }

        public static string SetupRecurringJobReturnJobId(string jobId, TimeSpan interval, string message)
        {
            string cronExpression = $"*/{(int)interval.TotalMinutes} * * * *";
            RecurringJob.AddOrUpdate<ITxtSender>(
                jobId,
                txtSender => txtSender.Sender($"PlanliTekrarliGorevMetod : {DateTime.Now:U} - Mesaj: {message}"),
                cronExpression
            );
            Console.WriteLine($"Tekrarlı iş EKLENDİ Job ID: {jobId}, Interval: {interval.TotalMinutes} minutes");
            return jobId;
        }
        #endregion
    }
}

