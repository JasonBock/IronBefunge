using IronBefunge.Tests.Mocks;
using NUnit.Framework;
using Spackle;
using System.Globalization;
using System.Text;

namespace IronBefunge.Tests;

public static class InterpreterTests
{
	[Test]
	public static void InterpretWithNullFile()
	{
		var builder = new StringBuilder();
		using var writer = new StringWriter(builder);
		using var reader = new StringReader(string.Empty);
		Assert.That(() => new Interpreter((null as FileInfo)!, reader, writer),
			Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static async Task InterpretProgramFromFileWithCorrectFileExtension()
	{
		var fileName = $"{Guid.NewGuid():N}{Interpreter.FileExtension}";
		await File.WriteAllLinesAsync(fileName, ["@"]).ConfigureAwait(false);

		try
		{
			var builder = new StringBuilder();
			using var writer = new StringWriter(builder);
			using var reader = new StringReader(string.Empty);
			var interpreter = new Interpreter(new FileInfo(fileName), reader, writer);
			Assert.That(interpreter.Interpret, Is.EqualTo(0));
		}
		finally
		{
			File.Delete(fileName);
		}
	}

	[Test]
	public static async Task InterpretProgramFromFileWithRandom()
	{
		var fileName = $"{Guid.NewGuid():N}{Interpreter.FileExtension}";
		await File.WriteAllLinesAsync(fileName, ["@"]).ConfigureAwait(false);

		try
		{
			var builder = new StringBuilder();
			using var writer = new StringWriter(builder);
			using var reader = new StringReader(string.Empty);
			var random = new SecureRandom();
			var interpreter = new Interpreter(new FileInfo(fileName), reader, writer, random);
			Assert.That(interpreter.Interpret, Is.EqualTo(0));
		}
		finally
		{
			File.Delete(fileName);
		}
	}

	[Test]
	public static async Task InterpretProgramFromFileWithTrace()
	{
		var fileName = $"{Guid.NewGuid():N}{Interpreter.FileExtension}";
		await File.WriteAllLinesAsync(fileName, ["> @"]).ConfigureAwait(false);

		try
		{
			Assert.Multiple(() =>
			{
				var builder = new StringBuilder();
				var traceBuilder = new StringBuilder();
				using var writer = new StringWriter(builder);
				using var reader = new StringReader(string.Empty);
				using (var trace = new StringWriter(traceBuilder))
				{
					var interpreter = new Interpreter(new FileInfo(fileName), reader, writer, trace);
					Assert.That(() => interpreter.Interpret(), Is.EqualTo(0));
				}
				Assert.That(traceBuilder.ToString(), Is.Not.EqualTo(string.Empty));
			});
		}
		finally
		{
			File.Delete(fileName);
		}
	}

	[Test]
	public static void InterpretProgramWithTraceAndRandom() =>
		Assert.Multiple(() =>
		{
			var builder = new StringBuilder();
			var traceBuilder = new StringBuilder();
			using var writer = new StringWriter(builder);
			var random = new SecureRandom();
			using var reader = new StringReader(string.Empty);
			using (var trace = new StringWriter(traceBuilder))
			{
				var interpreter = new Interpreter(["> @"], reader, writer, trace, random);
				Assert.That(() => interpreter.Interpret(), Is.EqualTo(0));
			}
			Assert.That(traceBuilder.ToString(), Is.Not.EqualTo(string.Empty));
		});

	[Test]
	public static async Task InterpretProgramFromFileWithTraceAndRandom()
	{
		var fileName = $"{Guid.NewGuid():N}{Interpreter.FileExtension}";
		await File.WriteAllLinesAsync(fileName, ["> @"]).ConfigureAwait(false);

		try
		{
			Assert.Multiple(() =>
			{
				var builder = new StringBuilder();
				var traceBuilder = new StringBuilder();
				using var writer = new StringWriter(builder);
				var random = new SecureRandom();
				using var reader = new StringReader(string.Empty);
				using (var trace = new StringWriter(traceBuilder))
				{
					var interpreter = new Interpreter(new FileInfo(fileName), reader, writer, trace, random);
					Assert.That(() => interpreter.Interpret(), Is.EqualTo(0));
				}
				Assert.That(traceBuilder.ToString(), Is.Not.EqualTo(string.Empty));
			});
		}
		finally
		{
			File.Delete(fileName);
		}
	}

	[Test]
	public static async Task InterpretProgramFromFileWithIncorrectFileExtension()
	{
		var fileName = $"{Guid.NewGuid():N}.bf";
		await File.WriteAllLinesAsync(fileName, ["@"]).ConfigureAwait(false);

		try
		{
			var builder = new StringBuilder();

			using var writer = new StringWriter(builder);
			using var reader = new StringReader(string.Empty);
			Assert.That(() => { var interpreter = new Interpreter(new FileInfo(fileName), reader, writer); },
				Throws.TypeOf<ArgumentException>()
					.And.Message.EqualTo($"The file extension should be .b98; it is .bf (Parameter 'file')"));
		}
		finally
		{
			File.Delete(fileName);
		}
	}

	[Test]
	public static void InterpretPrintBeholdIronBefungeProgram()
	{
		var lines = new string[] {
				"\"!egnufeBnorI ,dloheB\">:#,_@" };

		var builder = new StringBuilder();

		using (var writer = new StringWriter(builder, CultureInfo.CurrentCulture))
		{
			using var reader = new StringReader(string.Empty);
			var interpreter = new Interpreter(lines, reader, writer);
			_ = interpreter.Interpret();
		}

		var result = builder.ToString();

		Assert.That(result, Is.EqualTo("Behold, IronBefunge!"), nameof(result));
	}

	[Test]
	public static void InterpretHelloWorldProgram()
	{
		var lines = new string[] {
				">                v",
				">v\"Hello world!\"0<",
				",:",
				"^_25*,@" };

		var builder = new StringBuilder();

		using (var writer = new StringWriter(builder, CultureInfo.CurrentCulture))
		{
			using var reader = new StringReader(string.Empty);
			var interpreter = new Interpreter(lines, reader, writer);
			_ = interpreter.Interpret();
		}

		var result = builder.ToString();

		Assert.That(result, Is.EqualTo($"Hello world!{new string('\n', 1)}"), nameof(result));
	}

	[Test]
	public static void InterpretHelloWorldProgramWithTrace()
	{
		var lines = new string[] {
				">                v",
				">v\"Hello world!\"0<",
				",:",
				"^_25*,@" };

		var builder = new StringBuilder();
		var traceBuilder = new StringBuilder();

		using (var writer = new StringWriter(builder, CultureInfo.CurrentCulture))
		{
			using var trace = new StringWriter(traceBuilder);
			using var reader = new StringReader(string.Empty);
			var interpreter = new Interpreter(lines, reader, writer, trace);
			_ = interpreter.Interpret();
		}

		var result = builder.ToString();

		Assert.Multiple(() =>
		{
			Assert.That(result, Is.EqualTo($"Hello world!{new string('\n', 1)}"), nameof(result));
			Assert.That(traceBuilder.ToString(), Is.Not.EqualTo(string.Empty), "trace");
		});
	}

	[Test]
	public static void InterpretRandomizerGoingDownProgram() =>
		InterpreterTests.Randomizer(Direction.Down, "2");

	[Test]
	public static void InterpretRandomizerGoingLeftProgram() =>
		InterpreterTests.Randomizer(Direction.Left, "1");

	[Test]
	public static void InterpretRandomizerGoingRightProgram() =>
		InterpreterTests.Randomizer(Direction.Right, "4");

	[Test]
	public static void InterpretRandomizerGoingUpProgram() =>
		InterpreterTests.Randomizer(Direction.Up, "3");

	private static void Randomizer(Direction direction, string expected)
	{
		var lines = new string[] {
				"v  > .v",
				"   3",
				">#v?4.@",
				"  12",
				"  >> .^" };

		var builder = new StringBuilder();

		using (var writer = new StringWriter(builder, CultureInfo.CurrentCulture))
		{
			using var reader = new StringReader(string.Empty);
			var randomizer = new MockSecureRandom(direction);
			var interpreter = new Interpreter(lines, reader, writer, randomizer);
			_ = interpreter.Interpret();
		}

		var result = builder.ToString();

		Assert.That(result, Is.EqualTo(expected), nameof(result));
	}
}