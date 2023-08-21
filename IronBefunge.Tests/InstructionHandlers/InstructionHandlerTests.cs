using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System.Collections.Immutable;
using System.Reflection;

namespace IronBefunge.Tests.InstructionHandlers;

public abstract class InstructionHandlerTests
{
	protected abstract ImmutableArray<char> GetExpectedHandledInstructions();

	protected abstract Type GetHandlerType();

	[Test]
	public void VerifyInstructions()
	{
		var handlerType = this.GetHandlerType();
		var handler = (Activator.CreateInstance(handlerType) as InstructionHandler)!;
		var expectedInstructions = this.GetExpectedHandledInstructions();

		Assert.Multiple(() =>
		{
			Assert.That(handlerType.GetTypeInfo().IsAssignableFrom(handler.GetType()), Is.True, nameof(handlerType));
			Assert.That(handler.Instructions, Has.Length.EqualTo(expectedInstructions.Length), nameof(handler.Instructions.Length));

			foreach (var expectedInstruction in expectedInstructions)
			{
				Assert.That(handler.Instructions, Does.Contain(expectedInstruction), expectedInstruction.ToString());
			}
		});
	}
}