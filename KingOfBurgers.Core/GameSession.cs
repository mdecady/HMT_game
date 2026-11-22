namespace KingOfBurgers.Core;

public class GameSession
{
    private readonly GameConfig _config;
    private readonly IIngredientSource _source;
    private readonly BurgerStack _stackOne;
    private readonly BurgerStack _stackTwo;
    private Ingredient _currentIngredient;

    public GameSession(GameConfig config, IIngredientSource source)
    {
        _config = config;
        _source = source;

        var bun = new Ingredient(IngredientType.Bun, config.BunWidth, config.BunHeight);
        _stackOne = new BurgerStack(bun);
        _stackTwo = new BurgerStack(bun);
        _currentIngredient = source.Next();
    }

    public GamePhase Phase => GamePhase.Swinging;
    public PlayerSlot CurrentPlayer => PlayerSlot.One;
    public int RoundNumber => 1;
    public PlayerSlot? Winner => null;

    public Ingredient CurrentIngredient => _currentIngredient;
    public double ClawX => 0;
    public double FallY => 0;

    public int GetScore(PlayerSlot p) => 0;
    public bool HasReachedTarget(PlayerSlot p) => GetScore(p) >= _config.TargetScore;
    public BurgerStack GetStack(PlayerSlot p) => p == PlayerSlot.One ? _stackOne : _stackTwo;

    public void Tick(double dtSeconds)
    {
        // TODO: качание и падение
    }

    public bool TryDrop() => false;           // TODO
    public bool TryAdvanceTurn() => false;    // TODO

    public void Restart()
    {
        // TODO
    }

    public event Action<DropResult>? Dropped;
    public event Action<int>? RoundEnded;
    public event Action<PlayerSlot>? Won;
}