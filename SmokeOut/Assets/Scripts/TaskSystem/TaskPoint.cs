using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class TaskPoint : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private TaskInfoSO taskInfoForPoint;

    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = false;



    private bool playerIsNear = false;
    private string taskId;
    private TaskState currentTaskState;
    private void Awake()
    {
        taskId = taskInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange += TaskStateChange;
        GameEventsManager.instance.inputEvents.onSubmitClicked += SubmitClicked;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange -= TaskStateChange;
        GameEventsManager.instance.inputEvents.onSubmitClicked -= SubmitClicked;
    }

    private void SubmitClicked()
    {
        if (!playerIsNear)
        {
            return;
        }

        if (startPoint)
        {
            Debug.Log("Starting task:" + taskId);
            GameEventsManager.instance.taskEvents.StartTask(taskId);
        }
        else if (finishPoint)
        {
            GameEventsManager.instance.taskEvents.FinishTask(taskId);
        }
    }
         
    private void TaskStateChange(Task task)
    {
        if (task.info.id.Equals(taskId))
        {
            currentTaskState = task.state;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            ///Debug.Log("The player is in the zone!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
