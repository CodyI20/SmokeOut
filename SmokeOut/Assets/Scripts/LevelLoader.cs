using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector] public static LevelLoader levelLoaderInstance { get; private set; }

    //Constant values for indices
    private const int MENU_SCENE_INDEX = 0;
    private const int FIRST_SCENE_INDEX = 1;
    private const int VICTORY_SCENE_INDEX = 2;
    private const int GAMEOVER_SCENE_INDEX = 3;

    private void Awake()
    {
        if (levelLoaderInstance == null)
            levelLoaderInstance = this;
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE_INDEX);
    }
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(FIRST_SCENE_INDEX);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(GAMEOVER_SCENE_INDEX);
    }

    public void LoadVictoryScreen()
    {
        SceneManager.LoadScene(VICTORY_SCENE_INDEX);
    }

    public void LoadScene(string scenePath)
    {
        SceneManager.LoadScene(scenePath);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void QuitApp()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    private void OnDestroy()
    {
        levelLoaderInstance = null;
    }
}
