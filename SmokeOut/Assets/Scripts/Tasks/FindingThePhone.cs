using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class FindingThePhone : TaskStep
{
    protected void PhoneCollected()
    {
        PickUpItem();
        Debug.Log("FoundPhone!");
        FinishTaskStep();
    }
}
