using UnityEngine;

public class SmokeChoice : MonoBehaviour
{
    private GameObject _UIDisplay;

    void Start()
    {
        _UIDisplay = GameObject.FindGameObjectWithTag("ChoiceUI");
        _UIDisplay.SetActive(false);
    }

    void CheckForChoicesState()
    {
        if (NegativeEffects._negativeEffect.choicesAppear)
        {
            GameManager.gameManagerInstance.PauseGame();
            _UIDisplay.SetActive(true);
        }
        else
        {
            GameManager.gameManagerInstance.ResumeGame();
            _UIDisplay.SetActive(false);
        }
    }


    public void ChooseSmoke()
    {
        NegativeEffects._negativeEffect.ClearEffects();
        NegativeEffects._negativeEffect.choicesAppear = false;
        PlayerHealth._playerHealth.TakeDamage();
        // Reduce player lives by 1;
    }

    public void ChooseNonSmoke()
    {
        NegativeEffects._negativeEffect.isIncreasing = false;
        NegativeEffects._negativeEffect.choicesAppear = false;
        // Implement some sort of "Good job" message
    }


    // Update is called once per frame
    void Update()
    {
        CheckForChoicesState();
    }
}