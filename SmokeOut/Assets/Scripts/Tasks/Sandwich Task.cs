using UnityEngine;

public class SandwichTask : TaskStep
{
    private GameObject bread;
    private GameObject cheese;
    private GameObject lettuce;


    private bool canInteractWithStove = false;
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
            Debug.Log("Sandwich made!");
            GameEventsManager.instance.detectEvents.FinishSandwich();
            TaskCompletionEvents("MakeTheSandwich");
        }
    }

    void CheckForItems()
    {
        if (bread == null && lettuce == null && cheese == null)
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
