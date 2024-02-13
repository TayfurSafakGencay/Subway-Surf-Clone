using Runtime.Contexts.Main.Enum;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Ground
{
  public class GroundEndPointBehaviour : MonoBehaviour
  {
    public GroundView GroundView;
    private void OnTriggerEnter(Collider other)
    {
      if (!other.gameObject.CompareTag(TagKey.Player) || !gameObject.CompareTag(TagKey.GroundEndPoint)) return;
      GroundView.EndPointChecker();
    }
  }
}