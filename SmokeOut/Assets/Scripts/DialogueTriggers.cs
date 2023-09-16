using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogues;
    private bool canTriggerDialogue = true;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager._dialogueManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTriggerDialogue && other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canTriggerDialogue && !dialogueManager.dialogueEnded && other.CompareTag("Player"))
        {
            EndDialogueOutOfRange();
        }
    }

    private void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogues);
        canTriggerDialogue = false;
    }

    private void EndDialogueOutOfRange()
    {
        dialogueManager.EndDialogue();
        canTriggerDialogue = true;
    }

}
