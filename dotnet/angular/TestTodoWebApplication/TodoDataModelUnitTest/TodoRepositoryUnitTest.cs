using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TodoDataModel.Core.DI;
using TodoDataModel.Core.Model;
using TodoDataModel.Core.Repository;
using TodoDataModel.Exceptions;

namespace TodoDataModelUnitTest
{
	[TestClass]
    public class TodoRepositoryUnitTest
    {
		/// <summary>
		/// The todo repository
		/// </summary>
		public ITodoRepository _todoRepository;

		public TodoModel _model;

		/// <summary>
		/// Tests the initialize.
		/// </summary>
		[TestInitialize]
		public void TestInitialize()
		{
			_todoRepository = new TodoRepository();
		}

		/// <summary>
		/// Adds the valid data test method.
		/// </summary>
		[TestMethod]
		public void AddValidDataTestMethod()
		{
			_model = new TodoModel("new item1", false, DateTime.Now);
			_model = _todoRepository.Add(_model);

			_model.Id.Should().BeGreaterThan(0);

			var todoFromDatabase = _todoRepository.Get(_model.Id);

			todoFromDatabase.Should().BeEquivalentTo(_model);
		}

		/// <summary>
		/// Try add empty data test method.
		/// </summary>
		[TestMethod]
		public void AddTryAddEmptyDataTestMethod()
		{
			AddActionWithEmptyValueCommon(new TodoModel("", false, DateTime.Now));
			AddActionWithEmptyValueCommon(new TodoModel("test 1", false, DateTime.MinValue));
		}

		/// <summary>
		/// Try add empty model test method.
		/// </summary>
		[TestMethod]
		public void AddTryAddEmptyModelTestMethod()
		{
			AddActionWithEmptyValueCommon(null);
		}

		/// <summary>
		/// Check return items test method.
		/// </summary>
		[TestMethod]
		public void GetAllCheckReturnItemsTestMethod()
		{
			_model = new TodoModel("item x", true, DateTime.Now);

			var items = _todoRepository.GetAll().ToList();

			_model = _todoRepository.Add(_model);

			items.Add(_model);

			items = items.OrderBy(i => i.Id).ToList();

			var itemsFromDatabase = _todoRepository.GetAll()
				.OrderBy(i => i.Id)
				.ToList();

			items.Count().Should().Be(itemsFromDatabase.Count());
			items.Should().BeEquivalentTo(itemsFromDatabase);
		}

		/// <summary>
		/// Try get with no exists identifier.
		/// </summary>
		[TestMethod]
		public void GetByIdTryGetWithNoExistsIdTestMethod()
		{
			Action action = () => _todoRepository.Get(-10);

			action.Should().Throw<NotFoundException>();
		}

		/// <summary>
		/// Try get items test method.
		/// </summary>
		[TestMethod]
		public void GetByIdTryGetItemsTestMethod()
		{
			_model = new TodoModel("item x", true, DateTime.Now);
			_model = _todoRepository.Add(_model);

			var itemFromDatabase = _todoRepository.Get(_model.Id);
			_model.Should().BeEquivalentTo(itemFromDatabase);
		}

		/// <summary>
		/// Updates the try to update not exists identifier test method.
		/// </summary>
		[TestMethod]
		public void UpdateTryToUpdateNotExistsIdTestMethod()
		{
			Action action = () => _todoRepository.Update(-1000, new TodoModel("title 1", false, DateTime.Now));

			action.Should().Throw<NotFoundException>();
		}

		/// <summary>
		/// Check data after update test method.
		/// </summary>
		[TestMethod]
		public void UpdateCheckDataAfterUpdateTestMethod ()
		{
			_model = new TodoModel("item x", true, DateTime.Now);
			_model = _todoRepository.Add(_model);

			_model.Title = "item xyz";
			_model.Completed = false;
			_model.CreateTime = DateTime.Now;

			_todoRepository.Update(_model.Id, _model);
			var itemFromDatabase = _todoRepository.Get(_model.Id);

			_model.Should().BeEquivalentTo(itemFromDatabase);
		}

		/// <summary>
		/// Try update empty data test method.
		/// </summary>
		[TestMethod]
		public void UpdateTryUpdateEmptyDataTestMethod ()
		{
			_model = new TodoModel("item x", true, DateTime.Now);
			_model = _todoRepository.Add(_model);

			_model.Title = string.Empty;

			UpdateActionWithEmptyValueCommon(_model.Id, _model);
		}

		/// <summary>
		/// Try update empty data test method.
		/// </summary>
		[TestMethod]
		public void UpdateTryUpdateEmptyModelTestMethod()
		{
			_model = new TodoModel("item x", true, DateTime.Now);
			_model = _todoRepository.Add(_model);

			UpdateActionWithEmptyValueCommon(_model.Id, null);
		}

		/// <summary>
		/// Removes the try to remove no exists identifier test method.
		/// </summary>
		[TestMethod]
		public void RemoveTryToRemoveNoExistsIdTestMethod ()
		{
			Action action = () => _todoRepository.Remove(-999);

			action.Should().Throw<NotFoundException>();
		}

		/// <summary>
		/// Removes the try to remove no exists identifier test method.
		/// </summary>
		[TestMethod]
		public void RemoveTryToRemoveItemMethod()
		{
			_model = new TodoModel("item x", true, DateTime.Now);
			_model = _todoRepository.Add(_model);

			_todoRepository.Remove(_model.Id);

			Action action = () => _todoRepository.Get(_model.Id);

			action.Should().Throw<NotFoundException>();

			_model = null;
		}

		/// <summary>
		/// Actions the with empty value common.
		/// </summary>
		/// <param name="model">The model.</param>
		private void AddActionWithEmptyValueCommon(TodoModel model)
		{
			Action action = () =>
			{
				_todoRepository.Add(model);
			};

			action.Should().Throw<BadRequestException>();
		}

		/// <summary>
		/// Updates the action with empty value common.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="model">The model.</param>
		private void UpdateActionWithEmptyValueCommon(int id, TodoModel model)
		{
			Action action = () =>
			{
				_todoRepository.Update(id, model);
			};

			action.Should().Throw<BadRequestException>();
		}

		/// <summary>
		/// Cleanup a test.
		/// </summary>
		[TestCleanup]
		public void TestCleanup ()
		{
			if (_todoRepository != null && _model != null && _model.Id > 0)
			{
				_todoRepository.Remove(_model.Id);
			}
		}
	}
}