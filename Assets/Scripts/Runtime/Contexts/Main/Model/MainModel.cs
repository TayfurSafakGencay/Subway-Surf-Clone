using System.Collections.Generic;
using System.Linq;
using Runtime.Contexts.Main.Vo;
using UnityEngine;

namespace Runtime.Contexts.Main.Model
{
  public class MainModel : IMainModel
  {
    public GroundVo GroundVo { get; set; }
    
    public List<ObstacleVo> ObstaclePool { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      ObstaclePool = new List<ObstacleVo>();
    }

    public Vector3 GetNewPosition()
    {
      float XPosition = GroundVo.GroundCount * GroundVo.GroundLength;
      
      return new Vector3(XPosition, 0, 0);
    }

    public void SetGroundVo(GroundVo vo)
    {
      GroundVo = vo;
    }

    public void AddObstacleToPool(ObstacleVo obstacleVo)
    {
      obstacleVo.Object.SetActive(false);

      ObstacleVo vo = new()
      {
        Key = obstacleVo.Key,
        Object = obstacleVo.Object
      };
      
      ObstaclePool.Add(vo);
    }

    public List<ObstacleVo> GetObstacleFromPool(int piece)
    {
      List<ObstacleVo> vo = new();
      for (int i = 0; i < piece; i++)
      {
        if (ObstaclePool.Count == 0)
          break;
        
        int random = Random.Range(0, ObstaclePool.Count);
        ObstaclePool.ElementAt(random).Object.SetActive(true);
        vo.Add(ObstaclePool.ElementAt(random));
        ObstaclePool.RemoveAt(random);
      }

      return vo;
    }
  }
}