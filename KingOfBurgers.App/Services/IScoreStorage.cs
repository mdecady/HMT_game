namespace KingOfBurgers.App.Services;

public interface IScoreStorage
{
	Task<IReadOnlyList<ScoreRecord>> LoadAsync(CancellationToken cancellationToken = default);
	Task SaveRecordAsync(ScoreRecord record, CancellationToken cancellationToken = default);
}