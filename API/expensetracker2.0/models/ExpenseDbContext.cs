using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace expensetracker2._0.Models;

public partial class ExpenseDbContext : DbContext
{
    public ExpenseDbContext()
    {
    }

    public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CheckExpense> CheckExpenses { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategoryView> ExpenseCategoryViews { get; set; }

    public virtual DbSet<ExpenseDetail> ExpenseDetails { get; set; }

    public virtual DbSet<IncomeEntry> IncomeEntries { get; set; }

    public virtual DbSet<IncomeSource> IncomeSources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-NG3REHE;Database=expenseDB;Integrated Security=False;user id=Zohaib;password=asshveenmeer;Connection Timeout=30;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.BudgetId).HasName("PK__budget__E38E7924742EE61C");

            entity.ToTable("budget");

            entity.Property(e => e.Budget1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("budget");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Interval)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("interval");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B75725028");

            entity.ToTable("Category");
        });

        modelBuilder.Entity<CheckExpense>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CHECK_EXPENSE");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EXPENSE__3214EC07F899F525");

            entity.ToTable("EXPENSE");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExpenseCategoryView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ExpenseCategoryView");

            entity.Property(e => e.ExpenseDetailAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpenseDetailDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ExpenseDetail>(entity =>
        {
            entity.HasKey(e => e.ExpenseDetailId).HasName("PK__ExpenseD__2AE4571B96D8DBEF");

            entity.ToTable("ExpenseDetail");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Expense).WithMany(p => p.ExpenseDetails)
                .HasForeignKey(d => d.ExpenseId)
                .HasConstraintName("FK_ExpenseDetail_Expense");
        });

        modelBuilder.Entity<IncomeEntry>(entity =>
        {
            entity.HasKey(e => e.IncomeEntryId).HasName("PK__IncomeEn__73B499D0E19D1187");

            entity.ToTable("IncomeEntry");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.IncomeSource).WithMany(p => p.IncomeEntries)
                .HasForeignKey(d => d.IncomeSourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IncomeEntry_IncomeSource");
        });

        modelBuilder.Entity<IncomeSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__IncomeSo__3214EC0740E96EEA");

            entity.ToTable("IncomeSource");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
