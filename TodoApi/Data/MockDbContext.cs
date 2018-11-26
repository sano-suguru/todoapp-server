using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data {
  public class MyDbContext : DbContext {
    public MyDbContext(DbContextOptions<MyDbContext> options)
      : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
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
      modelBuilder.Entity<TodoItem>().HasData(
        new TodoItem {
          TodoId = 1,
          Title = "Title1",
          DueDate = DateTime.Parse("2018/12/01"),
          Completed = false,
          UserId = 1
        },
        new TodoItem {
          TodoId = 2,
          Title = "Title2",
          DueDate = DateTime.Parse("2018/12/02"),
          Completed = false,
          UserId = 1
        },
        new TodoItem {
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
