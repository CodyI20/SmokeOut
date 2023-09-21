using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showering : TaskStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.detectEvents.onFinishShowering += ShoweringFinished;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.detectEvents.onFinishShowering -= ShoweringFinished;
    }

    private void ShoweringFinished()
    {
        FinishTaskStep();
    }
}
