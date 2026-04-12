using System.Security.Cryptography;

namespace IronBefunge.Tests.Mocks;

internal sealed class MockSecureRandom
	: RandomNumberGenerator
{
	private readonly Direction direction;

	public MockSecureRandom(Direction direction)
		: base() => this.direction = direction;

	public override void GetBytes(byte[] data)
	{
		data[0] = (byte)this.direction;
		data[1] = 0;
		data[2] = 0;
		data[3] = 0;
	}
}