using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    private Dictionary<string, Task> taskMap;

    private int currentPlayerLevel = 1;

    private void Awake()
    {
        taskMap = CreateTaskMap();
    }
    private void OnEnable()
    {
        Debug.Log("Checking if onEnable is activated before initialisation code");
        GameEventsManager.instance.taskEvents.onStartTask += StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceTask;
        GameEventsManager.instance.taskEvents.onFinishTask += FinishTask;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.taskEvents.onStartTask -= StartTask;
        GameEventsManager.instance.taskEvents.onAdvanceTask -= AdvanceTask;
        GameEventsManager.instance.taskEvents.onFinishTask -= FinishTask;

    }

    private void Start()
    {
        foreach (Task task in taskMap.Values)
        {
            GameEventsManager.instance.taskEvents.TaskStateChange(task);
        }
    }

    private void ChangeTaskState(string id, TaskState state)
    {
        //Debug.Log("Changing the task state");
        Task task = GetTaskById(id);
        task.state = state;
        GameEventsManager.instance.taskEvents.TaskStateChange(task);
    }



    private bool CheckRequirementsMet(Task task)
    {
        // start true and prove to be false
        bool meetsRequirements = true;

        // check player level requirements
        if (currentPlayerLevel < task.info.levelRequirement)
        {
            meetsRequirements = false;
        }

        // check task prerequisites for completion
        foreach (TaskInfoSO prerequisiteTaskInfo in task.info.taskPrerequisites)
        {
            if (GetTaskById(prerequisiteTaskInfo.id).state != TaskState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        // loop through ALL tasks
        foreach (Task task in taskMap.Values)
        {
            // if the player is now meeting the requirements, switch over to the CAN_START state
            if (task.state == TaskState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(task))
            {
                ChangeTaskState(task.info.id, TaskState.CAN_START);
            }
        }
    }


    private void StartTask(string id)
    {
        Debug.Log("Started the task");
        Task task = GetTaskById(id);
        task.InstantiateCurrentTaskStep(this.transform);
        ChangeTaskState(task.info.id, TaskState.IN_PROGRESS);
        TaskManagerUI._taskManagerUI.CreateTaskItem(id, task.info.displayName);
    }

    private void AdvanceTask(string id)
    {
        Task task = GetTaskById(id);

        // move on to the next step
        task.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (task.CurrentStepExists())
        {
            Debug.Log("Moving on to the next step");
            task.InstantiateCurrentTaskStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for this task
        else
        {
            Debug.Log("You have finished all the steps");
            ChangeTaskState(task.info.id, TaskState.CAN_FINISH);
        }
    }

    private void FinishTask(string id)
    {
        Debug.Log("You have finished the task");
        Task task = GetTaskById(id);
        ChangeTaskState(task.info.id, TaskState.FINISHED);
        TaskManagerUI._taskManagerUI.MarkTaskAsComplete(id);
    }

    private Dictionary<string, Task> CreateTaskMap()
    {
        //Load all TaskInfoSO Scriptable Objects under the Assets/Resources/Tasks folder
        TaskInfoSO[] allTasks = Resources.LoadAll<TaskInfoSO>("Tasks");

        //Create a task map
        Dictionary<string, Task> idToTaskMap = new Dictionary<string, Task>();
        foreach (TaskInfoSO taskInfo in allTasks)
        {
            if (idToTaskMap.ContainsKey(taskInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating task map: " + taskInfo.id);
            }
            idToTaskMap.Add(taskInfo.id, new Task(taskInfo));

        }
        return idToTaskMap;
    }

    private Task GetTaskById(string id)
    {
        Task task = taskMap[id];
        if (task == null)
        {
            Debug.LogError("ID not found in the Task Map: " + id);
        }
        return task;
    }

}
