using MediatR;

namespace MediatRJobDemo.Common;

public class Notify : INotification
{

    public string Message { get; set; }
}

