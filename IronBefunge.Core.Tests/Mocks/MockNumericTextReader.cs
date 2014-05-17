using System.IO;

namespace IronBefunge.Core.Tests.Mocks
{
	internal sealed class MockNumericTextReader
		: TextReader
	{
		internal MockNumericTextReader(string value)
			: base()
		{
			this.Value = value;
		}

		public override string ReadLine()
		{
			return this.Value;
		}

		private string Value { get; set; }
	}
}
