using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Dialogue;
using TMPro;

namespace Game.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.Find("Player").GetComponent<PlayerConversant>();
            AIText.text = playerConversant.GetText();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
