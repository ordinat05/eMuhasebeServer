using AutoMapper;
using eMuhasebeServer.Application.Features.Companies.CreateCompany;
using eMuhasebeServer.Application.Features.Companies.UpdateCompany;
using eMuhasebeServer.Application.Features.FileContentTableRows.CreateFileContentTableRow;
using eMuhasebeServer.Application.Features.FileContentTableRows.UpdateFileContentTableRow;
using eMuhasebeServer.Application.Features.FolderFileTrees.CreateFolderFileTree;
using eMuhasebeServer.Application.Features.FolderFileTrees.UpdateFolderFileTree;
using eMuhasebeServer.Application.Features.KatCizelgeler.CreateKatCizelge;
using eMuhasebeServer.Application.Features.SideBarLeftMenu.CreateSideBarLeft;
using eMuhasebeServer.Application.Features.SideBarLeftMenu.UpdateSideBarLeft;
using eMuhasebeServer.Application.Features.Users.CreateUser;
using eMuhasebeServer.Application.Features.Users.CreateUserUnauthorized;
using eMuhasebeServer.Application.Features.Users.UpdateUser;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Entities.Dtos.KatCizelgeDto;

namespace eMuhasebeServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, AppUser>();
            CreateMap<CreateUserUnauthorizedCommand, AppUser>();
            CreateMap<UpdateUserCommand, AppUser>();

            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<UpdateCompanyCommand, Company>();

            CreateMap<CreateSideBarLeftCommand, SideBarLeft>();
            CreateMap<UpdateSideBarLeftCommand, SideBarLeft>();

            CreateMap<CreateFileContentTableRowCommand, FileContentTableRow>();
            CreateMap<UpdateFileContentTableRowCommand, FileContentTableRow>();

            CreateMap<CreateFolderFileTreeCommand, FolderFileTree>();
            CreateMap<UpdateFolderFileTreeCommand, FolderFileTree>();

            CreateMap<KatCizelgeOlusturmaDto, KatCizelgeHeader>();
            CreateMap<KatCizelgeHeader, KatCizelgeListDto>();
            CreateMap<KatCizelge, KatCizelgeDto>();
            CreateMap<CizelgeOlusturCommand, KatCizelgeHeader>();
        }
    }
}
