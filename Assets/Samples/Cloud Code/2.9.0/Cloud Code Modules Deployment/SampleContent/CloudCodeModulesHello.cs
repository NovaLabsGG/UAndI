using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.Subscriptions;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.EventSystems;
#if INPUT_SYSTEM_PRESENT
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;
#endif
using UnityEngine.UI;

class CloudCodeModulesHello : MonoBehaviour
{
    [SerializeField]
    Button m_Button;
    [SerializeField]
    TMP_InputField m_InputField;
    [SerializeField]
    StandaloneInputModule m_DefaultInputModule;

    public string playerId="oD56qJZn11Gsmgac42vV84YfJB0j";

#if INPUT_SYSTEM_PRESENT
    void Awake()
    {
        m_DefaultInputModule.enabled = false;
        m_DefaultInputModule.gameObject.AddComponent<InputSystemUIInputModule>();
        TouchSimulation.Enable();
    }
#endif

    async void Start()
    {
        m_Button.interactable = false;
        m_Button.onClick.AddListener(Call);

        await InitializeServices();
        await SignInAnonymously();
        await SubscribeToPlayerMessages();

        m_Button.interactable = true;
    }

    private async void Call()
    {
        await CallModuleEndpoint();
    }

    static async Task InitializeServices()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
        }
    }

    static async Task SignInAnonymously()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log(AuthenticationService.Instance.PlayerId);
        }
    }

    async Task CallModuleEndpoint()
    {
        m_Button.interactable = false;

        try
        {
            var arguments = new Dictionary<string, object>();
            arguments.Add("name", m_InputField.text);
            arguments.Add("playerId", playerId);
            var result = await CloudCodeService.Instance.CallModuleEndpointAsync<string>("HelloSample", "SayHello", arguments);
            Debug.Log(result);
        }
        finally
        {
            m_Button.interactable = true;
        }
    }
    
    // This method creates a subscription to player messages and logs out the messages received,
    // the state changes of the connection, when the player is kicked and when an error occurs.
    Task SubscribeToPlayerMessages()
    {
        // Register callbacks, which are triggered when a player message is received
        var callbacks = new SubscriptionEventCallbacks();
        callbacks.MessageReceived += @event =>
        {
            Debug.Log(DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"));
            Debug.Log($"Got player subscription Message: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        };
        callbacks.ConnectionStateChanged += @event =>
        {
            Debug.Log($"Got player subscription ConnectionStateChanged: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        };
        callbacks.Kicked += () =>
        {
            Debug.Log($"Got player subscription Kicked");
        };
        callbacks.Error += @event =>
        {
            Debug.Log($"Got player subscription Error: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        };
        return CloudCodeService.Instance.SubscribeToPlayerMessagesAsync(callbacks);
    }
}
