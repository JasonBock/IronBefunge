using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IronBefunge.InstructionHandlers
{
	internal sealed class InstructionMapper
	{
		internal InstructionMapper()
			: base() =>
			this.InitializeMappings();

		internal void Handle(ExecutionContext context) =>
			this.Mappings[context.Current.Value].Handle(context);

		private void InitializeMappings()
		{
			this.Mappings = new Dictionary<char, IInstructionHandler>();

			var baseHandlerType = typeof(InstructionHandler);

			var handlerTypes = from type in baseHandlerType.GetTypeInfo().Assembly.GetTypes()
									 where type.Namespace == this.GetType().Namespace
									 where baseHandlerType.IsAssignableFrom(type)
									 where type != baseHandlerType
									 select type;

			foreach (var handlerType in handlerTypes)
			{
				var handler = Activator.CreateInstance(handlerType) as InstructionHandler;

				foreach (var instruction in handler.Instructions)
				{
					this.Mappings.Add(instruction, handler);
				}
			}
		}

		private Dictionary<char, IInstructionHandler> Mappings { get; set; }
	}
}