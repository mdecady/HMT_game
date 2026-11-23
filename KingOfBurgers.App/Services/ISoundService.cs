namespace KingOfBurgers.App.Services;

public interface ISoundService
{
	bool IsMuted { get; set; }
	Task PlayAsync(SoundEffect effect, CancellationToken cancellationToken = default);
}
