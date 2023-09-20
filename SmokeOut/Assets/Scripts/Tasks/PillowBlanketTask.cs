using System.Collections.Generic;
using UnityEngine;

public class PillowBlanketTask : MonoBehaviour
{
    private GameObject _pillow;
    private GameObject _blanket;

    private void Start()
    {
        _pillow = GameObject.Find("Pillow");
        _blanket = GameObject.Find("Blanket");
    }

    void Update()
    {
        CheckTaskDone();
    }

    void CheckTaskDone()
    {
        if (_pillow == null && _blanket == null)
        {
            Debug.Log("Pillow&BlanketComplete!");
            Destroy(gameObject);
            //TaskManagerUI._taskManagerUI.MarkTaskAsComplete("PillowTask");
        }
    }
}
