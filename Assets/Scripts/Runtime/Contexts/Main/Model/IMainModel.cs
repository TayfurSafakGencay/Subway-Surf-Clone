using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;
using UnityEngine;

namespace Runtime.Contexts.Main.Model
{
  public interface IMainModel
  {
    GroundVo GroundVo { get; set; }

    List<ObstacleVo> ObstaclePool { get; set; }

    Vector3 GetNewPosition();

    void SetGroundVo(GroundVo vo);

    void AddObstacleToPool(ObstacleVo obstacleVo);

    List<ObstacleVo> GetObstacleFromPool(int piece);
  }
}