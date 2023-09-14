using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanTheRoomTaskStep : TaskStep
{
    private int trashCollected = 0;
    private int trashToComplete = 5;

    private void Update()
    {
        TrashCollection.Trash = trashCollected;
    }
    private void TrashCollected()
    {
        

        if (trashCollected < trashToComplete)
        {
            trashCollected++;
        }

        if (trashCollected >= trashToComplete)
        {
            FinishTaskStep();
        }
    }   

}

