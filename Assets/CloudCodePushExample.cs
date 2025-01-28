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
            Debug.Log(AuthenticationService.Instance.PlayerId);
            await SubscribeToPlayerMessages();
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
}