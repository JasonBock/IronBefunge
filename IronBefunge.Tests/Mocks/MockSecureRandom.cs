using Spackle;

namespace IronBefunge.Tests.Mocks
{
	internal sealed class MockSecureRandom
		: SecureRandom
	{
		private int value;

		public MockSecureRandom(Direction hardCoded)
			: base() => this.value = (int)hardCoded;

		public override int Next(int maxValue) => this.value;
	}
}
