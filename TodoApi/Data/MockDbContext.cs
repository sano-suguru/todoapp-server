using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data {
  public class MockDbContext : DbContext {
    public MockDbContext(DbContextOptions<MockDbContext> options)
      : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<User>(entity => {
        entity.Property(user => user.Name).IsRequired();
        entity.Property(user => user.Password).IsRequired();
      });
      modelBuilder.Entity<User>().HasData(
        new User {
          UserId = 1,
          Name = "User1",
          Password = "Password1"
        },
        new User {
          UserId = 2,
          Name = "User2",
          Password = "Password2"
        }
      );

      modelBuilder.Entity<Todo>(entity => {
        entity.Property(todo => todo.Title).IsRequired();
        entity.HasOne(todo => todo.User)
          .WithMany(u => u.Todos)
          .HasForeignKey(nameof(User.UserId));
      });
      modelBuilder.Entity<Todo>().HasData(
        new {
          TodoId = 1,
          Title = "Title1",
          DueDate = DateTime.Parse("2018/12/01"),
          Completed = false,
          UserId = 1
        },
        new {
          TodoId = 2,
          Title = "Title2",
          DueDate = DateTime.Parse("2018/12/02"),
          Completed = false,
          UserId = 1
        },
        new {
          TodoId = 3,
          Title = "Title3",
          DueDate = DateTime.Parse("2018/12/03"),
          Completed = false,
          UserId = 2
        }
      );
    }
  }
}
