using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ChewingGumInteraction : MonoBehaviour
{
    public static ChewingGumInteraction Instance { get; private set; }
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float requiredChewDuration = 5f;
    [Range(0.5f, 2f)]
    [SerializeField] private float failureDuration = 1f; // Time until task failure if key isn't pressed
    [SerializeField] private GameObject _gumUI;

    private GameObject _gumBigUI;
    private Slider chewSlider;
    private float maxSliderValue; // This will store the requiredChewDuration value.
    private TextMeshProUGUI text;


    private bool isChewing = false;
    private bool hasPressedKey = false;
    private float chewTimer = 0f;
    private float timeItPressed = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _gumBigUI = GameObject.FindGameObjectWithTag("GumUI");
        chewSlider = _gumBigUI.GetComponentInChildren<Slider>();
        text = _gumBigUI.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Press the interact key ({interactionKey}) repeatedly for {requiredChewDuration} seconds";
        _gumBigUI.SetActive(false);
    }

    private void Update()
    {
        if (isChewing && GameManager._gameState != GameState.Paused)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                timeItPressed = Time.timeSinceLevelLoad;
                hasPressedKey = true;

                if (chewTimer >= requiredChewDuration)
                {
                    ChewingComplete();
                }
            }
            if (hasPressedKey)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (keyCode != interactionKey && Input.GetKeyDown(keyCode))
                    {
                        ChewingInterrupted();
                    }
                }
            }
            if (hasPressedKey && Time.timeSinceLevelLoad < timeItPressed + failureDuration)
            {
                chewTimer += Time.deltaTime;
                chewSlider.value = chewTimer; // Update the slider value.
            }
            else if (hasPressedKey && Time.timeSinceLevelLoad >= timeItPressed + failureDuration)
            {
                ChewingInterrupted();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isChewing)
        {
            isChewing = true;
            _gumUI.SetActive(true);
            _gumBigUI.SetActive(true);
            maxSliderValue = requiredChewDuration; // Set the max value of the slider.
            chewSlider.maxValue = maxSliderValue; // Set the slider's max value.
            chewSlider.value = 0f; // Initialize the slider's value to 0.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isChewing)
        {
            ChewingInterrupted();
        }
    }

    private void ChewingComplete()
    {
        // Handle the successful completion of chewing, e.g., progress in the game.
        Debug.Log("Chewing Complete!");
        //TaskCompletionEvents();
        isChewing = false;
        chewTimer = 0f;
        _gumBigUI.SetActive(false);
        Destroy(gameObject);
        GameEventsManager.instance.detectEvents.FinishChewing(); 

    }

    private void ChewingInterrupted()
    {
        // Handle interruptions, e.g., reset the process and provide feedback.
        Debug.Log("Chewing Interrupted!");
        // Hide the UI and reset the chewing process.
        _gumUI.SetActive(false);
        _gumBigUI.SetActive(false);
        isChewing = false;
        hasPressedKey = false;
        chewTimer = 0f;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
