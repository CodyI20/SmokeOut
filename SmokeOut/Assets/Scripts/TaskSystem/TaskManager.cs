using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    private Dictionary<string, Task> taskMap;

    private void Awake()
    {
        taskMap = CreateTaskMap();

        Task task = GetTaskById("CleanTheRoomTask");
        Debug.Log(task.info.displayName);
        Debug.Log(task.info.levelRequirement);
        Debug.Log(task.state);
        Debug.Log(task.CurrentStepExists());


    }

    private Dictionary<string,Task> CreateTaskMap()
    {
        //Load all QuestInfoSO Scriptable Objects under the Assets/Resources/Tasks folder
        TaskInfoSO[] allTasks = Resources.LoadAll<TaskInfoSO>("Tasks");
        
        //Create a task map
        Dictionary<string, Task> idToTaskMap = new Dictionary<string, Task>();
        foreach (TaskInfoSO taskInfo in allTasks)
        {
            if (idToTaskMap.ContainsKey(taskInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating task map:" + taskInfo.id);
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
                Debug.LogError("ID not found in the Task Map" + id);

            }
            return task;
        }
     
}
