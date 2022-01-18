namespace WebApp
{
    public class TaskJob : ITaskJob
    {
        public TaskJob()
        {
            Queue= new Queue<string>();
        }
        public Queue<string> Queue { get; set; } 

        //public void EnueueItem(string message)
        //{
        //    Queue.Enqueue(message);
        //}

        //public void DequeueItem()
        //{
        //    Queue.Dequeue();
        //}
    }
}
