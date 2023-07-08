using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        public string text;
        public List<string> children = new List<string>();
        public Rect rect = new Rect(20, 20, 200, 100);

        public Rect GetRect()
        {
            return rect;
        }
    }
}