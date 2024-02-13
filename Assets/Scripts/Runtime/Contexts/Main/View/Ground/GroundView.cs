using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Ground
{
  public class GroundView : EventView
  {
    public GameObject GroundObject;

    public Transform ObstacleContainer;

    [HideInInspector]
    public int Id;

    public List<ObstacleVo> Obstacles;

    public void Init(GroundVo vo, int id)
    {
      GroundObject.transform.localScale = new Vector3(vo.GroundLength, 1, vo.GroundWidth);
      transform.position = new Vector3(id * vo.GroundLength, 0, 0);
      Id = id;
    }

    public void EndPointChecker()
    {
      dispatcher.Dispatch(GroundEvent.EndPointChecker);
    }
  }
}