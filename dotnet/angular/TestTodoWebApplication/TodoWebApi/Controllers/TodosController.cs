using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using TodoDataModel.Core.DI;
using TodoDataModel.Exceptions;
using TodoWebApi.Models;
using TodoWebApi.Utils;

namespace TodoWebApi.Controllers
{
	/// <summary>
	/// TodosController class.
	/// </summary>
	/// <seealso cref="Controller" />
	[Route("api/[controller]")]
	[EnableCors("SiteCorsPolicy")]
	public class TodosController : Controller
	{
		/// <summary>
		/// The todo repository
		/// </summary>
		private ITodoRepository _todoRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="TodosController"/> class.
		/// </summary>
		/// <param name="todoRepository">The todo repository.</param>
		public TodosController(ITodoRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult GetAll()
		{
			return OnActionResultAction(() => _todoRepository
				.GetAll()
				.ToTodoWebApiModelList());
		}

		/// <summary>
		/// Gets the details.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public ActionResult GetDetails(int id)
		{
			return OnActionResultAction(() => _todoRepository
				.Get(id).ToTodoWebApiModel());
		}

		/// <summary>
		/// Adds the specified todo model.
		/// </summary>
		/// <param name="todoModel">The todo model.</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Add([FromBody] TodoWebApiModel todoModel)
		{
			return OnActionResultAction(() => _todoRepository
				.Add(todoModel.ToTodoModel()));
		}

		/// <summary>
		/// Updates the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="todoModel">The todo model.</param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public ActionResult Update(int id, [FromBody] TodoWebApiModel todoModel)
		{
			return OnActionResultAction(() => _todoRepository
				.Update(id, todoModel.ToTodoModel()));
		}

		/// <summary>
		/// Removes the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public ActionResult Remove(int id)
		{
			return OnActionResultAction(() => _todoRepository
				.Remove(id));
		}

		/// <summary>
		/// Called when web api execute.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		private ActionResult OnActionResultAction (Action action)
		{
			try
			{
				action();
				return Ok();
			}
			catch (BadRequestException err)
			{
				return BadRequest(err.Message);
			}
			catch (NotFoundException err)
			{
				return NotFound(err.Message);
			}
		}

		/// <summary>
		/// Called when action result action.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		private ActionResult OnActionResultAction<TModel>(Func<TModel> action)
			where TModel: class
		{
			try
			{
				return Json(action());
			}
			catch (BadRequestException err)
			{
				return BadRequest(err.Message);
			}
			catch (NotFoundException err)
			{
				return NotFound(err.Message);
			}
		}
	}
}