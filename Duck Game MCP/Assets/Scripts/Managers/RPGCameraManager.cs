using UnityEngine;
using Unity.Cinemachine;

public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager sharedInstance = null;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private string virtualCameraTag = "VirtualCamera";
    
    [Header("Startup")]
    [SerializeField] private bool findCameraOnStart = true;
    [SerializeField] private bool warnIfCameraMissing = true;
    
}
