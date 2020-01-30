using System;

namespace TodoDataModel.DatabaseModel
{
	public partial class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
