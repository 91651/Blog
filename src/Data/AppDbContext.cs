using Blog.Data.Entities;
using Blog.Data.Entities.Identity;
using EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Blog.Data.Entities.File;

namespace Blog.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : AppDbContext<string>(options)
{
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

public class AppDbContext<TKey> : IdentityDbContext<User, Role, string>
        where TKey : IEquatable<TKey>
{
    public AppDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable(nameof(User));
        modelBuilder.Entity<Role>().ToTable(nameof(Role));
        modelBuilder.Entity<IdentityUserClaim<TKey>>().ToTable(nameof(UserClaim));
        modelBuilder.Entity<IdentityUserRole<TKey>>().ToTable(nameof(UserRole));
        modelBuilder.Entity<IdentityUserLogin<TKey>>().ToTable(nameof(UserLogin));
        modelBuilder.Entity<IdentityRoleClaim<TKey>>().ToTable(nameof(RoleClaim));
        modelBuilder.Entity<IdentityUserToken<TKey>>().ToTable(nameof(UserToken));
    }
}