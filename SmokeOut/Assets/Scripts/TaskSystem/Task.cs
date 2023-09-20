using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskInfoSO info;

    public TaskState state;

    private int currentTaskStepIndex;

    public Task(TaskInfoSO taskInfo)
    {
        this.info = taskInfo;
        this.state = TaskState.REQUIREMENTS_NOT_MET;
        this.currentTaskStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentTaskStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentTaskStepIndex < info.taskStepPrefabs.Length);
    }
    public void InstantiateCurrentTaskStep(Transform parentTransform)
    {
        GameObject taskStepPrefab = GetCurrentTaskStepPrefab();
        if (taskStepPrefab != null) 
        {
            TaskStep taskStep = Object.Instantiate<GameObject>(taskStepPrefab, parentTransform)
                .GetComponent<TaskStep>();
            taskStep.InitializeTaskStep(info.id);
        }

    }

    private GameObject GetCurrentTaskStepPrefab()
    {
        GameObject taskStepPrefab = null;
        if (CurrentStepExists())
        {
            taskStepPrefab = info.taskStepPrefabs[currentTaskStepIndex];

        }
        else
        {
            Debug.LogWarning("Tried to get task step prefab, but stepIndex was out of range indicating that" + "there's no current step:QuestId" + info.id + ",stepIndex=" + currentTaskStepIndex);

        }
        return taskStepPrefab;
    }
}
