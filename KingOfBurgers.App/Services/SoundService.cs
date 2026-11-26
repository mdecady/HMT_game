using System.IO;
using System.Media;

namespace KingOfBurgers.App.Services;

public sealed class SoundService : ISoundService
{
	private readonly string _soundsDirectory;

	public bool IsMuted { get; set; }

	public SoundService(string? soundsDirectory = null)
	{
		_soundsDirectory = soundsDirectory
			?? Path.Combine(AppContext.BaseDirectory, "Assets", "Sounds");
	}

	public Task PlayAsync(SoundEffect effect, CancellationToken cancellationToken = default)
	{
		if (IsMuted || cancellationToken.IsCancellationRequested)
		{
			return Task.CompletedTask;
		}

		return Task.Run(() => PlayInternal(effect), cancellationToken);
	}

	private void PlayInternal(SoundEffect effect)
	{
		try
		{
			var wavPath = Path.Combine(_soundsDirectory, $"{effect}.wav");
			if (File.Exists(wavPath))
			{
				using var player = new SoundPlayer(wavPath);
				player.PlaySync();
				return;
			}

			PlayFallbackTone(effect);
		}
		catch
		{
			// Ошибка аудиоустройства или повреждённый файл не должны ронять игру
		}
	}

	private static void PlayFallbackTone(SoundEffect effect)
	{
		var (frequency, durationMs) = effect switch
		{
			SoundEffect.Drop => (420, 60),
			SoundEffect.Perfect => (880, 140),
			SoundEffect.Good => (660, 100),
			SoundEffect.Poor => (480, 100),
			SoundEffect.Miss => (220, 220),
			SoundEffect.InvalidAction => (180, 90),
			SoundEffect.Win => (1046, 300),
			_ => (440, 80)
		};

		Console.Beep(frequency, durationMs);
	}
}