using UnityEngine;

public class UIHoverEffects : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private AudioSource hoverSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseEnter()
    {
        Debug.Log("Hey");
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }
}
