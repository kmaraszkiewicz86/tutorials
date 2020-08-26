using System;
using System.ComponentModel;

namespace TodoDataModel.Core.Model
{
	/// <summary>
	/// Class todo
	/// </summary>
	public class TodoModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[DisplayName("Identifier")]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		[DisplayName("Title")]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Todo"/> is completed.
		/// </summary>
		/// <value>
		///   <c>true</c> if task completed; otherwise, <c>false</c>.
		/// </value>
		[DisplayName("Completed")]
		public bool Completed { get; set; }

		/// <summary>
		/// Gets or sets the create time.
		/// </summary>
		/// <value>
		/// The create time.
		/// </value>
		[DisplayName("Create time")]
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TodoModel"/> class.
		/// </summary>
		public TodoModel()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TodoModel"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="title">The title.</param>
		/// <param name="completed">if set to <c>true</c> [completed].</param>
		/// <param name="createTime">The create time.</param>
		public TodoModel(int id, string title, bool completed, DateTime createTime)
		{
			Id = id;
			Title = title;
			Completed = completed;
			CreateTime = createTime;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TodoModel"/> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="completed">if set to <c>true</c> [completed].</param>
		/// <param name="createTime">The create time.</param>
		public TodoModel(string title, bool completed, DateTime createTime)
		{
			Title = title;
			Completed = completed;
			CreateTime = createTime;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as TodoModel);
		}

		/// <summary>
		/// Equalses the specified todo model.
		/// </summary>
		/// <param name="todoModel">The todo model.</param>
		/// <returns></returns>
		public bool Equals (TodoModel todoModel)
		{
			return todoModel.Id == Id
				&& todoModel.Title.Equals(Title)
				&& todoModel.Completed == Completed
				&& todoModel.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
					.Equals(CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
		}
	}
}
