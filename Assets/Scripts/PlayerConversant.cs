using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;
        [SerializeField] Sprite[] playerSprites;

        [SerializeField] GameObject DialogueUI;
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;
        bool hasSingleChoice = false;

        public event Action onConversationUpdated;

        void Awake()
        {
            // Activate the UI for setup
            DialogueUI.SetActive(true);
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            DialogueUI.SetActive(true);
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();    
            TriggerEnterActions();
            onConversationUpdated();
        }

        public void Quit()
        {
            currentDialogue = null;
            TriggerExitActions();
            currentNode = null;
            isChoosing = false;
            hasSingleChoice = false;
            onConversationUpdated();
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

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterActions();
            isChoosing = false;

            // Skip to next node
            Next();
        }

        // Sets the current node to the first child node
        public void Next()
        {
            DialogueNode[] childNodes = currentDialogue.GetAllChildren(currentNode).ToArray();

            // Player text
            if (currentDialogue.GetPlayerChildren(currentNode).Count() == 1)
            {
                hasSingleChoice = true;
                NextText(childNodes);
                return;
            }
            // Player choices
            else if (currentDialogue.GetPlayerChildren(currentNode).Count() > 0)
            {
                hasSingleChoice = false; // Only needed for two consecutive choice lists
                TriggerExitActions();
                isChoosing = true;
                onConversationUpdated();
                return;
            }

            // AI text
            hasSingleChoice = false;
            NextText(childNodes);
        }

        // Triggers actions and moves to next dialogue node
        private void NextText(DialogueNode[] childNodes)
        {
            TriggerExitActions();
            currentNode = childNodes[0];
            TriggerEnterActions();
            onConversationUpdated();
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

        // Returns name of conversant for current node
        public string GetConversantName()
        {
            if (isChoosing || hasSingleChoice)
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetName();
            }
        }

        public Sprite GetConversantSprite()
        {
            if (isChoosing || hasSingleChoice)
            {
                return playerSprites[0];
            }
            else
            {
                return currentConversant.GetConversantSprite(0);
            }
        }

        private void TriggerEnterActions()
        {
            if (currentNode != null && currentNode.GetOnEnterActions().Count() > 0)
            {
                foreach (string action in currentNode.GetOnEnterActions())
                {
                    Debug.Log(action);
                }
            }
        }

        private void TriggerExitActions()
        {
            if (currentNode != null && currentNode.GetOnExitActions().Count() > 0)
            {
                foreach (string action in currentNode.GetOnExitActions())
                {
                    Debug.Log(action);
                }
            }
        }
    }
}
