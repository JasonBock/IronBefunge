using System.Collections.Immutable;
using System.Globalization;

namespace IronBefunge.InstructionHandlers;

internal sealed class NumberInstructionHandler
	: InstructionHandler
{
	internal const char ZeroInstruction = '0';
	internal const char OneInstruction = '1';
	internal const char TwoInstruction = '2';
	internal const char ThreeInstruction = '3';
	internal const char FourInstruction = '4';
	internal const char FiveInstruction = '5';
	internal const char SixInstruction = '6';
	internal const char SevenInstruction = '7';
	internal const char EightInstruction = '8';
	internal const char NineInstruction = '9';
	internal const char TenInstruction = 'a';
	internal const char ElevenInstruction = 'b';
	internal const char TwelveInstruction = 'c';
	internal const char ThirteenInstruction = 'd';
	internal const char FourteenInstruction = 'e';
	internal const char FifteenInstruction = 'f';

	internal override ImmutableArray<char> GetInstructions() =>
		ImmutableArray.Create(NumberInstructionHandler.ZeroInstruction,
			NumberInstructionHandler.OneInstruction, NumberInstructionHandler.TwoInstruction,
			NumberInstructionHandler.ThreeInstruction, NumberInstructionHandler.FourInstruction,
			NumberInstructionHandler.FiveInstruction, NumberInstructionHandler.SixInstruction,
			NumberInstructionHandler.SevenInstruction, NumberInstructionHandler.EightInstruction,
			NumberInstructionHandler.NineInstruction, NumberInstructionHandler.TenInstruction,
			NumberInstructionHandler.ElevenInstruction, NumberInstructionHandler.TwelveInstruction,
			NumberInstructionHandler.ThirteenInstruction, NumberInstructionHandler.FourteenInstruction,
			NumberInstructionHandler.FifteenInstruction);

	internal override void OnHandle(ExecutionContext context) =>
		context.Values.Push(int.Parse(
			new string(context.Current.Value, 1), NumberStyles.HexNumber, CultureInfo.CurrentCulture));
}