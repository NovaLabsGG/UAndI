using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.Subscriptions;
using Unity.Services.Core;
using UnityEngine;
namespace CloudCode
{
	public class CloudCodePushExample : MonoBehaviour
	{
        void SetupEvents() {
            AuthenticationService.Instance.SignedIn += () => {
                // Shows how to get a playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

                // Shows how to get an access token
                Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

            };

            AuthenticationService.Instance.SignInFailed += (err) => {
                Debug.LogError(err);
            };

            AuthenticationService.Instance.SignedOut += () => {
                Debug.Log("Player signed out.");
            };

            AuthenticationService.Instance.Expired += () =>
            {
                Debug.Log("Player session could not be refreshed and expired.");
            };
        }
    	async void Start()
        {
            await UnityServices.InitializeAsync();
            
            SetupEvents();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await SubscribeToPlayerMessages();
        }

        // This method creates a subscription to player messages and logs out the messages received,
        // the state changes of the connection, when the player is kicked and when an error occurs.
        Task SubscribeToPlayerMessages()
        {
            // Register callbacks, which are triggered when a player message is received
            var callbacks = new SubscriptionEventCallbacks();
            callbacks.MessageReceived += OnCallbacksOnMessageReceived;
            callbacks.ConnectionStateChanged += OnCallbacksOnConnectionStateChanged;
            callbacks.Kicked += OnCallbacksOnKicked;
            callbacks.Error += OnCallbacksOnError;
            return CloudCodeService.Instance.SubscribeToPlayerMessagesAsync(callbacks);
        }

        private void OnCallbacksOnError(string @event)
        {
            Debug.Log($"Got player subscription Error: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        }

        private void OnCallbacksOnKicked()
        {
            Debug.Log($"Got player subscription Kicked");
        }

        private void OnCallbacksOnConnectionStateChanged(EventConnectionState @event)
        {
            Debug.Log($"Got player subscription ConnectionStateChanged: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        }

        private void OnCallbacksOnMessageReceived(IMessageReceivedEvent @event)
        {
            Debug.Log(DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"));
            Debug.Log($"Got player subscription Message: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        }
    }
}