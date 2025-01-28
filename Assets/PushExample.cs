using System;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.GeneratedBindings;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PushTest", fileName = "PushTest")]
public class PushTest : ScriptableObject
{
    [ContextMenu("Invoke")]
    public async void Invoke()
    {
        try
        {
            var module = new HelloWorldBindings(CloudCodeService.Instance);
            var result = await module.SayHello("Whyyy");
            Debug.Log(result);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}