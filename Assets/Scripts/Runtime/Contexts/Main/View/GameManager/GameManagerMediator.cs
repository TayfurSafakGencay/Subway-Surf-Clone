using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.Ground;
using Runtime.Contexts.Main.View.ObstacleCreator;
using Runtime.Contexts.Main.View.PlayerMovement;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Contexts.Main.View.GameManager
{
  public class GameManagerMediator : EventMediator
  {
    [Inject]
    public GameManagerView view { get; set; }
    
    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
    }

    private void Start()
    {
      GroundVo groundVo = new()
      {
        GroundCount = view.GroundCount,
        GroundLength = view.GroundLenght,
        GroundWidth = view.GroundWidth
      };
      
      mainModel.SetGroundVo(groundVo);
      
      for (int i = 0; i < view.GroundCount; i++)
      {
        int localI = i;
        
        Addressables.InstantiateAsync(GameObjectKey.Ground, view.GroundContainers).Completed += (handle) =>
        {
          if (handle.Status == AsyncOperationStatus.Succeeded)
          {
            GameObject instantiatedObject = handle.Result;
            GroundView groundView = instantiatedObject.GetComponent<GroundView>();
            groundView.Init(groundVo, localI);

            if (localI == view.GroundCount - 1)
              dispatcher.Dispatch(MainEventKey.SetInitialObstacles);
          }
        };
      }

      Addressables.InstantiateAsync(GameObjectKey.Player).Completed += (handle) =>
      {
        if (handle.Status != AsyncOperationStatus.Succeeded) return;
        GameObject instantiatedObject = handle.Result;
        dispatcher.Dispatch(MainEventKey.PlayerCreated, instantiatedObject);
      };
    }

    public override void OnRemove()
    {
    }
  }
}