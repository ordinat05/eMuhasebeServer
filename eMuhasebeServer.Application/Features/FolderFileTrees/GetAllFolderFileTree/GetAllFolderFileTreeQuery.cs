using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FolderFileTrees.GetAllFolderFileTree;


public sealed record GetAllFolderFileTreeQuery() : IRequest<Result<List<FolderFileTree>>>;


