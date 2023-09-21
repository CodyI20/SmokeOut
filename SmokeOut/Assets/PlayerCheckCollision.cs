using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Phone"))
        {
            GameEventsManager.instance.detectEvents.DetectPhone();
            //Debug.Log("Phone detected in range");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Phone"))
        {
            GameEventsManager.instance.detectEvents.UndetectPhone();
            //Debug.Log("Phone undetected in range");
        }
    }
}