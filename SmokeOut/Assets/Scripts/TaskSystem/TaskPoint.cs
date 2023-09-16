using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class TaskPoint : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private TaskInfoSO taskInfoForPoint;


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
    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onTaskStateChange -= TaskStateChange;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!playerIsNear)
            {
                return;
            }
            Debug.Log("You pressed the interact button");
            GameEventsManager.instance.taskEvents.StartTask(taskId);
            GameEventsManager.instance.taskEvents.AdvanceTask(taskId);
            GameEventsManager.instance.taskEvents.FinishTask(taskId);
        }
    }


        
    private void TaskStateChange(Task task)
    {
        if (task.info.id.Equals(taskId))
        {
            currentTaskState = task.state;
            Debug.Log("Quest with id:" + taskId + "updated to state" + currentTaskState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            Debug.Log("The player is in the zone!");
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
