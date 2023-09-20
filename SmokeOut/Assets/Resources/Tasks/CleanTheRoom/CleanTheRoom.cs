using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class CleanTheRoomTaskStep : TaskStep
{
    public GameObject[] trash;
    
    void Start()
    {
        trash = GameObject.FindGameObjectsWithTag("Trash");

        foreach(GameObject go in trash) 
        {
            go.GetComponent<ObjectPickUp>().enabled = true;
            go.AddComponent(typeof(HoverOutline));
        }
    }


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

