using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using IronBefunge.Core.InstructionHandlers;
using Spackle;
using Xunit;

namespace IronBefunge.Core.Tests.InstructionHandlers
{
	public abstract class InstructionHandlerTests
	{
		internal abstract ReadOnlyCollection<char> GetExpectedHandledInstructions();

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

					if (before != null)
					{
						before(context);
					}

					handler.Handle(context);
					var result = writer.GetStringBuilder().ToString();

					if (after != null)
					{
						after(context, result);
					}
				}
			}
		}

		[Fact]
		public void VerifyInstructions()
		{
			var handlerType = this.GetHandlerType();
			var handler = Activator.CreateInstance(handlerType) as InstructionHandler;

			Assert.True(handlerType.IsAssignableFrom(handler.GetType()));

			var expectedInstructions = this.GetExpectedHandledInstructions();

			Assert.Equal(expectedInstructions.Count, handler.Instructions.Count);

			foreach (var expectedInstruction in expectedInstructions)
			{
				Assert.True(handler.Instructions.Contains(expectedInstruction));
			}
		}
	}
}
