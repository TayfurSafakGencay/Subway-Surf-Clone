using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.ObstacleCreator
{
  public class ObstacleCreatorView : EventView
  {
    public List<GameObject> Obstacles;

    public Transform ObstaclePool;
  }
}