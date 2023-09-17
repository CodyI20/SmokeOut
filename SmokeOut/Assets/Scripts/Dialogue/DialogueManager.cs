using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _dialogueManager { get; private set; }
    public DialogueTrigger _dialogueInProgress;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [HideInInspector][SerializeField] private Button[] optionButtons; // Add buttons for options

    [HideInInspector]
    [SerializeField] private Button[] specialChoiceButtons;
    [SerializeField] private GameObject specialButtonPrefab;
    [SerializeField] private Transform specialButtonContainer;
    [SerializeField] private GameObject specialDialogueBox;
    public bool specialDialogueInProgress = false;

    private Queue<Dialogue> dialogueQueue;
    private Dialogue currentDialogue; // Track the current dialogue

    [SerializeField] private GameObject choiceButtonPrefab; // Reference to the choice button prefab
    [SerializeField] private Transform choiceButtonContainer; // Reference to the container for choice buttons

    private void Awake()
    {
        if (_dialogueManager == null)
            _dialogueManager = this;
    }

    void Start()
    {
        dialogueQueue = new Queue<Dialogue>();
        HideOptions();
    }

    public void StartDialogue(Dialogue[] dialogues)
    {
        dialogueQueue.Clear();

        foreach (Dialogue dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        DisplayNextDialogue();
    }

    public void StartSpecialDialogue(Dialogue[] specialDialogues)
    {
        specialDialogueInProgress = true;
        dialogueQueue.Clear();

        foreach (Dialogue dialogue in specialDialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        DisplayNextSpecialDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            TaskManager._taskManager.MarkTaskAsComplete(nameText.text);
            Destroy(_dialogueInProgress);
            if (_dialogueInProgress._outline != null)
                Destroy(_dialogueInProgress._outline);
            if (_dialogueInProgress._outlineHover != null)
                Destroy(_dialogueInProgress._outlineHover);
            EndDialogue();
            return;
        }

        currentDialogue = dialogueQueue.Dequeue();
        nameText.text = currentDialogue.speakerName;
        dialogueText.text = currentDialogue.dialogueText;
        dialogueBox.SetActive(true);

        HideOptions();

        if (currentDialogue.options != null && currentDialogue.options.Count > 0)
        {
            for (int i = 0; i < currentDialogue.options.Count; i++)
            {
                // Create a new choice button from the prefab
                GameObject choiceButtonObject = Instantiate(choiceButtonPrefab, choiceButtonContainer);
                Button choiceButton = choiceButtonObject.GetComponent<Button>();
                TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = currentDialogue.options[i].optionText;

                int optionIndex = i;
                choiceButton.onClick.AddListener(() => OnOptionSelected(optionIndex));
            }
        }
    }

    public void DisplayNextSpecialDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            // Perform any special actions when special dialogues end
            EndSpecialDialogue();
            return;
        }

        currentDialogue = dialogueQueue.Dequeue();
        nameText.text = currentDialogue.speakerName;
        specialDialogueBox.SetActive(true);

        HideOptions();

        if (currentDialogue.specialChoices != null && currentDialogue.specialChoices.Count > 0)
        {
            for (int i = 0; i < currentDialogue.specialChoices.Count; i++)
            {
                // Create a new special choice button from the prefab
                GameObject specialChoiceButtonObject = Instantiate(specialButtonPrefab, specialButtonContainer);
                Button specialChoiceButton = specialChoiceButtonObject.GetComponent<Button>();
                TextMeshProUGUI choiceText = specialChoiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = currentDialogue.specialChoices[i].optionText;

                int specialChoiceIndex = i;
                specialChoiceButton.onClick.AddListener(() => OnSpecialChoiceSelected(specialChoiceIndex));
            }
        }
    }

    private void OnOptionSelected(int optionIndex)
    {
        if (optionIndex >= 0 && optionIndex < currentDialogue.options.Count)
        {
            Dialogue[] nextDialogues = currentDialogue.options[optionIndex].nextDialogues;
            dialogueQueue.Clear(); // Clear the queue
            foreach (Dialogue dialogue in nextDialogues)
            {
                dialogueQueue.Enqueue(dialogue);
            }
            DisplayNextDialogue();
        }
    }

    private void OnSpecialChoiceSelected(int specialChoiceIndex)
    {
        if (currentDialogue.specialChoices[specialChoiceIndex].goodOption == false)
        {
            NegativeEffects._negativeEffect.ClearEffects();
            EndSpecialDialogue();
        }
        else
        {
            NegativeEffects._negativeEffect.isIncreasing = false;
            EndSpecialDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        _dialogueInProgress = null;
        HideOptions();
    }

    public void EndSpecialDialogue()
    {
        specialDialogueBox.SetActive(false);
        specialDialogueInProgress = false;
        HideSpecialButtons();
    }

    private void HideOptions()
    {
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Transform child in choiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void HideSpecialButtons()
    {
        foreach (Transform child in specialButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDestroy()
    {
        _dialogueManager = null;
    }
}
