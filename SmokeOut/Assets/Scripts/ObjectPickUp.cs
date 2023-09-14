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
        if (isInRange && isHovering && Input.GetMouseButtonDown(0)) // Change the button (0) to the desired mouse button.
        {
            // Perform the pickup action here.
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
        // Implement your item pickup logic here.
        // For example, you can disable the item's GameObject or add it to the player's inventory.
        // You might also want to play a sound effect or show a message to indicate the item has been picked up.
        gameObject.SetActive(false); // Disables the item.
        Destroy(gameObject,2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
