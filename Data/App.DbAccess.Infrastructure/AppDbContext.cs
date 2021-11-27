using App.DbAccess.Entities;
using App.DbAccess.Entities.Identity;
using App.EFCore;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using File = App.DbAccess.Entities.File;

namespace App.DbAccess.Infrastructure
{
    public class AppDbContext : AppDbContext<string>
    {
        public AppDbContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AppDbContext<TKey> : ApiAuthorizationDbContext<User>
        where TKey : IEquatable<TKey>
    {
        public AppDbContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<IdentityRole>().ToTable(nameof(Role));
            modelBuilder.Entity<IdentityUserClaim<TKey>>().ToTable(nameof(UserClaim));
            modelBuilder.Entity<IdentityUserRole<TKey>>().ToTable(nameof(UserRole));
            modelBuilder.Entity<IdentityUserLogin<TKey>>().ToTable(nameof(UserLogin));
            modelBuilder.Entity<IdentityRoleClaim<TKey>>().ToTable(nameof(RoleClaim));
            modelBuilder.Entity<IdentityUserToken<TKey>>().ToTable(nameof(UserToken));
        }
    }
}
