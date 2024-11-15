using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.ForgotPassword;


public sealed record ForgotPasswordCommand(string Email) : IRequest<Result<string>>;
