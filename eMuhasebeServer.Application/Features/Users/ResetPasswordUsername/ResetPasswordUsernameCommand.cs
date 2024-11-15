using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.ResetPasswordUsername;

public sealed record ResetPasswordUsernameCommand(string Email, string Token, string Password) : IRequest<Result<string>>;
