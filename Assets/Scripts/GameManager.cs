using System.Collections;
using System.Collections.Generic;
using Game.Dialogue;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    PlayerConversant playerConversant;

    void Start()
    {
        playerConversant = GameObject.FindObjectOfType<PlayerConversant>();
        buttons.SetActive(true);
    }

    void Update()
    {
        // Previous Dialogue Ended - Reset
        if (playerConversant.GetCurrentDialogue() == null)
        {
            buttons.SetActive(true);
        }
    }
}
