using System.Reflection;

namespace IronBefunge.InstructionHandlers;

internal sealed class InstructionMapper
{
	private readonly Dictionary<char, IInstructionHandler> mappings = [];

	internal InstructionMapper()
		: base()
	{
		var baseHandlerType = typeof(InstructionHandler);

		var handlerTypes = from type in baseHandlerType.GetTypeInfo().Assembly.GetTypes()
								 where type.Namespace == this.GetType().Namespace
								 where baseHandlerType.IsAssignableFrom(type)
								 where type != baseHandlerType
								 select type;

		foreach (var handlerType in handlerTypes)
		{
			var handler = (InstructionHandler)Activator.CreateInstance(handlerType)!;

			foreach (var instruction in handler.Instructions)
			{
				this.mappings.Add(instruction, handler);
			}
		}
	}

	internal void Handle(ExecutionContext context)
	{
		if (this.mappings.TryGetValue(context.Current.Value, out var value))
		{
			value.Handle(context);
		}
		else
		{
			context.RunTrace($"Warning: Instruction \"{context.Current}\" is not understood.");
		}
	}
}