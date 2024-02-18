using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Contexts.Main.View.Player.PlayerMovement
{
  public class PlayerMovementView : EventView
  {
    [HideInInspector]
    public Side Side = Side.Mid;

    [HideInInspector]
    public float NewZPosition;

    [HideInInspector]
    public bool SwipeLeft;

    [HideInInspector]
    public bool SwipeRight;
    
    [HideInInspector]
    public bool SwipeUp;
    
    [HideInInspector]
    public bool SwipeDown;

    [HideInInspector]
    public float ZValue;

    [HideInInspector]
    public CharacterController CharacterController;

    [HideInInspector]
    public float x;

    public float HorizontalSpeed;
    
    public float Speed = 7f;

    public float JumpPower = 7f;

    public bool InJump;

    public bool InSlide;

    [HideInInspector]
    public float y;

    [HideInInspector]
    public Animator Animator;

    [HideInInspector]
    public float ColliderHeight;

    [HideInInspector]
    public float ColliderCenterY;
    
    internal float SlideCounter;

    protected override void Start()
    {
      base.Start();

      ZValue = 3;
      CharacterController = GetComponent<CharacterController>();
      Animator = GetComponent<Animator>();

      ColliderHeight = CharacterController.height;
      ColliderCenterY = CharacterController.center.y;
    }
  }
}