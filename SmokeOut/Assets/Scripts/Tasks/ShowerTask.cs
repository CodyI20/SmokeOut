using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ShowerTask : TaskSuperclass
{
    private const KeyCode keyToPress = KeyCode.E;

    private MeshRenderer m_MeshRenderer;


    [SerializeField] private float timeToHoldKeyDown;

    [SerializeField] private GameObject _UIElement;

    [SerializeField] private Material _changedMaterial;
    [SerializeField] private Material _initialMaterial;
    private Material _currentMaterial;

    private bool canInteract = false;
    private float timeItHeldKey = 0f;
    private bool heldKeyFirstTime = false;
    private float timeItStartedHoldingKey = 0f;

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        _currentMaterial = _initialMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyDown();
        UpdateMaterial();
    }

    void CheckForKeyDown()
    {
        if (Input.GetKey(keyToPress))
        {
            _currentMaterial = _changedMaterial;
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
            _currentMaterial = _initialMaterial;
            heldKeyFirstTime = false;
            timeItHeldKey = 0f;
            timeItStartedHoldingKey = 0f;
        }
    }

    void CheckForTaskComplete()
    {
        if (timeItHeldKey >= timeToHoldKeyDown)
        {
            _currentMaterial = _initialMaterial;
            TaskCompletionEvents();
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
            _currentMaterial = _initialMaterial;
            _UIElement.SetActive(false);
        }
    }

    void UpdateMaterial()
    {
        m_MeshRenderer.material = _currentMaterial;
    }
}
