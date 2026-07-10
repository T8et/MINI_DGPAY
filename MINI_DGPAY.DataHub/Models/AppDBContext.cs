using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MINI_DGPAY.DataHub.Models;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BtAccount> BtAccounts { get; set; }

    public virtual DbSet<BtTransaction> BtTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=MINI_DGPAY;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BtAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__BT_ACCOU__1788CC4C0E3D22B8");

            entity.ToTable("BT_ACCOUNT");

            entity.Property(e => e.UserId).HasMaxLength(10);
            entity.Property(e => e.CreateBy).HasMaxLength(10);
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.ModifyBy).HasMaxLength(10);
            entity.Property(e => e.ModifyDate).HasColumnType("date");
            entity.Property(e => e.UserBalance).HasColumnType("decimal(25, 8)");
            entity.Property(e => e.UserMobileNo).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(80);
            entity.Property(e => e.UserPass).HasMaxLength(10);
        });

        modelBuilder.Entity<BtTransaction>(entity =>
        {
            entity.HasKey(e => e.TranId).HasName("PK__BT_TRANS__F70897C903F42C10");

            entity.ToTable("BT_TRANSACTION");

            entity.Property(e => e.TranId).HasMaxLength(10);
            entity.Property(e => e.TranAmount).HasColumnType("decimal(25, 8)");
            entity.Property(e => e.TranDate).HasColumnType("date");
            entity.Property(e => e.TranRecver).HasMaxLength(20);
            entity.Property(e => e.TranRemk).HasMaxLength(200);
            entity.Property(e => e.TranSender).HasMaxLength(20);
            entity.Property(e => e.TranType).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
