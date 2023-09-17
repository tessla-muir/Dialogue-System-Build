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
        bool isChoosing = false;
        bool hasSingleChoice = false;

        private void Awake() 
        {
            currentNode = currentDialogue.GetRootNode();    
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public bool HasSingleChoice()
        {
            return hasSingleChoice;
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        // Sets the current node to the first child node
        public void Next()
        {
            DialogueNode[] childNodes = currentDialogue.GetAllChildren(currentNode).ToArray();

            if (currentDialogue.GetPlayerChildren(currentNode).Count() == 1)
            {
                hasSingleChoice = true;
                currentNode = childNodes[0];
                return;
            }
            else if (currentDialogue.GetPlayerChildren(currentNode).Count() > 0)
            {
                isChoosing = true;
                return;
            }

            currentNode = childNodes[0];
        }

        // Returns true if the dialogue continues after this node (thus has a child node)
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
