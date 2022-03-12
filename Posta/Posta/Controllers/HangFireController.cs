using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Posta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangFireController : ControllerBase
    {
        [HttpPost]
        [Route("fire-and-forget")]
        public IActionResult FireAndForget(string jobName)
        {
            var jodId = BackgroundJob.Enqueue(() => Console.WriteLine($"Run job: '{jobName}'"));
            return Ok($"Job ID = {jodId}");
        }
    }
}
