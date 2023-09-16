using Unity.VisualScripting;
using UnityEngine;

public class HoverOutline : MonoBehaviour
{
    private QuickOutline outlineComponent;

    [Header("Outline Settings")]
    [SerializeField] private Color outlineColor = Color.blue; // Set your desired outline color here
    [SerializeField] private float outlineWidth = 2f; // Set your desired outline width here

    [Header("Select the interaction type!")]
    [SerializeField] private InteractionType interactionType = InteractionType.Mouse;

    private void Awake()
    {
        // Find the Outline component on this GameObject
        outlineComponent = GetComponent<QuickOutline>();
        if (outlineComponent == null)
        {
            // If the Outline component is not found, add one
            outlineComponent = gameObject.AddComponent<QuickOutline>();
        }

        // Set the outline color and width
        outlineComponent.OutlineColor = outlineColor;
        outlineComponent.OutlineWidth = outlineWidth;

        // Disable the outline initially
        outlineComponent.enabled = false;
    }

    private void OnMouseEnter()
    {
        if(interactionType == InteractionType.Mouse)
        {
            outlineComponent.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if(interactionType == InteractionType.Mouse)
        {
            outlineComponent.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactionType == InteractionType.Trigger && other.CompareTag("Player"))
        {
            outlineComponent.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactionType == InteractionType.Trigger && other.CompareTag("Player"))
        {
            outlineComponent.enabled = false;
        }
    }
}
