using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogues;
    private bool canTriggerDialogue = true;

    private bool inRangeToTrigger = false;

    private DialogueManager dialogueManager;
    [HideInInspector] public QuickOutline _outline;
    [HideInInspector] public HoverOutline _outlineHover;

    [Header("Only if on PLAYER")]
    [Tooltip("Time in seconds until the next choice")][SerializeField] private float timeTillChoice = 0f;

    private void Awake()
    {
        _outlineHover = GetComponent<HoverOutline>();
    }

    private void Start()
    {
        _outline = GetComponent<QuickOutline>();
        dialogueManager = DialogueManager._dialogueManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTriggerDialogue && other.CompareTag("Player"))
        {
            inRangeToTrigger = true;
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("Player") && !dialogueManager.specialDialogueInProgress && Input.GetKeyDown(KeyCode.M))
        {
            StartSpecialDialogue();
        }
        if (inRangeToTrigger && Input.GetKeyDown(KeyCode.T))
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndDialogueOutOfRange();
        }
    }

    private void StartSpecialDialogue()
    {
        dialogueManager.StartSpecialDialogue(dialogues);
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
