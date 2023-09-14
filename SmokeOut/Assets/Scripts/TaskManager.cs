using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private Transform taskListParent;
    [SerializeField] private GameObject taskItemPrefab;
    [SerializeField] private List<TaskData> predefinedTasks;

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
        GameObject taskItem = Instantiate(taskItemPrefab, taskListParent);
        TextMeshProUGUI textComponent = taskItem.GetComponentInChildren<TextMeshProUGUI>();
        Button completeButton = taskItem.GetComponentInChildren<Button>();

        textComponent.text = taskData.taskName;

        // Attach a function to the complete button to toggle task completion
        completeButton.onClick.AddListener(() => ToggleTaskComplete(taskData, textComponent));

        // Update text appearance based on the task's completion state
        if (taskData.isComplete)
        {
            SetTextStrikethrough(textComponent);
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
        textComponent.fontStyle |= FontStyles.Strikethrough;
    }

    void RemoveTextStrikethrough(TextMeshProUGUI textComponent)
    {
        textComponent.fontStyle &= ~FontStyles.Strikethrough;
    }
}
