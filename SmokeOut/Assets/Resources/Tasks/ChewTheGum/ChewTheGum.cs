using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : TaskStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.detectEvents.onFinishChewing += ChewFinished;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.detectEvents.onFinishChewing -= ChewFinished;
    }

    private void ChewFinished()
    {
        Debug.Log("Chew Finished");
        FinishTaskStep();
    }
}
