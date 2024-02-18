using Runtime.Contexts.Main.Enum;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.CameraFollow
{
  public class CameraFollowMediator : EventMediator
  {
    [Inject]
    public CameraFollowView view { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(MainEvent.PlayerCreated, OnPlayerCreated);
    }
    
    private void Update()
    {
      if (!view.startFollow)
        return;
      
      Vector3 targetPosition = view.Player.position + view.OffSet;
      // targetPosition.x = 0;
      transform.position = targetPosition;
    }

    private void OnPlayerCreated(IEvent payload)
    {
      GameObject player = (GameObject)payload.data;

      view.Player = player.transform;
      view.OffSet = transform.position - view.Player.position;
      view.startFollow = true;
    }
    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEvent.PlayerCreated, OnPlayerCreated);
    }
  }
}