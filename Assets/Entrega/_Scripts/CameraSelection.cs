using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraSelection : MonoBehaviour
{
    public CinemachineVirtualCameraBase virtualCamera;
    public CinemachineFreeLook freeLook;
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            virtualCamera.Priority = 11; 
            freeLook.gameObject.SetActive(false);

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            virtualCamera.Priority = 0; 
            freeLook.gameObject.SetActive(true);

        }
    }
}
