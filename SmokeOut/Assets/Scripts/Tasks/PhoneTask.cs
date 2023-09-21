using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTask : TaskStep
{
    protected override void PickUpItem()
    {
        base.PickUpItem();
        TaskCompletionEvents("Pick up your phone", true);
    }
}
