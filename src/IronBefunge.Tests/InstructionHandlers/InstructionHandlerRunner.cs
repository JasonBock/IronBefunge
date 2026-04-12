using IronBefunge.InstructionHandlers;
using System.Globalization;
using System.Security.Cryptography;

namespace IronBefunge.Tests.InstructionHandlers;

internal static class InstructionHandlerRunner
{
	internal static void Run(IInstructionHandler handler, List<Cell> cells,
		Action<ExecutionContext>? before, Action<ExecutionContext, string>? after)
	{
		using var reader = new StringReader(string.Empty);
		using var random = RandomNumberGenerator.Create();
		InstructionHandlerRunner.Run(handler, cells, before, after,
			random, reader);
	}

	internal static void Run(IInstructionHandler handler, List<Cell> cells,
		Action<ExecutionContext>? before, Action<ExecutionContext, string>? after,
		TextReader reader)
	{
		using var random = RandomNumberGenerator.Create();
		InstructionHandlerRunner.Run(handler, cells, before, after,
			random, reader);
	}

	internal static void Run(IInstructionHandler handler, List<Cell> cells,
		Action<ExecutionContext>? before, Action<ExecutionContext, string>? after,
		RandomNumberGenerator randomizer)
	{
		using var reader = new StringReader(string.Empty);
		InstructionHandlerRunner.Run(handler, cells, before, after,
			randomizer, reader);
	}

	private static void Run(IInstructionHandler handler, List<Cell> cells,
		Action<ExecutionContext>? before, Action<ExecutionContext, string>? after,
		RandomNumberGenerator randomizer, TextReader reader)
	{
		using var writer = new StringWriter(CultureInfo.CurrentCulture);
		using (reader)
		{
			var context = new ExecutionContext(cells, reader, writer, null!, randomizer);

			before?.Invoke(context);

			handler.Handle(context);
			var result = writer.GetStringBuilder().ToString();

			after?.Invoke(context, result);
		}
	}
}