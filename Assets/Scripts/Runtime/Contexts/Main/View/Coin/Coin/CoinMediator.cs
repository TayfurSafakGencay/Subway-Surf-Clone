using Runtime.Contexts.Main.Enum;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Coin.Coin
{
  public class CoinMediator : EventMediator
  {
    [Inject]
    public CoinView view { get; set; }

    public override void OnRegister()
    {
    }

    private void Start()
    {
      if (!(transform.position.z < 20)) return;
      dispatcher.Dispatch(MainEvent.SendCoinToPool, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(TagKey.Player))
      {
        GameObject instantiate = Instantiate(view.CoinSound);
        Destroy(instantiate, 0.5f);

        dispatcher.Dispatch(MainEvent.CollectCoin, gameObject);
      }
      else if (other.CompareTag(TagKey.ObstacleSelector) || other.CompareTag(TagKey.Coin))
      {
        dispatcher.Dispatch(MainEvent.SendCoinToPool, gameObject);
      }
    }

    public override void OnRemove()
    {
    }
  }
}