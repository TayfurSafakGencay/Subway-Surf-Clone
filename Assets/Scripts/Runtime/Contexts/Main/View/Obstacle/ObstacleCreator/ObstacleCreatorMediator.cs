using System.Collections.Generic;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.Obstacle;
using Runtime.Contexts.Main.Vo;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.ObstacleCreator
{
  public class ObstacleCreatorMediator : EventMediator
  {
    [Inject]
    public ObstacleCreatorView view { get; set; }

    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(MainEvent.SetInitialObstacles, SetObstacles);
      dispatcher.AddListener(MainEvent.SetObstacleParentAsPool, SetObstacleParentAsPool);
      dispatcher.AddListener(MainEvent.SetObstacleListParentAsPool, SetObstacleListParentAsPool);
    }

    private void SetObstacles()
    {
      int id = 0;
      for (int i = 0; i < view.Obstacles.Count; i++)
      {
        ObstacleKey obstacleKey = (ObstacleKey)i;
      
        for (int j = 0; j < view.Obstacles[i].piece; j++)
        {
          GameObject instantiatedObstacle = Instantiate(view.Obstacles[i].ObstacleObject, view.ObstaclePool);
          instantiatedObstacle.GetComponent<ObstacleView>().Init(id);
      
          ObstacleVo vo = new()
          {
            Key = obstacleKey,
            Object = instantiatedObstacle,
            Id = id
          };
      
          mainModel.AddObstacleToPool(vo);
          
          id += 1;
        }
      }
      
      InitialObstacles();
    }

    private void SetObstacleParentAsPool(IEvent payload)
    {
      ObstacleVo vo = (ObstacleVo)payload.data;
      
      vo.Object.transform.parent = view.ObstaclePool;
      vo.Object.SetActive(false);
    }

    private void SetObstacleListParentAsPool(IEvent payload)
    {
      List<ObstacleVo> list = (List<ObstacleVo>)payload.data;

      for (int i = 0; i < list.Count; i++)
      {
        list[i].Object.transform.parent = view.ObstaclePool;
        list[i].Object.SetActive(false);
      }
    }

    private void InitialObstacles()
    {
      for (int i = 0; i < mainModel.GroundVo.GroundCount; i++)
      {
        if (i == 0)
          continue;

        dispatcher.Dispatch(MainEvent.GroundInitialObstacles, i);
      }
    }
    
    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEvent.SetInitialObstacles, SetObstacles);
      dispatcher.RemoveListener(MainEvent.SetObstacleParentAsPool, SetObstacleParentAsPool);
      dispatcher.RemoveListener(MainEvent.SetObstacleListParentAsPool, SetObstacleListParentAsPool);
    }
  }
}