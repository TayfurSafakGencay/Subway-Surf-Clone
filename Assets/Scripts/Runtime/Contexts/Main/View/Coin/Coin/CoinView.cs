using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Coin.Coin
{
  public class CoinView : EventView
  {
    public GameObject CoinSound;
    private void OnEnable()
    {
      transform.DORotate(new Vector3(0f, 360f, 0f), 2.5f, RotateMode.LocalAxisAdd)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }
  }
}