using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class SimpleTask : TaskStep
{
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            FinishTaskStep();
        }
    }
}
