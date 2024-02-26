using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// A simple free camera to be added to a Unity game object.
/// 
/// Keys:
///	wasd / arrows	- movement
///	q/e 			- up/down (local space)
///	r/f 			- up/down (world space)
///	pageup/pagedown	- up/down (world space)
///	hold shift		- enable fast movement mode
///	right mouse  	- enable free look
///	mouse			- free look / rotation
///     
/// </summary>
public class FreeCam : MonoBehaviour
{
    /// <summary>
    /// Normal speed of camera movement.
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Speed of camera movement when shift is held down,
    /// </summary>
    public float fastMovementSpeed = 100f;

    /// <summary>
    /// Sensitivity for free look.
    /// </summary>
    public float freeLookSensitivity = 3f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel.
    /// </summary>
    public float zoomSensitivity = 10f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel (fast mode).
    /// </summary>
    public float fastZoomSensitivity = 50f;

    /// <summary>
    /// Set to true when free looking (on right mouse button).
    /// </summary>
    private bool looking = false;

    void Update()
    {
        var fastMode = ControlFreak2.CF2Input.GetKey(KeyCode.LeftShift) || ControlFreak2.CF2Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        if (ControlFreak2.CF2Input.GetKey(KeyCode.A) || ControlFreak2.CF2Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.D) || ControlFreak2.CF2Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.W) || ControlFreak2.CF2Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.S) || ControlFreak2.CF2Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.Q))
        {
            transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.E))
        {
            transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.R) || ControlFreak2.CF2Input.GetKey(KeyCode.PageUp))
        {
            transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (ControlFreak2.CF2Input.GetKey(KeyCode.F) || ControlFreak2.CF2Input.GetKey(KeyCode.PageDown))
        {
            transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + ControlFreak2.CF2Input.GetAxis("Mouse X") * freeLookSensitivity;
            float newRotationY = transform.localEulerAngles.x - ControlFreak2.CF2Input.GetAxis("Mouse Y") * freeLookSensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float axis = ControlFreak2.CF2Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
            transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        }

        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
        }
        else if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
        }
    }

    void OnDisable()
    {
        StopLooking();
    }

    /// <summary>
    /// Enable free looking.
    /// </summary>
    public void StartLooking()
    {
        looking = true;
        ControlFreak2.CFCursor.visible = false;
        ControlFreak2.CFCursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Disable free looking.
    /// </summary>
    public void StopLooking()
    {
        looking = false;
        ControlFreak2.CFCursor.visible = true;
        ControlFreak2.CFCursor.lockState = CursorLockMode.None;
    }
}