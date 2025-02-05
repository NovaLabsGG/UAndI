using System.Collections.Generic;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace _Root.Scripts.Lobbies.Runtime
{
    [CreateAssetMenu(fileName = "PlayerSession", menuName = "Scriptable/Multiplayer/PlayerSession", order = 0)]
    public class PlayerSessionScriptable : ScriptableObject
    {
        public ISession CurrentSession;
        
        [SerializeField] private List<string> joinedPlayerIds;
        [SerializeField] private UnityEvent<List<string>> onPlayerJoined;
        [SerializeField] private string nextSceneOnPlayerJoined="question";

        public void OnJoinedSession(ISession session)
        {
            CurrentSession = session;
            session.PlayerJoined -= SessionOnPlayerJoined;
            session.PlayerJoined += SessionOnPlayerJoined;
        }

        private void SessionOnPlayerJoined(string id)
        {
            joinedPlayerIds.Add(id);
            onPlayerJoined.Invoke(joinedPlayerIds);
            SceneManager.LoadScene(nextSceneOnPlayerJoined);
        }

        public void Reset()
        {
            if (CurrentSession != null) CurrentSession.PlayerJoined -= SessionOnPlayerJoined;
        }
    }
}