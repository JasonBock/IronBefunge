using System.Collections.ObjectModel;

namespace IronBefunge.Core.InstructionHandlers
{
	internal abstract class InstructionHandler
		: IInstructionHandler
	{
		private ReadOnlyCollection<char> instructions;

		internal abstract ReadOnlyCollection<char> GetInstructions();

		public void Handle(ExecutionContext context)
		{
			this.InitializeInstructions();
			this.OnHandle(context);
		}

		private void InitializeInstructions()
		{
			if (this.instructions == null)
			{
				this.instructions = this.GetInstructions();
			}
		}

		internal abstract void OnHandle(ExecutionContext context);

		public ReadOnlyCollection<char> Instructions
		{
			get
			{
				this.InitializeInstructions();
				return this.instructions;
			}
		}
	}
}
