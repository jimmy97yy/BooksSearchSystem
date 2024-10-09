using Microsoft.EntityFrameworkCore;
using BooksSearchSystem.Models;

namespace BooksSearchSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 定義資料庫中的表格
        public DbSet<User> Users { get; set; } // 對應 User 表格
        public DbSet<Book> Books { get; set; } // 對應 Book 表格
        public DbSet<Review> Reviews { get; set; } // 對應 Review 表格

        // 配置表格之間的關聯
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 定义 Book 的主键
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Isbn); // 设置 Isbn 为主键

            // 配置 Review 相关的外键关系
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews) // 确保这里使用的是 Reviews
                .HasForeignKey(r => r.Isbn);
        }

    }
}
