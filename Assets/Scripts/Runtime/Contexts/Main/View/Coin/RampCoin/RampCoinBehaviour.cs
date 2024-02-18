using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Contexts.Main.View.Coin.RampCoin
{
  public class RampCoinBehaviour : MonoBehaviour
  {
    public int ChancePercent;
    private void Start()
    {
      int chance = Random.Range(0, 101);

      gameObject.SetActive(chance < ChancePercent);
    }
  }
}