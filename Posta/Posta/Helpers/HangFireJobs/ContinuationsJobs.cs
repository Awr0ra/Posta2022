namespace Posta.Helpers.HangFireJobs
{
    public class ContinuationsJobs
    {
        /// <summary>
        /// 1st part of job
        /// </summary>
        /// <param name="information"></param>
        public void Part1st(string information)
        {
            Console.WriteLine($"Run 1st part of job: '{information}'");
            Task.Delay(1000);
        }
        /// <summary>
        /// 2nd part of job
        /// </summary>
        /// <param name="information"></param>
        public void Part2nd(string information)
        {
            Console.WriteLine($"Run 2nd part of job: '{information}'");
        }
    }
}


