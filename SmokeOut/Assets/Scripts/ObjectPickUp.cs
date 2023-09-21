using UnityEngine;

public class ObjectPickUp : TaskStep
{
    private bool canPickUpItem = false;
    [SerializeField] private float timeTillItGetsDestroyed = 1f;

    private void Update()
    {
        if (GameManager._gameState == GameState.Paused)
            return;

        if(canPickUpItem && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    protected virtual void PickUpItem()
    {
        PlayerMovement.player.PlayInteractAnimation();
        TaskCompletionEvents("Pick up your phone", true);
        GameEventsManager.instance.inputEvents.PickUp();
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
