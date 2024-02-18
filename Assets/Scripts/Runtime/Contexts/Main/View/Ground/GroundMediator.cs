using System.Collections.Generic;
using System.Linq;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.Obstacle;
using Runtime.Contexts.Main.View.Obstacle.Obstacle;
using Runtime.Contexts.Main.Vo;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Ground
{
  public enum GroundEvent
  {
    EndPointChecker,
  }
  public class GroundMediator : EventMediator
  {
    [Inject]
    public GroundView view { get; set; }
    
    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(GroundEvent.EndPointChecker, EndPointChecker);
      
      dispatcher.AddListener(MainEvent.GroundInitialObstacles, OnSetObstacles);
      dispatcher.AddListener(MainEvent.GroundInitialCoins, OnSetCoins);
      dispatcher.AddListener(MainEvent.ConflictObstacles, OnConflictObstacles);
    }

    public void EndPointChecker()
    {
      Vector3 newPosition = mainModel.GetNewPosition();

      transform.position += newPosition;

      mainModel.AddObstacleListToPool(view.Obstacles.Values.ToList());
      view.Obstacles.Clear();
      SetObstacles();
      SetCoins();
    }

    public void OnSetObstacles(IEvent payload)
    {
      int groundId = (int)payload.data;
      if (view.Id != groundId)
        return;
      
      SetObstacles();
    }
    
    public void OnSetCoins(IEvent payload)
    {
      int groundId = (int)payload.data;
      if (view.Id != groundId)
        return;
      
      SetCoins();
    }

    private void SetObstacles()
    {
      int obstacleCount = Random.Range(25, 50);
      Dictionary<int, ObstacleVo> list = mainModel.GetObstacleFromPool(obstacleCount);

      view.Obstacles = list;

      for (int i = 0; i < view.Obstacles.Count; i++)
      {
        GameObject obstacle = view.Obstacles.ElementAt(i).Value.Object;
        ObstacleView obstacleView = obstacle.GetComponent<ObstacleView>();
        obstacleView.SetGroundId(view.Id);
        
        obstacle.transform.parent = view.ObstacleContainer;
        
        float[] xAxisChoices = {-3f, 0, 3f};
        int randomIndex = Random.Range(0, xAxisChoices.Length);
        float xAxis = xAxisChoices[randomIndex];
        
        int zAxis = Random.Range((int)(-mainModel.GroundVo.GroundLength / 30), (int)(mainModel.GroundVo.GroundLength / 30));
        zAxis *= 15;
        float yAxis = obstacle.transform.localPosition.y;
        obstacle.transform.localPosition = new Vector3(xAxis, yAxis, zAxis);
      }
    }

    private void SetCoins()
    {
      mainModel.AddCoins(view.Coins);
      view.Coins.Clear();
      
      int coinGroupCount = Random.Range(6, 8);

      for (int i = 0; i < coinGroupCount; i++)
      {
        int coinCountOfGroup = Random.Range(1, 9);
        
        float[] xAxisChoices = {-3f, 0, 3f};
        int randomIndex = Random.Range(0, xAxisChoices.Length);
        float xAxis = xAxisChoices[randomIndex];
        float yAxis = 1.5f;

        float firstHalf = -mainModel.GroundVo.GroundLength / 2 + 1 + coinCountOfGroup;
        float secondHalf = mainModel.GroundVo.GroundLength / 2 - 1 - coinCountOfGroup;
        float zAxis = Random.Range(firstHalf, secondHalf);

        List<GameObject> coins = mainModel.GetCoins(coinCountOfGroup);

        for (int j = 0; j < coins.Count; j++)
        {
          coins[j].SetActive(true);
          coins[j].transform.parent = view.CoinContainer;
          coins[j].transform.localPosition = new Vector3(xAxis, yAxis, zAxis);
          view.Coins.Add(coins[j]);
          zAxis += 1;
        }
      }
    }

    private void OnConflictObstacles(IEvent payload)
    {
      KeyValuePair<int, int> data = (KeyValuePair<int, int>)payload.data;
      
      if(data.Key != view.Id) return;
      if (!view.Obstacles.ContainsKey(data.Value)) return;

      ObstacleVo obstacleVo = view.Obstacles[data.Value];
      mainModel.AddObstacleToPool(obstacleVo);
      view.Obstacles.Remove(data.Value);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(GroundEvent.EndPointChecker, EndPointChecker);
      
      dispatcher.RemoveListener(MainEvent.GroundInitialObstacles, OnSetObstacles);
      dispatcher.RemoveListener(MainEvent.GroundInitialCoins, OnSetCoins);
      dispatcher.RemoveListener(MainEvent.ConflictObstacles, OnConflictObstacles);
    }
  }
}