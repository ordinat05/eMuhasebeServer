using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUserUnauthorized;

public sealed record CreateUserUnauthorizedCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    List<Guid> CompanyIds
    ) : IRequest<Result<string>>;
