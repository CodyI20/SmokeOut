using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTheSandwich: TaskStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.detectEvents.onFinishSandwich += SandwichFinished;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.detectEvents.onFinishSandwich -= SandwichFinished;
    }

    private void SandwichFinished()
    {
        FinishTaskStep();
    }
}