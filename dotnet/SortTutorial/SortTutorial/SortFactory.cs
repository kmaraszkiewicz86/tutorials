using System;
using System.Linq;
using System.Reflection;
using SortTutorial.Enums;
using SortTutorial.SortManagers;

namespace SortTutorial
{
	/// <summary>
	/// 
	/// </summary>
	internal static class SortFactory
	{
		/// <summary>
		/// Creates the base sort manager.
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="sortFactoryType">Type of the sort factory.</param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static BaseSortManager<TType> CreateBaseSortManager<TType>(this TType[] array, SortFactoryType sortFactoryType)
			where TType : IComparable
		{
			var type = Assembly.GetAssembly(typeof(SortFactory)).GetTypes()
				.Single(t => t.Name.StartsWith(sortFactoryType.ToString()));
			
			if (type == null)
			{
				throw new Exception($"Could not found class by {sortFactoryType.ToString()} name");
			}

			type = type.MakeGenericType(typeof(TType));

			return Activator.CreateInstance(type, new[] {array}) as BaseSortManager<TType>;
		}
	}
}