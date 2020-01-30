using System;

namespace SortTutorial.SortManagers
{
	/// <summary>
	/// 
	/// </summary>
	internal abstract class BaseSortManager<TType> where TType : IComparable
	{
		/// <summary>
		/// The array
		/// </summary>
		protected TType[] _array;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseSortManager{TType}"/> class.
		/// </summary>
		/// <param name="array">The array.</param>
		protected BaseSortManager(TType[] array)
		{
			if (array == null)
				throw new Exception($"{nameof(_array)} is required");

			var tableLength = array.Length;

			if (tableLength == 0)
				throw new Exception($"{nameof(_array)} must have at least 2 elements");

			_array = array;
		}

		/// <summary>
		/// Sorts the specified ascending.
		/// </summary>
		/// <param name="ascending">if set to <c>true</c> [ascending].</param>
		public void Sort(bool ascending)
		{
			Console.WriteLine("=============================================================");
			Console.WriteLine($"{GetType().Name}");
			Console.WriteLine("=============================================================");

			var startDatetime = DateTime.Now;
			SortImplementation(ascending);
			Console.WriteLine("Elapsed time:");
			Console.WriteLine((DateTime.Now - startDatetime).ToString("G"));
		}

		/// <summary>
		/// Sorts the specified ascending.
		/// </summary>
		/// <param name="ascending">if set to <c>true</c> [ascending].</param>
		protected abstract void SortImplementation(bool ascending);

		/// <summary>
		/// Swaps the specified array.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="firstIndex">The first index.</param>
		/// <param name="secondIndex">Index of the second.</param>
		protected void Swap(int firstIndex, int secondIndex)
		{
			TType tmpValue = _array[firstIndex];
			_array[firstIndex] = _array[secondIndex];
			_array[secondIndex] = tmpValue;
		}

		/// <summary>
		/// Returns true if ... is valid.
		/// </summary>
		/// <param name="ascending">if set to <c>true</c> [ascending].</param>
		/// <param name="firstIndex">The first index.</param>
		/// <param name="secondIndex">Index of the second.</param>
		/// <returns>
		///   <c>true</c> if the specified ascending is valid; otherwise, <c>false</c>.
		/// </returns>
		protected bool IsValid(bool ascending, int firstIndex, int secondIndex)
		{
			if (ascending)
				return _array[firstIndex].CompareTo(_array[secondIndex]) < 0;
			else
				return _array[firstIndex].CompareTo(_array[secondIndex]) > 0;
		}
	}
}