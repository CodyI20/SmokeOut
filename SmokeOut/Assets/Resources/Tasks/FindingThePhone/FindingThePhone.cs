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
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            phoneIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            phoneIsNear = false;
        }
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
