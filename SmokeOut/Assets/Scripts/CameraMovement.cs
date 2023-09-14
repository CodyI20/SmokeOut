using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private enum CameraPanType
    {
        MouseOutOfBounds = 1,
        Pan = 2
    };
    [SerializeField] private float panSpeed = 3f;
    [SerializeField] private float panBorderThickness = 15f;

    [SerializeField] private float dragSpeed = 10f;
    private Vector3 dragOrigin;

    private Vector3 _initialCameraPlayerDistanceVector;
    private bool lockCamera = false;

    [SerializeField] private CameraPanType panType;

    private void Start()
    {
        _initialCameraPlayerDistanceVector = transform.position - PlayerMovement.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchPanType();
        switch (panType)
        {
            case CameraPanType.MouseOutOfBounds:
                PanViewLeagueStyle();
                break;
            default:
                PanViewDrag();
                break;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            lockCamera ^= true;
        }
        if (lockCamera)
        {
            LockCameraOnPlayer();
        }
    }

    void SwitchPanType()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Swaps between camera movement types
        {
            panType = (panType == CameraPanType.Pan) ? CameraPanType.MouseOutOfBounds : CameraPanType.Pan;
            Debug.Log(panType);
        }
    }

    void LockCameraOnPlayer()
    {
        transform.position = PlayerMovement.player.transform.position + _initialCameraPlayerDistanceVector;
    }

    void PanViewLeagueStyle()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.x < panBorderThickness)
            pos.x -= panSpeed * Time.deltaTime;
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            pos.x += panSpeed * Time.deltaTime;
        if (Input.mousePosition.y < panBorderThickness)
            pos.y -= panSpeed * Time.deltaTime;
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            pos.y += panSpeed * Time.deltaTime;

        transform.position = pos;
    }

    void PanViewDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);

        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);


        transform.position = pos;
    }
}
