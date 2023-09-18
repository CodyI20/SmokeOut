using UnityEngine;
using System;

public class InputEvents
{

    public event Action onSubmitClicked;
    public void SubmitClicked()
    {
        if (onSubmitClicked != null) 
        {
            onSubmitClicked();
        }
    }

    public event Action onPickUp;
    public void PickUp()
    {
        if (onPickUp != null)
        {
            onPickUp();
        }
    }

}
