using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    private bool canPickUpItem = false;

    private void Update()
    {
        if (GameManager._gameState == GameState.Paused)
            return;

        if(canPickUpItem && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    private void PickUpItem()
    {
        TrashTask.Instance.IncreaseTrashNumber();
        gameObject.SetActive(false); // Disables the item.
        Destroy(gameObject,2f);
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
