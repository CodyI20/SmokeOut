using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance { get; private set; }
    //private HashSet<RequiredPickUp> _allRequiredPickUpsInScene;
    private int _scoreToWin;
    /// private DialogueManager _dialogueManager;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _optionsUI;
    private bool dialogueWasOpen = false;


    /// <summary>
    /// These variables are static so that they persist through scene changes. This is due to the fact that the GameManager is unique
    /// </summary>
    private static GameObject _dialogueUI;
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
            //PlayerHealth.OnPlayerDeath += PlayerDied;
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
        _dialogueUI = GameObject.FindGameObjectWithTag("DialogueUI");
        if (_dialogueUI)
        {
            _dialogueUI.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //_dialogueManager = DialogueManager._dialogueManager;
        //_scoreToWin = CountAllRequiredPickUps();
        //PlayerHealth.playerHealth.OnPlayerDamageTaken += PlayerTookDamage;
    }

    void PlayerTookDamage()
    {
        LevelLoader.levelLoaderInstance.ReloadCurrentScene();
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
    }

    void PauseGame()
    {
        if (_dialogueUI != null && _dialogueUI.activeSelf)
        {
            _dialogueUI.SetActive(false);
            dialogueWasOpen = true;
        }
        _gameState = GameState.Paused;
        AudioListener.pause = true;
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        if(_dialogueUI != null && dialogueWasOpen)
        {
            _dialogueUI.SetActive(true);
            dialogueWasOpen = false;
        }
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
