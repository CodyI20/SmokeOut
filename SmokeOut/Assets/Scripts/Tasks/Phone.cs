using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : ObjectPickUp
{
    protected override void PickUpItem()
    {
        base.PickUpItem();
        Debug.Log("FoundPhone!");
    }
}
