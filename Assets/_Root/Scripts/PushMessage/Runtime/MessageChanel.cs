using _Root.Scripts.ScriptableBases.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Scripts.PushMessage.Runtime
{
    [CreateAssetMenu(fileName = "Messsge Chanel", menuName = "Scriptable/Cloud/Message Chanel", order = 0)]
    public class MessageChanel : ResetScriptableObject
    {
        public string type;
        public UnityEvent<string> onMessage;
        [SerializeField] private string message;

        public string Message
        {
            get => message;
            set
            {
                onMessage.Invoke(message);
                message = value;
            }
        }


        public override void Reset() => Message = string.Empty;
    }
}