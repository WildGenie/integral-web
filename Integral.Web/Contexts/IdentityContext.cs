using System;
using Integral.Claims;
using Integral.Extensions;
using Integral.Logins;
using Integral.Roles;
using Integral.Tokens;
using Integral.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integral.Contexts
{
    public sealed class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationUser.Id));

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.UserName))
                    .HasMaxLength(256);

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.PasswordHash));

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.Email))
                    .HasMaxLength(256);

                entityTypeBuilder.Property<bool>(nameof(ApplicationUser.EmailConfirmed))
                    .HasBooleanIntegerConverstion()
                    .HasColumnType("INTEGER");

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.PhoneNumber));

                entityTypeBuilder.Property<bool>(nameof(ApplicationUser.PhoneNumberConfirmed))
                    .HasBooleanIntegerConverstion()
                    .HasColumnType("INTEGER");

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.NormalizedUserName))
                    .HasMaxLength(256);

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.NormalizedEmail))
                    .HasMaxLength(256);

                entityTypeBuilder.Property<bool>(nameof(ApplicationUser.TwoFactorEnabled))
                    .HasBooleanIntegerConverstion()
                    .HasColumnType("INTEGER");

                entityTypeBuilder.Property<bool>(nameof(ApplicationUser.LockoutEnabled))
                    .HasBooleanIntegerConverstion()
                    .HasColumnType("INTEGER");

                entityTypeBuilder.Property<DateTimeOffset?>(nameof(ApplicationUser.LockoutEnd))
                    .HasColumnType("TEXT");

                entityTypeBuilder.Property<int>(nameof(ApplicationUser.AccessFailedCount));

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.ConcurrencyStamp))
                    .IsConcurrencyToken();

                entityTypeBuilder.Property<string>(nameof(ApplicationUser.SecurityStamp));

                entityTypeBuilder.HasKey(nameof(ApplicationUser.Id));

                entityTypeBuilder.HasIndex(nameof(ApplicationUser.NormalizedEmail))
                    .IsUnique();

                entityTypeBuilder.HasIndex(nameof(ApplicationUser.NormalizedUserName))
                    .IsUnique();

                entityTypeBuilder.ToTable("Users");
            });

            modelBuilder.Entity<ApplicationRole>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationRole.Id));

                entityTypeBuilder.Property<string>(nameof(ApplicationRole.Name))
                    .HasMaxLength(256);

                entityTypeBuilder.Property<string>(nameof(ApplicationRole.NormalizedName))
                    .HasMaxLength(256);

                entityTypeBuilder.Property<string>(nameof(ApplicationRole.ConcurrencyStamp))
                    .IsConcurrencyToken();

                entityTypeBuilder.HasKey(nameof(ApplicationRole.Id));

                entityTypeBuilder.HasIndex(nameof(ApplicationRole.NormalizedName))
                    .IsUnique();

                entityTypeBuilder.ToTable("Roles");
            });

            modelBuilder.Entity<ApplicationUserRole>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationUserRole.UserId));

                entityTypeBuilder.Property<int>(nameof(ApplicationUserRole.RoleId));

                entityTypeBuilder.HasKey(nameof(ApplicationUserRole.UserId), nameof(ApplicationUserRole.RoleId));

                entityTypeBuilder.HasIndex(nameof(ApplicationUserRole.RoleId));

                EntityTypeBuilder<ApplicationUserRole> applicationUserRoleEntityTypeBuilder = entityTypeBuilder.ToTable("UserRoles");

                applicationUserRoleEntityTypeBuilder.HasOne<ApplicationRole>(navigationExpression: null)
                    .WithMany()
                    .HasForeignKey(nameof(ApplicationUserRole.RoleId))
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                applicationUserRoleEntityTypeBuilder.HasOne<ApplicationUser>(navigationExpression: null)
                    .WithMany()
                    .HasForeignKey(nameof(ApplicationUserRole.UserId))
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserClaim>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationUserClaim.Id))
                    .ValueGeneratedOnAdd();

                entityTypeBuilder.Property<int>(nameof(ApplicationUserClaim.UserId))
                    .IsRequired();

                entityTypeBuilder.Property<string>(nameof(ApplicationUserClaim.ClaimType));

                entityTypeBuilder.Property<string>(nameof(ApplicationUserClaim.ClaimValue));

                entityTypeBuilder.HasKey(nameof(ApplicationUserClaim.Id));

                entityTypeBuilder.HasIndex(nameof(ApplicationUserClaim.UserId));

                EntityTypeBuilder<ApplicationUserClaim> applicationUserClaimEntityTypeBuilder = entityTypeBuilder.ToTable("UserClaims");

                applicationUserClaimEntityTypeBuilder.HasOne<ApplicationUser>(navigationExpression: null)
                    .WithMany()
                    .HasForeignKey(nameof(ApplicationUserClaim.UserId))
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserLogin>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationUserLogin.UserId))
                    .IsRequired();

                entityTypeBuilder.Property<string>(nameof(ApplicationUserLogin.LoginProvider));

                entityTypeBuilder.Property<string>(nameof(ApplicationUserLogin.ProviderKey));

                entityTypeBuilder.Property<string>(nameof(ApplicationUserLogin.ProviderDisplayName));

                entityTypeBuilder.HasKey(nameof(ApplicationUserLogin.LoginProvider), nameof(ApplicationUserLogin.ProviderKey));

                entityTypeBuilder.HasIndex(nameof(ApplicationUserLogin.UserId));

                EntityTypeBuilder<ApplicationUserLogin> applicationUserLoginEntityTypeBuilder = entityTypeBuilder.ToTable("UserLogins");

                applicationUserLoginEntityTypeBuilder.HasOne<ApplicationUser>(navigationExpression: null)
                    .WithMany()
                    .HasForeignKey(nameof(ApplicationUserLogin.UserId))
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserToken>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationUserToken.UserId));

                entityTypeBuilder.Property<string>(nameof(ApplicationUserToken.LoginProvider));

                entityTypeBuilder.Property<string>(nameof(ApplicationUserToken.Name));

                entityTypeBuilder.Property<string>(nameof(ApplicationUserToken.Value));

                entityTypeBuilder.HasKey(nameof(ApplicationUserToken.UserId), nameof(ApplicationUserToken.LoginProvider), nameof(ApplicationUserToken.Name));

                EntityTypeBuilder<ApplicationUserToken> applicationUserTokenEntityTypeBuilder = entityTypeBuilder.ToTable("UserTokens");

                applicationUserTokenEntityTypeBuilder.HasOne<ApplicationUser>(navigationExpression: null)
                    .WithMany()
                    .HasForeignKey(nameof(ApplicationUserToken.UserId))
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRoleClaim>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<int>(nameof(ApplicationRoleClaim.Id))
                    .ValueGeneratedOnAdd();

                entityTypeBuilder.Property<int>(nameof(ApplicationRoleClaim.RoleId))
                    .IsRequired();

                entityTypeBuilder.Property<string>(nameof(ApplicationRoleClaim.ClaimType));

                entityTypeBuilder.Property<string>(nameof(ApplicationRoleClaim.ClaimValue));

                entityTypeBuilder.HasKey(nameof(ApplicationRoleClaim.Id));

                entityTypeBuilder.HasIndex(nameof(ApplicationRoleClaim.RoleId));

                EntityTypeBuilder<ApplicationRoleClaim> applicationRoleClaimEntityTypeBuilder = entityTypeBuilder.ToTable("RoleClaims");

                applicationRoleClaimEntityTypeBuilder.HasOne<ApplicationRole>(navigationExpression: null)
                    .WithMany()
                    .HasForeignKey(nameof(ApplicationRoleClaim.RoleId))
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }
}
