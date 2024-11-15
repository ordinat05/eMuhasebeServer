

using AutoMapper;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUserUnauthorized;

internal sealed class CreateUserUnauthorizedCommandHandler : IRequestHandler<CreateUserUnauthorizedCommand, Result<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICompanyUserRepository _companyUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateUserUnauthorizedCommandHandler(
        UserManager<AppUser> userManager,
        ICompanyUserRepository companyUserRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator)
    {
        _userManager = userManager;
        _companyUserRepository = companyUserRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(CreateUserUnauthorizedCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcı adı kontrolü
        bool isUserNameExists = await _userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);
        if (isUserNameExists)
        {
            return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış.");
        }

        // E-posta kontrolü
        bool isEmailExists = await _userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken);
        if (isEmailExists)
        {
            return Result<string>.Failure("Bu mail adresi daha önce kullanılmış.");
        }

        // Kullanıcı oluşturma işlemi
        var appUser = _mapper.Map<AppUser>(request);
        var result = await _userManager.CreateAsync(appUser, request.Password);

        if (!result.Succeeded)
        {
            return Result<string>.Failure(result.Errors.Select(e => e.Description).ToList());
        }

        // Şirket bağlantılarını oluşturma
        var companyUsers = request.CompanyIds.Select(id => new CompanyUser
        {
            AppUserId = appUser.Id,
            CompanyId = id
        }).ToList();

        await _companyUserRepository.AddRangeAsync(companyUsers, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // E-posta gönderme işlemi için event yayınlama
        await _mediator.Publish(new AppUserEvent(appUser.Id), cancellationToken);

        return Result<string>.Succeed("Kullanıcı başarıyla oluşturuldu.");
    }
}