using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class FindingThePhone : TaskStep
{   
    private bool phoneIsNear = false;
     private void OnEnable()
    {
        GameEventsManager.instance.detectEvents.onDetectPhone += PhoneDetected;
        GameEventsManager.instance.detectEvents.onUndetectPhone += PhoneNotDetected;

    }
    private void OnDisable()
    {
        GameEventsManager.instance.detectEvents.onDetectPhone -= PhoneDetected;
        GameEventsManager.instance.detectEvents.onUndetectPhone -= PhoneNotDetected;
    }

    private void PhoneDetected()
    {
        phoneIsNear = true;
    }

    private void PhoneNotDetected()
    {
        phoneIsNear = false;
    }

    void Update()
    {
       if (phoneIsNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FinishTaskStep();
            }
           
        }
    }

}
