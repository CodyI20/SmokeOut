using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanTheRoomTaskStep : TaskStep
{
    private int trashCollected = 0;
    private int trashToComplete = 5;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onPickUp += TrashCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onPickUp -= TrashCollected;
    }

    private void TrashCollected()
    {
        if ((trashCollected < trashToComplete))
        {
            trashCollected++;
        }

        if (trashCollected >= trashToComplete)
        {
            FinishTaskStep();
        }
    }   
}

