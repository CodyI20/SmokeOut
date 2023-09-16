using System.Collections.Generic;
using UnityEngine;

public class CamObjectFader : MonoBehaviour
{
    private HashSet<GameObject> obstructingObjects = new HashSet<GameObject>();
    private HashSet<MeshRenderer> meshRenderersToEnable = new HashSet<MeshRenderer>();
    GameObject m_Player;

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

        int layerMask = LayerMask.GetMask("WallLayer");

        // Cast a single ray to check for obstructions.
        RaycastHit hit;
        bool playerObstructed = Physics.Raycast(cameraPosition, directionToPlayer, out hit, directionToPlayer.magnitude, layerMask);

        // Handle the obstructing object.
        GameObject hitObject = hit.collider ? hit.collider.gameObject : null;

        if (hitObject != null)
        {
            if (!obstructingObjects.Contains(hitObject))
            {
                DisableMeshRenderer(hitObject);
                obstructingObjects.Add(hitObject);
            }
        }

        // Check for objects that were obstructing but no longer do.
        HashSet<GameObject> objectsToRemove = new HashSet<GameObject>(obstructingObjects);
        objectsToRemove.ExceptWith(new[] { hitObject });

        foreach (GameObject objToRemove in objectsToRemove)
        {
            EnableMeshRenderer(objToRemove);
            obstructingObjects.Remove(objToRemove);
        }

        // Enable all mesh renderers if the player is not obstructed.
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
            meshRenderersToEnable.Add(meshRenderer);
        }
    }

    void EnableMeshRenderer(GameObject obj)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
            meshRenderersToEnable.Remove(meshRenderer);
        }
    }

    void EnableAllMeshRenderers()
    {
        foreach (MeshRenderer renderer in meshRenderersToEnable)
        {
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
        meshRenderersToEnable.Clear();
    }
}
