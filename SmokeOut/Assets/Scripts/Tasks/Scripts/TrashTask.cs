using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTask : MonoBehaviour
{
    public static TrashTask Instance { get; private set; } // Singleton pattern to access it everywhere
    public int trashPickedUp;
    public int trashToPickUp;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        HashSet<GameObject> trash = new HashSet<GameObject>(GameObject.FindGameObjectsWithTag("TrashTask"));
        trashToPickUp = trash.Count;
        trash.Clear();
    }

    public void IncreaseTrashNumber()
    {
        if (trashPickedUp < trashToPickUp)
        {
            trashPickedUp++;
        }
    }

    private void Update()
    {
        if(trashPickedUp == trashToPickUp)
        {
            TaskManagerUI._taskManagerUI.MarkTaskAsComplete("Trash");
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
