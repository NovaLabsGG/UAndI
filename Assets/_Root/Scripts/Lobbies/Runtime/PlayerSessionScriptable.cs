using System.Collections.Generic;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Scripts.Lobbies.Runtime
{
    [CreateAssetMenu(fileName = "PlayerSession", menuName = "Scriptable/Multiplayer/PlayerSession", order = 0)]
    public class PlayerSessionScriptable : ScriptableObject
    {
        public ISession currentSession;
        [SerializeField] private List<string> joinedPlayerIds;
        public UnityEvent<List<string>> onPlayerJoined;

        public void OnJoinedSession(ISession session)
        {
            currentSession = session;
            session.PlayerJoined -= SessionOnPlayerJoined;
            session.PlayerJoined += SessionOnPlayerJoined;
        }

        private void SessionOnPlayerJoined(string id)
        {
            joinedPlayerIds.Add(id);
            onPlayerJoined.Invoke(joinedPlayerIds);
        }

        public void Reset()
        {
            if (currentSession != null) currentSession.PlayerJoined -= SessionOnPlayerJoined;
        }
    }
}