using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.PlayerMovement
{
  public class PlayerMovementMediator : EventMediator
  {
    [Inject]
    public PlayerMovementView view { get; set; }

    public override void OnRegister()
    {
    }

    private void Jump()
    {
      float height = GetComponent<Collider>().bounds.size.y;
      bool isGrounded = Physics.Raycast(transform.position, Vector3.down, height / 2 + 0.1f, view.GroundMask);

      if (!isGrounded)
        return;
      
      view.Rigidbody.AddForce(Vector3.up * view.Jump);
    }

    private void Move()
    { 
      view.Speed += Time.fixedDeltaTime / 100; 
      Vector3 forwardMove = transform.forward * view.Speed * Time.fixedDeltaTime;
      Vector3 horizontalMove = transform.right * view.HorizontalInput * view.HorizontalSpeed * Time.fixedDeltaTime;
      view.Rigidbody.MovePosition(view.Rigidbody.position + forwardMove + horizontalMove);
    }

    private void FixedUpdate()
    {
      Move();
    }

    private void Update()
    {
      view.HorizontalInput = Input.GetAxis(view.Horizontal);
      
      if (Input.GetKeyDown(KeyCode.Space))
        Jump();
    }

    public override void OnRemove()
    {
    }
  }
}