using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManagerUI : MonoBehaviour
{
    [SerializeField] private Transform taskListParent;
    [SerializeField] private GameObject taskItemPrefab;
    [SerializeField] private List<TaskData> tasks;
    private HashSet<GameObject> taskItems = new HashSet<GameObject>();
    private GameObject taskItemToBeRemoved = null;

    [SerializeField] private AudioSource _taskDoneAudio;

    public static TaskManagerUI _taskManagerUI { get; private set; }

    private void Awake()
    {
        if (_taskManagerUI == null)
            _taskManagerUI = this;
    }

    private void Start()
    {
        foreach(TaskData item in tasks)
        {
            CreateTaskItem(item.taskName);
        }
    }

    public void CreateTaskItem(string id)
    {
        GameObject taskItem = Instantiate(taskItemPrefab, taskListParent);
        TaskIdentifier taskIdentifier = taskItem.GetComponent<TaskIdentifier>();
        taskIdentifier.identifier = id;
        taskItems.Add(taskItem);

        TextMeshProUGUI textComponent = taskItem.GetComponentInChildren<TextMeshProUGUI>();

        textComponent.text = id;
    }

    public void MarkTaskAsComplete(string taskName)
    {
        foreach (GameObject task in taskItems)
        {
            if (task.GetComponent<TaskIdentifier>().identifier == taskName)
            {
                SetTextStrikethrough(task.GetComponentInChildren<TextMeshProUGUI>());
                taskItemToBeRemoved = task;
                break;
            }
        }
        if (taskItemToBeRemoved != null)
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
        if (_taskDoneAudio != null)
            _taskDoneAudio.Play();
        textComponent.fontStyle |= FontStyles.Strikethrough;
    }

    void RemoveTextStrikethrough(TextMeshProUGUI textComponent)
    {
        textComponent.fontStyle &= ~FontStyles.Strikethrough;
    }

    private void OnDestroy()
    {
        _taskManagerUI = null;
    }
}
