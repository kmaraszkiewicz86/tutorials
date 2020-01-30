using System;

namespace SortTutorial.SortManagers
{
	internal class QuickSortManager<TType>: BaseSortManager<TType>
		where TType : IComparable
    {
		public QuickSortManager(TType[] array) : base(array)
        {
        }

		protected override void SortImplementation(bool isAscending)
		{
            SortImplementation(isAscending, 0, _array.Length - 1);
		}

        private void SortImplementation(bool isAscending, int lower, int upper)
        {
            var i = lower;
            var j = upper;
            var pivot = _array[(lower + upper) / 2];
            while (i < j)
            {
                while (_array[i].CompareTo(pivot) < 0) i++;
                while (_array[j].CompareTo(pivot) > 0) j--;
                if (i <= j)
                {
                    Swap(i, j);
                    i++;
                    j--;
                }
            }
            if (lower < j) SortImplementation(isAscending, lower, j);
            if (i < upper) SortImplementation(isAscending, i, upper);
        }
	}
}