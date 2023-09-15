using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue
{
    [TextArea(3,10)]
    public string[] dialogueLines;
    [SerializeField] private List<string> options;
}
