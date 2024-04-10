using Lunavex.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newsletter.Domain.Models;

namespace Newsletter.Application.Features.Auth;
public sealed record  LoginCommand(string UserNameOrEmail,string Password):IRequest<Result<string>>;

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var appUser = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail, cancellationToken);
        if(appUser is null)
        {
            return Result<string>.Failure("User not found");
        }
        bool checkPassword = await userManager.CheckPasswordAsync(appUser, request.Password);
        if(!checkPassword)
        {
            return Result<string>.Failure("Password is wrong");
        }
        return "Login is successful";
    }
}
