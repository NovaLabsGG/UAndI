using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Root.Scripts.ScriptableBases.Runtime;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.Subscriptions;
using UnityEngine;

namespace _Root.Scripts.PushMessage.Runtime
{
    [CreateAssetMenu(menuName = "Scriptable/Cloud/Push Message", fileName = "Push Message")]
    public class PushMessageScriptableObject : ResetScriptableObject
    {
        public string module = "Cloud";
        public string function = "Message";
        public MessageChanel[] messageTypes;
        private ISubscriptionEvents _subscriptionEvents;


        public async void SendPlayerPushMessage(
            string otherPlayerId,
            string type,
            string message
        )
        {
            try
            {
                var arguments = new Dictionary<string, object>
                {
                    { "playerId", otherPlayerId },
                    { "type", type },
                    { "message", message }
                };
                var sendPlayerMessageResult = await CloudCodeService.Instance.CallModuleEndpointAsync<string>(
                    module,
                    function,
                    arguments
                );
                Debug.Log(sendPlayerMessageResult);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public async Task SubscribeToPlayerMessagesAsync()
        {
            var callbacks = new SubscriptionEventCallbacks();
            callbacks.MessageReceived += OnCallbacksOnMessageReceived;
            callbacks.ConnectionStateChanged += OnCallbacksOnConnectionStateChanged;
            callbacks.Kicked += OnCallbacksOnKicked;
            callbacks.Error += OnCallbacksOnError;
            _subscriptionEvents = await CloudCodeService.Instance.SubscribeToPlayerMessagesAsync(callbacks);
        }

        private void OnCallbacksOnError(string @event)
        {
            Debug.Log($"Got player subscription Error: {@event}");
        }

        private void OnCallbacksOnKicked()
        {
            Debug.Log($"Got player subscription Kicked");
        }

        private void OnCallbacksOnConnectionStateChanged(EventConnectionState @event)
        {
            Debug.Log(
                $"Got player subscription ConnectionStateChanged: {@event}");
        }

        private void OnCallbacksOnMessageReceived(IMessageReceivedEvent messageReceivedEvent)
        {
            Debug.Log(DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"));
            foreach (var messageType in messageTypes)
            {
                if (messageType.type == messageReceivedEvent.MessageType)
                {
                    messageType.Message = messageReceivedEvent.Message;
                    break;
                }
            }
        }

        public override void Reset()
        {
            _subscriptionEvents?.UnsubscribeAsync();
        }
    }
}