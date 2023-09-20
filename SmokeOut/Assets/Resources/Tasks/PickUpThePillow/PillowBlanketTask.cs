
using System.Collections.Generic;
using UnityEngine;

public class PillowBlanketTaskStep : MonoBehaviour
{
    [SerializeField] private GameObject _pillow;
    [SerializeField] private GameObject _blanket;
    private List<GameObject> _itemsCollected;

    private void Awake()
    {
        _itemsCollected = new List<GameObject> { _blanket, _pillow };
    }

    void Update()
    {
        CheckTaskDone();
    }

    void CheckTaskDone()
    {
        foreach(GameObject item in _itemsCollected.ToArray())
        {
            if (item.activeSelf == false)
                _itemsCollected.Remove(item);
        }
        if (_itemsCollected.Count == 0)
        {
            Debug.Log("TaskComplete!");
            TaskManagerUI._taskManagerUI.MarkTaskAsComplete("PillowTask");
        }
    }
}
