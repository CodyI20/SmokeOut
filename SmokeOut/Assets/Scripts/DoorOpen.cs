using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private bool isOpen = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isOpen && other.CompareTag("Player"))
        {
            _animator.SetTrigger("OpenDoor");
        }
    }
}
