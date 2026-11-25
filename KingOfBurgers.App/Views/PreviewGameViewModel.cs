using System.Collections.ObjectModel;
using System.Windows.Input;
using KingOfBurgers.Core;

namespace KingOfBurgers.App.Views;

/// Временная заглушка
/// Будет заменена на настоящий ?файл?
public class PreviewGameViewModel
{
    public string StatusText { get; set; } = "Ход: Игрок 1";
    public int RoundNumber { get; set; } = 3;
    public PlayerSlot CurrentPlayer { get; set; } = PlayerSlot.One;
    public GamePhase Phase { get; set; } = GamePhase.Swinging;

    public int PlayerOneScore { get; set; } = 350;
    public int PlayerTwoScore { get; set; } = 200;

    public bool PlayerOneReachedTarget { get; set; } = false;
    public bool PlayerTwoReachedTarget { get; set; } = false;

    public bool IsMenuVisible { get; set; } = true; 
    public bool IsGameVisible { get; set; } = false;
    public bool IsVictoryVisible { get; set; } = false;
    public string WinnerText { get; set; } = "Победил Игрок 1!";

    public double ClawX { get; set; } = 35;
    public double FallY { get; set; } = 260;
    public Ingredient CurrentIngredient { get; set; } = new(IngredientType.Cheese, 110, 22);

    public ObservableCollection<PlacedLayer> PlayerOneLayers { get; } = new()
    {
        new PlacedLayer(new Ingredient(IngredientType.Bun, 140, 28), 0, 0),
        new PlacedLayer(new Ingredient(IngredientType.Patty, 120, 26), -8, 28),
        new PlacedLayer(new Ingredient(IngredientType.Tomato, 105, 22), 5, 54),
    };

    public ObservableCollection<PlacedLayer> PlayerTwoLayers { get; } = new()
    {
        new PlacedLayer(new Ingredient(IngredientType.Bun, 140, 28), 0, 0),
        new PlacedLayer(new Ingredient(IngredientType.Letuce, 125, 24), 10, 28),
    };

    public ICommand? DropCommand { get; set; }
    public ICommand? StartGameCommand { get; set; }
    public ICommand? RestartCommand { get; set; }
    public ICommand? BackToMenuCommand { get; set; }
}