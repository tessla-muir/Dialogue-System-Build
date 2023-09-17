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
            playerConversant.onConversationUpdated += UpdateUI;
            button.onClick.AddListener(Next);

            // Initialize the UI and inactivate it
            UpdateUI();
            gameObject.SetActive(false);
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
        }

        void UpdateUI()
        {
            if (!playerConversant.IsActive()) return;

            // Activate text UI or choice UI
            textResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            // Choice UI is displayed
            if (playerConversant.IsChoosing())
            {
                // Set player sprite & name
                speakerImage.sprite = playerSprite;
                speakerName.text = "Player";
                
                BuildChoiceList();
            }
            // Text UI is displayed for player
            else if (playerConversant.HasSingleChoice())
            {
                // Set player sprite & name
                speakerImage.sprite = playerSprite;
                speakerName.text = "Player";

                BuildTextResponse();
            }
            // Text UI is displayed for AI
            else
            {
                // Set AI sprite & name
                speakerImage.sprite = AISprite;
                speakerName.text = "Wizard";

                BuildTextResponse();
            }
        }

        private void BuildTextResponse()
        {
            speakerText.text = playerConversant.GetText();

            // Change look of button when at end of dialogue
            if (!playerConversant.HasNext())
            {
                button.transform.GetChild(0).GetComponent<Image>().sprite = quitSprite;
            }
        }

        private void BuildChoiceList()
        {
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

                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                });
            }
        }
    }
}
