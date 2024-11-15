using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.DeleteSideBarLeftById;

public sealed record DeleteSideBarLeftByIdCommand(Guid Id) : IRequest<Result<string>>;
