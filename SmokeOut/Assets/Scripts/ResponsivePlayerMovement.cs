using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;
    private Rigidbody rb;

    //Creating a player singleton for easy access to the player all the time ( Since there will only be one player )
    public static PlayerMovement player { get; private set; }

    void Awake()
    {
        if (player == null) //Setting the player to this game object if it's not already assigned ( Doing this in Awake ensures that any other object will be able to find the player. )
            player = this;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        /// Dialogue system implement. Freezing the player when the dialouge is opened
        

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // Rotate the player to face the direction of movement.
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Apply movement force.
        Vector3 movement = moveDirection * playerSpeed * Time.deltaTime;
        rb.velocity = new Vector3(movement.x, 0, movement.z);

        
    }

    void OnDestroy()
    {
        player = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameEventsManager.instance.inputEvents.TriggerInteract();
        }
    }

  
}

