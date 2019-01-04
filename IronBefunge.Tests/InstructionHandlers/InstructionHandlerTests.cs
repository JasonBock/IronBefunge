using IronBefunge.InstructionHandlers;
using Spackle;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Reflection;
using Xunit;

namespace IronBefunge.Tests.InstructionHandlers
{
	public abstract class InstructionHandlerTests
	{
		internal abstract ImmutableArray<char> GetExpectedHandledInstructions();

		internal abstract Type GetHandlerType();

		internal static void Run(IInstructionHandler handler, List<Cell> cells,
			Action<ExecutionContext> before, Action<ExecutionContext, string> after)
		{
			using (var reader = new StringReader(string.Empty))
			{
				using (var random = new SecureRandom())
				{
					InstructionHandlerTests.Run(handler, cells, before, after,
						random, reader);
				}
			}
		}

		internal static void Run(IInstructionHandler handler, List<Cell> cells,
			Action<ExecutionContext> before, Action<ExecutionContext, string> after,
			TextReader reader)
		{
			using (var random = new SecureRandom())
			{
				InstructionHandlerTests.Run(handler, cells, before, after,
					random, reader);
			}
		}

		internal static void Run(IInstructionHandler handler, List<Cell> cells,
			Action<ExecutionContext> before, Action<ExecutionContext, string> after,
			SecureRandom randomizer)
		{
			using (var reader = new StringReader(string.Empty))
			{
				InstructionHandlerTests.Run(handler, cells, before, after,
					randomizer, reader);
			}
		}

		private static void Run(IInstructionHandler handler, List<Cell> cells,
			Action<ExecutionContext> before, Action<ExecutionContext, string> after,
			SecureRandom randomizer, TextReader reader)
		{
			using (var writer = new StringWriter(CultureInfo.CurrentCulture))
			{
				using (reader)
				{
					var context = new ExecutionContext(cells, reader, writer, randomizer);

					before?.Invoke(context);

					handler.Handle(context);
					var result = writer.GetStringBuilder().ToString();

					after?.Invoke(context, result);
				}
			}
		}

		[Fact]
		public void VerifyInstructions()
		{
			var handlerType = this.GetHandlerType();
			var handler = Activator.CreateInstance(handlerType) as InstructionHandler;

			Assert.True(handlerType.GetTypeInfo().IsAssignableFrom(handler.GetType()));

			var expectedInstructions = this.GetExpectedHandledInstructions();

			Assert.Equal(expectedInstructions.Length, handler.Instructions.Length);

			foreach (var expectedInstruction in expectedInstructions)
			{
				Assert.Contains(expectedInstruction, handler.Instructions);
			}
		}
	}
}
