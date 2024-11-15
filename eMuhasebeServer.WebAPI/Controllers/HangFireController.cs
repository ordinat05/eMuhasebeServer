using eMuhasebeServer.Domain.ModelDto.Hangfire;
using eMuhasebeServer.WebAPI.HangFire;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        #region Enqueue test
        //[HttpGet]
        [HttpGet("get")]
        public IActionResult Get()
        {
            BackgroundJob.Enqueue(() => HangFireTestServices.Test());
            return Ok("Hangfire IActionResult Get() çalıştı");
        }
        #endregion

        #region teksefer Enqueue
        [HttpGet("teksefer")]
        public IActionResult Teksefer()
        {

            string message = $"***Tek Seferlik Çalışan Metod*** {DateTime.Now:U}";
            MyHangFire.MyTxtJobs_Enqueue(message);

            Console.WriteLine(message);
            return Ok($"İşlem başarıyla tamamlandı. ***Tek Seferlik Çalışan Metod*** Konsola yazdırılan mesaj: {message}");

        }
        #endregion

        #region zamanlanmisGorev Schedule
        [HttpGet("zamanlanmisGorev")]
        public IActionResult ZamanlanmisGorev()
        {
            string message = $"***ZamanlanmisGorev Metod*** {DateTime.Now:U}";
            MyHangFire.MyTxtJobs_Schedule(message);

            Console.WriteLine(message);
            return Ok($"İşlem başarıyla tamamlandı. ***ZamanlanmisGorev Metod*** Konsola yazdırılan mesaj: {message}");
        }
        #endregion

        #region Vagon Görev ContinueJobWith
        [HttpGet("vagonGorev")]
        public IActionResult VagonGorev()
        {
            string message = $"***Schedule Vagon Görev Metod*** {DateTime.Now:U}";
            MyHangFire.MyTxtJobs_ContinueJobWith(message);

            Console.WriteLine(message);
            return Ok($"İşlem başarıyla tamamlandı. ***Schedule Vagon Görev*** Konsola yazdırılan mesaj: {message}");
        }
        #endregion

        #region tekrarliGorev RecurringJob_AddOrUpdate
        [HttpGet("tekrarliGorev")]
        public IActionResult TekrarliGorev()
        {
            string message = $"***RecurringJob Tekrarlı Görev Metod*** {DateTime.Now:U}";
            MyHangFire.MyTxtJobs_RecurringJob_AddOrUpdate(message);

            Console.WriteLine(message);
            return Ok($"İşlem başarıyla tamamlandı. ***Schedule Vagon Görev*** Konsola yazdırılan mesaj: {message}");
        }
        #endregion

        #region planliTekrarliGorevTest  (Schedule + ContinueJobWith + RecurringJob_AddOrUpdate)
        [HttpGet("planliTekrarliGorev")]
        public IActionResult PlanliTekrarliGorev()
        {
            string jobId = "bunaIdGeneratorKullanılabilir";
            DateTime startDate = new DateTime(2024, 08, 25, 02, 10, 0); // {24.08.2024 23:53:00}
            TimeSpan interval = TimeSpan.FromMinutes(1); // {00:01:00}
            string message = $"IActionResult içerisindeki message {DateTime.Now:U}";

            // Hangfire.cs metoduna dataları gönder.
            MyHangFire.PlanliTekrarliGorevMetod(jobId, startDate, interval, message);

            return Ok($"RETURN IActionResult Uygulandı.");
        }
        #endregion

        #region planliTekrarliGorevFrontend (Schedule + ContinueJobWith + RecurringJob_AddOrUpdate)
        [HttpPost("planliTekrarliGorevFrontend")]
        public IActionResult PlanliTekrarliGorevFrontend([FromBody] ScheduleJobRequest request)
        {
            // Request'ten gelen verileri kullan
            string jobId = request.JobId ?? "DefaultJobId";
            DateTime startDate = request.StartDate; //2024-10-24T19:38:00.000Z
            TimeSpan interval = TimeSpan.FromMinutes(request.Interval); // {00:01:00}
            string message = request.Message ?? "Default Message";

            // Hangfire.cs metoduna dataları gönder
            MyHangFire.PlanliTekrarliGorevMetod(jobId, startDate, interval, message);

            return Ok($"RETURN Haftalık Tekrarlı Görev {startDate} tarihinde başlatılmak üzere planlandı.");
        }
        #endregion

        #region Kayıtlı İşlerin çekilmesi
        [HttpGet("listJobs")]
        public IActionResult ListJobs()
        {
            var jobList = new List<JobInfo>();

            using (var connection = JobStorage.Current.GetConnection())
            {
                var monitor = JobStorage.Current.GetMonitoringApi();


                //Enqueued      Enqueue Tek işlem gönder
                //Scheduled     Zamanlanmış işler           VAR
                //Processing    Aktif işler                 VAR
                //Succeeded     tamamlanmış işler       
                //Failed        hatalı durumundakiler
                //Deleted       silinmişler
                //Awaiting      beklemedekiler ?    
                //Recurring     Tekrarlı Gorevler           VAR

                // Tekrarlanan işleri al
                var recurringJobs = JobStorage.Current.GetConnection().GetRecurringJobs();
                foreach (var job in recurringJobs)
                {
                    jobList.Add(new JobInfo
                    {
                        Id = job.Id,
                        Type = "Recurring",
                        CreatedAt = job.CreatedAt,
                        NextExecution = job.NextExecution
                    });
                }

                // Zamanlanmış işleri al
                var scheduledJobs = monitor.ScheduledJobs(0, int.MaxValue);
                foreach (var job in scheduledJobs)
                {
                    jobList.Add(new JobInfo
                    {
                        Id = job.Key,
                        Type = "Scheduled",
                        CreatedAt = null, // ScheduledJobDto doesn't have CreatedAt
                        NextExecution = job.Value.EnqueueAt
                    });
                }

                // Aktif işleri al
                var processingJobs = monitor.ProcessingJobs(0, int.MaxValue);
                foreach (var job in processingJobs)
                {
                    jobList.Add(new JobInfo
                    {
                        Id = job.Key,
                        Type = "Processing",
                        CreatedAt = job.Value.StartedAt,
                        NextExecution = null
                    });
                }
                // Beklemedeki (Enqueued) işleri al
                var enqueuedJobs = monitor.EnqueuedJobs("default", 0, int.MaxValue);
                foreach (var job in enqueuedJobs)
                {
                    jobList.Add(new JobInfo
                    {
                        Id = job.Key,
                        Type = "Enqueued",
                        CreatedAt = job.Value.EnqueuedAt,
                        NextExecution = null
                    });
                }
            }

            return Ok(jobList);
        }
        #endregion

        #region planliTekrarliGorevFrontendReturnJobId (Schedule + ContinueJobWith + RecurringJob_AddOrUpdate)
        [HttpPost("planliTekrarliGorevFrontendReturnJobId")]
        public IActionResult PlanliTekrarliGorevFrontendReturnJobId([FromBody] ScheduleJobRequestReturnJobId request)
        {
            string jobId = request.JobId ?? "DefaultJobId";
            DateTime startDate = request.StartDate;
            TimeSpan interval = TimeSpan.FromMinutes(request.Interval);
            string message = request.Message ?? "Default Message";
            string token = request.Token ?? "";
            var jobIds = MyHangFire.PlanliTekrarliGorevMetodReturnJobId(jobId, startDate, interval, message);
            return Ok(new
            {
                JobIds = jobIds,
                Request = request  // ScheduleJobRequestReturnJobId'yi de döndürüyoruz
            });
        }
        #endregion
        #region DeleteJob
        [HttpPost("deleteJob")]
        public IActionResult DeleteJob([FromBody] DeleteJobRequest request)
        {
            try
            {
                bool isDeleted = false;

                switch (request.JobIdType)
                {
                    case "Scheduled":
                        isDeleted = BackgroundJob.Delete(request.JobId);
                        break;
                    case "Recurring":
                        RecurringJob.RemoveIfExists(request.JobId);
                        isDeleted = true; // RemoveIfExists doesn't return a boolean, so we assume success
                        break;
                    case "Continuation":
                        // Continuation jobs can't be deleted directly, but we can try to delete the associated background job
                        isDeleted = BackgroundJob.Delete(request.JobId);
                        break;
                    default:
                        return BadRequest($"Unsupported job type: {request.JobIdType}");
                }

                if (isDeleted)
                {
                    return Ok(new { Message = $"Job with ID {request.JobId} of type {request.JobIdType} has been deleted successfully." });
                }
                else
                {
                    return NotFound($"Job with ID {request.JobId} of type {request.JobIdType} was not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the job: {ex.Message}");
            }
        }
        #endregion
    }


    #region TestService buranın değil. Ayrı olması lazım. Kullanılmıyor.

    public class HangFireTestServices
    {
        public static void Test()
        {
            Console.WriteLine("HangFire şimdi çalışıyor : " + DateTime.Now);
        }
        public class HangfireJobs
        {
            public static void ScheduleTest()
            {
                // Şimdiden 20 saniye sonra çalışacak şekilde planla
                BackgroundJob.Schedule(
                    () => Test2(),
                    TimeSpan.FromSeconds(20)
                );
            }

            public static void Test2()
            {
                Console.WriteLine("HangFire 20 saniye sonra çalışıyor : " + DateTime.Now);
            }
        }

        public static void ConfigureScheduledJobs()
        {
            ScheduleJob(new DateTime(2024, 8, 22, 22, 0, 15), "HangFire tarih 22.08.2024 saat 22:00:15 de çalıştı...");
            ScheduleJob(new DateTime(2024, 8, 22, 22, 10, 5), "HangFire tarih 22.08.2024 saat 22:10:05 de çalıştı...");
        }

        private static void ScheduleJob(DateTime scheduledTime, string message)
        {
            var delay = scheduledTime - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                BackgroundJob.Schedule(
                    () => RunScheduledTask(message),
                    delay
                );
                Console.WriteLine($"Görev planlandı: {scheduledTime}");
            }
            else
            {
                Console.WriteLine($"Belirtilen tarih geçmiş: {scheduledTime}. Lütfen gelecekte bir tarih belirleyin.");
            }
        }

        public static void RunScheduledTask(string message)
        {
            Console.WriteLine(message);
        }






    }

    #endregion


    //#region JobInfo Kayıtlı İşlerin çekilmesi Actionu için Model
    //public class JobInfo
    //{
    //    public string? Id { get; set; }
    //    public string? Type { get; set; }
    //    public DateTime? CreatedAt { get; set; }
    //    public DateTime? NextExecution { get; set; }
    //}
    //#endregion


    //#region Frontend den planliTekrarliGorevFrontend Actionu için model
    //public class ScheduleJobRequest
    //{
    //    public string? JobId { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public int Interval { get; set; }
    //    public string? Message { get; set; }
    //}
    //#endregion

    //#region Frontend den planliTekrarliGorevFrontend Actionu için model
    //public class ScheduleJobRequestReturnJobId
    //{
    //    public string? JobId { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public int Interval { get; set; }
    //    public string? Message { get; set; }
    //}
    //#endregion

    public class DeleteJobRequest
    {
        public string? JobId { get; set; }
        public string? JobIdType { get; set; }
    }
}
