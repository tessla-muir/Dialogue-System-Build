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
        [SerializeField] TextMeshProUGUI speakerText;

        [SerializeField] Button button;
        [SerializeField] Sprite quitSprite;

        [SerializeField] GameObject textResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;

        [SerializeField] TextMeshProUGUI speakerName;
        [SerializeField] Image speakerImage;
        [SerializeField] Sprite playerSprite;
        [SerializeField] Sprite AISprite;


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
            // Activate text UI or choice UI
            textResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            // Choice UI is displayed
            if (playerConversant.IsChoosing())
            {
                // Set player sprite & name
                speakerImage.sprite = playerSprite;
                speakerName.text = "Player";

                // Empty out choices
                // Possible performance delay - could keep track of choices and update accordingly instead
                foreach (Transform choice in choiceRoot)
                {
                    Destroy(choice.gameObject);
                }

                // Fill out choices
                foreach (DialogueNode choice in playerConversant.GetChoices())
                {
                    // Create choice button and set text
                    GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                    choiceInstance.GetComponentInChildren<TextMeshProUGUI>().text = choice.GetText();
                }
            }
            // Text UI is displayed for player
            else if (playerConversant.HasSingleChoice())
            {
                // Set player sprite & name
                speakerImage.sprite = playerSprite;
                speakerName.text = "Player";

                speakerText.text = playerConversant.GetText();

                // Change look of button when at end of dialogue
                if (!playerConversant.HasNext())
                {
                    button.transform.GetChild(0).GetComponent<Image>().sprite = quitSprite;
                }
            }
            // Text UI is displayed for AI
            else
            {
                // Set AI sprite & name
                speakerImage.sprite = AISprite;
                speakerName.text = "Wizard";

                speakerText.text = playerConversant.GetText();

                // Change look of button when at end of dialogue
                if (!playerConversant.HasNext())
                {
                    button.transform.GetChild(0).GetComponent<Image>().sprite = quitSprite;
                }
            }
        }
    }
}
