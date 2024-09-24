using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCapTest.Web.Controllers
{
    [Route("api/stress-test")]
    [ApiController]
    public class StressTestController : ControllerBase
    {
        [HttpGet("cpu")]
        public IActionResult StressTestCpu(CancellationToken ct)
        {
            long sum = 0;


            for (int i = 0; i < 10_000; i++)
            {
                _ = Task.Factory.StartNew(() =>
                {
                    // Simulate a CPU-bound loop that takes time to complete
                    for (long i = 0; i < 1_000_000_000; i++)
                    {
                        sum += i;
                    }
                });
            }

            return Ok();
        }

        [HttpGet("non-block")]
        public ActionResult<string> NonBlockingTask()
        {
            Console.WriteLine($"Non-blocking request executed on thread {Thread.CurrentThread.ManagedThreadId}");
            return Ok("Non-blocking operation completed.");
        }
    }
}
