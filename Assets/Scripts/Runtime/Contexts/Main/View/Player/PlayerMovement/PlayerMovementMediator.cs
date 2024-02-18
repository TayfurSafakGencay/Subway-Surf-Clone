using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Player.PlayerMovement
{
  public enum Side
  {
    Left,
    Mid,
    Right
  }
  public class PlayerMovementMediator : EventMediator
  {
    [Inject]
    public PlayerMovementView view { get; set; }

    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(MainEvent.GameStarted, OnGameStarted);
      dispatcher.AddListener(MainEvent.GameEnded, OnGameEnded);
    }

    private void OnGameStarted()
    {
      view.Animator.Play(AnimationKey.Run);
    }

    private void Update()
    {
      if (mainModel.GameEnded || !mainModel.GameStarted)
        return;

      view.Speed += Time.deltaTime / 5;
      mainModel.SetSpeed(view.Speed);
      
      view.SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
      view.SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
      view.SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
      view.SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

      if (view.SwipeLeft)
      {
        if (view.Side == Side.Mid)
        {
          view.NewZPosition = -view.ZValue;
          view.Side = Side.Left;
        }
        else if (view.Side == Side.Right)
        {
          view.NewZPosition = 0;
          view.Side = Side.Mid;
        }
      }
      else if (view.SwipeRight)
      {
        if (view.Side == Side.Mid)
        {
          view.NewZPosition = view.ZValue;
          view.Side = Side.Right;
        }
        else if (view.Side == Side.Left)
        {
          view.NewZPosition = 0;
          view.Side = Side.Mid;
        }
      }

      Vector3 moveVector = new(view.x - transform.position.x, view.y * Time.deltaTime, view.Speed * Time.deltaTime);
      view.x = Mathf.Lerp(view.x, view.NewZPosition, Time.deltaTime * view.HorizontalSpeed);
      view.CharacterController.Move(moveVector);
      Jump();
      Slide();
    }

    public void Jump()
    {
      if (view.CharacterController.isGrounded)
      {
        if (view.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationKey.Falling))
        {
          view.Animator.Play(AnimationKey.Landing);
          view.InJump = false;
        }
        
        if (view.SwipeUp)
        {
          view.y = view.JumpPower;
          view.Animator.CrossFadeInFixedTime(AnimationKey.Jump, 0.1f);
          view.InJump = true;
        }
      }
      else
      {
        view.y -= view.JumpPower * 2 * Time.deltaTime;
        if (view.CharacterController.velocity.y < -0.3f)
          view.Animator.Play(AnimationKey.Falling);
      }
    }

    public void Slide()
    {
      view.SlideCounter -= Time.deltaTime;

      if (view.SlideCounter <= 0f)
      {
        view.SlideCounter = 0;
        view.CharacterController.center = new Vector3(0, view.ColliderCenterY, 0);
        view.CharacterController.height = view.ColliderHeight;
        view.InSlide = false;
      }

      if (view.SwipeDown)
      {
        view.SlideCounter = 1f;
        view.y -= 10f;
        view.CharacterController.center = new Vector3(0, view.ColliderCenterY / 2, 0);
        view.CharacterController.height = view.ColliderHeight / 2f;
        view.Animator.CrossFadeInFixedTime(AnimationKey.Slide, 0.1f);
        view.InSlide = true;
        view.InJump = false;
      }
    }

    private void OnGameEnded()
    {
      view.CharacterController.center = new Vector3(0, 0, 0);
      view.CharacterController.height = 0.2f;

      view.Animator.Play(AnimationKey.Death);

      view.Speed = 0;
    }
    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEvent.GameStarted, OnGameStarted);
      dispatcher.RemoveListener(MainEvent.GameEnded, OnGameEnded);
    }
  }
}