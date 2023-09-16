using Unity.VisualScripting;
using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    private Outline outlineComponent;

    [Header("Outline Settings")]
    [SerializeField] private Color outlineColor = Color.green; // Set your desired outline color here
    [SerializeField] private float outlineWidth = 3f; // Set your desired outline width here

    private void Start()
    {
        // Find the Outline component on this GameObject
        outlineComponent = GetComponent<Outline>();
        if (outlineComponent == null)
        {
            // If the Outline component is not found, add one
            outlineComponent = gameObject.AddComponent<Outline>();
        }

        // Set the outline color and width
        outlineComponent.OutlineColor = outlineColor;
        outlineComponent.OutlineWidth = outlineWidth;

        // Disable the outline initially
        outlineComponent.enabled = false;
    }

    private void OnMouseEnter()
    {
        // Enable the outline when the mouse enters the object
        outlineComponent.enabled = true;
    }

    private void OnMouseExit()
    {
        // Disable the outline when the mouse exits the object
        outlineComponent.enabled = false;
    }
}
