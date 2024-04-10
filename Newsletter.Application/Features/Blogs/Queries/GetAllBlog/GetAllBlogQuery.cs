using Lunavex.Result;
using MediatR;
using Newsletter.Domain.Models;

namespace Newsletter.Application.Features.Blogs.Queries.GetAllBlog;
public sealed record GetAllBlogQuery(
    string Search) : IRequest<Result<List<Blog>>>;