using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.SideBarLeftMenu.SaveAllSideBarLeft
{
    public class SaveAllSideBarLeftCommand : IRequest<Result<string>>
    {
        public List<SideBarLeftDto> Nodes { get; set; } = new List<SideBarLeftDto>();

    }
    public class SideBarLeftDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsExpanded { get; set; }
        public Guid? ParentId { get; set; }
        public int Order { get; set; }
        public string? IconCss { get; set; } // Eklenmiş
        public List<SideBarLeftDto>? Children { get; set; }
    }

    public class SaveAllSideBarLeftCommandHandler : IRequestHandler<SaveAllSideBarLeftCommand, Result<string>>
    {
        private readonly ISideBarLeftRepository _sideBarLeftRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaveAllSideBarLeftCommandHandler(ISideBarLeftRepository sideBarLeftRepository, IUnitOfWork unitOfWork)
        {
            _sideBarLeftRepository = sideBarLeftRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(SaveAllSideBarLeftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Tüm mevcut öğeleri silelim
                await _sideBarLeftRepository.DeleteAllAsync(cancellationToken);

                // Yeni ağaç yapısını kaydedelim
                await SaveNodesRecursively(request.Nodes, null, 1, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<string>.Succeed("Tüm menü öğeleri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Menü öğeleri güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        private async Task SaveNodesRecursively(List<SideBarLeftDto> nodes, Guid? parentId, int order, CancellationToken cancellationToken)
        {
            if (nodes == null) return;

            foreach (var nodeDto in nodes)
            {
                var node = new SideBarLeft
                {
                    Id = nodeDto.Id,
                    Name = nodeDto.Name,
                    IsExpanded = nodeDto.IsExpanded,
                    ParentId = parentId,
                    Order = order++,
                    IconCss = nodeDto.IconCss // IconCss'i koruyalım
                };

                await _sideBarLeftRepository.AddAsync(node, cancellationToken);

                if (nodeDto.Children != null && nodeDto.Children.Any())
                {
                    await SaveNodesRecursively(nodeDto.Children, node.Id, 1, cancellationToken);
                }
            }
        }
    }
}
