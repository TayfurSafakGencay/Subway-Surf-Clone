using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.CameraFollow
{
  public class CameraFollowView : EventView
  {
    public Transform Player;

    public Vector3 OffSet;

    [HideInInspector]
    public bool startFollow;
  }
}