using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerActivator : MonoBehaviour
{
    public static ControllerActivator instance;
    [Header("**************3rd Person Controller Refrences************")]
    public GameObject _3rdPerson;
    public GameObject _3rdpersonUI;
    public GameObject _3rdPersonCamera;
    public GameObject FPSCam;

    private void Awake()
    {
        instance = this;
    }

    public void Activate3rdPersonController()
    {
        _3rdPerson.SetActive(true);
        _3rdpersonUI.SetActive(true);
        _3rdPersonCamera.SetActive(true);
    }
    public void DeActivateThirdPersonController()
    {
        _3rdPerson.SetActive(false);
        _3rdpersonUI.SetActive(false);
        _3rdPersonCamera.SetActive(false);
        FPSCam.SetActive(true);
    }

    public void SetPositionandRotation3rdController(GameObject OtherObject)
    {
        _3rdPerson.transform.SetPositionAndRotation(OtherObject.transform.position, OtherObject.transform.rotation);
    }
}