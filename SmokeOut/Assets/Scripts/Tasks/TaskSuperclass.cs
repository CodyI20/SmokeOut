using UnityEngine;

public class TaskSuperclass : MonoBehaviour
{
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
}
