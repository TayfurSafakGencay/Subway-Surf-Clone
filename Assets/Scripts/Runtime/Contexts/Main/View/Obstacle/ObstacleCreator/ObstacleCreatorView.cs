using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.ObstacleCreator
{
  public class ObstacleCreatorView : EventView
  {
    public Transform ObstaclePool;

    [Space(15)]
    public List<ObstacleCreatorVo> Obstacles;
  }
}