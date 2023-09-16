using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _dialogueManager {  get; private set; }
    public bool dialogueEnded = false;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Button[] optionButtons; // Add buttons for options

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

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            dialogueEnded = true;
            TaskManager._taskManager.MarkTaskAsComplete(nameText.text);
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
        //else
        //{
        //    // No options, proceed automatically
        //    StartCoroutine(AutoProceed());
        //}
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

    //private IEnumerator AutoProceed()
    //{
    //    yield return new WaitForSeconds(1000f);
    //    DisplayNextDialogue();
    //}

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        HideOptions();
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

    private void OnDestroy()
    {
        _dialogueManager = null;
    }
}
