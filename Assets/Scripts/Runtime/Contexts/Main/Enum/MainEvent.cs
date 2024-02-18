namespace Runtime.Contexts.Main.Enum
{
  public enum MainEvent
  {
    PlayerCreated,
    
    GroundInitialObstacles,
    SetInitialObstacles,
    GroundInitialCoins,
    SetInitialCoins,
    
    ConflictObstacles,
    
    SetObstacleParentAsPool,
    SetObstacleListParentAsPool,
    
    CollectCoin,
    SendCoinToPool,
    AddCoinsToPool,
    
    GetDamage,
    GameEnded,
    GameStarted,
  }
}