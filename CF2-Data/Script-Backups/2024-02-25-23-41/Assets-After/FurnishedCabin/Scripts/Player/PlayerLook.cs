using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName = "Mouse X";
    [SerializeField] private string mouseYInputName = "Mouse Y";
    [SerializeField] private float mouseSensitivity = 150f;

    [SerializeField] private Transform playerBody;
    private float xAxisClamp;
    private bool m_cursorIsLocked = true;

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }

    private void LockCursor()
    {
       
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (ControlFreak2.CF2Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            ControlFreak2.CFCursor.lockState = CursorLockMode.Locked;
            ControlFreak2.CFCursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            ControlFreak2.CFCursor.lockState = CursorLockMode.None;
            ControlFreak2.CFCursor.visible = true;
        }
        
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = ControlFreak2.CF2Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = ControlFreak2.CF2Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
