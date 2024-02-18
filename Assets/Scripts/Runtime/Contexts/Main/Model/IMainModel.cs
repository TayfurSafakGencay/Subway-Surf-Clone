using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;
using UnityEngine;

namespace Runtime.Contexts.Main.Model
{
  public interface IMainModel
  {
    bool GameStarted { get; set; }
    
    void StartGame();
    
    bool GameEnded { get; set; }
    
    bool RecordBroken { get; set; }

    GroundVo GroundVo { get; set; }

    Dictionary<int, ObstacleVo> ObstaclePool { get; set; }
    
    List<GameObject> CoinPool { get; set; }

    Vector3 GetNewPosition();

    void SetGroundVo(GroundVo vo);

    void AddObstacleToPool(ObstacleVo obstacleVo);

    void AddObstacleListToPool(List<ObstacleVo> obstacleList);

    Dictionary<int, ObstacleVo> GetObstacleFromPool(int piece);

    void AddCoins(List<GameObject> coins);

    List<GameObject> GetCoins(int count);
    
    void SetScore(int newScore);
    
    int GetScore();

    void SetSpeed(float newSpeed);

    float GetSpeed();

    void ObstacleCollision();

    int GetHealth();
  }
}