using AutoMapper;
using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.GetAllSideBarLeft;

public sealed record GetAllSideBarLeftQuery() : IRequest<Result<List<SideBarLeft>>>
{
}
