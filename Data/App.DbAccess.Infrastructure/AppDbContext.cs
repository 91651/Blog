using App.DbAccess.Entities;
using App.DbAccess.Entities.Identity;
using App.EFCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = App.DbAccess.Entities.File;

namespace App.DbAccess.Infrastructure
{
    public class AppDbContext : AppDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
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
    public class AppDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, UserClaim<TKey>, UserRole<TKey>, UserLogin<TKey>, RoleClaim<TKey>, UserToken<TKey>>
        where TUser : User<TKey>
        where TRole : Role<TKey>
        where TKey : IEquatable<TKey>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Identity 中定义的实体
            modelBuilder.Entity<User<TKey>>().ToTable("User");//必须指定表名，才能改变数据库中对应表名
            modelBuilder.Entity<Role<TKey>>().ToTable("Role");
            modelBuilder.Entity<UserClaim<TKey>>().ToTable("UserClaim");
            modelBuilder.Entity<UserRole<TKey>>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin<TKey>>().ToTable("UserLogin");
            modelBuilder.Entity<RoleClaim<TKey>>().ToTable("RoleClaim");
            modelBuilder.Entity<UserToken<TKey>>().ToTable("UserToken");
        }
    }
}
