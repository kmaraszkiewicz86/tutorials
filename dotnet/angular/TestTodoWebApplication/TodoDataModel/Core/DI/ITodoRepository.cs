using System.Collections.Generic;
using TodoDataModel.Core.Model;

namespace TodoDataModel.Core.DI
{
	/// <summary>
	/// ITodoRepository interface
	/// </summary>
	public interface ITodoRepository
	{
		/// <summary>
		/// Adds the specified todo model.
		/// </summary>
		/// <param name="todoModel">The todo model.</param>
		/// <returns><see cref="TodoModel"/></returns>
		TodoModel Add(TodoModel todoModel);

		/// <summary>
		/// Updates the specified todo model by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="todoModel">The todo model.</param>
		void Update(int id, TodoModel todoModel);

		/// <summary>
		/// Removes the specified todo model by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		void Remove(int id);

		/// <summary>
		/// Gets all todos
		/// </summary>
		/// <returns></returns>
		IEnumerable<TodoModel> GetAll();

		/// <summary>
		/// Gets the specified todo model by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		TodoModel Get(int id);
	}
}