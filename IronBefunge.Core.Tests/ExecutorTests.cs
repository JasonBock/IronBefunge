using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Xunit;

namespace IronBefunge.Core.Tests
{
	public static class ExecutorTests
	{
		[Fact]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
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
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public static void CreateWithNullReader()
		{
			using (var writer = new StringWriter(CultureInfo.CurrentCulture))
			{
				Assert.Throws<ArgumentNullException>(() =>
					new Executor(ImmutableArray.Create<Cell>(), null, writer));
			}
		}

		[Fact]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
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