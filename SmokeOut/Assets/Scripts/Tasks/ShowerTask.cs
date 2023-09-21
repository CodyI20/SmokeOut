using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowerTask : TaskStep
{
    private KeyCode keyToPress;

    private MeshRenderer m_MeshRenderer;


    [SerializeField] private float timeToHoldKeyDown;

    [SerializeField] private GameObject _UIElement;
    private TextMeshProUGUI _UIElementText;

    [SerializeField] private Material _changedMaterial;
    [SerializeField] private Material _initialMaterial;
    private Material _currentMaterial;

    private GameObject _showerBigUI;
    private Slider showerSlider;
    private float maxSliderValue; // This will store the requiredChewDuration value.
    private TextMeshProUGUI text;

    private float timeItHeldKey = 0f;
    private bool isInRange = false;
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
        _showerBigUI = GameObject.FindGameObjectWithTag("ShowerUI");
        showerSlider = _showerBigUI.GetComponentInChildren<Slider>();
        text = _showerBigUI.GetComponentInChildren<TextMeshProUGUI>();
        _UIElementText = _UIElement.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Hold the interact key ({keyToPress}) for {timeToHoldKeyDown} seconds";
        _UIElementText.text = $"Hold ({keyToPress})";
        _showerBigUI.SetActive(false);
        keyToPress = (KeyCode)Random.Range(97, 123);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyDown();
        UpdateMaterial();
        text.text = $"Hold the interact key ({keyToPress}) for {timeToHoldKeyDown} seconds";
    }

    void CheckForKeyDown()
    {
        if (isInRange)
        {
            if (Input.GetKey(keyToPress))
            {
                _currentMaterial = _changedMaterial;
                if (!heldKeyFirstTime)
                {
                    heldKeyFirstTime = true;
                    timeItStartedHoldingKey = Time.timeSinceLevelLoad;
                }
                if (Time.timeSinceLevelLoad - timeItStartedHoldingKey >= 1f)
                {
                    timeItHeldKey++;
                    keyToPress = (KeyCode)Random.Range(97, 123);
                    timeItStartedHoldingKey = Time.timeSinceLevelLoad;
                }
                CheckForTaskComplete();
            }
            if(heldKeyFirstTime)
            {
                PlayerMovement.player._playerSpeed = 0f;
            }
        }
        else
        {
            heldKeyFirstTime = false;
            timeItStartedHoldingKey = 0f;
            timeItHeldKey = 0f;
        }
        if (showerSlider != null)
        {
            showerSlider.value = timeItHeldKey;
        }
    }

    void CheckForTaskComplete()
    {
        if (timeItHeldKey >= timeToHoldKeyDown)
        {
            _currentMaterial = _initialMaterial;
            _UIElement.SetActive(false);
            _showerBigUI.SetActive(false);
            PlayerMovement.player._playerSpeed = PlayerMovement.player.initialPlayerSpeed;
            Debug.Log("CompletedShower!");
            GameEventsManager.instance.detectEvents.FinishShowering();
            TaskCompletionEvents("Shower");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            _UIElement.SetActive(true);
            _showerBigUI.SetActive(true);
            maxSliderValue = timeToHoldKeyDown; // Set the max value of the slider.
            showerSlider.maxValue = maxSliderValue; // Set the slider's max value.
            showerSlider.value = 0f; // Initialize the slider's value to 0.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            _currentMaterial = _initialMaterial;
            _UIElement.SetActive(false);
            _showerBigUI.SetActive(false);
        }
    }

    void UpdateMaterial()
    {
        m_MeshRenderer.material = _currentMaterial;
    }
}