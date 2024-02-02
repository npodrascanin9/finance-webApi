using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FinanceAPI.DataAccess.Models;

namespace FinanceAPI.DataAccess
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<FinanceDocument> FinanceDocuments { get; set; } = null!;
        public virtual DbSet<DocumentTransaction> DocumentTransactions { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.CompanyRegistrationNumber, "UQ__Clients__A30DD59F308754FE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompanyRegistrationNumber).HasMaxLength(20);

                entity.Property(e => e.Vat).HasMaxLength(20);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK__Clients__TenantI__4F7CD00D");

                entity.HasOne(d => d.VatNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.Vat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Clients__Vat__5070F446");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Vat)
                    .HasName("PK__Companie__C5F0F2B584593D67");

                entity.Property(e => e.Vat).HasMaxLength(20);

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("FK__Companies__Docum__4BAC3F29");
            });

            modelBuilder.Entity<FinanceDocument>(entity =>
            {
                entity.HasIndex(e => e.AccountNumber, "UQ__Document__BE2ACD6FC823EABA")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountNumber).HasMaxLength(10);

                entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<DocumentTransaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentTransactions)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DocumentT__Docum__48CFD27E");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Products__A25C5AA6BA5A2E4E");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.AssignedAt).HasColumnType("date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpiresAt).HasColumnType("date");

                entity.Property(e => e.IsSupported).HasComputedColumnSql("(case when [ExpiresAt]<=getdate() then CONVERT([bit],(1)) else CONVERT([bit],(0)) end)", false);

                entity.Property(e => e.Rule).HasColumnName("RULE");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
