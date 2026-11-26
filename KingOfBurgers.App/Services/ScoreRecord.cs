using KingOfBurgers.Core;

namespace KingOfBurgers.App.Services;

public sealed record ScoreRecord(
	DateTime PlayedAtUtc,
	PlayerSlot Winner,
	string WinnerName,
	int PlayerOneScore,
	int PlayerTwoScore,
	int RoundsPlayed);