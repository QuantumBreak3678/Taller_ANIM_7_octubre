using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class CameraSelection : MonoBehaviour
{
    public CinemachineVirtualCameraBase virtualCamera;
    public CinemachineFreeLook freeLook;
    public Rig rigValue;
    public CanvasGroup CanvasGroup; 
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            virtualCamera.Priority = 11; 
            freeLook.gameObject.SetActive(false);
            rigValue.weight = 0;
            CanvasGroup.alpha = 0; 

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            virtualCamera.Priority = 0; 
            freeLook.gameObject.SetActive(true);
            rigValue.weight = 1; 
            CanvasGroup.alpha = 1; 

        }
    }
}
