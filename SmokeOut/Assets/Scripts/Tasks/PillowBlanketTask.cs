using System.Collections.Generic;
using UnityEngine;

public class PillowBlanketTask : MonoBehaviour
{
    private GameObject _pillow;
    private GameObject _blanket;
    private List<GameObject> _itemsCollected;

    private void Start()
    {
        _pillow = GameObject.Find("Pillow");
        _blanket = GameObject.Find("Blanket");
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
            Destroy(gameObject);
            //TaskManagerUI._taskManagerUI.MarkTaskAsComplete("PillowTask");
        }
    }
}
