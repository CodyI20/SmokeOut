using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowerTask : TaskStep
{
    private KeyCode keyToPress;

    private MeshRenderer m_MeshRenderer;


    [SerializeField] private float timeToDoTask;

    [SerializeField] private GameObject _UIElement;
    private TextMeshProUGUI _UIElementText;

    [SerializeField] private Material _changedMaterial;
    [SerializeField] private Material _initialMaterial;
    private Material _currentMaterial;

    private GameObject _showerBigUI;
    private Slider showerSlider;
    private float maxSliderValue; // This will store the requiredChewDuration value.
    private TextMeshProUGUI text;

    [SerializeField] private float timeTillFailure = 0.5f;
    private bool isInRange = false;
    private bool startedTask = false;
    private float timeItStartedTask = 0f;
    private float timeItPressedKey = 0f;
    private float timePassed = 0f;
    private bool pressedKey = false;

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
        _showerBigUI.SetActive(false);
        keyToPress = (KeyCode)Random.Range(97, 123);
        text.text = $"Press the key ({keyToPress})";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyDown();
        UpdateMaterial();
        text.text = $"Press the key ({keyToPress})";
    }

    void CheckForFirstKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            timeItPressedKey = Time.timeSinceLevelLoad;
            if (!startedTask)
            {
                startedTask = true;
                _currentMaterial = _changedMaterial;
                timeItStartedTask = Time.timeSinceLevelLoad;
            }
            if (startedTask)
            {
                CheckForTaskComplete();
            }
            keyToPress = (KeyCode)UnityEngine.Random.Range(97, 123);
        }
    }

    void CheckForKeyDown()
    {
        if (isInRange)
        {
            if (startedTask)
            {
                timePassed += Time.deltaTime;
                CheckForTaskComplete();
            }
            if (Input.GetKeyDown(keyToPress))
            {
                timeItPressedKey = Time.timeSinceLevelLoad;
                Debug.Log($"timeItPressedKey: {timeItPressedKey}");
                if (!startedTask)
                {
                    if (_audioSource != null)
                        _audioSource.Play();
                    GameEventsManager.instance.detectEvents.Showering();
                    startedTask = true;
                    _currentMaterial = _changedMaterial;
                    timeItStartedTask = Time.timeSinceLevelLoad;
                }
                keyToPress = (KeyCode)Random.Range(97, 123);
            }
            if (startedTask && Time.timeSinceLevelLoad >= timeItPressedKey + timeTillFailure)
            {
                CancelShowering();
            }
            //foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            //{
            //    if (keyCode == keyToPress)
            //    {
            //        if (Input.GetKeyDown(keyCode))
            //        {
            //            timeItPressedKey = Time.timeSinceLevelLoad;
            //            if (!startedTask)
            //            {
            //                startedTask = true;
            //                _currentMaterial = _changedMaterial;
            //                timeItStartedTask = Time.timeSinceLevelLoad;
            //            }
            //            if (startedTask)
            //            {
            //                CheckForTaskComplete();
            //            }
            //            keyToPress = (KeyCode)UnityEngine.Random.Range(97, 123);
            //        }
            //    }
            //    else
            //        CancelShowering();
            //}
            showerSlider.value = timePassed;
        }
    }

    void CancelShowering()
    {
        Debug.Log("Cancelling shower...");
        timeItPressedKey = 0f;
        timeItStartedTask = 0f;
        startedTask = false;
        _currentMaterial = _initialMaterial;
        _showerBigUI.SetActive(false);
        showerSlider.value = 0f;
        timePassed = 0f;
        if (_audioSource != null)
            _audioSource.Stop();
        GameEventsManager.instance.detectEvents.NotShowering();
    }

    void CheckForTaskComplete()
    {
        if (Time.timeSinceLevelLoad >= timeItStartedTask + timeToDoTask)
        {
            _currentMaterial = _initialMaterial;
            _showerBigUI.SetActive(false);
            if(_audioSource != null) _audioSource.Stop();
            Debug.Log("CompletedShower!");
            GameEventsManager.instance.detectEvents.FinishShowering();
            TaskCompletionEvents("Showering");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInRange)
        {
            isInRange = true;
            _showerBigUI.SetActive(true);
            maxSliderValue = timeToDoTask; // Set the max value of the slider.
            showerSlider.maxValue = maxSliderValue; // Set the slider's max value.
            showerSlider.value = 0f; // Initialize the slider's value to 0.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInRange)
        {
            isInRange = false;
            CancelShowering();
        }
    }

    void UpdateMaterial()
    {
        m_MeshRenderer.material = _currentMaterial;
    }
}