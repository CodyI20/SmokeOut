using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    private string taskId;

    private HoverOutline _hoverOutline;
    private QuickOutline _outline;
    [SerializeField] protected AudioSource _audioSource;

    private void Awake()
    {
        _hoverOutline = GetComponent<HoverOutline>();
    }

    protected void TaskCompletionEvents()
    {
        _outline = GetComponent<QuickOutline>();
        if (_outline != null)
            Destroy(_outline);
        Destroy(_hoverOutline);
        PlayAudioSource();
    }

    private void PlayAudioSource()
    {
        if (_audioSource != null)
            _audioSource?.Play();
    }



    public void InitializeTaskStep(string taskId)
    {
        this.taskId = taskId;
    }

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventsManager.instance.taskEvents.AdvanceTask(taskId);

            Destroy(this.gameObject);
        }

    }

    /// Object pick up

}
