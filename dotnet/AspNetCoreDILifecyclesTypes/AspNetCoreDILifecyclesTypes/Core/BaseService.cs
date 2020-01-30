using System;

namespace AspNetCoreDILifecyclesTypes.Core
{
	public abstract class BaseService
	{
		private readonly Guid _testGuid;

		protected BaseService()
		{
			_testGuid = Guid.NewGuid();
		}

		public string GetGuidString()
		{
			return _testGuid.ToString();
		}
	}
}
