using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Entities {
  public class TodoItem {
    [Key]
    public int TodoId { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public bool Completed { get; set; }
   
    public int UserId { get; set; }
    public User User { get; set; }
  }
}
