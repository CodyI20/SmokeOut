using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    [TextArea(3, 10)]
    public string dialogueText;
    public List<DialogueOption> options; // Add this field for dialogue options
}
