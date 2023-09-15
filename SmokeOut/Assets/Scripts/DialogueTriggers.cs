using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggers : MonoBehaviour
{
    //PRIVATE VARIABLES
    [SerializeField] private Dialogue _dialoguePlayed; //This variable uses the Dialogue class in order to play a specific dialogue needed for the interaction at hand
    private GameObject _player; //This variable will be used to keep track of the player easier, using the singleton pattern created
    [SerializeField] private DialogueUIDisplay _dialogueUI; //This variable keeps track of the UI display of the dialogue
    [SerializeField] private int correctChoiceNumber = 1; //This value will help with deciding which choice is the correct one 
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerMovement.player == null)
            return;
        else
            _player = PlayerMovement.player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialogueManager._dialogueManger.BeginDialogueInteraction(_dialoguePlayed);
        }
    }
}
