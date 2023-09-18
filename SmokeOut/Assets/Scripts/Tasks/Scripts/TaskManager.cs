using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private Transform taskListParent;
    [SerializeField] private GameObject taskItemPrefab;
    [SerializeField] private List<TaskData> predefinedTasks;
    private HashSet<GameObject> taskItems = new HashSet<GameObject>();
    private GameObject taskItemToBeRemoved = null;

    [SerializeField] private AudioSource _taskDoneAudio;

    public static TaskManager _taskManager { get; private set; }

    private void Awake()
    {
        if (_taskManager == null)
            _taskManager = this;
    }

    private void Start()
    {
        InitializeTasks();
    }

    void InitializeTasks()
    {
        foreach (TaskData taskData in predefinedTasks)
        {
            CreateTaskItem(taskData);
        }
    }

    void CreateTaskItem(TaskData taskData)
    {
        taskData.isComplete = false;
        GameObject taskItem = Instantiate(taskItemPrefab, taskListParent);
        TaskIdentifier taskIdentifier = taskItem.GetComponent<TaskIdentifier>();
        taskIdentifier.identifier = taskData.taskName;
        taskItems.Add(taskItem);

        TextMeshProUGUI textComponent = taskItem.GetComponentInChildren<TextMeshProUGUI>();

        textComponent.text = taskData.taskDescription;

        // Attach a function to the complete button to toggle task completion
        //completeButton.onClick.AddListener(() => ToggleTaskComplete(taskData, textComponent));

        // Update text appearance based on the task's completion state
        if (taskData.isComplete)
        {
            SetTextStrikethrough(textComponent);
        }
    }

    public void MarkTaskAsComplete(string taskName)
    {
        foreach(GameObject task in taskItems)
        {
            if(task.GetComponent<TaskIdentifier>().identifier == taskName)
            {
                SetTextStrikethrough(task.GetComponentInChildren<TextMeshProUGUI>());
                taskItemToBeRemoved = task;
                break;
            }
        }
        if(taskItemToBeRemoved != null)
        {
            taskItems.Remove(taskItemToBeRemoved);
            taskItemToBeRemoved = null;
        }
    }

    void ToggleTaskComplete(TaskData taskData, TextMeshProUGUI textComponent)
    {
        taskData.isComplete = !taskData.isComplete;

        if (taskData.isComplete)
        {
            SetTextStrikethrough(textComponent);
        }
        else
        {
            RemoveTextStrikethrough(textComponent);
        }
    }

    void SetTextStrikethrough(TextMeshProUGUI textComponent)
    {
        _taskDoneAudio.Play();
        textComponent.fontStyle |= FontStyles.Strikethrough;
    }

    void RemoveTextStrikethrough(TextMeshProUGUI textComponent)
    {
        textComponent.fontStyle &= ~FontStyles.Strikethrough;
    }

    private void OnDestroy()
    {
        _taskManager = null;
    }
}
