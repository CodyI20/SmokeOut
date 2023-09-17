using UnityEngine;

public class WallRemover : MonoBehaviour
{
    MeshRenderer _meshRenderer;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.layer = 2;
            _meshRenderer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.layer = 0;
            _meshRenderer.enabled = true;
        }
    }
}
