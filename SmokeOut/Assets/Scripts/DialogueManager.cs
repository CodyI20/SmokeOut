using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// This class serves as a base for all dialogue-related interactions
    /// </summary>

    //Creating a singleton patter for the dialogue manager so that it can be accessed by any script with ease (and since there will only be one dialogue manager in the whole project)
    public static DialogueManager _dialogueManger {  get; private set; }

    public bool dialogueIsPlaying = false;
    public Queue<string> sentences; //This variable will keep track of all the sentences that are about to show up, for a smooth interaction

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private void Awake()
    {
        if (_dialogueManger == null)
            _dialogueManger = this;
        sentences = new Queue<string>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginDialogueInteraction(Dialogue dialogue)
    {
        Debug.Log("Starting interaction...");
        //sentences.Clear();
        //_dialogueManger.dialogueIsPlaying = true;

        //nameText.text = dialogue.name;
        //foreach (string sentence in dialogue.dialogueLines)//Queue all the sentences from the current dialogue
        //{
        //    sentences.Enqueue(sentence);
        //}
        //ContinueDialogueInteraction();
    }

    private void ContinueDialogueInteraction()
    {
        if(sentences.Count == 0)
        {
            EndDialogueInteraction();
            return;
        }

        dialogueText.text = sentences.Dequeue();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogueInteraction();
        }
    }

    private void EndDialogueInteraction()
    {
        Debug.Log("End of the interaction");
    }

    private void OnDestroy()
    {
        _dialogueManger = null;
    }
}
