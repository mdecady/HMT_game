using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KingOfBurgers.App.Services;

public sealed class JsonScoreStorage : IScoreStorage
{
	private static readonly JsonSerializerOptions SerializerOptions = new()
	{
		WriteIndented = true,
		Converters = { new JsonStringEnumConverter() }
	};

	private readonly string _filePath;
	private readonly SemaphoreSlim _gate = new(1, 1);

	public JsonScoreStorage(string? filePath = null)
	{
		if (string.IsNullOrWhiteSpace(filePath))
		{
			var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			_filePath = Path.Combine(appData, "KingOfBurgers", "scores.json");
		}
		else
		{
			_filePath = filePath;
		}
	}

	public async Task<IReadOnlyList<ScoreRecord>> LoadAsync(CancellationToken cancellationToken = default)
	{
		await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			return await LoadUnlockedAsync(cancellationToken).ConfigureAwait(false);
		}
		finally
		{
			_gate.Release();
		}
	}

	public async Task SaveRecordAsync(ScoreRecord record, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(record);

		await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			var records = await LoadUnlockedAsync(cancellationToken).ConfigureAwait(false);
			records.Add(record);

			var directory = Path.GetDirectoryName(_filePath);
			if (!string.IsNullOrEmpty(directory))
			{
				Directory.CreateDirectory(directory);
			}

			await using var stream = new FileStream(
				_filePath,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None,
				bufferSize: 4096,
				useAsync: true);

			await JsonSerializer.SerializeAsync(stream, records, SerializerOptions, cancellationToken)
				.ConfigureAwait(false);
		}
		finally
		{
			_gate.Release();
		}
	}

	private async Task<List<ScoreRecord>> LoadUnlockedAsync(CancellationToken cancellationToken)
	{
		if (!File.Exists(_filePath))
		{
			return [];
		}

		try
		{
			await using var stream = new FileStream(
				_filePath,
				FileMode.Open,
				FileAccess.Read,
				FileShare.Read,
				bufferSize: 4096,
				useAsync: true);

			var data = await JsonSerializer.DeserializeAsync<List<ScoreRecord>>(
					stream,
					SerializerOptions,
					cancellationToken)
				.ConfigureAwait(false);

			return data ?? [];
		}
		catch (JsonException)
		{
			return []; // если чёт не так (поврежден файл), то пустой список
		}
	}
}