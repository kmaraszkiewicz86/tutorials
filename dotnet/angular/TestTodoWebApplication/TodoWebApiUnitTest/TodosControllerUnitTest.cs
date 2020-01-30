using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoDataModel.Core.DI;
using TodoDataModel.Core.Model;
using TodoDataModel.Exceptions;
using TodoWebApi.Controllers;
using TodoWebApi.Models;

namespace TodoWebApiUnitTest
{
	/// <summary>
	/// TodosControllerUnitTest class
	/// </summary>
	[TestClass]
	public class TodosControllerUnitTest
	{
		/// <summary>
		/// The todos controller
		/// </summary>
		private TodosController _todosController;

		/// <summary>
		/// The todo repository mock
		/// </summary>
		private Mock<ITodoRepository> _todoRepositoryMock;

		/// <summary>
		/// Gets the test todo model.
		/// </summary>
		/// <value>
		/// The test todo model.
		/// </value>
		private IEnumerable<TodoModel> TestTodoModel =>
			new List<TodoModel>()
			{
						new TodoModel("test1", true, DateTime.Now),
						new TodoModel("test2", true, DateTime.Now),
						new TodoModel("test3", true, DateTime.Now)
			};

		/// <summary>
		/// Gets the single todo model.
		/// </summary>
		/// <value>
		/// The single todo model.
		/// </value>
		private TodoModel SingleTodoModel =>
			new TodoModel(1, "test 1", false, DateTime.Now);

		/// <summary>
		/// Tests the initialize.
		/// </summary>
		[TestInitialize]
		public void TestInitialize()
		{
			_todoRepositoryMock = new Mock<ITodoRepository>();
			_todosController = new TodosController((ITodoRepository)_todoRepositoryMock.Object);
		}

		/// <summary>
		/// Gets all should resturn items test method.
		/// </summary>
		[TestMethod]
		public void GetAllShouldResturnItemsTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.GetAll()).Returns(TestTodoModel);
			var result = ((JsonResult)_todosController.GetAll()).Value
				as IEnumerable<TodoWebApi.Models.TodoWebApiModel>;

			result.Count().Should().BeGreaterThan(0);

			_todoRepositoryMock.Verify(x => x.GetAll(), Times.Once);
		}

		/// <summary>
		/// Details should return not found exception test method.
		/// </summary>
		[TestMethod]
		public void GetDetailsShouldReturnNotFoundExceptionTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
				.Returns(() => throw new NotFoundException("Test error message"));
			var result = _todosController.GetDetails(-1000);

			result.Should().BeOfType<NotFoundObjectResult>();
		}

		/// <summary>
		/// Gets the details should return bad request exception test method.
		/// </summary>
		[TestMethod]
		public void GetDetailsShouldReturnBadRequestExceptionTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
				.Returns(() => throw new BadRequestException("Test error message"));
			var result = _todosController.GetDetails(-1000);

			result.Should().BeOfType<BadRequestObjectResult>();
		}

		/// <summary>
		/// Adds the should return ok test method.
		/// </summary>
		[TestMethod]
		public void AddShouldReturnOkTestMethod ()
		{
			_todoRepositoryMock.Setup(
				x => x.Add(It.Is<TodoModel>(t => t.Title == "test1" && !t.Completed 
					&& t.CreateTime == DateTime.Now)))
				.Returns(SingleTodoModel);

			var result = _todosController.Add(new TodoWebApiModel { Title = "test1", Completed = false, CreateTime = DateTime.Now });

			result.Should().BeOfType<JsonResult>();
		}

		/// <summary>
		/// Adds the should return bad request test method.
		/// </summary>
		[TestMethod]
		public void AddShouldReturnBadRequestTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Add(It.IsAny<TodoModel>())).Returns(() => throw new BadRequestException("Empty error"));
			var result = _todosController.Add(new TodoWebApiModel { Title = "test1", Completed = false, CreateTime = DateTime.Now });

			result.Should().BeOfType<BadRequestObjectResult>();
		}

		/// <summary>
		/// Updates the should return ok test method.
		/// </summary>
		[TestMethod]
		public void UpdateShouldReturnOkTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TodoModel>()));

			var result = _todosController.Update(1, new TodoWebApiModel { Id = 10,  Title = "test1", Completed = false, CreateTime = DateTime.Now });

			result.Should().BeOfType<OkResult>();
		}

		/// <summary>
		/// Updates the should return bad request test method.
		/// </summary>
		[TestMethod]
		public void UpdateShouldReturnBadRequestTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TodoModel>()))
				.Throws(new BadRequestException("Empty error"));

			var result = _todosController.Update(0, new TodoWebApiModel { Id = 10, Title = "test1", Completed = false, CreateTime = DateTime.Now });

			result.Should().BeOfType<BadRequestObjectResult>();
		}

		/// <summary>
		/// Updates the should return not found test method.
		/// </summary>
		[TestMethod]
		public void UpdateShouldReturnNotFoundTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TodoModel>()))
				.Throws(new NotFoundException("Empty error"));

			var result = _todosController.Update(0, new TodoWebApiModel { Id = 10, Title = "test1", Completed = false, CreateTime = DateTime.Now });

			result.Should().BeOfType<NotFoundObjectResult>();
		}

		/// <summary>
		/// Removes the should return ok test method.
		/// </summary>
		[TestMethod]
		public void RemoveShouldReturnOkTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Remove(It.IsAny<int>()));

			var result = _todosController.Remove(1);

			result.Should().BeOfType<OkResult>();
		}

		/// <summary>
		/// Removes the should return bad request test method.
		/// </summary>
		[TestMethod]
		public void RemoveShouldReturnBadRequestTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Remove(It.IsAny<int>()))
				.Throws(new BadRequestException("Empty error"));

			var result = _todosController.Remove(1);

			result.Should().BeOfType<BadRequestObjectResult>();
		}

		/// <summary>
		/// Removes the should return not found test method.
		/// </summary>
		[TestMethod]
		public void RemoveShouldReturnNotFoundTestMethod()
		{
			_todoRepositoryMock.Setup(x => x.Remove(It.IsAny<int>()))
				.Throws(new NotFoundException("Empty error"));

			var result = _todosController.Remove(1);

			result.Should().BeOfType<NotFoundObjectResult>();
		}
	}
}