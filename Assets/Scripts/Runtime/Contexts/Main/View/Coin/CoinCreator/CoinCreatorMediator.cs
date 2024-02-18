using System.Collections.Generic;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Coin.CoinCreator
{
  public class CoinCreatorMediator : EventMediator
  {
    [Inject]
    public CoinCreatorView view { get; set; }
    
    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(MainEvent.CollectCoin, CollectedCoin);
      dispatcher.AddListener(MainEvent.SendCoinToPool, CollectedCoin);
      dispatcher.AddListener(MainEvent.AddCoinsToPool, CollectedCoins);
      dispatcher.AddListener(MainEvent.SetInitialCoins, InitialCoins);
    }

    private void Start()
    {
      for (int i = 0; i < mainModel.GroundVo.GroundLength * mainModel.GroundVo.GroundCount; i++)
      {
        GameObject coin = Instantiate(view.Coin, view.CoinPool);
        mainModel.CoinPool.Add(coin);
        coin.SetActive(false);
        coin.name = "Coin";
      }
    }
    
    private void InitialCoins()
    {
      for (int i = 0; i < mainModel.GroundVo.GroundCount; i++)
      {
        dispatcher.Dispatch(MainEvent.GroundInitialCoins, i);
      }
    }

    private void CollectedCoin(IEvent payload)
    {
      GameObject coin = (GameObject)payload.data;

      coin.transform.parent = view.CoinPool;
      coin.SetActive(false);
    }
    
    private void CollectedCoins(IEvent payload)
    {
      List<GameObject> coins = (List<GameObject>)payload.data;

      for (int i = 0; i < coins.Count; i++)
      {
        coins[i].transform.parent = view.CoinPool;
        coins[i].SetActive(false);
      }
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEvent.CollectCoin, CollectedCoin);
      dispatcher.RemoveListener(MainEvent.SendCoinToPool, CollectedCoin);
      dispatcher.RemoveListener(MainEvent.AddCoinsToPool, CollectedCoins);
      dispatcher.RemoveListener(MainEvent.SetInitialCoins, InitialCoins);
    }
  }
}