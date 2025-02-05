using _Root.Scripts.Lobbies.Runtime;
using UnityEngine;

namespace _Root.Scripts.Answers.Runtime
{
    [CreateAssetMenu(fileName = "Answer", menuName = "Scriptable/Answer")]
    public class Answer : ScriptableObject
    {
        [SerializeField] private PlayerSessionScriptable playerSessionScriptable;
        [SerializeField] private int[] selfAnswers;
        [SerializeField] private int[] otherAnswers;

        public void Init(int length)
        {
            selfAnswers = new int[length];
            otherAnswers = new int[length];
        }

        public void SetSelfAnswer(int index)
        {
            Debug.Log("Send Data");
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

        private void Reset()
        {
            selfAnswers = null;
            otherAnswers = null;
        }
    }
}