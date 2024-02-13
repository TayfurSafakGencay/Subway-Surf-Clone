using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.PlayerMovement
{
  public class PlayerMovementView : EventView
  {
    public float Speed = 5f;

    public float HorizontalSpeed = 5f;
    
    public Rigidbody Rigidbody;

    public float Jump = 400f;

    public LayerMask GroundMask;

    [HideInInspector]
    public float HorizontalInput;

    [HideInInspector]
    public  string Horizontal = "Horizontal";

    [HideInInspector]
    public bool MoveLeft = true;

    [HideInInspector]
    public bool MoveRight = true;
  }
}