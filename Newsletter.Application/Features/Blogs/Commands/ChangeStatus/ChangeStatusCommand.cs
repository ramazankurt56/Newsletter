using Lunavex.Result;
using MediatR;

namespace Newsletter.Application.Features.Blogs.Commands.ChangeStatus;
public sealed record ChangeStatusCommand(
    Guid Id) : IRequest<Result<string>>;