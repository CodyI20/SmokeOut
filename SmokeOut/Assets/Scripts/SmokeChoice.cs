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
            _UIDisplay.SetActive(true);
    }

    public void ChooseSmoke()
    {
        NegativeEffects._negativeEffect.ClearEffects();
        NegativeEffects._negativeEffect.choicesAppear = false;
        _UIDisplay.SetActive(false);
        // Reduce player lives by 1;
    }

    public void ChooseNonSmoke()
    {
        NegativeEffects._negativeEffect.isIncreasing = false;
        NegativeEffects._negativeEffect.choicesAppear = false;
        _UIDisplay.SetActive(false);
        // Implement some sort of "Good job" message
    }


    // Update is called once per frame
    void Update()
    {
        CheckForChoicesState();
    }
}
