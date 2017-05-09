using System.IO;

namespace IronBefunge.Tests.Mocks
{
	internal sealed class MockAsciiTextReader
		: TextReader
	{
		internal MockAsciiTextReader(int value)
			: base() => this.Value = value;

		public override int Read() => this.Value;

		private int Value { get; set; }
	}
}
