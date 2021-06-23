using IronBefunge.InstructionHandlers;
using NUnit.Framework;
using Spackle;
using System.Collections.Generic;
using System.IO;

namespace IronBefunge.Tests.InstructionHandlers
{
	public static class InstructionMapperTests
	{
		[Test]
		public static void Handle()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var trace = new StringWriter();

			Assert.Multiple(() =>
			{
				Assert.That(trace.GetStringBuilder().ToString().Length, Is.EqualTo(0));

				using (var randomizer = new SecureRandom())
				{
					var context = new ExecutionContext(new List<Cell>
					{
						new Cell(new Point(0, 0), '>')
					}, reader, writer, trace, randomizer);

					var mapper = new InstructionMapper();
					mapper.Handle(context);
				}

				Assert.That(trace.GetStringBuilder().ToString().Length, Is.EqualTo(0));
			});
		}

		[Test]
		public static void HandleWithUnimplementedInstruction()
		{
			using var reader = new StringReader(string.Empty);
			using var writer = new StringWriter();
			using var trace = new StringWriter();

			Assert.Multiple(() =>
			{
				Assert.That(trace.GetStringBuilder().ToString(), Is.Empty);

				using (var randomizer = new SecureRandom())
				{
					var context = new ExecutionContext(new()
					{
						new(new(0, 0), 'M')
					}, reader, writer, trace, randomizer);

					var mapper = new InstructionMapper();
					mapper.Handle(context);
				}

				var traceContent = trace.GetStringBuilder().ToString();
				Assert.That(traceContent,
					Contains.Substring("message = Warning: Instruction \"0, 0 - 'M'\" is not understood."), "Message");
				Assert.That(traceContent,
					Contains.Substring("Current = 0, 0 - 'M'"), "Current");
				Assert.That(traceContent,
					Contains.Substring("Direction = Right"), "Direction");
				Assert.That(traceContent,
					Contains.Substring("InStringMode = False"), "InStringMode");
				Assert.That(traceContent,
					Contains.Substring("Values = "), "Values");
			});
		}
	}
}