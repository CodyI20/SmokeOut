
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingPlayer : MonoBehaviour
{

    private void OnEnable()
    {
        GameEventsManager.instance.detectEvents.onShowering += Freeze;
        GameEventsManager.instance.detectEvents.onNotShowering += Thaw;
        GameEventsManager.instance.detectEvents.onDialogue += Freeze;
        GameEventsManager.instance.detectEvents.onNonDialogue += Thaw;
        GameEventsManager.instance.detectEvents.onFinishShowering += Thaw;

    }
    public void OnDisable()
    {
        GameEventsManager.instance.detectEvents.onShowering -= Freeze;
        GameEventsManager.instance.detectEvents.onNotShowering -= Thaw;
        GameEventsManager.instance.detectEvents.onDialogue -= Freeze;
        GameEventsManager.instance.detectEvents.onNonDialogue -= Thaw;
        GameEventsManager.instance.detectEvents.onFinishShowering -= Thaw;
    }
    void Freeze()
    {
        GetComponent<PlayerMovement>().enabled = false;
    }

    void Thaw()
    {
        GetComponent<PlayerMovement>().enabled = true;
    }
}
