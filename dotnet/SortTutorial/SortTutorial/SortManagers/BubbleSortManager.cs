using System;

namespace SortTutorial.SortManagers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TType">The type of the type.</typeparam>
	/// <seealso cref="SortTutorial.SortManagers.BaseSortManager{TType}" />
	internal class BubbleSortManager<TType> : BaseSortManager<TType>
		where TType : IComparable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BubbleSortManager{TType}"/> class.
		/// </summary>
		/// <param name="array">The array.</param>
		public BubbleSortManager(TType[] array) : base(array)
		{

		}

		/// <summary>
		/// Sorts the specified ascending.
		/// </summary>
		/// <param name="ascending">if set to <c>true</c> [ascending].</param>
		protected override void SortImplementation(bool ascending)
		{
			for (var index = 0; index < _array.Length; index++)
			{
				var isAnyChange = false;
				for (int j = 0; j < _array.Length - 1; j++)
				{
					if (IsValid(ascending, j + 1, j))
					{
						isAnyChange = true;
						Swap(j + 1, j);
					}
				}

				if (!isAnyChange)
				{
					break;
				}
			}
		}
	}
}