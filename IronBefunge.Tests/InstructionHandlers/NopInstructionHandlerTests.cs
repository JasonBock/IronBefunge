using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IronBefunge.Tests.InstructionHandlers
{
	public sealed class NopInstructionHandlerTests
		: InstructionHandlerTests
	{
		internal override ImmutableArray<char> GetExpectedHandledInstructions() =>
			ImmutableArray.Create(NopInstructionHandler.NopInstruction);

		internal override Type GetHandlerType() => typeof(NopInstructionHandler);

		[Test]
		public static void HandleGet()
		{
			var cells = new List<Cell>() 
			{ 
				new Cell(new Point(0, 0), NopInstructionHandler.NopInstruction)
			};

			var stackCount = 0;
			var direction = Direction.Down;

			InstructionHandlerTests.Run(new NopInstructionHandler(), cells, (context) =>
			{
				stackCount = context.Values.Count;
				direction = context.Direction;
			}, (context, result) =>
			{
				Assert.Multiple(() =>
				{
					Assert.That(context.Values.Count, Is.EqualTo(stackCount), nameof(context.Values.Count));
					Assert.That(context.Direction, Is.EqualTo(direction), nameof(context.Direction));
				});
			});
		}
	}
}