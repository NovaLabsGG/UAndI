using System.Collections.Generic;
using _Root.Scripts.ScriptableBases.Runtime;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Root.Scripts.Lobbies.Runtime
{
    [CreateAssetMenu(fileName = "PlayerSession", menuName = "Scriptable/Multiplayer/PlayerSession", order = 0)]
    public class PlayerSessionScriptableObject : ResetScriptableObject
    {
        public ISession CurrentSession;

        public List<string> joinedPlayerIds;
        [SerializeField] private string nextSceneOnPlayerJoined = "question";

        public void OnJoinedSession(ISession session)
        {
            CurrentSession = session;
            if (!session.IsHost)
            {
                SessionOnPlayerJoined(session.Host);
                return;
            }
            session.PlayerJoined -= SessionOnPlayerJoined;
            session.PlayerJoined += SessionOnPlayerJoined;
        }
        
        
        
        private void SessionOnPlayerJoined(string id)
        {
            joinedPlayerIds.Add(id);
            SceneManager.LoadScene(nextSceneOnPlayerJoined);
        }

        public override void Reset()
        {
            if (CurrentSession != null)
            {
                CurrentSession.PlayerJoined -= SessionOnPlayerJoined;
                joinedPlayerIds.Clear();
            }
        }
    }
}