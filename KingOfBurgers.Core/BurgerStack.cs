namespace KingOfBurgers.Core;

public class BurgerStack
{
    private readonly List<PlacedLayer> _layers = new();

    public BurgerStack(Ingredient bun)
    {
        _layers.Add(new PlacedLayer(bun, 0, 0));
    }

    public IReadOnlyList<PlacedLayer> Layers => _layers;

    public PlacedLayer Top => _layers[^1];

    // Число слоёв без нижней булочки
    public int Height => _layers.Count - 1;

    public double TotalHeightPx => _layers.Sum(l => l.Ingredient.Height);

    internal void Add(PlacedLayer layer)
    {
        _layers.Add(layer);
    }
}