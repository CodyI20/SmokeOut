using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanTheRoomTaskStep : TaskStep
{
<<<<<<< Updated upstream
    private int trashCollected = 0;
    private int trashToComplete = 5;

    private void Update()
    {
        TrashCollection.Trash = trashCollected;
    }
    private void TrashCollected()
    {
        

        if (trashCollected < trashToComplete)
=======
    public ObjectPickUp pickUp;

    private int trashCollected = 0;
    private int trashToComplete = 1;

    void Update()
    {
        if ((trashCollected < trashToComplete) && (pickUp.pickUpItem))
>>>>>>> Stashed changes
        {
            trashCollected++;
        }

<<<<<<< Updated upstream
        if (trashCollected >= trashToComplete)
        {
            FinishTaskStep();
        }
    }   

=======
        if ((pickUp.pickUpItem) && (trashCollected >= trashToComplete)) 
        {
            FinishTaskStep();
        }
    }
>>>>>>> Stashed changes
}

