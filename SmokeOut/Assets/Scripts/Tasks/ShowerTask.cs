using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowerTask : TaskSuperclass
{
    [SerializeField] private KeyCode keyToPress = KeyCode.E;

    private MeshRenderer m_MeshRenderer;


    [SerializeField] private float timeToHoldKeyDown;

    [SerializeField] private GameObject _UIElement;

    [SerializeField] private Material _changedMaterial;
    [SerializeField] private Material _initialMaterial;
    private Material _currentMaterial;

    //private GameObject _showerBigUI;
    //private Slider showerSlider;
    //private float maxSliderValue; // This will store the requiredChewDuration value.
    //private TextMeshProUGUI text;

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
        //_showerBigUI = GameObject.FindGameObjectWithTag("ShowerUI");
        //showerSlider = _showerBigUI.GetComponentInChildren<Slider>();
        //text = _showerBigUI.GetComponentInChildren<TextMeshProUGUI>();
        //text.text = $"Hold the interact key ({keyToPress}) for {timeToHoldKeyDown} seconds";
        //_showerBigUI.SetActive(false);
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
        //if(showerSlider!= null)
        //{
        //    showerSlider.value = timeItHeldKey;
        //}
    }

    void CheckForTaskComplete()
    {
        if (timeItHeldKey >= timeToHoldKeyDown)
        {
            _currentMaterial = _initialMaterial;
            TaskCompletionEvents();
            //TaskManagerUI._taskManagerUI.MarkTaskAsComplete("Shower");
            _UIElement.SetActive(false);
            //_showerBigUI.SetActive(false);
            Debug.Log("CompletedShower!");
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UIElement.SetActive(true);
            //_showerBigUI.SetActive(true);
            //maxSliderValue = timeToHoldKeyDown; // Set the max value of the slider.
            //showerSlider.maxValue = maxSliderValue; // Set the slider's max value.
            //showerSlider.value = 0f; // Initialize the slider's value to 0.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentMaterial = _initialMaterial;
            _UIElement.SetActive(false);
            //_showerBigUI.SetActive(false);
        }
    }

    void UpdateMaterial()
    {
        m_MeshRenderer.material = _currentMaterial;
    }
}
