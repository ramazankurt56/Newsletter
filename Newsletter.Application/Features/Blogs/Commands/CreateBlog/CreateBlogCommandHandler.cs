﻿using AutoMapper;
using GenericRepository;
using Lunavex.Result;
using MediatR;
using Newsletter.Application.Extensions;
using Newsletter.Domain.Event;
using Newsletter.Domain.Models;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Features.Blogs.Commands.Create;

internal sealed class CreateBlogCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork, IMapper mapper,IMediator mediator) : IRequestHandler<CreateBlogCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        Blog blog = mapper.Map<Blog>(request);
        blog.Url = request.Title.ConvertTitleToUrl();

        await blogRepository.AddAsync(blog, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (blog.IsPublish)
        {
            await mediator.Publish(new BlogEvent(blog.Id));
        }

        return "Blog create is successful";
    }
}
    