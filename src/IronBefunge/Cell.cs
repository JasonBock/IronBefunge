namespace IronBefunge;

/// <summary>
/// Defines the X and Y positions of a value in FungeSpace.
/// </summary>
public record Cell(Point Location, char Value);