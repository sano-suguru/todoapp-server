using System.Collections.Generic;

namespace TodoApi.Entities {
  public class User {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public IList<Todo> Todos { get; set; }
  }
}
