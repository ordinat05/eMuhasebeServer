﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eMuhasebeServer.Application.Features.FolderFileTrees.DeleteFolderFileTree;

public sealed record DeleteFolderFileTreeCommand(Guid Id) : IRequest<Result<string>>;
