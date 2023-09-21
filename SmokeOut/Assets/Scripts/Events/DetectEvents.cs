using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEvents : MonoBehaviour
{
    /// Phone task detection
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

    ///Chewing the gum task detection

    public event Action onFinishChewing;
    public void FinishChewing()
    {
        if(onDetectPhone != null)
        {
            onFinishChewing();
        }
    }

    ///Taking the shower task detection

    public event Action onFinishShowering;
    public void FinishShowering()
    {
        if(onFinishShowering != null)
        {
            onFinishShowering();
        }
    }

    ///Getting the pillow,blanket task detection

    public event Action onFinishPillow;
    public void FinishPillow()
    {
        if(onFinishPillow != null)
        {
            onFinishPillow();
        }
    }

    ///Making the sandwich task detection
    public event Action onFinishSandwich;
    public void FinishSandwich()
    {
        if(onFinishSandwich != null)
        {
            onFinishSandwich();
        }
    }

    ///Showering inprocess
    public event Action onShowering;
    public void Showering()
    {
        if (onShowering != null)
        {
            onShowering();
        }
    }

    ///Showering cancelled
    public event Action onNotShowering;
    public void NotShowering()
    {
        if (onNotShowering != null)
        {
            onNotShowering();
        }
    }

    public event Action onDialogue;
    public void Dialogue()
    {
        if (onDialogue != null)
        {
            onDialogue();
        }
    }

    public event Action onNonDialogue;
    public void NonDialogue()
    {
        if (onDialogue != null)
        {
            onNonDialogue();
        }
    }

}
