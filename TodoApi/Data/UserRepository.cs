using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data {
  public interface IUserRepository {
    User Read(int userId);
  }

  public class UserRepository : IUserRepository {
    readonly MockDbContext myDbContext;

    public UserRepository(MockDbContext myDbContext) {
      this.myDbContext = myDbContext;
    }

    public User Read(int userId) {
      var user = myDbContext.Users
        .AsNoTracking()
        .SingleOrDefault(x => x.UserId == userId);
      return user;
    }
  }
}
