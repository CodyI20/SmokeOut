using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEvents : MonoBehaviour
{
    public event Action onDetectPhone;
    public void DetectPhone()
    {
        if (onDetectPhone != null)
        {
            onDetectPhone();
        }
    }

    public event Action onUndetectPhone;

    public void UndetectPhone()
    {
        if(onUndetectPhone != null)
        {
            onUndetectPhone();
        }
    }


}
