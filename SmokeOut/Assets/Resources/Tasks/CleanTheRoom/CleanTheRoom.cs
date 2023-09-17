using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanTheRoomTaskStep : TaskStep
{
    public ObjectPickUp pickUp;

    private int trashCollected = 0;
    private int trashToComplete = 1;

    void Update()
    {
        if ((trashCollected < trashToComplete) && (pickUp.pickUpItem))
        {
            trashCollected++;
        }

        if (trashCollected >= trashToComplete)
        {
            FinishTaskStep();
        }
    }   
}

