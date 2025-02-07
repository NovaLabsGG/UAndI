using _Root.Scripts.Lobbies.Runtime;
using _Root.Scripts.PushMessage.Runtime;
using _Root.Scripts.ScriptableBases.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Root.Scripts.Answers.Runtime
{
    [CreateAssetMenu(fileName = "Answer", menuName = "Scriptable/Answer")]
    public class Answer : ResetScriptableObject
    {
        [FormerlySerializedAs("playerSessionScriptable")] [SerializeField] private PlayerSessionScriptableObject playerSessionScriptableObject;
        [FormerlySerializedAs("pushMessageScriptable")] [SerializeField] private PushMessageScriptableObject pushMessageScriptableObject;
        [SerializeField] private int[] selfAnswers;
        [SerializeField] private int[] otherAnswers;

        public void Init(int length)
        {
            selfAnswers = new int[length];
            otherAnswers = new int[length];
        }

        public void SetSelfAnswer(int index)
        {
            foreach (var joinedPlayerId in playerSessionScriptableObject.joinedPlayerIds)
            {
                pushMessageScriptableObject.SendPlayerPushMessage(joinedPlayerId, "Progress", index.ToString());
            }
        }

        public void SetOtherAnswer(int index)
        {
            Debug.Log("Send Data");
        }

        public void SubmitSelfAnswer(int[] answer)
        {
            selfAnswers = answer;
        }

        public void SubmitOtherAnswer(int[] answer)
        {
            otherAnswers = answer;
        }

        public override void Reset()
        {
            selfAnswers = null;
            otherAnswers = null;
        }
    }
}