using UnityEngine;

public class ObjectPickUp : TaskStep
{
    protected override void PickUpItem()
    {
        base.PickUpItem();
        PlayerMovement.player.PlayInteractAnimation();
        GameEventsManager.instance.inputEvents.PickUp();
    }
}
