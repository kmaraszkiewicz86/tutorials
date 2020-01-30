using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TodoDataModel.Core.DI;
using TodoDataModel.Core.Model;
using TodoDataModel.DatabaseModel;
using TodoDataModel.Helper;

namespace TodoDataModel.Core.Repository
{
	/// <summary>
	/// TodoRepository class
	/// </summary>
	public class TodoRepository : BaseRepository, ITodoRepository
	{
		/// <summary>
		/// Adds the specified todo model.
		/// </summary>
		/// <param name="todoModel">The todo model.</param>
		public TodoModel Add(TodoModel todoModel)
		{
			return OnDbConnection(db =>
			{
				todoModel.Validate();
				var todo = todoModel.ToTodo();

				db.Todo.Add(todo);
				db.SaveChanges();

				return todo.ToTodoModel();
			});
		}

		/// <summary>
		/// Gets the specified todo model by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public TodoModel Get(int id)
		{
			return OnDbConnection(db => db.Todo.Where(t => t.Id == id)
						?.FirstOrDefault()
						.CheckVisibility()
						.ToTodoModel());
		}

		/// <summary>
		/// Gets all todos
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public IEnumerable<TodoModel> GetAll()
		{
			return OnDbConnection(db =>
				db.Todo.ToList().ToTodoModelList());
		}

		/// <summary>
		/// Removes the specified todo model by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Remove(int id)
		{
			OnDbConnection(db =>
			{
				db.Todo.Remove(db.Todo.Where(t => t.Id == id)
					.FirstOrDefault()
					.CheckVisibility());

				db.SaveChanges();
			});
		}

		/// <summary>
		/// Updates the specified todo model by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="todoModel">The todo model.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Update(int id, TodoModel todoModel)
		{
			todoModel.ValidateOnUpdate();

			OnDbConnection(db =>
			{
				var todo = db.Todo.Where(t => t.Id == id)
					.FirstOrDefault()
					.CheckVisibility();

				todo.Title = todoModel.Title;
				todo.Completed = todoModel.Completed;

				var entry = db.Entry<Todo>(todo);
				entry.Property(t => t.Title);
				entry.Property(t => t.Completed);
				entry.Property(t => t.CreateTime);

				entry.State = EntityState.Modified;

				db.SaveChanges();
			});
		}
	}
}