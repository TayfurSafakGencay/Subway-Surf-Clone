using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.Ground;
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
        GroundCount = 2,
        GroundLength = 200,
        GroundWidth = 9
      };
      
      mainModel.SetGroundVo(groundVo);
      
      for (int i = 0; i < 4; i++)
      {
        int localI = i;
        
        Addressables.InstantiateAsync(GameObjectKey.Ground, view.GroundContainers).Completed += (handle) =>
        {
          if (handle.Status == AsyncOperationStatus.Succeeded)
          {
            GameObject instantiatedObject = handle.Result;
            GroundView groundView = instantiatedObject.GetComponent<GroundView>();
            groundView.Init(groundVo, localI);

            if (localI != 4 - 1) return;
            dispatcher.Dispatch(MainEvent.SetInitialObstacles);
            dispatcher.Dispatch(MainEvent.SetInitialCoins);
          }
        };
      }

      Addressables.InstantiateAsync(GameObjectKey.Player, transform.parent).Completed += (handle) =>
      {
        if (handle.Status != AsyncOperationStatus.Succeeded) return;
        GameObject instantiatedObject = handle.Result;
        instantiatedObject.transform.position = new Vector3(instantiatedObject.transform.position.x,
          0.5f, instantiatedObject.transform.position.z);
        dispatcher.Dispatch(MainEvent.PlayerCreated, instantiatedObject);
      };
    }

    public override void OnRemove()
    {
    }
  }
}