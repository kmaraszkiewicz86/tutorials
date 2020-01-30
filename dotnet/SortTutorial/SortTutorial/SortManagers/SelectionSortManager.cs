using System;

namespace SortTutorial.SortManagers
{
	internal class SelectionSortManager<TType> : BaseSortManager<TType> where TType: IComparable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SelectionSortManager{TType}"/> class.
		/// </summary>
		/// <param name="array">The array.</param>
		public SelectionSortManager(TType[] array) : base(array)
		{
		}

		/// <summary>
		/// SORTOWANIE PRZEZ WYBIERANIE
		/// Algorytm tworzy dzieli tablice na dwie grupy.
		/// Posortowana i nieposrotowana.
		/// W pierwszym kroku wyszukuje najmniejszej wartosci z tablicy i zamienia ja pozycjami z pierwszym elementem
		/// Wskaznij przechodzi na nastepny index. Wyszukiwanie zaczyna sie od nastepnego indeksu i szuka najnmniejszego elementu ze zbioru.
		/// Wykonuje wyszukiwanie do momentu az dotrze to ostatniego elementu
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="ascending">The array.</param>
		/// <exception cref="Exception">
		/// array
		/// </exception>
		protected override void SortImplementation(bool ascending)
		{
			var tableLength = _array.Length;

			for (var index = 0; index < tableLength; index++)
			{
				var minValueIndex = index;
				for (int i = index; i < tableLength; i++)
				{
					if (_array[i].CompareTo(_array[minValueIndex]) < 0)
					{
						minValueIndex = i;
					}
				}

				Swap(index, minValueIndex);
			}
		}
	}
}