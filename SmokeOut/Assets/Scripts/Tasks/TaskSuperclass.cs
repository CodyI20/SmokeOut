using UnityEngine;

public class TaskSuperclass : MonoBehaviour
{
    private HoverOutline _hoverOutline;
    private QuickOutline _outline;

    private void Awake()
    {
        _hoverOutline = GetComponent<HoverOutline>();
    }

    protected void DestroyOutlines()
    {
        _outline = GetComponent<QuickOutline>();
        if (_outline != null)
            Destroy(_outline);
        Destroy(_hoverOutline);
    }
}
