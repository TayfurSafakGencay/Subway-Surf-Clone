using System.Collections.Generic;
using System.Linq;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Vo;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Runtime.Contexts.Main.Model
{
  public class MainModel : IMainModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }
    
    public int Score { get; set; }
    
    public float Speed { get; set; }
    
    public GroundVo GroundVo { get; set; }
    
    public Dictionary<int, ObstacleVo> ObstaclePool { get; set; }

    public List<GameObject> CoinPool { get; set; }
    
    public int Health  { get; set; }
    
    public bool GameEnded { get; set; }
    
    public bool RecordBroken { get; set; }
    
    public bool GameStarted { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      ObstaclePool = new Dictionary<int, ObstacleVo>();
      CoinPool = new List<GameObject>();

      Health = 3;
      GameEnded = false;
      RecordBroken = false;
      GameStarted = false;
    }

    public void StartGame()
    {
      GameStarted = true;
      
      dispatcher.Dispatch(MainEvent.GameStarted);
    }

    public Vector3 GetNewPosition()
    {
      float ZPosition = GroundVo.GroundCount * GroundVo.GroundLength;
      
      return new Vector3(0, 0, ZPosition);
    }

    public void SetGroundVo(GroundVo vo)
    {
      GroundVo = vo;
    }

    public void AddObstacleToPool(ObstacleVo obstacleVo)
    {
      obstacleVo.Object.SetActive(false);

      ObstaclePool.Add(obstacleVo.Id, obstacleVo);
      
      dispatcher.Dispatch(MainEvent.SetObstacleParentAsPool, obstacleVo);
    }

    public void AddObstacleListToPool(List<ObstacleVo> obstacleList)
    {
      for (int i = 0; i < obstacleList.Count; i++)
        ObstaclePool.Add(obstacleList.ElementAt(i).Id, obstacleList.ElementAt(i));
      
      dispatcher.Dispatch(MainEvent.SetObstacleListParentAsPool, obstacleList);
    }


    public Dictionary<int, ObstacleVo> GetObstacleFromPool(int piece)
    {
      Dictionary<int, ObstacleVo> vo = new();
      for (int i = 0; i < piece; i++)
      {
        if (ObstaclePool.Count == 0)
          break;
        
        int random = Random.Range(0, ObstaclePool.Count);
        ObstaclePool.ElementAt(random).Value.Object.SetActive(true);
        vo.Add(ObstaclePool.ElementAt(random).Key, ObstaclePool.ElementAt(random).Value);
        ObstaclePool.Remove(ObstaclePool.ElementAt(random).Key);
      }

      return vo;
    }

    public void AddCoins(List<GameObject> coins)
    {
      for (int i = 0; i < coins.Count; i++)
      {
        CoinPool.Add(coins[i]);
      }
      
      dispatcher.Dispatch(MainEvent.AddCoinsToPool, coins);
    }

    public List<GameObject> GetCoins(int count)
    {
      List<GameObject> coins = new();
      for (int i = 0; i < count; i++)
      {
        if (CoinPool.Count == 0)
          break;
        
        coins.Add(CoinPool[0]);
        CoinPool.RemoveAt(0);
      }

      return coins;
    }

    public void ObstacleCollision()
    {
      Health -= 1;
      
      dispatcher.Dispatch(MainEvent.GetDamage);

      if (Health != 0) return;
      
      GameEnded = true;
      dispatcher.Dispatch(MainEvent.GameEnded);
    }

    public int GetHealth()
    {
      return Health;
    }

    public void SetScore(int newScore)
    {
      Score = newScore;

      if (Score <= PlayerPrefs.GetInt("Highest Score")) return;
      PlayerPrefs.SetInt("Highest Score", Score);
      RecordBroken = true;
    }

    public int GetScore()
    {
      return Score;
    }
    
    public void SetSpeed(float newSpeed)
    {
      Speed = newSpeed;
    }

    public float GetSpeed()
    {
      return Speed;
    }
  }
}