using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartUpDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    

}
