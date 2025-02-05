using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.Questions.Runtime.View
{
    public class QuestionView : MonoBehaviour
    {
        public TMP_Text text;
        public Transform optionSpawnTarget;
        public OptionView optionsPrefab;
        public List<OptionView> pool;

        public void Set(string question, string[] options)
        {
            PopulatePool(optionsPrefab, options.Length);
            for (var i = 0; i < options.Length; i++)
            {
                pool[i].SetComponentData(options[i]);
            }
        }

        public static void OnClicked(int index)
        {
            Debug.Log(index);
        }

        private void PopulatePool(OptionView optionViewPrefab, int length)
        {
            while (pool.Count < length)
            {
                var optionView = Instantiate(optionViewPrefab, optionSpawnTarget);
                optionView.SetIndex(pool.Count);
                pool.Add(optionView);
            }
        }
    }
}