using System;
using System.Collections;
using _Root.Scripts.Lobbies.Runtime;
using _Root.Scripts.Questions.Runtime.View;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityProgressBar;

namespace _Root.Scripts.Questions.Runtime
{
    public class QuestionsSession : MonoBehaviour
    {
        public string url = "http://localhost:8000/questions?seed={0}&num={1}";

        public int seed = 10;
        [SerializeField] private int count = 5;
        [FormerlySerializedAs("playerSessionScriptable")] [SerializeField] private PlayerSessionScriptableObject playerSessionScriptableObject;
        [SerializeField] private Question[] questions;
        [SerializeField] private QuestionView questionView;

        IEnumerator Start()
        {
            UnityWebRequest www = new UnityWebRequest(string.Format(url, seed, count));
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            switch (www.result)
            {
                case UnityWebRequest.Result.InProgress:
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(www.downloadHandler.text);
                    questions = JsonHelper.FromJson<Question>(www.downloadHandler.text);
                    questionView.Set(questions);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
    }
}