using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public class Condition : MonoBehaviour
    {
        // Ideally, these fields are converted to enums that are based on surrounding systems like quests & inventory
        [SerializeField] string predicate;
        [SerializeField] string[] parameters;

        public bool Check(IEnumerable<IPredicatEvaluator> evaluators)
        {
            foreach (var evaluator in evaluators)
            {
                bool? result = evaluator.Evaluate(predicate, parameters);
                if (result == null)
                {
                    continue;
                }

                if (result == false) return false;
            }
            return true;
        }
    }
}
