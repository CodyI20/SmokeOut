using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingPlayer : MonoBehaviour
{
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            this.GetComponent<PlayerMovement>().enabled = false;

        }
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            this.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
