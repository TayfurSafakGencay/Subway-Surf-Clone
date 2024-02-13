using System.Collections.Generic;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
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
      
      dispatcher.AddListener(MainEventKey.GroundInitialObstacles, SetObstacles);
    }

    public void EndPointChecker()
    {
      Vector3 newPosition = mainModel.GetNewPosition();

      transform.position += newPosition;
    }

    public void SetObstacles(IEvent payload)
    {
      KeyValuePair<int, List<ObstacleVo>> obstacleList = (KeyValuePair<int, List<ObstacleVo>>)payload.data;

      if (view.Id != obstacleList.Key)
        return;

      view.Obstacles = obstacleList.Value;

      for (int i = 0; i < view.Obstacles.Count; i++)
      {
        view.Obstacles[i].Object.transform.parent = view.ObstacleContainer;
        
        float[] zAxisChoices = {-2.5f, 0, 2.5f};
        int randomIndex = Random.Range(0, zAxisChoices.Length);
        float zAxis = zAxisChoices[randomIndex];
        
        int xAxis = Random.Range((int)(-mainModel.GroundVo.GroundLength / 4 + 1), (int)(mainModel.GroundVo.GroundLength / 4 - 1));
        xAxis *= 2;
        float yAxis = view.Obstacles[i].Object.transform.localPosition.y;
        view.Obstacles[i].Object.transform.localPosition = new Vector3(xAxis, yAxis, zAxis);
      }
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(GroundEvent.EndPointChecker, EndPointChecker);
      
      dispatcher.RemoveListener(MainEventKey.GroundInitialObstacles, SetObstacles);
    }
  }
}