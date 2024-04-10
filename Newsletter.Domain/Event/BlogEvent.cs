using MediatR;

namespace Newsletter.Domain.Event;
public sealed record BlogEvent(
    Guid BlogId) : INotification;