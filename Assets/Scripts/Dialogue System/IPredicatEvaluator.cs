using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public interface IPredicatEvaluator
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}