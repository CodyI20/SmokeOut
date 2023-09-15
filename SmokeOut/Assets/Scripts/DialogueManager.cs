using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// This class serves as a base for all dialogue-related interactions
    /// </summary>

    //Creating a singleton patter for the dialogue manager so that it can be accessed by any script with ease (and since there will only be one dialogue manager in the whole project)
    public static DialogueManager _dialogueManger {  get; private set; }

    private void Awake()
    {
        if (_dialogueManger == null)
            _dialogueManger = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _dialogueManger = null;
    }
}
