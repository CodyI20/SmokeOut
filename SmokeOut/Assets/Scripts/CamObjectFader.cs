using System.Collections.Generic;
using UnityEngine;

public class CamObjectFader : MonoBehaviour
{
    List<MeshRenderer> _renderers;
    GameObject m_Player;

    private void Awake()
    {
        _renderers = new List<MeshRenderer>();
    }

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player == null) return;
    }

    void Update()
    {
        CheckIfDoFade();
    }

    void CheckIfDoFade()
    {
        if (m_Player == null) return;

        Vector3 playerPosition = m_Player.transform.position;
        Vector3 cameraPosition = transform.position;
        Vector3 directionToPlayer = playerPosition - cameraPosition;

        // Layer mask to exclude objects you don't want to consider as obstructions
        int layerMask = LayerMask.GetMask("WallLayer"); // Replace "ObstacleLayer" with your actual layer name.

        // Check if anything is obstructing the view between the camera and the player.
        RaycastHit[] hits = Physics.RaycastAll(cameraPosition, directionToPlayer, directionToPlayer.magnitude, layerMask);

        bool playerObstructed = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != m_Player)
            {
                // An object is obstructing the view.
                playerObstructed = true;
                DisableMeshRenderer(hit.collider.gameObject);
                _renderers.Add(hit.collider.gameObject.GetComponent<MeshRenderer>());
                break;
            }
        }

        if (!playerObstructed)
        {
            EnableAllMeshRenderers();
        }
    }

    void DisableMeshRenderer(GameObject obj)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    void EnableAllMeshRenderers()
    {
        foreach (MeshRenderer renderer in _renderers)
        {
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
        _renderers.Clear();
    }
}
