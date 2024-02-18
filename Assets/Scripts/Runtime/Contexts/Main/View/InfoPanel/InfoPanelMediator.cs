using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Contexts.Main.View.InfoPanel
{
  public enum InfoPanelEvent
  {
    PlayAgain,
    StartGame,
    MuteMusic,
  }

  public class InfoPanelMediator : EventMediator
  {
    [Inject]
    public InfoPanelView view { get; set; }

    [Inject]
    public IMainModel mainModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(InfoPanelEvent.PlayAgain, OnPlayAgain);
      view.dispatcher.AddListener(InfoPanelEvent.StartGame, OnStartGame);
      view.dispatcher.AddListener(InfoPanelEvent.MuteMusic, OnMuteMusic);

      dispatcher.AddListener(MainEvent.GetDamage, OnGetDamage);
      dispatcher.AddListener(MainEvent.GameEnded, OnGameEnded);
      dispatcher.AddListener(MainEvent.CollectCoin, OnCoinCollected);

      Init();
    }

    private void Init()
    {
      OnGetDamage();

      OpenStartPanel();
    }

    private void Update()
    {
      if (mainModel.RecordBroken)
        view.ScoreText.color = Color.green;

      view.ScoreText.text = "Score: <b>" + mainModel.GetScore() + "</b>";
      view.SpeedText.text = "Speed: <b>" + mainModel.GetSpeed().ToString("F") + "</b>";

      if (mainModel.GameStarted) return;
      
      bool start = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                   Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ||
                   Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
                   Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

      if (start)
        OnStartGame();
    }

    private void OnGetDamage()
    {
      view.HealthText.text = "HP: <b>" + mainModel.GetHealth() + "</b>";
    }

    private void OnGameEnded()
    {
      OpenEndGamePanel();
    }

    private void OnPlayAgain()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

      OpenInGamePanel();
    }

    private void OnStartGame()
    {
      mainModel.StartGame();

      OpenInGamePanel();
    }

    private void OnCoinCollected(IEvent payload)
    {
      view.OnCoinCollectedAnimations();
    }

    private void OpenStartPanel()
    {
      view.GameEndedScreen.SetActive(false);
      view.InGameScreen.SetActive(false);
      view.StartScreen.SetActive(true);

      view.HighestScoreTextForStartPanel.text = "Highest Score: <b>" + PlayerPrefs.GetInt("Highest Score") + "</b>";
    }

    private void OpenInGamePanel()
    {
      view.GameEndedScreen.SetActive(false);
      view.StartScreen.SetActive(false);
      view.InGameScreen.SetActive(true);
    }

    private void OpenEndGamePanel()
    {
      view.StartScreen.SetActive(false);
      view.InGameScreen.SetActive(false);
      view.GameEndedScreen.SetActive(true);

      view.CurrentScoreText.text = "Current Score: <b>" + mainModel.GetScore() + "</b>";

      if (mainModel.RecordBroken)
      {
        view.NewHighestScoreText.SetActive(true);
        view.HighestScoreText.color = Color.green;
      }
      else
      {
        view.NewHighestScoreText.SetActive(false);
      }

      view.HighestScoreText.text = "Highest Score: <b>" + PlayerPrefs.GetInt("Highest Score") + "</b>";
    }

    private void OnMuteMusic()
    {
      view.IsMusicMuted = !view.IsMusicMuted;
      
      view.AudioSource.mute = view.IsMusicMuted;

      view.MusicButtonText.text = view.IsMusicMuted ? "Unmute Music" : "Mute Music";
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(InfoPanelEvent.PlayAgain, OnPlayAgain);
      view.dispatcher.RemoveListener(InfoPanelEvent.StartGame, OnStartGame);
      view.dispatcher.RemoveListener(InfoPanelEvent.MuteMusic, OnMuteMusic);

      dispatcher.RemoveListener(MainEvent.GetDamage, OnGetDamage);
      dispatcher.RemoveListener(MainEvent.GameEnded, OnGameEnded);
      dispatcher.RemoveListener(MainEvent.CollectCoin, OnCoinCollected);
    }
  }
}