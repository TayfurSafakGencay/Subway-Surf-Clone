using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Player.Player
{
  public class PlayerView : EventView
  {
    [HideInInspector]
    public int TotalScore;

    [HideInInspector]
    public int PositionScore;

    [HideInInspector]
    public int CoinScore;

    public List<Renderer> Renderers;

    [HideInInspector]
    public bool DamageTaken;

    public GameObject CrashEffect;
  }
}