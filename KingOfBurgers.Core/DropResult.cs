namespace KingOfBurgers.Core;

public record DropResult(PlayerSlot Player, DropOutcome Outcome, double Offset, int ScoreDelta, PlacedLayer? Placed);