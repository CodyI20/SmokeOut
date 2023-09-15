using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggers : MonoBehaviour
{
    [SerializeField] private float interactionRange = 1f; //This variable will be used to detect whether or not the player is close enough to interact with the objects this script is attached to
    [SerializeField] private Dialogue _dialoguePlayed; //This variable uses the Dialogue class in order to play a specific dialogue needed for the interaction at hand
    private const KeyCode INTERACTION_KEY = KeyCode.T; //This variable will be used to determine which key needs to be pressed in order for the interaction to begin
    private GameObject _player; //This variable will be used to keep track of the player easier, using the singleton pattern created

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerMovement.player == null)
            return;
        else
            _player = PlayerMovement.player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerIsInRange() && Input.GetKeyDown(INTERACTION_KEY))
        {
            BeginDialogueInteraction();
        }
    }

    bool PlayerIsInRange()
    {
        return Vector3.Distance(_player.transform.position, transform.position) < interactionRange;
    }

    void BeginDialogueInteraction()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
