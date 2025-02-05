using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scripts.Questions.Runtime.View
{
    public class OptionView : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private Button button;
        private QuestionView _questionView;

        private void Reset()
        {
            tmpText = GetComponentInChildren<TMP_Text>();
            button = GetComponentInChildren<Button>();
        }

        public void Init(QuestionView questionView, int index)
        {
            _questionView = questionView;
            this.index = index;
        }

        public void SetComponentData(string option)
        {
            tmpText.text = option;
            button.onClick.RemoveListener(NotifyClick);
            button.onClick.AddListener(NotifyClick);
        }

        private void NotifyClick() => _questionView.OnClicked(index);

        private void OnDisable()
        {
            button.onClick.RemoveListener(NotifyClick);
        }
    }
}