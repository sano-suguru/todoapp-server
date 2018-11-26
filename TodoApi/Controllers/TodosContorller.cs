using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Controllers {
  [Route("api/users/{userId}/todos")]
  [ApiController]
  public class TodosController: ControllerBase {
    readonly ITodoRepository todoRepository;
    readonly IUserRepository userRepository;

    public TodosController(ITodoRepository todoRepository, IUserRepository userRepository) {
      this.todoRepository = todoRepository;
      this.userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Get(int userId) {
      var user = userRepository.Read(userId);
      if (user is null) { return NotFound(); }
      return Ok(todoRepository.List(userId));
    }

    [HttpGet("{todoId}", Name = "GetById")]
    public IActionResult Get([FromRoute]int userId, [FromRoute]int todoId) {
      var user = userRepository.Read(userId);
      if (user is null) {
        return NotFound();
      }
      var todo = todoRepository.Read(todoId);
      if (todo is null || todo.UserId != userId) {
        return NotFound();
      }
      return Ok(todo);
    }

    [HttpPost]
    public IActionResult Post(int userId, [FromBody] Todo todo) {
      var user = userRepository.Read(userId);
      if (user is null) {
        return NotFound();
      }

      if (todo.UserId != userId) {
        return Forbid();
      }

      todoRepository.Create(todo);
      return CreatedAtRoute("GetById", new { userId, todoId = todo.TodoId }, todo);
    }

    [HttpPut("{todoId}")]
    public IActionResult Put(int userId ,int todoId, [FromBody]Todo todo) {
      var user = userRepository.Read(userId);
      if (user is null) { return NotFound(); }

      var target = todoRepository.Read(todoId);
      if (target is null) { return NotFound(); }

      if (todo.UserId != userId) {
        return Forbid();
      }

      todoRepository.Update(target, todo);
      return CreatedAtRoute("GetById", new { userId, todoId }, target);
    }

    [HttpDelete("{todoId}")]
    public IActionResult Delete(int userId, int todoId) {
      var user = userRepository.Read(userId);
      if (user is null) { return NotFound(); }

      var target = todoRepository.Read(todoId);
      if (target is null) { return NotFound(); }

      if (target.UserId != userId) {
        return Forbid();
      }

      todoRepository.Delete(target);
      return NoContent();
    }
  }
}
