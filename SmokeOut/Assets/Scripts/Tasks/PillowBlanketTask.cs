using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PillowBlanketTask : TaskStep
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private GameObject _pillow;
    private GameObject _blanket;
    private bool foundItems = false;
    private bool isInRange = false;

    private void Start()
    {
        _pillow = GameObject.Find("Pillow");
        _blanket = GameObject.Find("Blanket");
    }

    void Update()
    {
        CheckTaskDone();
    }

    void CheckTaskDone()
    {
        if (_pillow == null && _blanket == null)
        {
            foundItems = true;
        }
        if (foundItems && isInRange && Input.GetKeyDown(interactKey))
        {
            EndTask();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    void EndTask()
    {
        //Do finish task method here
        Debug.Log("CompletedPillowBlanket!");
        TaskCompletionEvents("PillowTask");
    }
}
