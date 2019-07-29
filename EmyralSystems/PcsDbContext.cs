using System;
using EmyralSystems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmyralSystems
{
    public partial class PcsDbContext : DbContext
    {
        public PcsDbContext()
        {
        }

        public PcsDbContext(DbContextOptions<PcsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<CardAssignmentHistory> CardAssignmentHistory { get; set; }
        public virtual DbSet<CardBox> CardBox { get; set; }
        public virtual DbSet<CardLoad> CardLoad { get; set; }
        public virtual DbSet<CardLoadTask> CardLoadTask { get; set; }
        public virtual DbSet<CardTransaction> CardTransaction { get; set; }
        public virtual DbSet<CardUser> CardUser { get; set; }
        public virtual DbSet<CoBrand> CoBrand { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<FileLibrary> FileLibrary { get; set; }
        public virtual DbSet<Reseller> Reseller { get; set; }
        public virtual DbSet<ResellerBalance> ResellerBalance { get; set; }
        public virtual DbSet<ResellerCoBrand> ResellerCoBrand { get; set; }
        public virtual DbSet<ResellerCurrency> ResellerCurrency { get; set; }
        public virtual DbSet<ResellerTransaction> ResellerTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;uid=root;pwd=user123;database=pcsdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ClaimType).HasColumnType("longtext");

                entity.Property(e => e.ClaimValue).HasColumnType("longtext");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.HasIndex(e => e.ResellerId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext");

                entity.Property(e => e.DisplayName).HasColumnType("longtext");

                entity.Property(e => e.HostName).HasColumnType("longtext");

                entity.Property(e => e.Name).HasColumnType("varchar(256)");

                entity.Property(e => e.NormalizedName).HasColumnType("varchar(256)");

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.RoleType).HasColumnType("int(11)");

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.AspNetRoles)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ClaimType).HasColumnType("longtext");

                entity.Property(e => e.ClaimValue).HasColumnType("longtext");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasColumnType("varchar(255)");

                entity.Property(e => e.ProviderKey).HasColumnType("varchar(255)");

                entity.Property(e => e.ProviderDisplayName).HasColumnType("longtext");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PRIMARY");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.LoginProvider).HasColumnType("varchar(255)");

                entity.Property(e => e.Name).HasColumnType("varchar(255)");

                entity.Property(e => e.Value).HasColumnType("longtext");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccessFailedCount).HasColumnType("int(11)");

                entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext");

                entity.Property(e => e.Email).HasColumnType("varchar(256)");

                entity.Property(e => e.EmailConfirmed).HasColumnType("bit(1)");

                entity.Property(e => e.LockoutEnabled).HasColumnType("bit(1)");

                entity.Property(e => e.NormalizedEmail).HasColumnType("varchar(256)");

                entity.Property(e => e.NormalizedUserName).HasColumnType("varchar(256)");

                entity.Property(e => e.PasswordHash).HasColumnType("longtext");

                entity.Property(e => e.PhoneNumber).HasColumnType("longtext");

                entity.Property(e => e.PhoneNumberConfirmed).HasColumnType("bit(1)");

                entity.Property(e => e.SecurityStamp).HasColumnType("longtext");

                entity.Property(e => e.TwoFactorEnabled).HasColumnType("bit(1)");

                entity.Property(e => e.UserName).HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasIndex(e => e.CardBoxId);

                entity.HasIndex(e => e.CardUserId);

                entity.HasIndex(e => e.CoBrandId);

                entity.HasIndex(e => e.ResellerId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Balance).HasColumnType("longtext");

                entity.Property(e => e.CardBoxId).HasColumnType("int(11)");

                entity.Property(e => e.CardNumber).HasColumnType("longtext");

                entity.Property(e => e.CardUserId).HasColumnType("int(11)");

                entity.Property(e => e.CoBrandId).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.IsAssigned).HasColumnType("bit(1)");

                entity.Property(e => e.LastSync).HasDefaultValueSql("'0001-01-01 00:00:00.000000'");

                entity.Property(e => e.LoadCount)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.HasOne(d => d.CardBox)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.CardBoxId);

                entity.HasOne(d => d.CardUser)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.CardUserId);

                entity.HasOne(d => d.CoBrand)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.CoBrandId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<CardAssignmentHistory>(entity =>
            {
                entity.HasIndex(e => e.ResellerId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.Description).HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.TotalCards).HasColumnType("int(11)");

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.CardAssignmentHistory)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<CardBox>(entity =>
            {
                entity.HasIndex(e => e.CobrandId);

                entity.HasIndex(e => e.FileId);

                entity.HasIndex(e => e.ResellerId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BoxId)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.CobrandId).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.FileId).HasColumnType("int(11)");

                entity.Property(e => e.IsAssigned).HasColumnType("bit(1)");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.HasOne(d => d.Cobrand)
                    .WithMany(p => p.CardBox)
                    .HasForeignKey(d => d.CobrandId);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.CardBox)
                    .HasForeignKey(d => d.FileId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.CardBox)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<CardLoad>(entity =>
            {
                entity.HasIndex(e => e.FileId);

                entity.HasIndex(e => e.ResellerId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BatchId)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.CardLoadStatus).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.Data).HasColumnType("longtext");

                entity.Property(e => e.FileId).HasColumnType("int(11)");

                entity.Property(e => e.ImportType).HasColumnType("int(11)");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.TransactionId).HasColumnType("longtext");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.CardLoad)
                    .HasForeignKey(d => d.FileId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.CardLoad)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<CardLoadTask>(entity =>
            {
                entity.HasIndex(e => e.CardLoadId);

                entity.HasIndex(e => e.CoBrandId);

                entity.HasIndex(e => e.CurrencyId);

                entity.HasIndex(e => e.ResellerTransactionId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CardLoadId).HasColumnType("int(11)");

                entity.Property(e => e.CoBrandId).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.Csv)
                    .HasColumnName("CSV")
                    .HasColumnType("longtext");

                entity.Property(e => e.CurrencyId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FormatFileDate).HasDefaultValueSql("'0001-01-01 00:00:00.000000'");

                entity.Property(e => e.FormatFileNumber)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LoadingFee).HasDefaultValueSql("'0'");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.MonthlyFee).HasDefaultValueSql("'0'");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.Remarks).HasColumnType("longtext");

                entity.Property(e => e.ResellerProfit).HasDefaultValueSql("'0'");

                entity.Property(e => e.ResellerTransactionId).HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.HasOne(d => d.CardLoad)
                    .WithMany(p => p.CardLoadTask)
                    .HasForeignKey(d => d.CardLoadId);

                entity.HasOne(d => d.CoBrand)
                    .WithMany(p => p.CardLoadTask)
                    .HasForeignKey(d => d.CoBrandId);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CardLoadTask)
                    .HasForeignKey(d => d.CurrencyId);

                entity.HasOne(d => d.ResellerTransaction)
                    .WithMany(p => p.CardLoadTask)
                    .HasForeignKey(d => d.ResellerTransactionId);
            });

            modelBuilder.Entity<CardTransaction>(entity =>
            {
                entity.HasIndex(e => e.CardId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasDefaultValueSql("'0'");

                entity.Property(e => e.CardId).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.Currency).HasColumnType("longtext");

                entity.Property(e => e.MerchantId).HasColumnType("longtext");

                entity.Property(e => e.MerchantName).HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.TranDate).HasDefaultValueSql("'0001-01-01 00:00:00.000000'");

                entity.Property(e => e.TranType).HasColumnType("longtext");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardTransaction)
                    .HasForeignKey(d => d.CardId);
            });

            modelBuilder.Entity<CardUser>(entity =>
            {
                entity.HasIndex(e => e.AddressProofId);

                entity.HasIndex(e => e.ResellerId);

                entity.HasIndex(e => e.UserIdentificationProofId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddressProofId).HasColumnType("int(11)");

                entity.Property(e => e.Comments).HasColumnType("longtext");

                entity.Property(e => e.ContactNumber).HasColumnType("longtext");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.Email).HasColumnType("longtext");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.ResellerId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserIdentification)
                    .IsRequired()
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserIdentificationProofId).HasColumnType("int(11)");

                entity.HasOne(d => d.AddressProof)
                    .WithMany(p => p.CardUserAddressProof)
                    .HasForeignKey(d => d.AddressProofId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.CardUser)
                    .HasForeignKey(d => d.ResellerId);

                entity.HasOne(d => d.UserIdentificationProof)
                    .WithMany(p => p.CardUserUserIdentificationProof)
                    .HasForeignKey(d => d.UserIdentificationProofId);
            });

            modelBuilder.Entity<CoBrand>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CmsHostname).HasColumnType("longtext");

                entity.Property(e => e.CmsPassword).HasColumnType("longtext");

                entity.Property(e => e.CmsUsername).HasColumnType("longtext");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.Email).HasColumnType("longtext");

                entity.Property(e => e.EmailPassword).HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<FileLibrary>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ContentType).HasColumnType("longtext");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.FileName).HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.Size).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Reseller>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ChargeTo).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.HostName)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.MonthlyFeeChargeTo)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'100'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Type).HasColumnType("int(11)");
            });

            modelBuilder.Entity<ResellerBalance>(entity =>
            {
                entity.HasIndex(e => e.CurrencyId);

                entity.HasIndex(e => e.ResellerId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.CurrencyId).HasColumnType("int(11)");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ResellerBalance)
                    .HasForeignKey(d => d.CurrencyId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.ResellerBalance)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<ResellerCoBrand>(entity =>
            {
                entity.HasKey(e => new { e.ResellerId, e.CoBrandId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CoBrandId);

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.CoBrandId).HasColumnType("int(11)");

                entity.HasOne(d => d.CoBrand)
                    .WithMany(p => p.ResellerCoBrand)
                    .HasForeignKey(d => d.CoBrandId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.ResellerCoBrand)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<ResellerCurrency>(entity =>
            {
                entity.HasKey(e => new { e.ResellerId, e.CurrencyId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CurrencyId);

                entity.Property(e => e.ResellerId).HasColumnType("int(11)");

                entity.Property(e => e.CurrencyId).HasColumnType("int(11)");

                entity.Property(e => e.LoadingFeePercent).HasDefaultValueSql("'0'");

                entity.Property(e => e.MaxLoading).HasDefaultValueSql("'0'");

                entity.Property(e => e.MinLoading).HasDefaultValueSql("'0'");

                entity.Property(e => e.MinLoadingFeeAmount).HasDefaultValueSql("'0'");

                entity.Property(e => e.MinResellerProfitAmount).HasDefaultValueSql("'0'");

                entity.Property(e => e.MonthlyFee).HasDefaultValueSql("'0'");

                entity.Property(e => e.ResellerProfitPercent).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ResellerCurrency)
                    .HasForeignKey(d => d.CurrencyId);

                entity.HasOne(d => d.Reseller)
                    .WithMany(p => p.ResellerCurrency)
                    .HasForeignKey(d => d.ResellerId);
            });

            modelBuilder.Entity<ResellerTransaction>(entity =>
            {
                entity.HasIndex(e => e.ResellerBalanceId);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("longtext");

                entity.Property(e => e.ModifiedBy).HasColumnType("longtext");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.ResellerBalanceId).HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.HasOne(d => d.ResellerBalance)
                    .WithMany(p => p.ResellerTransaction)
                    .HasForeignKey(d => d.ResellerBalanceId);
            });
        }
    }
}
