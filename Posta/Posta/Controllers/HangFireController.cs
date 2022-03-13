using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Posta.Helpers.HangFireJobs;

namespace Posta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangFireController : ControllerBase
    {
        [HttpPost]
        [Route("fire-and-forget")]
        public IActionResult FireAndForget(string jobName = "AWR FireAndForget job")
        {
            var jodId = BackgroundJob.Enqueue(() => Console.WriteLine($"Run job: '{jobName}'"));
            return Ok($"Job ID = {jodId}");
        }

        [HttpPost]
        [Route("delayed")]
        public IActionResult Delayed(string jobName = "AWR Delayed job")
        {
            //var jodId = BackgroundJob.Schedule(() => DelayedJob(jobName), TimeSpan.FromSeconds(60));
            var jodId = BackgroundJob.Schedule(() => Console.WriteLine($"Run delayed job: '{jobName}'"), TimeSpan.FromSeconds(60));
            return Ok($"Job ID = {jodId}");
        }
 
        [HttpPost]
        [Route("continuations")]
        public IActionResult Continuations(string jobName = "AWR Continuations job")
        {
            ContinuationsJobs jobs = new ContinuationsJobs();

            var jodId1st = BackgroundJob.Enqueue(() => jobs.Part1st(jobName));
            BackgroundJob.ContinueJobWith(jodId1st, () => jobs.Part1st(jodId1st +" -> "+ jobName));

            return Ok($"Job ID = {jodId1st}");
        }

        
    }
}
