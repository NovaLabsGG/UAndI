using UnityEngine;

namespace _Root.Scripts.Lobbies.Runtime
{
    public class OnNJoinTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerSessionScriptable playerSessionScriptable; 

        private void OnDestroy()
        {
            playerSessionScriptable.Reset();
        }
    }
}