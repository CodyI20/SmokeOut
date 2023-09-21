using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance { get; private set; }
    //private HashSet<RequiredPickUp> _allRequiredPickUpsInScene;
    private int _scoreToWin;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _optionsUI;


    /// <summary>
    /// These variables are static so that they persist through scene changes. This is due to the fact that the GameManager is unique
    /// </summary>
    public static float timeToFinish = 420f;
    private static GameObject _overlayUI;
    public static GameState _gameState;
    ///
    public int ScoreToWin
    {
        get
        {
            return _scoreToWin;
        }
    }

    private void Awake()
    {
        _gameState = GameState.Resumed;
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
            PlayerHealth.OnPlayerDeath += PlayerHasDied;
            //PlayerHealth.playerHealth.OnPlayerDamageTaken += PlayerTookDamage;
        }
        else
        {
            Destroy(gameObject);
        }
        _overlayUI = GameObject.FindGameObjectWithTag("OverlayUI");
        if (_overlayUI)
        {
            _overlayUI.SetActive(false);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //_scoreToWin = CountAllRequiredPickUps();
        //PlayerHealth.playerHealth.OnPlayerDamageTaken += PlayerTookDamage;
    }

    //void PlayerDied()
    //{
    //    if (PlayerHealth.playerHealth.Lives > 0)
    //    {
    //        LevelLoader.levelLoaderInstance.ReloadCurrentScene();
    //    }
    //    else
    //    {
    //        PlayerData.ResetPlayerData();
    //        LevelLoader.levelLoaderInstance.LoadGameOver();
    //    }
    //}

    //int CountAllRequiredPickUps()
    //{
    //    _allRequiredPickUpsInScene = new HashSet<RequiredPickUp>(FindObjectsOfType<RequiredPickUp>());
    //    return _allRequiredPickUpsInScene.Count;
    //}

    public void ToggleOverlayUI()
    {
        if (_overlayUI != null)
        {
            _overlayUI.SetActive(!_overlayUI.activeSelf);
            if (_overlayUI.activeSelf)
            {
                PauseGame();
            }
            else
                ResumeGame();
        }
    }

    bool PlayerFinishedTasks()
    {
        return TaskManagerUI._taskManagerUI.hasFilledTaskList && TaskManagerUI._taskManagerUI.taskItems.Count == 0;
    }

    public void PlayerHasDied()
    {
        LevelLoader.levelLoaderInstance.LoadGameOver();
    }

    void ReloadScene()
    {
        LevelLoader.levelLoaderInstance.ReloadCurrentScene();
    }

    public void ToggleMenuOptionsUI()
    {
        if (_menuUI && _optionsUI)
        {
            _menuUI.SetActive(!_menuUI.activeSelf);
            _optionsUI.SetActive(!_optionsUI.activeSelf);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOverlayUI();
        }
        if (LevelLoader.levelLoaderInstance.GetCurrentSceneIndex() == 1 && PlayerFinishedTasks())
            ReloadScene();
    }

    public void PauseGame()
    {
        
        _gameState = GameState.Paused;
        AudioListener.pause = true;
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        
        _gameState = GameState.Resumed;
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        if (gameManagerInstance == this)
        {
            gameManagerInstance = null;
        }
        //PlayerHealth.OnPlayerDeath -= PlayerDied;//unsubscribe
        //if (PlayerHealth.playerHealth != null)
        //{
        //    Debug.Log("UnSubbing");
        //    PlayerHealth.playerHealth.OnPlayerDamageTaken -= PlayerTookDamage;
        //}
    }
}
