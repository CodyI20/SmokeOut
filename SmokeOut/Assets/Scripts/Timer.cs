using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer timerInstance {  get; private set; }

    private float totalTime;
    private TextMeshProUGUI timerText;
    private float currentTime;
    private bool isRunning = true;

    private void Awake()
    {
        if(timerInstance == null)
        {
            timerInstance = this;
        }
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        totalTime = GameManager.timeToFinish;
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (GameManager._gameState != GameState.Paused && isRunning)
        {
            // Update the timer
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                EndOfTimer();
            }
            UpdateTimerDisplay();
        }
    }

    void EndOfTimer()
    {
        // Timer has reached zero
        currentTime = 0;
        isRunning = false;
        totalTime -= 60f;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        // Format the time as "minutes:seconds"
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update the UI text element
        timerText.text = timerString;
    }

    private void OnDestroy()
    {
        timerInstance = null;
    }
}
