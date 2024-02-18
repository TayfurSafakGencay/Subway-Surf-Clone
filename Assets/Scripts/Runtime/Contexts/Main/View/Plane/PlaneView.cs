using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Plane
{
  public class PlaneView : EventView
  {
    [HideInInspector]
    public Rigidbody Rigidbody;

    public GameObject Propeller;

    protected override void Start()
    {
      Rigidbody = transform.GetComponent<Rigidbody>();

      Propeller.transform.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.LocalAxisAdd)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }

    protected override void OnDestroy()
    {
      transform.DOKill();
    }
  }
}