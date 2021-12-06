namespace IronBefunge.Tests.Mocks;

internal sealed class MockNumericTextReader
	: TextReader
{
	internal MockNumericTextReader(string value)
		: base() => this.Value = value;

	public override string ReadLine() => this.Value;

	private string Value { get; }
}