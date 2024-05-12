using NUnit.Framework;
using Spackle;
using System.Collections.Immutable;
using System.Globalization;

namespace IronBefunge.Tests;

public static class ExecutorTests
{
	[Test]
	public static void CreateWithNullRandomizer()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter(CultureInfo.CurrentCulture);
		Assert.That(() => new Executor([], reader, writer, (null as SecureRandom)!),
			Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void CreateWithNullReader()
	{
		using var writer = new StringWriter(CultureInfo.CurrentCulture);
		Assert.That(() => new Executor([], null!, writer),
			Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void CreateWithNullWriter()
	{
		using var reader = new StringReader(string.Empty);
		Assert.That(() => new Executor([], reader, null!),
			Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void CreateWithNullTrace()
	{
		using var reader = new StringReader(string.Empty);
		Assert.That(() => new Executor([], reader, null!, (null as TextWriter)!),
			Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void Execute()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var executor = new Executor(
			new Parser([">  @"]).Parse(),
			reader, writer);
		Assert.That(() => executor.Execute(), Is.EqualTo(0));
	}

	[Test]
	public static void ExecuteWithTrace()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var trace = new StringWriter();
		using var executor = new Executor(
			new Parser([">  @"]).Parse(),
			reader, writer, trace);
		Assert.That(() => executor.Execute(), Is.EqualTo(0));
	}

	[Test]
	public static void ExecuteWithTraceWhenTraceIsNull()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		Assert.That(() => new Executor(
			new Parser([">  @"]).Parse(),
			reader, writer, (null as TextWriter)!), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void ExecuteWithRandom()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var random = new SecureRandom();
		using var executor = new Executor(
			new Parser([">  @"]).Parse(),
			reader, writer, random);
		Assert.That(() => executor.Execute(), Is.EqualTo(0));
	}

	[Test]
	public static void ExecuteWithRandomWhenTraceIsNull()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		Assert.That(() => new Executor(
			new Parser([">  @"]).Parse(),
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
			new Parser([">  @"]).Parse(),
			reader, writer, trace, random);
		Assert.That(() => executor.Execute(), Is.EqualTo(0));
	}

	[Test]
	public static void ExecuteWithTraceAndRandomizerWhereTraceIsNull()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var random = new SecureRandom();
		Assert.That(() => new Executor(
			new Parser([">  @"]).Parse(),
			reader, writer, null!, random), Throws.TypeOf<ArgumentNullException>());
	}

	[Test]
	public static void ExecuteWithInStringMode()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var executor = new Executor(
			new Parser(["> \" \" @"]).Parse(),
			reader, writer, writer);
		Assert.That(() => executor.Execute(), Is.EqualTo(0));
	}

	[Test]
	public static void ExecuteWithQuitInstruction()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var executor = new Executor(
			new Parser([">9q"]).Parse(),
			reader, writer, writer);
		Assert.That(() => executor.Execute(), Is.EqualTo(9));
	}

	[Test]
	public static void ExecuteWithQuitInstructionAndNothingOnTheStack()
	{
		using var reader = new StringReader(string.Empty);
		using var writer = new StringWriter();
		using var executor = new Executor(
			new Parser([">q"]).Parse(),
			reader, writer, writer);
		Assert.That(() => executor.Execute(), Is.EqualTo(0));
	}
}