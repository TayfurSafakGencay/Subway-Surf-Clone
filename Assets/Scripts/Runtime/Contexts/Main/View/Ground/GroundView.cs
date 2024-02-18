using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Ground
{
  public class GroundView : EventView
  {
    public Transform ObstacleContainer;

    public Transform CoinContainer;

    [HideInInspector]
    public int Id;

    public Dictionary<int, ObstacleVo> Obstacles = new();

    [HideInInspector]
    public List<GameObject> Coins;

    public void Init(GroundVo vo, int id)
    {
      transform.position = new Vector3(0, 0, id * vo.GroundLength);
      Id = id;
    }

    public void EndPointChecker()
    {
      dispatcher.Dispatch(GroundEvent.EndPointChecker);
    }
  }
}