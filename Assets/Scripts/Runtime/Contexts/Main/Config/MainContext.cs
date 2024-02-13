using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.CameraFollow;
using Runtime.Contexts.Main.View.GameManager;
using Runtime.Contexts.Main.View.Ground;
using Runtime.Contexts.Main.View.ObstacleCreator;
using Runtime.Contexts.Main.View.PlayerMovement;
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
      mediationBinder.Bind<PlayerMovementView>().To<PlayerMovementMediator>();
      mediationBinder.Bind<CameraFollowView>().To<CameraFollowMediator>();
      mediationBinder.Bind<ObstacleCreatorView>().To<ObstacleCreatorMediator>();
    }
  }
}