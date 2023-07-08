using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    [System.Serializable]
    public class DialogueNode
    {
        public string uniqueID;
        public string text;
        public string[] children;
        public Rect rect = new Rect(20, 20, 200, 100);
    }
}