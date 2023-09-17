using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public Dialogue[] nextDialogues; // Array of dialogues to proceed to

    [Header("Only for SPECIAL choices!")]
    public bool goodOption = false;
}
