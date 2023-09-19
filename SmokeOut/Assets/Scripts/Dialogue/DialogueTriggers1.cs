using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogues;
    private bool canTriggerDialogue = true;

    private bool inRangeToTrigger = false;

    private DialogueManager dialogueManager;
    [HideInInspector] public QuickOutline _outline;
    [HideInInspector] public HoverOutline _outlineHover;

    private void Awake()
    {
        _outlineHover = GetComponent<HoverOutline>();
    }

    private void Start()
    {
        _outline = GetComponent<QuickOutline>();
        dialogueManager = DialogueManager._dialogueManager;
    }

    private void OnTriggerEnter(Collider other) // Search for a way to make it possible for the dialogue to start only when the mouse is hovering over the object and mouse0 is clicked
    {
        if (canTriggerDialogue && other.CompareTag("Player"))
        {
            inRangeToTrigger = true;
        }
    }

    private void Update()
    {
        if (inRangeToTrigger && Input.GetKeyDown(KeyCode.T))
        {
            //Debug.Log("You oppned the dialouge");
            StartDialogue();
            GameEventsManager.instance.inputEvents.SubmitClicked();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndDialogueOutOfRange();
        }
    }

    private void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogues);
        dialogueManager._dialogueInProgress = this;
        canTriggerDialogue = false;
    }

    private void EndDialogueOutOfRange()
    {
        dialogueManager.EndDialogue();
        inRangeToTrigger = false;
        canTriggerDialogue = true;
    }

}
