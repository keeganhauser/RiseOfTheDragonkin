public enum SceneName
{
    StartingTown01,
    ForestArea01,
    CombatScene,
    Dungeon01
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum CharacterAction
{
    Idle,
    Move,
    Attack
}

public enum GameState
{
    MainMenu,
    Overworld,
    Cave,
    Combat
}

public enum BattleState
{
    NotStarted,
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public enum CombatAction
{
    Attack,
    Defend,
    Item,
    Escape
}