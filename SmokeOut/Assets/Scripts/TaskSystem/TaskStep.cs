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

    protected void TaskCompletionEvents(string id = null, bool destoryGameObject = false)
    {
        _outline = GetComponent<QuickOutline>();
        if (_outline != null)
            Destroy(_outline);
        Destroy(_hoverOutline);
        if (id != null)
        {
            TaskManagerUI._taskManagerUI.MarkTaskAsComplete(id);
        }
        PlayAudioSource();
        if (destoryGameObject)
        {
            if(_audioSource != null)
            {
                Destroy(gameObject, _audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(_audioSource != null)
            {
                Destroy(this, _audioSource.clip.length);
            }
            else
            {
                Destroy(this);
            }
        }
    }

    private void PlayAudioSource()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
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

    private bool canPickUpItem = false;
    [SerializeField] private float timeTillItGetsDestroyed = 1f;

    private void Update()
    {
        if (GameManager._gameState == GameState.Paused)
            return;

        if (canPickUpItem && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    protected virtual void PickUpItem()
    {
        PlayerMovement.player.PlayInteractAnimation();
        Destroy(gameObject, timeTillItGetsDestroyed);
        //GameEventsManager.instance.inputEvents.PickUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUpItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUpItem = false;
        }
    }
}
