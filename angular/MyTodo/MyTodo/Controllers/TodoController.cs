using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTodo.Models;

namespace MyTodo.Controllers
{
    [Produces("application/json")]
    [Route("api/todos")]
    public class TodoController : Controller
    {
		private TodoContext _db;

		public TodoController(TodoContext context)
		{
			_db = context;
		}

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<Todo> Get()
        {
			return _db.Todos.ToList();
        }

		// GET: api/todos/pending-only
		[HttpGet]
		[Route("pending-only")]
		public IEnumerable<Todo> GetPendingOnly()
		{
			_db.Todos.RemoveRange(_db.Todos.Where(x =>
			x.Completed == true));
			_db.SaveChanges();
			return _db.Todos.ToList();
		}

		// POST api/todos
		[HttpPost]
		public Todo Post([FromBody]Todo value)
		{
			_db.Todos.Add(value);
			_db.SaveChanges();
			return value;
		}

		// PUT api/todos/id
		[HttpPut("{id}")]
		public Todo Put(int id, [FromBody]Todo value)
		{
			var todo = _db.Todos.FirstOrDefault(x => x.Id
			== id);
			todo.Title = value.Title;
			todo.Completed = value.Completed;
			_db.Entry(todo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_db.SaveChanges();
			return value;
		}

		// DELETE api/todos/id
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var todo = _db.Todos.FirstOrDefault(x => x.Id
			== id);
			_db.Entry(todo).State =
			Microsoft.EntityFrameworkCore.EntityState.Deleted;
			_db.SaveChanges();
		}
	}
}
