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
        GameEventsManager.instance.inputEvents.onTriggerInteract += TriggerDetect;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange -= TaskStateChange;
        GameEventsManager.instance.inputEvents.onTriggerInteract -= TriggerDetect;
    }

    private void TriggerDetect()
    {
        if (!playerIsNear)
        {
            return;
        }

        if (currentTaskState.Equals(TaskState.CAN_START) && startPoint)
        {
            Debug.Log("Starting task:" + taskId);
            GameEventsManager.instance.taskEvents.StartTask(taskId);
        }
        else if (currentTaskState.Equals(TaskState.CAN_FINISH) && finishPoint)
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

    private void Update()
    {
        if (currentTaskState.Equals(TaskState.CAN_FINISH) )
        {
            GameEventsManager.instance.taskEvents.FinishTask(taskId);
        }
    }
}
