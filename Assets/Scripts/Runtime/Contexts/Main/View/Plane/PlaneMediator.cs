using Runtime.Contexts.Main.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Plane
{
  public class PlaneMediator : EventMediator
  {
    [Inject]
    public PlaneView view { get; set; }
    
    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
    }
    
    private void FixedUpdate()
    {
      if (mainModel.GameEnded || !mainModel.GameStarted)
        return;
      
      Vector3 forwardMove = transform.forward * 20 * Time.fixedDeltaTime;
      view.Rigidbody.MovePosition(view.Rigidbody.position + forwardMove);
    }

    public override void OnRemove()
    {
    }
  }
}