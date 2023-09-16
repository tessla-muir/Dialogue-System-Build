using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;
        DialogueNode currentNode = null;

        private void Awake() 
        {
            currentNode = currentDialogue.GetRootNode();    
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public IEnumerable<string> GetChoices()
        {
            yield return "I'm doing well";
            yield return "Eh. It could be better.";
            yield return "Leave me alone";
        }

        // Sets the current node to the first child node
        public void Next()
        {
            DialogueNode[] childNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = childNodes[0];
        }

        public bool HasNext()
        {
            if (currentDialogue.GetAllChildren(currentNode).Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
