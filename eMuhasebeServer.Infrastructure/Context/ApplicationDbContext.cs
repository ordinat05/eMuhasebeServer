using eMuhasebeServer.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace eMuhasebeServer.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<SideBarLeft> SideBarLeftMenu { get; set; }  
        public DbSet<FileContent> FileContents { get; set; }  
        public DbSet<FileContent2> FileContents2 { get; set; }  
        public DbSet<FileContentTableRow> FileContentTableRows { get; set; }  
        public DbSet<FolderFileTree> FolderFilesTree { get; set; }
        public DbSet<KatCizelgeHeader> KatCizelgeHeaders { get; set; }
        public DbSet<KatCizelge>  KatCizelgeler {  get; set; }
        public DbSet<DocumentViewerf1menu7> DocumentViewersf1menu7 { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);

            //builder.Entity<KatCizelgeHeader>(entity =>
            //{
            //    entity.Property(e => e.Tag).HasMaxLength(255);
            //    entity.Property(e => e.FilePath).HasMaxLength(255);
            //    entity.Property(e => e.HaveTblIlceId).HasMaxLength(50);
            //});

            //builder.Entity<KatCizelge>(entity =>
            //{
            //    entity.Property(e => e.KatAdi).HasMaxLength(50);
            //    entity.Property(e => e.DaireAdi).HasMaxLength(50);
            //});

            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();

         


        }
    }
}


//builder.Entity<AppUser>().ToTable("Users", t => t.ExcludeFromMigrations());
//builder.Entity<IdentityRole<Guid>>().ToTable("Roles", t => t.ExcludeFromMigrations());

//builder.Entity<IdentityRole>().ToTable("Roles", t => t.ExcludeFromMigrations());
//builder.Entity<AppRole>().ToTable("Roles", t => t.ExcludeFromMigrations());