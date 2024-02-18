using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.CameraFollow;
using Runtime.Contexts.Main.View.Coin;
using Runtime.Contexts.Main.View.Coin.Coin;
using Runtime.Contexts.Main.View.Coin.CoinCreator;
using Runtime.Contexts.Main.View.GameManager;
using Runtime.Contexts.Main.View.Ground;
using Runtime.Contexts.Main.View.InfoPanel;
using Runtime.Contexts.Main.View.Obstacle.Obstacle;
using Runtime.Contexts.Main.View.Obstacle.ObstacleCreator;
using Runtime.Contexts.Main.View.Plane;
using Runtime.Contexts.Main.View.Player.Player;
using Runtime.Contexts.Main.View.Player.PlayerMovement;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.Config
{
  public class MainContext : MVCSContext
  {
    public MainContext(MonoBehaviour view) : base(view)
    {
    }

    public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();

      injectionBinder.Bind<IMainModel>().To<MainModel>().ToSingleton();

      mediationBinder.Bind<GameManagerView>().To<GameManagerMediator>();
      mediationBinder.Bind<GroundView>().To<GroundMediator>();
      mediationBinder.Bind<CameraFollowView>().To<CameraFollowMediator>();
      mediationBinder.Bind<ObstacleCreatorView>().To<ObstacleCreatorMediator>();
      mediationBinder.Bind<ObstacleView>().To<ObstacleMediator>();
      mediationBinder.Bind<CoinView>().To<CoinMediator>();
      mediationBinder.Bind<CoinCreatorView>().To<CoinCreatorMediator>();
      mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
      mediationBinder.Bind<PlayerMovementView>().To<PlayerMovementMediator>();
      mediationBinder.Bind<InfoPanelView>().To<InfoPanelMediator>();
      mediationBinder.Bind<PlaneView>().To<PlaneMediator>();
    }
  }
}