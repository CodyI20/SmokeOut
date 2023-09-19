using System;
using UnityEngine;

public class ChewingGumInteraction : TaskSuperclass
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float requiredChewDuration = 5f;
    [SerializeField] private float failureDuration = 1f; // Time until task failure if key isn't pressed
    [SerializeField] private GameObject _gumUI;

    private bool isChewing = false;
    private bool hasPressedKey = false;
    private float chewTimer = 0f;

    private void Update()
    {
        if (isChewing)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                hasPressedKey = true;

                if (chewTimer >= requiredChewDuration)
                {
                    ChewingComplete();
                }
            }
            if (hasPressedKey)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (keyCode != interactionKey && Input.GetKeyDown(keyCode))
                    {
                        ChewingInterrupted();
                    }
                }
            }
            if(hasPressedKey)
            {
                chewTimer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isChewing)
        {
            isChewing = true;
            _gumUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isChewing)
        {
            ChewingInterrupted();
        }
    }

    private void ChewingComplete()
    {
        // Handle the successful completion of chewing, e.g., progress in the game.
        Debug.Log("Chewing Complete!");
        DestroyOutlines();
        isChewing = false;
        chewTimer = 0f;
        Destroy(gameObject);
    }

    private void ChewingInterrupted()
    {
        // Handle interruptions, e.g., reset the process and provide feedback.
        Debug.Log("Chewing Interrupted!");
        // Hide the UI and reset the chewing process.
        _gumUI.SetActive(false);
        isChewing = false;
        hasPressedKey = false;
        chewTimer = 0f;
    }
}
