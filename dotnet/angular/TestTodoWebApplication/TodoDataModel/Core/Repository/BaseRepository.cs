using System;
using TodoDataModel.DatabaseModel;

namespace TodoDataModel.Core.Repository
{
	/// <summary>
	/// BaseRepository class.
	/// </summary>
	public abstract class BaseRepository
	{
		/// <summary>
		/// Called when database connection.
		/// </summary>
		/// <param name="action">The action.</param>
		protected void OnDbConnection(Action<TodoContext> action)
		{
			using (var client = new TodoContext())
			{
				action(client);
			}
		}

		/// <summary>
		/// Called when database connection.
		/// </summary>
		/// <typeparam name="T">Any class</typeparam>
		/// <param name="actionWithReturn">The action with return.</param>
		/// <returns><typeparamref name="T"/></returns>
		protected T OnDbConnection<T>(Func<TodoContext, T> actionWithReturn)
					where T : class
		{
			var result = default(T);

			OnDbConnection(db =>
			{
				result = actionWithReturn(db);
			});

			return result;
		}
	}
}
