using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ShowerTask : MonoBehaviour
{
    private const KeyCode keyToPress = KeyCode.Q;


    [SerializeField] private float timeToHoldKeyDown;

    private bool canInteract = false;
    private float timeItHeldKey = 0f;
    private bool heldKeyFirstTime = false;
    private float timeItStartedHoldingKey = 0f;

    private HoverOutline _hoverOutline;
    private QuickOutline _outline;

    private void Awake()
    {
        _hoverOutline = GetComponent<HoverOutline>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
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
        }
        else
        {
            CheckForTaskComplete();
            heldKeyFirstTime = false;
            timeItHeldKey = 0f;
            timeItStartedHoldingKey = 0f;
        }
    }

    void CheckForTaskComplete()
    {
        if (timeItHeldKey >= timeToHoldKeyDown)
        {
            _outline = GetComponent<QuickOutline>();
            if (_outline != null)
                Destroy(_outline);
            Destroy(_hoverOutline);
            //TaskManagerUI._taskManagerUI.MarkTaskAsComplete("Shower");
            Debug.Log("CompletedShower!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
