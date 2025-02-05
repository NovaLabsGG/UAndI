using System;
using System.Collections;
using _Root.Scripts.Lobbies.Runtime;
using UnityEngine;
using UnityEngine.Networking;

namespace _Root.Scripts.Questions.Runtime
{
    public class QuestionsSession : MonoBehaviour
    {
        public string url = "http://localhost:8000/questions?seed={0}&num={1}";

        public int seed = 10;
        public int count = 5;
        public PlayerSessionScriptable playerSessionScriptable;
        public Question[] questions;

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