using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public class Condition : MonoBehaviour
    {
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
