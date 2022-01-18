
namespace WebApp
{
    public interface ITaskJob
    {
        Queue<string> Queue { get; set; }
    }
}