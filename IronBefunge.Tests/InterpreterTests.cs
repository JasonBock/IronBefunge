using IronBefunge.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IronBefunge.Tests
{
	public static class InterpreterTests
	{
		[Test]
		public static async Task InterpretProgramFromFileWithCorrectFileExtension()
		{
			var fileName = $"{Guid.NewGuid().ToString("N")}{Interpreter.FileExtension}";
			await File.WriteAllLinesAsync(fileName, new string[] { "@" });

			try
			{
				var builder = new StringBuilder();

				using var writer = new StringWriter(builder, CultureInfo.CurrentCulture);
				using var reader = new StringReader(string.Empty);
				using var interpreter = new Interpreter(new FileInfo(fileName), reader, writer);
			}
			finally
			{
				File.Delete(fileName);
			}
		}

		[Test]
		public static async Task InterpretProgramFromFileWithIncorrectFileExtension()
		{
			var fileName = $"{Guid.NewGuid().ToString("N")}.bf";
			await File.WriteAllLinesAsync(fileName, new string[] { "@" });

			try
			{
				var builder = new StringBuilder();

				using var writer = new StringWriter(builder, CultureInfo.CurrentCulture);
				using var reader = new StringReader(string.Empty);
				Assert.That(() => { using var interpreter = new Interpreter(new FileInfo(fileName), reader, writer); }, 
					Throws.TypeOf<ArgumentException>());

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
				using var interpreter = new Interpreter(lines, reader, writer);
				interpreter.Interpret();
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
				using var interpreter = new Interpreter(lines, reader, writer);
				interpreter.Interpret();
			}

			var result = builder.ToString();

			Assert.That(result, Is.EqualTo($"Hello world!{new string('\n', 1)}"), nameof(result));
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

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
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
				new Interpreter(lines, reader, writer, new MockSecureRandom(direction)).Interpret();
			}

			var result = builder.ToString();

			Assert.That(result, Is.EqualTo(expected), nameof(result));
		}
	}
}