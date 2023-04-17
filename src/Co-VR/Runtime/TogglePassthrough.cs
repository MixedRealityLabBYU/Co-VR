using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePassthrough : MonoBehaviour
{ 
    public GameObject XRRig;
    public Camera mainCamera;

    public void Toggle ()
    {
        XRRig.GetComponent<OVRPassthroughLayer>().enabled = !XRRig.GetComponent<OVRPassthroughLayer>().enabled;
        if(XRRig.GetComponent<OVRPassthroughLayer>().enabled)
        {
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
        else
        {
            mainCamera.clearFlags = CameraClearFlags.Skybox;
        }
    }

    public void SetPassthrough(bool isOn)
    {
        XRRig.GetComponent<OVRPassthroughLayer>().enabled = isOn;
        if(XRRig.GetComponent<OVRPassthroughLayer>().enabled)
        {
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
        else
        {
            mainCamera.clearFlags = CameraClearFlags.Skybox;
        }

    }
}
