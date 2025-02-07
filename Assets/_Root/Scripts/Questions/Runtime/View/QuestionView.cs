using System.Collections.Generic;
using System.Linq;
using _Root.Scripts.PushMessage.Runtime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityProgressBar;

namespace _Root.Scripts.Questions.Runtime.View
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Transform optionSpawnTarget;
        [SerializeField] private OptionView optionsPrefab;
        [SerializeField] private List<OptionView> pool;
        [SerializeField] private int[] answer;
        [SerializeField] private int index;
        [SerializeField] private string nextSceneOnAllQuestionAnswered = "answer";
        [SerializeField] private ProgressBar selfProgressBar;
        [SerializeField] private ProgressBar otherProgressBar;
        [SerializeField] private float extra = .3f;

        public MessageChanel messageChanel;

        private Question[] _questions;
        [SerializeField] private UnityEvent<int> onOptionSelection;
        [SerializeField] private UnityEvent<int[]> onAllAnswer;

        public void Set(Question[] questions)
        {
            _questions = questions;
            index = 0;
            selfProgressBar.Value = extra / questions.Length;
            answer = new int[questions.Length];
            ShowQuestion(questions[0]);
            messageChanel.onMessage.AddListener(OnMessageFromOther);
        }

        private void OnMessageFromOther(string arg0)
        {
            int otherIndex = int.Parse(arg0);
            Debug.Log(arg0);
            otherProgressBar.Value = (otherIndex + extra) / _questions.Length;
        }

        private void ShowQuestion(Question question)
        {
            text.text = question.text;
            PopulatePool(optionsPrefab, question.options.Length);
            for (var i = 0; i < question.options.Length; i++)
            {
                pool[i].SetComponentData(question.text);
            }
        }

        public void OnClicked(int selectedIndex)
        {
            answer[index] = selectedIndex;
            if (index == _questions.Length - 1)
            {
                onAllAnswer.Invoke(answer);
                SceneManager.LoadScene(nextSceneOnAllQuestionAnswered);
                return;
            }

            ShowQuestion(_questions[index++]);
            selfProgressBar.Value = (index + extra) / _questions.Length;
            onOptionSelection.Invoke(index);
        }

        private void PopulatePool(OptionView optionViewPrefab, int length)
        {
            while (pool.Count < length)
            {
                var optionView = Instantiate(optionViewPrefab, optionSpawnTarget);
                optionView.Init(this, pool.Count);
                pool.Add(optionView);
            }

            for (int i = 0; i < length; i++) pool[i].gameObject.SetActive(true);
            for (int i = length; i < pool.Count; i++) pool[i].gameObject.SetActive(false);
        }
    }
}