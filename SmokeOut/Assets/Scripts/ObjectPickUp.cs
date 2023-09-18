using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    [SerializeField] private float pickupRange = 3f; // The range within which the player can pick up the item.

    private bool isInRange = false; // To track if the player is in range of the item.
    private bool isHovering = false; // To track if the mouse cursor is hovering over the item.


    private void Update()
    {
        if (GameManager._gameState == GameState.Paused)
            return;
        isInRange = Vector3.Distance(PlayerMovement.player.transform.position, transform.position) < pickupRange;

        // Check if the player is in range and the mouse cursor is hovering over the item.
        if (isInRange && isHovering && Input.GetMouseButtonDown(0))
        {
            PickUpItem();
        }
    }

    private void OnMouseEnter()
    {
        // Called when the mouse cursor enters the item's collider.
        isHovering = true;
    }

    private void OnMouseExit()
    {
        // Called when the mouse cursor exits the item's collider.
        isHovering = false;
    }

    private void PickUpItem()
    {
        gameObject.SetActive(false); // Disables the item.
        Destroy(gameObject, 2f);
        Debug.Log("You picked up a trash");
        GameEventsManager.instance.inputEvents.PickUp();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
