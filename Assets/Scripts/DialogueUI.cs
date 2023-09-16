using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace Game.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button button;
        [SerializeField] Sprite quitSprite;

        void Start()
        {
            playerConversant = GameObject.Find("Player").GetComponent<PlayerConversant>();
            button.onClick.AddListener(Next);

            // Initialize the UI
            UpdateUI();
        }

        // Continues dialogue, updating UI
        void Next()
        {
            if (playerConversant.HasNext())
            {
                playerConversant.Next();
            }
            else
            {
                // Exit Dialogue UI by deactivating it
                GameObject.FindObjectOfType<DialogueUI>().gameObject.SetActive(false);
            }
            
            UpdateUI();
        }

        void UpdateUI()
        {
            AIText.text = playerConversant.GetText();

            if (!playerConversant.HasNext())
            {
                button.transform.GetChild(0).GetComponent<Image>().sprite = quitSprite;
            }
        }
    }
}
