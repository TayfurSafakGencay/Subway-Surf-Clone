using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Obstacle.Obstacle
{
  public class ObstacleView : EventView
  {
    [HideInInspector]
    public int Id;

    [HideInInspector]
    public int GroundId;

    public void Init(int id)
    {
      Id = id;
    }

    public void SetGroundId(int groundId)
    {
      GroundId = groundId;
    }
  }
}