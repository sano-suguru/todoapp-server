using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data {
  public interface ITodoRepository {
    IEnumerable<Todo> List(int userId);
    void Create(Todo todo);
    Todo Read(int todoId);
    void Update(Todo target, Todo todo);
    void Delete(Todo todo);
  }

  public class TodoRepository: ITodoRepository {
    readonly MockDbContext myDbContext;

    public TodoRepository(MockDbContext myDbContext) {
      this.myDbContext = myDbContext;
    }
    public IEnumerable<Todo> List(int userId) {
      var todos = myDbContext.TodoItems
        .AsNoTracking()
        .Where(x => x.UserId == userId)
        .AsEnumerable();
      return todos;
    }

    public void Create(Todo todo) {
      myDbContext.TodoItems.Add(todo);
      myDbContext.SaveChanges();
    }

    public void Update(Todo target, Todo todo) {
      target.Title = todo.Title;
      target.DueDate = todo.DueDate;
      target.Completed = todo.Completed;
      myDbContext.TodoItems.Update(target);
      myDbContext.SaveChanges();
    }

    public Todo Read(int todoId) {
      var todo = myDbContext.TodoItems
        .AsNoTracking()
        .SingleOrDefault(x => x.TodoId == todoId);
      return todo;
    }

    public void Delete(Todo todo) {
      myDbContext.TodoItems.Remove(todo);
      myDbContext.SaveChanges();
    }
  }
}
