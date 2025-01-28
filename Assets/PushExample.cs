using System;
using System.Collections.Generic;
using Unity.Services.CloudCode;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PushTest", fileName = "PushTest")]
public class PushScriptableObject : ScriptableObject
{
    public string playerId = "oD56qJZn11Gsmgac42vV84YfJB0j";

    [ContextMenu("Invoke")]
    public async void Invoke()
    {
        try
        {
            var sendPlayerMessageResult = await CloudCodeService.Instance.CallModuleEndpointAsync<string>("HelloSample",
                "SendPlayerMessage",
                new Dictionary<string, object>
                {
                    { "message", "hello with a player message from PushExample!" },
                    { "messageType", "testType" },
                    { "playerId", playerId }
                });
            Debug.Log(sendPlayerMessageResult);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}