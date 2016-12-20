using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using IronBefunge.Core.Tests.Mocks;
using Xunit;

namespace IronBefunge.Core.Tests
{
	public static class InterpreterTests
	{
		[Fact]
		public static void InterpretPrintBeholdIronBefungeProgram()
		{
			var lines = new string[] { 
				"\"!egnufeBnorI ,dloheB\">:#,_@" };

			var builder = new StringBuilder();

			using (var writer = new StringWriter(builder, CultureInfo.CurrentCulture))
			{
				using (var reader = new StringReader(string.Empty))
				{
					using (var interpreter = new Interpreter(lines, reader, writer))
					{
						interpreter.Interpret();
					}
				}
			}

			var result = builder.ToString();

			Assert.Equal("Behold, IronBefunge!", result);
		}

		[Fact]
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
				using (var reader = new StringReader(string.Empty))
				{
					using (var interpreter = new Interpreter(lines, reader, writer))
					{
						interpreter.Interpret();
					}
				}
			}

			var result = builder.ToString();

			Assert.Equal("Hello world!" + new string('\n', 1), result);
		}

		[Fact]
		public static void InterpretRandomizerGoingDownProgram()
		{
			InterpreterTests.Randomizer(Direction.Down, "2");
		}

		[Fact]
		public static void InterpretRandomizerGoingLeftProgram()
		{
			InterpreterTests.Randomizer(Direction.Left, "1");
		}

		[Fact]
		public static void InterpretRandomizerGoingRightProgram()
		{
			InterpreterTests.Randomizer(Direction.Right, "4");
		}

		[Fact]
		public static void InterpretRandomizerGoingUpProgram()
		{
			InterpreterTests.Randomizer(Direction.Up, "3");
		}

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
				using (var reader = new StringReader(string.Empty))
				{
					new Interpreter(lines, reader, writer, new MockSecureRandom(direction)).Interpret();
				}
			}

			var result = builder.ToString();

			Assert.Equal(expected, result);
		}
	}
}
