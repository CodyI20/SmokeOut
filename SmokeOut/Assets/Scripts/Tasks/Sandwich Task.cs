using UnityEngine;

public class SandwichTask : TaskSuperclass
{
    private GameObject bread;
    private GameObject cheese;
    private GameObject lettuce;


    private bool canInteractWithStove = false;

    private bool breadCollected = false;
    private bool cheeseCollected = false;
    private bool lettuceCollected = false;
    private bool collectedAllItems = false;

    // Start is called before the first frame update
    void Start()
    {
        bread = GameObject.Find("Bread");
        cheese = GameObject.Find("Cheese");
        lettuce = GameObject.Find("Lettuce");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForItems();
        if(canInteractWithStove && Input.GetKeyDown(KeyCode.E))
        {
            TaskCompletionEvents();
            //TaskManagerUI._taskManagerUI.MarkTaskAsComplete("Sandwich");
            Debug.Log("Sandwich made!");
            Destroy(this);
        }
    }

    void CheckForItems()
    {
        if (bread == null)
        {
            breadCollected = true;
        }
        else if (cheese == null)
        {
            cheeseCollected = true;
        }
        else if (lettuce == null)
            lettuceCollected = true;

        if (breadCollected && cheeseCollected && lettuceCollected)
            collectedAllItems = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collectedAllItems && other.CompareTag("Player"))
        {
            canInteractWithStove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteractWithStove = false;
        }
    }


}
