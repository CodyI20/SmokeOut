using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasChosen()
    {
        return false;
    }
}
