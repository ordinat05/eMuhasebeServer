﻿using eMuhasebeServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMuhasebeServer.Infrastructure.Configurations;

internal sealed class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(EntityTypeBuilder<CompanyUser> builder)
    {
        builder.HasKey(x => new { x.AppUserId, x.CompanyId});
        builder.HasQueryFilter(filter => !filter.Company!.IsDeleted);

        //builder.HasOne(p => p.AppUser)
        //    .WithMany(p => p.CompanyUsers)
        //    .HasForeignKey(p => p.AppUserId)
        //    .OnDelete(DeleteBehavior.NoAction);
        // iki tablo birbirini istediği için hata verdi. Bunu kaldırıyoruz

    }
}
