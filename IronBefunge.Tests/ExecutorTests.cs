using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using Xunit;

namespace IronBefunge.Tests
{
	public static class ExecutorTests
	{
		[Fact]
		public static void CreateWithNullRandomizer()
		{
			using (var reader = new StringReader(string.Empty))
			{
				using (var writer = new StringWriter(CultureInfo.CurrentCulture))
				{
					Assert.Throws<ArgumentNullException>(() =>
						new Executor(ImmutableArray.Create<Cell>(), reader, writer, null));
				}
			}
		}

		[Fact]
		public static void CreateWithNullReader()
		{
			using (var writer = new StringWriter(CultureInfo.CurrentCulture))
			{
				Assert.Throws<ArgumentNullException>(() =>
					new Executor(ImmutableArray.Create<Cell>(), null, writer));
			}
		}

		[Fact]
		public static void CreateWithNullWriter()
		{
			using (var reader = new StringReader(string.Empty))
			{
				Assert.Throws<ArgumentNullException>(() =>
					new Executor(ImmutableArray.Create<Cell>(), reader, null));
			}
		}
	}
}