using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TodoDataModel.Core.Model;
using TodoDataModel.DatabaseModel;
using TodoDataModel.Exceptions;
using System.ComponentModel;

namespace TodoDataModel.Helper
{
	/// <summary>
	/// TodoHelper class
	/// </summary>
	internal static class TodoHelper
	{
		/// <summary>
		/// To the todo.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static Todo ToTodo(this TodoModel model)
		{
			if (model == null)
				return null;

			return new Todo()
			{
				Id = model.Id,
				Title = model.Title,
				Completed = model.Completed,
				CreateTime = model.CreateTime
			};
		}

		/// <summary>
		/// To the todo model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static TodoModel ToTodoModel(this Todo model)
		{
			if (model == null)
				return null;

			return new TodoModel(model.Id, model.Title, model.Completed,
				model.CreateTime);
		}

		/// <summary>
		/// To the todo model list.
		/// </summary>
		/// <param name="todos">The todos.</param>
		/// <returns></returns>
		public static IEnumerable<TodoModel> ToTodoModelList(this IEnumerable<Todo> todos)
		{
			return todos?.Select(t => t.ToTodoModel());
		}

		/// <summary>
		/// Validates the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <exception cref="TodoDataModel.Exceptions.BadRequestException">
		/// Model cannot be empty
		/// or
		/// Title cannot be empty
		/// </exception>
		public static void Validate(this TodoModel model)
		{
			model.ValidateOnUpdate();

			if (model.CreateTime == DateTime.MinValue)
				throw new BadRequestException($"{model.GetPropertyAttributeName(nameof(model.CreateTime), typeof(DisplayNameAttribute))} cannot be empty");
		}

		/// <summary>
		/// Validates the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <exception cref="TodoDataModel.Exceptions.BadRequestException">
		/// Model cannot be empty
		/// or
		/// Title cannot be empty
		/// </exception>
		public static void ValidateOnUpdate(this TodoModel model)
		{
			if (model == null)
				throw new BadRequestException("Model cannot be empty");

			if (string.IsNullOrWhiteSpace(model.Title))
				throw new BadRequestException($"{model.GetPropertyAttributeName(nameof(model.Title), typeof(DisplayNameAttribute))} cannot be empty");
		}

		/// <summary>
		/// Checks the visibility.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns><see cref="Todo"/></returns>
		/// <exception cref="TodoDataModel.Exceptions.NotFoundException">Todo not found</exception>
		public static TModel CheckVisibility<TModel>(this TModel model)
			where TModel : class
		{
			if (model == null)
				throw new NotFoundException("Todo not found");

			return model;
		}

		/// <summary>
		/// Gets the name of the property attribute.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static string GetPropertyAttributeName(this TodoModel model, string propertyName, Type attributeName)
		{
			var attribute = model.GetType().GetProperty(propertyName)
				.GetCustomAttribute(attributeName);

			return attribute
				.GetType()
				.GetProperty("DisplayName")
				.GetValue(attribute, null)
				.ToString();
		}
	}
}