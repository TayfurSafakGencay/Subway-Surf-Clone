using System.Collections;
using System.Collections.Generic;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.Vo;
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
      dispatcher.AddListener(MainEventKey.SetInitialObstacles, SetObstacles);
    }

    private void SetObstacles()
    {
      for (int i = 0; i < view.Obstacles.Count; i++)
      {
        ObstacleKey obstacleKey = (ObstacleKey)i;

        for (int j = 0; j < 10; j++)
        {
          GameObject instantiatedObstacle = Instantiate(view.Obstacles[i], view.ObstaclePool);

          ObstacleVo vo = new()
          {
            Key = obstacleKey,
            Object = instantiatedObstacle
          };

          mainModel.AddObstacleToPool(vo);
        }
      }

      InitialObstacles();
    }

    private void InitialObstacles()
    {
      for (int i = 0; i < mainModel.GroundVo.GroundCount; i++)
      {
        if (i == 0)
          continue;

        int obstacleCount = Random.Range(5, 10);
        List<ObstacleVo> list = mainModel.GetObstacleFromPool(obstacleCount);
        KeyValuePair<int, List<ObstacleVo>> groundObstacles = new(i, list);
        
        dispatcher.Dispatch(MainEventKey.GroundInitialObstacles, groundObstacles);
      }
    }
    
    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEventKey.SetInitialObstacles, SetObstacles);
    }
  }
}