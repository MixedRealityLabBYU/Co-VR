using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using Normal.Realtime;

public class Origin : MonoBehaviour
{
    public Transform XRCamera;
    private bool onNetwork = false;
    private Transform savedPosition;

    void Start()
    {
        savedPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onNetwork) {
            savedPosition.position = transform.position;
            savedPosition.rotation = transform.rotation;
        } else {
            transform.position = savedPosition.position;
            transform.rotation = savedPosition.rotation;
        }
    }

    public void EnableNetworkTransform()
    {
        onNetwork = true;
        gameObject.GetComponent<RealtimeView>().enabled = true;
        gameObject.GetComponent<RealtimeTransform>().enabled = true;
        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
        gameObject.GetComponent<RealtimeView>().RequestOwnership();
    }

    public void DisableNetworkTransform()
    {
        onNetwork = false;
        gameObject.GetComponent<RealtimeView>().enabled = false;
        gameObject.GetComponent<RealtimeTransform>().enabled = false;

    }

    public void OnJoin()
    {
        EnableNetworkTransform();
    }

    public void SetOrigin() 
    {
        transform.position = new Vector3(XRCamera.position.x, 0, XRCamera.position.z);
        transform.rotation = Quaternion.Euler(0, XRCamera.rotation.eulerAngles.y, 0);
    }
}