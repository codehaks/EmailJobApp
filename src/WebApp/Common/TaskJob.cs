using System.Collections.Concurrent;

namespace WebApp
{
    public class TaskJob : ITaskJob
    {
        public TaskJob()
        {
            Queue= new ConcurrentQueue<string>();
        }
        public ConcurrentQueue<string> Queue { get; set; } 
    }
}
