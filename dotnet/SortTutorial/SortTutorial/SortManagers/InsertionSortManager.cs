using System;

namespace SortTutorial.SortManagers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TType">The type of the type.</typeparam>
	/// <seealso cref="SortTutorial.SortManagers.BaseSortManager{TType}" />
	internal class InsertionSortManager<TType> : BaseSortManager<TType> where TType : IComparable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InsertionSortManager{TType}"/> class.
		/// </summary>
		/// <param name="array">The array.</param>
		public InsertionSortManager(TType[] array) : base(array)
		{

		}

		/// <summary>
		/// Sorts the specified ascending.
		/// </summary>
		/// <param name="ascending">if set to <c>true</c> [ascending].</param>
		protected override void SortImplementation(bool ascending)
		{
			for (int index = 1; index < _array.Length; index++)
			{
				var j = index;
				while (j > 0 && IsValid(ascending, j, j - 1))
				{
					Swap(j - 1, j);
					j--;
				}
			}
		}
	}
}