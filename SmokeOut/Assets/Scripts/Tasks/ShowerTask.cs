using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ShowerTask : TaskSuperclass
{
    private const KeyCode keyToPress = KeyCode.E;


    [SerializeField] private float timeToHoldKeyDown;

    [SerializeField] private GameObject _UIElement;

    private bool canInteract = false;
    private float timeItHeldKey = 0f;
    private bool heldKeyFirstTime = false;
    private float timeItStartedHoldingKey = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyDown();
    }

    void CheckForKeyDown()
    {
        if (Input.GetKey(keyToPress))
        {
            if (!heldKeyFirstTime)
            {
                timeItStartedHoldingKey = Time.timeSinceLevelLoad;
                heldKeyFirstTime = true;
            }
            if (heldKeyFirstTime && Time.timeSinceLevelLoad - timeItStartedHoldingKey >= 1f)
            {
                timeItHeldKey++;
                timeItStartedHoldingKey = Time.timeSinceLevelLoad;
            }
            CheckForTaskComplete();
        }
        else
        {
            heldKeyFirstTime = false;
            timeItHeldKey = 0f;
            timeItStartedHoldingKey = 0f;
        }
    }

    void CheckForTaskComplete()
    {
        if (timeItHeldKey >= timeToHoldKeyDown)
        {
            DestroyOutlines();
            //TaskManagerUI._taskManagerUI.MarkTaskAsComplete("Shower");
            _UIElement.SetActive(false);
            Debug.Log("CompletedShower!");
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            _UIElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            _UIElement.SetActive(false);
        }
    }
}
