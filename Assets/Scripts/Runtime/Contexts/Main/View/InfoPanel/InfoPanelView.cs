using System.Collections.Generic;
using DG.Tweening;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Contexts.Main.View.InfoPanel
{
  public class InfoPanelView : EventView
  {
    [Header("In Game Screen")]
    public GameObject InGameScreen;
    
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI HealthText;
    
    [Space(15)]
    [Header("End Game Screen")]
    public GameObject GameEndedScreen;

    public GameObject NewHighestScoreText;

    public TextMeshProUGUI CurrentScoreText;

    public TextMeshProUGUI HighestScoreText;

    [Space(15)]
    [Header("Start Game Screen")]
    public GameObject StartScreen;

    public TextMeshProUGUI HighestScoreTextForStartPanel;

    [Space(15)]
    public AudioSource AudioSource;

    public TextMeshProUGUI MusicButtonText;

    [Space(15)]
    public TextMeshProUGUI SpeedText;

    [HideInInspector]
    public bool IsMusicMuted;

    public void OnPlayAgain()
    {
      dispatcher.Dispatch(InfoPanelEvent.PlayAgain);
    }

    public void OnStartGame()
    {
      dispatcher.Dispatch(InfoPanelEvent.StartGame);
    }

    public void OnMuteMusic()
    {
      dispatcher.Dispatch(InfoPanelEvent.MuteMusic);
    }

    public void OnCoinCollectedAnimations()
    {
      // ScoreText.rectTransform.DOScale(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo);
      // ScoreText.rectTransform.DOScale(1.2f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

      Vector3 originalScale = transform.localScale;
      
      ScoreText.transform.DOScale(originalScale * 1.15f, 0.1f)
        .OnComplete(() => 
        {
          ScoreText.transform.DOScale(originalScale, 0.1f);
        });

    }
  }
}