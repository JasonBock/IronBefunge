using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IronBefunge.Core.InstructionHandlers
{
	internal sealed class SpaceInstructionHandler
		: InstructionHandler
	{
		internal const char GetInstruction = 'g';
		internal const char PutInstruction = 'p';

		internal override ReadOnlyCollection<char> GetInstructions()
		{
			return new List<char>() { SpaceInstructionHandler.GetInstruction,
				SpaceInstructionHandler.PutInstruction }.AsReadOnly();
		}

		private static void HandleGet(ExecutionContext context)
		{
			context.EnsureStack(2);
			var y = context.Values.Pop();
			var x = context.Values.Pop();
			var target = context.Find(new Point(x, y));

			if (target != null)
			{
				context.Values.Push(target.Value);
			}
			else
			{
				context.Values.Push(' ');
			}
		}

		private static void HandlePut(ExecutionContext context)
		{
			context.EnsureStack(3);
			var y = context.Values.Pop();
			var x = context.Values.Pop();
			var value = Convert.ToChar(context.Values.Pop());

			var location = new Point(x, y);
			var target = context.Find(location);

			if (target != null)
			{
				target.Value = value;
			}
			else if (value != ' ')
			{
				context.Cells.Add(new Cell(location, value));
			}
		}

		internal override void OnHandle(ExecutionContext context)
		{
			switch (context.Current.Value)
			{
				case SpaceInstructionHandler.GetInstruction:
					SpaceInstructionHandler.HandleGet(context);
					break;
				case SpaceInstructionHandler.PutInstruction:
					SpaceInstructionHandler.HandlePut(context);
					break;
			}
		}
	}
}
