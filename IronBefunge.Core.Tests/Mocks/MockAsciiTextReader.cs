using System.IO;

namespace IronBefunge.Core.Tests.Mocks
{
	internal sealed class MockAsciiTextReader
		: TextReader
	{
		internal MockAsciiTextReader(int value)
			: base()
		{
			this.Value = value;
		}

		public override int Read()
		{
			return this.Value;
		}

		private int Value { get; set; }
	}
}
