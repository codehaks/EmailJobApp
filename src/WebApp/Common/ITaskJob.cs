
using System.Collections.Concurrent;

namespace WebApp
{
    public interface ITaskJob
    {
        ConcurrentQueue<string> Queue { get; set; }
    }
}