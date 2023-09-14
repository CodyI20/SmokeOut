using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour
{
    public int Trash = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "trash")
        {
            Trash++;
            Debug.Log(Trash);
            Destroy(other.gameObject);
        }
    }

}
