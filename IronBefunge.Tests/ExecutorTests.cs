using NUnit.Framework;
using Spackle;
using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;

namespace IronBefunge.Tests
{
	public static class ExecutorTests
	{
		[Test]
		public static void CreateWithNullRandomizer()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter(CultureInfo.CurrentCulture);
			Assert.That(() => new Executor(ImmutableArray.Create<Cell>(), reader, writer, (null as SecureRandom)!),
				Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void CreateWithNullReader()
		{
			using var writer = new StringWriter(CultureInfo.CurrentCulture);
			Assert.That(() => new Executor(ImmutableArray.Create<Cell>(), null!, writer),
				Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void CreateWithNullWriter()
		{
			using var reader = new StringReader(string.Empty);
			Assert.That(() => new Executor(ImmutableArray.Create<Cell>(), reader, null!),
				Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void CreateWithNullTrace()
		{
			using var reader = new StringReader(string.Empty);
			Assert.That(() => new Executor(ImmutableArray.Create<Cell>(), reader, null!, (null as TextWriter)!),
				Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void Execute()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var executor = new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer);
			Assert.That(() => executor.Execute(), Throws.Nothing);
		}

		[Test]
		public static void ExecuteWithTrace()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var trace = new StringWriter();
			using var executor = new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer, trace);
			Assert.That(() => executor.Execute(), Throws.Nothing);
		}

		[Test]
		public static void ExecuteWithTraceWhenTraceIsNull()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			Assert.That(() => new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer, (null as TextWriter)!), Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void ExecuteWithRandom()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var random = new SecureRandom();
			using var executor = new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer, random);
			Assert.That(() => executor.Execute(), Throws.Nothing);
		}

		[Test]
		public static void ExecuteWithRandomWhenTraceIsNull()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			Assert.That(() => new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer, (null as SecureRandom)!), Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void ExecuteWithTraceAndRandomizer()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var trace = new StringWriter();
			using var random = new SecureRandom();
			using var executor = new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer, trace, random);
			Assert.That(() => executor.Execute(), Throws.Nothing);
		}

		[Test]
		public static void ExecuteWithTraceAndRandomizerWhereTraceIsNull()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var random = new SecureRandom();
			Assert.That(() => new Executor(
				new Parser(new[] { ">  @" }).Parse(),
				reader, writer, null!, random), Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public static void ExecuteWithInStringMode()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var executor = new Executor(
				new Parser(new[] { "> \" \" @" }).Parse(),
				reader, writer, writer);
			Assert.That(() => executor.Execute(), Throws.Nothing);
		}
	}
}