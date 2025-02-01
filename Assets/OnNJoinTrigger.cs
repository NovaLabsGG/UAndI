using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Services.Multiplayer;
using UnityEngine;

public class OnNJoinTrigger : MonoBehaviour
{
    public int playerCount = 2;
    public ISession Session;
    public List<string> playerIds;

    public void OnJoinedSession(ISession session)
    {
        Session = session;
        session.PlayerJoined += SessionOnPlayerJoined;
    }

    private void SessionOnPlayerJoined(string id)
    {
        playerIds.Add(id);
    }

    private void OnDestroy()
    {
        if (Session != null) Session.PlayerJoined -= SessionOnPlayerJoined;
    }
}