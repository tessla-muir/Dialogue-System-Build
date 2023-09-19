using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName = "";
        [SerializeField] Sprite[] conversantSprites;

        void Start()
        {
            playerConversant = GameObject.FindObjectOfType<PlayerConversant>();
        }

        public string GetName()
        {
            return conversantName;
        }

        public void StartDialogue()
        {
            playerConversant.StartDialogue(this, dialogue);
        }

        public Sprite GetSprite(int index)
        {
            return conversantSprites[index];
        }
    }
}
