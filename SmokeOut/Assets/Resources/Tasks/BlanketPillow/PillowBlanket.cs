using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowBlanket : TaskStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.detectEvents.onFinishPillow += FinishPillowing;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.detectEvents.onFinishPillow -= FinishPillowing;
    }

    private void FinishPillowing()
    {
        FinishTaskStep();
    }
}
