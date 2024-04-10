using GenericRepository;
using Lunavex.Result;
using MediatR;
using Newsletter.Domain.Event;
using Newsletter.Domain.Models;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Features.Blogs.Commands.ChangeStatus;
internal sealed class ChangeStatusCommandHandler(
    IBlogRepository blogRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator) : IRequestHandler<ChangeStatusCommand, Result<string>>

{
    public async Task<Result<string>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        Blog? blog = await blogRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (blog == null)
        {
            return Result<string>.Failure("Blog not found");
        }

        blog.IsPublish = !blog.IsPublish;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (blog.IsPublish)
        {
            await mediator.Publish(new BlogEvent(request.Id));
        }

        return "Change status is successful";
    }
}