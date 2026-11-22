namespace KingOfBurgers.Core;

public class GameConfig
{
    // Победа и очки
    public int TargetScore { get; init; } = 1000;
    public int MissPenalty { get; init; } = 50;
    public int PerfectScore { get; init; } = 100;
    public int MinHitScore { get; init; } = 10;

    // Пороги попадания: n = смещение / (ширина слоя / 2)
    public double PerfectThreshold { get; init; } = 0.1;
    public double GoodThreshold { get; init; } = 0.5;

    // Амплитуда качания клешни (px)
    public double AmplitudeBase { get; init; } = 60;
    public double AmplitudeGrowthPerLayer { get; init; } = 6;
    public double AmplitudeMax { get; init; } = 220;

    // Скорость качания (рад/с)
    public double AngularSpeedBase { get; init; } = 2.0;
    public double AngularSpeedGrowthPerLayer { get; init; } = 0.05;
    public double AngularSpeedMax { get; init; } = 4.5;

    // Размеры (px)
    public double BunWidth { get; init; } = 140;
    public double BunHeight { get; init; } = 30;
    public double IngredientMinWidth { get; init; } = 90;
    public double IngredientMaxWidth { get; init; } = 130;
    public double IngredientMinHeight { get; init; } = 20;
    public double IngredientMaxHeight { get; init; } = 30;

    // Падение (px/с²)
    public double Gravity { get; init; } = 900;
}