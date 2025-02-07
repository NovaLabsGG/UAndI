using System.Collections.Generic;
using _Root.Scripts.ScriptableBases.Runtime.Interfaces;
using UnityEngine;

namespace _Root.Scripts.ScriptableBases.Runtime
{
    public abstract class ResetScriptableObject : ScriptableObject, IReset
    {
        private static readonly HashSet<ResetScriptableObject> ResetAbleScriptables = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnSubsystemRegistration()
        {
            foreach (var resetAbleScriptable in ResetAbleScriptables) resetAbleScriptable.Reset();
        }

        protected void OnEnable()
        {
            ResetAbleScriptables.Add(this);
        }

        public abstract void Reset();
    }
}