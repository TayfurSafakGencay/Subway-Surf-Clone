using System.Collections;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Player.Player
{
  public class PlayerMediator : EventMediator
  {
    [Inject]
    public PlayerView view { get; set; }
    
    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(TagKey.Obstacle))
      {
        if (view.DamageTaken)
          return;
        
        mainModel.ObstacleCollision();

        view.DamageTaken = true;
        StartCoroutine(WaitBlinkEffect(3f, 0.2f));
        
        GameObject instantiate = Instantiate(view.CrashEffect, transform.position + new Vector3(0, 1f, 0),
          transform.rotation, transform);
        Destroy(instantiate, 1f);
      }
      else if (other.CompareTag(TagKey.Coin))
      {
        view.CoinScore += 100;
      }
    }

    private void Update()
    {
      if (mainModel.GameEnded || !mainModel.GameStarted)
        return;
      
      view.PositionScore = (int)transform.position.z;
      view.TotalScore = view.PositionScore + view.CoinScore;
      mainModel.SetScore(view.TotalScore);
    }

    private IEnumerator WaitBlinkEffect(float duration, float blinkFrequency)
    {
      bool boolean = true;

      float endTime = Time.time + (duration - duration * blinkFrequency);

      while (Time.time < endTime)
      {
        boolean = !boolean;

        for (int i = 0; i < view.Renderers.Count; i++)
          view.Renderers[i].enabled = boolean;
        yield return new WaitForSeconds(0.2f);
      }

      for (int i = 0; i < view.Renderers.Count; i++)
        view.Renderers[i].enabled = boolean;

      view.DamageTaken = false;
    }

    public override void OnRemove()
    {
    }
  }
}