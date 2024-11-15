
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using eMuhasebeServer.Application.InterfaceService;
using eMuhasebeServer.Infrastructure.Context;
using System.Linq.Expressions;
using eMuhasebeServer.Domain.Repositories;

namespace eMuhasebeServer.Infrastructure.Services
{
    public class FindIdinTheTableService : IFindIdinTheTableService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IDocumentViewerRepository _documentViewerRepository;


        public FindIdinTheTableService(ApplicationDbContext applicationDbContext, IDocumentViewerRepository documentViewerRepository)
        {
            _applicationDbContext = applicationDbContext;
            _documentViewerRepository = documentViewerRepository;
        }

        public async Task<object?> GetEntityByTableDocumentViewersf1menu7(string tableName, Guid id, CancellationToken cancellationToken)
        {
            try
            {
                // Şu an için sadece DocumentViewersf1menu7 tablosu destekleniyor
                if (!tableName.Equals("DocumentViewersf1menu7", StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException($"Desteklenmeyen tablo adı: {tableName}. Şu an için sadece DocumentViewersf1menu7 tablosu desteklenmektedir.");
                }

                // Repository üzerinden veriyi getir
                var entity = await _documentViewerRepository.GetByIdAsync(id, cancellationToken);

                if (entity == null)
                {
                    throw new KeyNotFoundException($"Belirtilen ID ile kayıt bulunamadı. ID: {id}");
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Veri çekilirken hata oluştu: {ex.Message}", ex);
            }
        }
    }
}