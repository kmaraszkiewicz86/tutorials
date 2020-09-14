using System.Collections.Generic;
using System.Linq;
using TodoDataModel.Core.Model;
using TodoWebApi.Models;

namespace TodoWebApi.Utils
{
	/// <summary>
	/// TodoWebApiModelUtils class.
	/// </summary>
	public static class TodoWebApiModelUtils
    {
		/// <summary>
		/// To the todo model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static TodoModel ToTodoModel(this TodoWebApiModel model)
		{
			if (model == null)
				return null;

			return new TodoModel(model.Id, model.Title, model.Completed, model.CreateTime);
		}

		/// <summary>
		/// To the todo web API model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static TodoWebApiModel ToTodoWebApiModel(this TodoModel model)
		{
			if (model == null)
				return null;

			return new TodoWebApiModel(model.Id, model.Title, model.Completed, model.CreateTime);
		}

		/// <summary>
		/// To the todo web API model list.
		/// </summary>
		/// <param name="models">The models.</param>
		/// <returns></returns>
		public static IEnumerable<TodoWebApiModel> ToTodoWebApiModelList (this IEnumerable<TodoModel> models)
		{
			if (models == null)
				return null;

			return models.Select(m => m.ToTodoWebApiModel());
		}
	}
}