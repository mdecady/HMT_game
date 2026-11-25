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
		if (!File.Exists(_filePath))
		{
			return [];
		}

		await using var stream = File.OpenRead(_filePath);
		var data = await JsonSerializer.DeserializeAsync<List<ScoreRecord>>(
				stream,
				SerializerOptions,
				cancellationToken)
			.ConfigureAwait(false);

		return data ?? [];
	}

	public async Task SaveRecordAsync(ScoreRecord record, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(record);

		var records = (await LoadAsync(cancellationToken).ConfigureAwait(false)).ToList();
		records.Add(record);

		var directory = Path.GetDirectoryName(_filePath);
		if (!string.IsNullOrEmpty(directory))
		{
			Directory.CreateDirectory(directory);
		}

		await using var stream = File.Create(_filePath);
		await JsonSerializer.SerializeAsync(stream, records, SerializerOptions, cancellationToken)
			.ConfigureAwait(false);
	}
}