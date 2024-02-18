using System;
using System.Collections.Generic;
using Runtime.Contexts.Main.Enum;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Obstacle.Obstacle
{
  public class ObstacleMediator : EventMediator
  {
    [Inject]
    public ObstacleView view { get; set; }

    public override void OnRegister()
    {
    }

    private void Start()
    {
      if (!(transform.position.z < 20)) return;
      KeyValuePair<int, int> keyValuePair = new(view.GroundId, view.Id);
      dispatcher.Dispatch(MainEvent.ConflictObstacles, keyValuePair);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!other.CompareTag(TagKey.ObstacleSelector)) return;
      ObstacleView obstacleView = other.gameObject.GetComponent<ObstacleView>();

      if (obstacleView.Id >= view.Id) return;

      KeyValuePair<int, int> keyValuePair = new(view.GroundId, view.Id);
      dispatcher.Dispatch(MainEvent.ConflictObstacles, keyValuePair);
    }

    public override void OnRemove()
    {
    }
  }
}