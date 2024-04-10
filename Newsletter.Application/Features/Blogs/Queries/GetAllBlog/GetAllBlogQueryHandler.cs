using Lunavex.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsletter.Domain.Models;
using Newsletter.Domain.Repositories;

namespace Newsletter.Application.Features.Blogs.Queries.GetAllBlog;
internal sealed class GetAllBlogQueryHandler(
    IBlogRepository blogRepository) : IRequestHandler<GetAllBlogQuery, Result<List<Blog>>>
{
    public async Task<Result<List<Blog>>> Handle(GetAllBlogQuery request, CancellationToken cancellationToken)
    {
        var response =
            await blogRepository
            .Where(p => p.Title.ToLower().Contains(request.Search.ToLower()))
            .OrderByDescending(p => p.PublishDate)
            .ToListAsync(cancellationToken);

        return response;
    }
}
